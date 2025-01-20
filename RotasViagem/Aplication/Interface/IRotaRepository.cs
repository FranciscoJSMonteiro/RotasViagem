using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RotasViagem.Entidade;

namespace RotasViagem.Aplication.Interfaces
{
    public interface IRotaRepository
    {
        List<Rotas> PegarTodasRotas();
        void AdicionarRotas(Rotas route);
        void SalvarRotas();
        void CargaInicial();
        void ConsultarRotasExistentes();
        string EncontrarMelhorRota(string Porigem, string Pdestino);
    }
}
