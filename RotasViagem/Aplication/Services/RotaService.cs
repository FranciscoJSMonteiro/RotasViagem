using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RotasViagem.Aplication.Interfaces;
using RotasViagem.Entidade;

namespace RotasViagem.Aplication.Services
{
    public class RotaService
    {
        private readonly IRotaRepository _repository;

        public RotaService(IRotaRepository repository)
        {
            _repository = repository;
        }

        public string EncontrarMelhorRota(string Porigem, string Pdestino)
        {
            var result = _repository.EncontrarMelhorRota(Porigem, Pdestino);
            return result;
        }

        public void AdicionarRota(string origem, string destino, int custo)
        {
            _repository.AdicionarRotas(new Rotas(origem, destino, custo));
            _repository.SalvarRotas();
        }
        public void CargaInicial()
        {
            _repository.CargaInicial();
        }
        public void ConsultarRotasExistentes()
        {
            _repository.ConsultarRotasExistentes();
        }
    }
}
