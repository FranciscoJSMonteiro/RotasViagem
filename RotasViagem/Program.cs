using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using RotasViagem.Aplication.Services;
using RotasViagem.Infra.Persistence;

namespace RotasViagem
{
    public class Program

    {




        static async Task Main(string[] args)

        {

            var repository = new FileRouteRepository();
            var service = new RotaService(repository);

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Bem-vindo ao sistema de rotas!");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");

            service.CargaInicial();
            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("Escolha uma opção:");
                Console.WriteLine("0. Consultar Rotas existentes");
                Console.WriteLine("1. Adicionar nova rota");
                Console.WriteLine("2. Consultar melhor rota");
                Console.WriteLine("3. Sair");

                var option = Console.ReadLine();
                switch (option)
                {
                    case "0":
                        Console.Clear();
                        try
                        {
                            service.ConsultarRotasExistentes();
                        }
                        catch (Exception)
                        {

                            Console.WriteLine("Houve um erro ao buscar as rotas  cadstradas!");
                        }
                        break;

                    case "1":
                        try
                        {
                            Console.WriteLine("Digite a rota no formato Origem,Destino,Valor:");
                            var routeInput = Console.ReadLine()?.Split(',');
                            service.AdicionarRota(routeInput[0].ToUpper(), routeInput[1].ToUpper(), int.Parse(routeInput[2]));
                            Console.WriteLine("Rota adicionada com sucesso!");
                        }
                        catch (Exception)
                        {

                            Console.WriteLine("Houve um erro ao adicionar a Rota.");
                        }


                        break;

                    case "2":

                        Console.WriteLine("Digite a consulta no formato Origem-Destino:");
                        try

                        {
                            var query = Console.ReadLine()?.Split('-');
                            var result = service.EncontrarMelhorRota(query[0], query[1]);
                            Console.WriteLine($"Melhor Rota: {result}");
                        }
                        catch (Exception)
                        {

                            Console.WriteLine("A consulta não foi digitada no formato Origem-Destino:");
                        }

                        break;

                    case "3":
                        return;

                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
        }


    }

}

