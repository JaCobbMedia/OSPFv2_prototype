using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSPFv2_prototype.Helpers;

namespace OSPFv2_prototype.Models
{
    class NetworkGraph
    {
        private int[][] edges;
        private IDictionary<string, int> map;
        private LinkedList<int> emptyNodes;
        private int emptyIndex;

        public NetworkGraph(int nodeNumber)
        {
            emptyIndex = 0;
            edges = RectangularArrays.GetRectangularIntArray(nodeNumber, nodeNumber);
            map = new Dictionary<string, int>();
            emptyNodes = new LinkedList<int>();

            for(int i = 0; i < nodeNumber; i++)
            {
                for(int j = 0; j < nodeNumber; j++)
                {
                    edges[i][j] = 0;
                }
            }
        }

        public int Size
        {
            get
            {
                return map.Count;
            }
        }

        public string[] GetEdges()
        {
            string[] node = new string[map.Count];
            int i = 0;
            foreach(var key in map.Keys)
            {
                node[i] = key;
                i++;
            }
            return node;
        }

        public bool AddEdge(string node)
        {
            int index;
            if (map.ContainsKey(node))
            {
                return false;
            }
            if(emptyNodes.Count == 0)
            {
                index = emptyIndex;
                map[node] = index;
                emptyIndex++;
            }
            else
            {
                index = emptyNodes.First.Value;
                emptyNodes.RemoveFirst();
                map[node] = index;
            }

            return true;
        }

        public int GetWeight(string src, string dest)
        {
            if (map.ContainsKey(src) && map.ContainsKey(dest))
            {
                return edges[map[src]][map[dest]];
            }
            else
                return -1;
        }

        public string[] GetNeighbors(string node)
        {
            int[] numbers;
            string[] neighbors;
            if (map.ContainsKey(node))
            {
                int count = 0;
                int index;
                map.TryGetValue(node, out index);
                for (int i = 0; i < edges.Length; i++)
                {
                    if (edges[index][i] > 0)
                    {
                        count++;
                    }
                }
                numbers = new int[count];
                neighbors = new string[count];
                count = 0;
                for (int i = 0; i < edges.Length; i++)
                {
                    if (edges[index][i] > 0)
                    {
                        numbers[count++] = i;
                    }
                }
                count = 0;
                foreach (var id in map.Keys)
                {
                    for (int i = 0; i < numbers.Length; i++)
                    {
                        if(numbers[i] == map[id])
                        {
                            neighbors[count++] = id;
                            break;
                        }
                    }
                }
                return neighbors;
            }
            else
                return null;
        }

        public bool RemoveEdge(string node)
        {
            int index;
            if (map.ContainsKey(node))
            {
                map.TryGetValue(node, out index);
                map.Remove(node);
                emptyNodes.AddLast(index);
                for (int i = 0; i < edges[index].Length; i++)
                {
                    edges[index][i] = 0;
                    edges[i][index] = 0;
                }
                return true;
            }
            else
                return false;
        }

        public bool SetLink(string src, string dest, int weight)
        {
            if (map.ContainsKey(src) && map.ContainsKey(dest))
            {
                edges[map[src]][map[dest]] = weight;
                edges[map[dest]][map[src]] = weight;
                return true;
            }
            else
                return false;
        }
    }
}
