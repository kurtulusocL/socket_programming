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

namespace Server
{
    public partial class Form1 : Form
    {
        Socket socket;
        NetworkStream stream;
        TcpListener listener;
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        ChatMessage chat = new ChatMessage();
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listener = new TcpListener(1453);
            listener.Start();

            socket = listener.AcceptSocket();
            stream = new NetworkStream(socket);
            Thread listen = new Thread(Socketlisten);
            listen.Start();
        }
        void Socketlisten()
        {
            while (socket.Connected)
            {
                ChatMessage took = (ChatMessage)binaryFormatter.Deserialize(stream);
                listBox1.Items.Add(took);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            chat.Sender = "Server";
            chat.Date = DateTime.Now;
            chat.MessageText = textBox1.Text;
            listBox1.Items.Add(chat);
            binaryFormatter.Serialize(stream, chat);
            stream.Flush();

            textBox1.Clear();
            textBox1.Focus();
        }
    }
}
