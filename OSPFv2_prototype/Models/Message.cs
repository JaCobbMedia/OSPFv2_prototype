using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSPFv2_prototype.Models
{
    class Message
    {
        private Node node;
        private string dest;
        private string text;

        public Message(Node node, string dest, string text)
        {
            this.node = node;
            this.text = text;
            this.dest = dest;
        }

        public void Run()
        {
            node.SendMessage(dest, text);
        }
    }
}
