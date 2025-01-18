using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RotasViagem.Aplication.Interface;
using RotasViagem.Entidade;

namespace RotasViagem.Aplication.Services
{
    public class RouteService
    {
        private readonly IRouteRepository _repository;

        public RouteService(IRouteRepository repository)
        {
            _repository = repository;
        }

        public string FindBestRoute(string origin, string destination)
        {
            var allRoutes = _repository.GetAllRoutes();
            var visited = new HashSet<string>();
            var bestRoute = new List<string>();
            var minCost = int.MaxValue;

            void Find(string current, List<string> path, int cost)
            {
                if (current == destination)
                {
                    if (cost < minCost)
                    {
                        minCost = cost;
                        bestRoute = new List<string>(path);
                    }
                    return;
                }

                visited.Add(current);

                foreach (var route in allRoutes.Where(r => r.Origin == current && !visited.Contains(r.Destination)))
                {
                    path.Add(route.Destination);
                    Find(route.Destination, path, cost + route.Cost);
                    path.RemoveAt(path.Count - 1);
                }

                visited.Remove(current);
            }

            Find(origin, new List<string> { origin }, 0);

            return bestRoute.Count > 0
                ? $"{string.Join(" - ", bestRoute)} ao custo de ${minCost}"
                : "Nenhuma rota encontrada.";
        }

        public void AddRoute(string origin, string destination, int cost)
        {
            _repository.AddRoute(new Route(origin, destination, cost));
            _repository.SaveRoutes();
        }
    }
}
