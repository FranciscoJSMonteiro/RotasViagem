using System;
using System.Collections.Generic;
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


        public static void Lerarquivos()
        {
            string path = @"c:\rota\";
            string file = @"rotas.txt";
            String line;
            StreamReader sr = new StreamReader(Path.Combine(path, file));
            //Read the first line of text
            line = sr.ReadLine();
            //Continue to read until you reach end of file
            while (line != null)
            {
                //write the line to console window
                Console.WriteLine(line);
                //Read the next line
                line = sr.ReadLine();
            }
            //close the file
            sr.Close();
            Console.ReadLine();

        }

        public static void CriarArquivo()
        {
            string path = @"c:\rota\";
            string file = @"rotas.txt";


            try
            {

                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(path);
                Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path));


                using (StreamWriter sw = File.CreateText(Path.Combine(path, file)))
                {
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
                Console.WriteLine("Pasta e arquivo com as rota iniciais foram criados com sucesso.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Houve um erro ao criar/escreve no arquivo({0}) - Erro {1}", Path.Combine(path, file), e.ToString());
            }




        }
        static async Task Main(string[] args)

        {

            var repository = new FileRouteRepository();
            var service = new RouteService(repository);

            Console.WriteLine("Bem-vindo ao sistema de rotas!");
             CriarArquivo();

            while (true)
            {
                Console.WriteLine("Escolha uma opção:");
                Console.WriteLine("0. Consultar Rotas existentes");
                Console.WriteLine("1. Adicionar nova rota");
                Console.WriteLine("2. Consultar melhor rota");
                Console.WriteLine("3. Sair");
                Console.WriteLine("4. Consultar Rotas existentes");
                var option = Console.ReadLine();
                switch (option)
                {
                    case "0":
                        Lerarquivos();
                        break;

                    case "1":
                        Console.WriteLine("Digite a rota no formato Origem,Destino,Valor:");
                        var routeInput = Console.ReadLine()?.Split(',');
                        service.AddRoute(routeInput[0].ToUpper(), routeInput[1].ToUpper(), int.Parse(routeInput[2]));
                        Console.WriteLine("Rota adicionada com sucesso!");
                        
                        break;

                    case "2":
                        Console.WriteLine("Digite a consulta no formato Origem-Destino:");
                        var query = Console.ReadLine()?.Split('-');
                        var result = service.FindBestRoute(query[0], query[1]);
                        Console.WriteLine($"Melhor Rota: {result}");
                        break;

                    case "3":
                        return;

                    case "4":
                        //CriarArquivo();
                       
                        break;

                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
        }


    }

}

