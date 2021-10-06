using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{
    [Serializable]
    public class ChatMessage
    {
        public string Sender { get; set; }
        public string MessageText { get; set; }
        public DateTime Date { get; set; }
        public override string ToString()
        {
            return Sender + ": (" + Date.ToString() + ")->" + MessageText;
        }
    }
}
