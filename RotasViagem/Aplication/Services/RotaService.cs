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
            var todasasRotas = _repository.PegarTodasRotas();//obtem todas as rotas cadastradas
            var visitados = new HashSet<string>();
            var melhorRota = new List<string>();
            var menorCusto = int.MaxValue;

            void Encontrar(string atual, List<string> path, int custo)
            {
                if (atual == Pdestino)
                {
                    if (custo < menorCusto) //se o custo for menor que o menor custo
                    {
                        menorCusto = custo; //o menor custo passa a ser o custo
                        melhorRota = new List<string>(path);// melhor rota  recebe as list de cidades vivitadas
                    }
                    return;
                }

                visitados.Add(atual);

                foreach (var rota in todasasRotas.Where(r => r.Origem == atual && !visitados.Contains(r.Destino)))
                {
                    path.Add(rota.Destino);
                    Encontrar(rota.Destino, path, custo + rota.Custo);
                    path.RemoveAt(path.Count - 1);
                }

                visitados.Remove(atual);
            }

            Encontrar(Porigem, new List<string> { Porigem }, 0);

            return melhorRota.Count > 0
                ? $"{string.Join(" - ", melhorRota)} ao custo de ${menorCusto}"
                : "Nenhuma rota encontrada.";
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
