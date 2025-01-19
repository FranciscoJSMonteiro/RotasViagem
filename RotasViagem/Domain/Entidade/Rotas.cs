using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotasViagem.Entidade
{
    public class Rotas
    {
        public string Origem { get; set; }
        public string Destino { get; set; }
        public int Custo { get; set; }

        public Rotas(string origem, string destino, int custo)
        {
            Origem = origem;
            Destino = destino;
            Custo = custo;
        }
    }
}