using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using OSPFv2_prototype.Models;

namespace OSPFv2_prototype
{
    class Network
    {
        private List<Node> nodes;
        public Network()
        {
            nodes = new List<Node>();
        }

        public void AddNode(string id)
        {
            nodes.Add(new Node(id));
        }

        public void RemoveNode(string id)
        {
            foreach(var node in nodes)
            {
                if (id.Equals(node.GetId()))
                {
                    foreach(var tempNode in node.GetNeighbors())
                    {
                        tempNode.RemoveNode(node);
                    }
                    nodes.Remove(node);
                    return;
                }
            }
        }

        public void AddLink(string src, string dest, int weight)
        {
            foreach(var node in nodes)
            {
                if (src.Equals(node.GetId()))
                {
                    foreach(var tempNode in nodes)
                    {
                        if (dest.Equals(tempNode.GetId()))
                        {
                            node.AddLink(tempNode, weight);
                        }
                    }
                }
            }
        }

        public void SendMessage(string src, string dest, string msg)
        {
            foreach(var node in nodes)
            {
                if (src.Equals(node.GetId()))
                {
                    Message message = new Message(node, dest, msg);
                    Thread thread = new Thread(message.Run);
                    thread.Start();
                }
            }
        }

        public Dictionary<string, string> GetList(string src)
        {
            foreach(var node in nodes)
            {
                if (src.Equals(node.GetId()))
                {
                    return node.GetList();
                }
            }
            return null;
        }

        public int GetWeight(string src, string dest)
        {
            int weight = 0;

            foreach(var node in nodes)
            {
                if (src.Equals(node.GetId()))
                {
                    weight = node.GetNetwork().GetWeight(src, dest);
                    return weight;
                }
            }

            return 0;
        }
    }
}
