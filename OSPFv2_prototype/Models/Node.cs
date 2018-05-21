using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using OSPFv2_prototype.Models;
using OSPFv2_prototype.Helpers;

namespace OSPFv2_prototype
{
    class Node
    {
        private string id;
        private List<Node> neighbors;
        private Dictionary<string, string> connections;
        private NetworkGraph netGraph;
        private List<int> packets;

        public Node(string id)
        {
            this.id = id;
            neighbors = new List<Node>();
            packets = new List<int>();
            netGraph = new NetworkGraph(15);
            netGraph.AddEdge(id);
            connections = new Dictionary<string, string>();
            connections.Add(id, id);
        }

        public string GetId()
        {
            return this.id;
        }

        public Dictionary<string, string> GetList()
        {
            return connections;
        }

        public void AddNeighbor(Node node)
        {
            neighbors.Add(node);
        }

        public Node[] GetNeighbors()
        {
            return neighbors.ToArray();
        }

        public NetworkGraph GetNetwork()
        {
            return netGraph;
        }

        public void AddLink(Node node, int weight)
        {
            neighbors.Add(node);
            node.AddNeighbor(this);
            netGraph.AddEdge(node.GetId());
            netGraph.SetLink(id, node.GetId(), weight);
            ReceivePacket(GeneratePacket(node));
        }

        public void RemoveNode(Node node)
        {
            neighbors.Remove(node);
            netGraph.RemoveEdge(node.GetId());
            ReceivePacket(new Packet(Packet.GetCounter(), id, netGraph));
        }

        public void SendMessage(string dest, string text)
        {
            Console.WriteLine("Message is currently at node: " + id);
            Thread.Sleep(5000);

            if (dest.Equals(id))
            {
                Console.WriteLine("Node: " + id + " received message: '" + text + "'\n\r");
            }
            else
            {
                string send;
                connections.TryGetValue(dest, out send);
                foreach (var node in neighbors)
                {
                    if (send != null && send.Equals(node.GetId()))
                    {
                        node.SendMessage(dest, text);
                        break;
                    }
                }
            }
        }

        private Packet GeneratePacket(Node node)
        {
            NetworkGraph tempNet = node.GetNetwork();

            foreach(var edge in tempNet.GetEdges())
            {
                netGraph.AddEdge(edge);

                foreach(var neighbor in tempNet.GetNeighbors(edge))
                {
                    netGraph.AddEdge(neighbor);
                    netGraph.SetLink(edge, neighbor, tempNet.GetWeight(edge, neighbor));
                }
            }

            return new Packet(Packet.GetCounter(), id, netGraph);
        }

        private void SendPacket (Packet packet)
        {
            foreach(var node in neighbors)
            {
                node.ReceivePacket(packet);
            }
        }

        public void ReceivePacket(Packet packet)
        {
            if (!packets.Contains(packet.GetNumber()))
            {
                packets.Add(packet.GetNumber());
                netGraph = packet.GetNetwork();
                connections = Dijkstra.DijkstraAlgorithm(netGraph, id);
                SendPacket(packet);
            }
        }

    }
}
