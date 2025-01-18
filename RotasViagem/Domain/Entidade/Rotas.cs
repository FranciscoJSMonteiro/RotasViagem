using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotasViagem.Entidade
{
    public class Route
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public int Cost { get; set; }

        public Route(string origin, string destination, int cost)
        {
            Origin = origin;
            Destination = destination;
            Cost = cost;
        }
    }
}