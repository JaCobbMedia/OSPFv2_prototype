using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSPFv2_prototype.Helpers;

namespace OSPFv2_prototype
{
    class Program
    {
        static void Main(string[] args)
        {
            Network network = new Network();

            network = Simulation.GenerateNodes(network);

            while (true)
            {
                string select;
                PrintMenu();

                Console.WriteLine("Enter command: ");
                select = Console.ReadLine();

                switch (select)
                {
                    case "1":
                        Console.WriteLine("Enter node id: ");
                        string node = Console.ReadLine();
                        PrintTable(node, network);
                        break;
                    case "2":
                        Console.WriteLine("Enter new node's id: ");
                        string id = Console.ReadLine();
                        network.AddNode(id);
                        Console.WriteLine("Node " + id + " has been added to network");
                        break;
                    case "3":
                        Console.WriteLine("Enter first node's id: ");
                        string fNode = Console.ReadLine();
                        Console.WriteLine("Enter second node's id: ");
                        string sNode = Console.ReadLine();
                        Console.WriteLine("Enter connection weight: ");
                        int weight = int.Parse(Console.ReadLine());
                        network.AddLink(fNode, sNode, weight);
                        break;
                    case "4":
                        Console.WriteLine("Enter node's id: ");
                        string delete = Console.ReadLine();
                        network.RemoveNode(delete);
                        Console.WriteLine("Node " + delete + " has been deleted from network");
                        break;
                    case "5":
                        Console.WriteLine("Enter source node's id: ");
                        string src = Console.ReadLine();
                        Console.WriteLine("Enter destination node's id: ");
                        string dest = Console.ReadLine();
                        Console.WriteLine("Enter message: ");
                        string msg = Console.ReadLine();
                        network.SendMessage(src, dest, msg);
                        break;
                    case "0":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("ERROR - Wrong command!");
                        PrintMenu();
                        break;
                }
            }
        }

        private static void PrintMenu()
        {
            Console.WriteLine("1 - Print Routing table");
            Console.WriteLine("2 - Add new Node");
            Console.WriteLine("3 - Connect nodes");
            Console.WriteLine("4 - Remove Node");
            Console.WriteLine("5 - Send message");
            Console.WriteLine("0 - Exit");
        }

        private static void PrintTable(string node, Network network)
        {
            Dictionary<string, string> connections = network.GetList(node);
            if (connections != null)
            {
                foreach (var key in connections.Keys)
                {
                    string get;
                    connections.TryGetValue(key, out get);
                    Console.WriteLine("Destinatin Node: " + key + " Sending to neighbor node: " + get + " with weight: " + network.GetWeight(node, get));
                }
            }
        }
    }
}
