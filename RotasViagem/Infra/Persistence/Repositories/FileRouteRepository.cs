using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RotasViagem.Aplication.Interfaces;
using RotasViagem.Entidade;

namespace RotasViagem.Infra.Persistence
{
    public class FileRouteRepository : IRotaRepository
    {
        private readonly string _filePath = @"c:\rota\rotas.txt";
        private List<Rotas> _Rotas;
        public void CargaInicial()
        {
            string caminho = @"c:\rota\";
            string arquivo = @"rotas.txt";

            try
            {
                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(caminho);
                Console.WriteLine("O diretório foi criado com sucesso em  {0}.", Directory.GetCreationTime(caminho));

                using (StreamWriter sw = File.CreateText(Path.Combine(caminho, arquivo)))
                {
                    //cargainicial
                    string[] x = new string[] {
                        "GRU,BRC,10",
                        "BRC,SCL,5",
                        "GRU,CDG,75",
                        "GRU,SCL,20",
                        "GRU,ORL,56",
                        "ORL,CDG,5 ",
                        "SCL,ORL,20"};

                    for (int i = 0; i < x.Length; i++)
                    {
                        sw.WriteLine(x[i]);
                    }
                    sw.Dispose();
                }
                Console.WriteLine("O arquivo com as rotas iniciais foi criado e carregado com sucesso.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Houve um erro ao criar/escreve no arquivo({0}) - Erro {1}", Path.Combine(caminho, arquivo), e.ToString());
            }

        }
        public void ConsultarRotasExistentes()
        {
            Console.WriteLine("** Rotas cadastradas **");
            string path = @"c:\rota\";
            string file = @"rotas.txt";
            String line;
            StreamReader sr = new StreamReader(Path.Combine(path, file));
            line = sr.ReadLine();
            Console.WriteLine("Arquivo encontrado....");
            Console.WriteLine("");
            Thread.Sleep(2500);
            Console.WriteLine("** Rotas cadastradas **");
            while (line != null)
            {
                Console.WriteLine(line);
                line = sr.ReadLine();
            }
            sr.Close();
            Console.WriteLine("** Fim das Rotas cadastradas **");
            Console.WriteLine("");
        }

        public FileRouteRepository()
        {
            _Rotas = CarregarRotas();
        }

        public List<Rotas> PegarTodasRotas() => _Rotas;

        public void AdicionarRotas(Rotas rotas)
        {
            _Rotas.Add(rotas);
        }

        public void SalvarRotas()
        {
            File.WriteAllLines(_filePath, _Rotas.Select(r => $"{r.Origem},{r.Destino},{r.Custo}"));
        }

        private List<Rotas> CarregarRotas()
        {
            if (!File.Exists(_filePath))
                return new List<Rotas>();

            return File.ReadAllLines(_filePath)
                .Select(line => line.Split(','))
                .Select(parts => new Rotas(parts[0], parts[1], int.Parse(parts[2])))
                .ToList();
        }

        public string EncontrarMelhorRota(string Porigem, string Pdestino)
        {
            var todasasRotas = PegarTodasRotas();//obtem todas as rotas cadastradas
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
    }
}
