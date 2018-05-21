using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSPFv2_prototype.Models;

namespace OSPFv2_prototype.Helpers
{
    class Simulation
    {
        public static Network GenerateNodes(Network net)
        {
            for(int i = 1; i <= 6; i++)
            {
                net.AddNode("N" + i.ToString());
            }

            net.AddLink("N1", "N2", 5);
            net.AddLink("N1", "N4", 4);
            net.AddLink("N2", "N3", 8);
            net.AddLink("N2", "N4", 1);
            net.AddLink("N3", "N5", 6);
            net.AddLink("N5", "N6", 11);
            net.AddLink("N4", "N6", 4);

            return net;
        }
    }
}
