using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RotasViagem.Aplication.Interface;
using RotasViagem.Entidade;

namespace RotasViagem.Infra.Persistence
{
    public class FileRouteRepository : IRouteRepository
    {
        private readonly string _filePath = @"c:\rota\rotas.txt";
        private List<Route> _routes;

        public FileRouteRepository()
        {
            _routes = LoadRoutes();
        }

        public List<Route> GetAllRoutes() => _routes;

        public void AddRoute(Route route)
        {
            _routes.Add(route);
        }

        public void SaveRoutes()
        {
            File.WriteAllLines(_filePath, _routes.Select(r => $"{r.Origin},{r.Destination},{r.Cost}"));
        }

        private List<Route> LoadRoutes()
        {
            if (!File.Exists(_filePath))
                return new List<Route>();

            return File.ReadAllLines(_filePath)
                .Select(line => line.Split(','))
                .Select(parts => new Route(parts[0], parts[1], int.Parse(parts[2])))
                .ToList();
        }
       
    }
}
