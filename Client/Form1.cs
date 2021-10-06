using Server.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        TcpClient client;
        NetworkStream stream;
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        ChatMessage chat = new ChatMessage();
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            client = new TcpClient(txtIP.Text, 1453);
            stream = client.GetStream();
            Thread listener = new Thread(ListenConnection);
            listener.Start();
        }
        void ListenConnection()
        {
            while (true)
            {
                ChatMessage message = (ChatMessage)binaryFormatter.Deserialize(stream);
                listBox1.Items.Add(message);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            chat.Sender = txtUsername.Text;
            chat.Date = DateTime.Now;
            chat.MessageText = txtMessage.Text;
            listBox1.Items.Add(chat);

            binaryFormatter.Serialize(stream, chat);
            stream.Flush();

            txtMessage.Clear();
            txtMessage.Focus();
        }
    }
}
