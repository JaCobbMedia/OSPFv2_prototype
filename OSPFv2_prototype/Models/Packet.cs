using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSPFv2_prototype.Models
{
    class Packet
    {
        private static int counter = 0;
        private int num;
        private string sender;
        private NetworkGraph netGraph;

        public Packet(int num, string sender, NetworkGraph netGraph)
        {
            this.num = num;
            this.sender = sender;
            this.netGraph = netGraph;
        }

        public static int GetCounter()
        {
            return counter++;
        }

        public int GetNumber()
        {
            return num;
        }

        public string GetSender()
        {
            return sender;
        }

        public NetworkGraph GetNetwork()
        {
            return netGraph;
        }
    }
}
