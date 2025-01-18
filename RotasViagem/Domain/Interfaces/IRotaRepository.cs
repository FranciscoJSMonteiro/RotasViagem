﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RotasViagem.Entidade;

namespace RotasViagem.Domain.Interfaces
{
    public interface IRouteRepository
    {
        List<Route> GetAllRoutes();
        void AddRoute(Route route);
        void SaveRoutes();
    }
}
