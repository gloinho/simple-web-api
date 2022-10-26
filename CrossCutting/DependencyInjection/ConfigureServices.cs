using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExercicioAPIStella.Domain.Interfaces;
using ExercicioAPIStella.Service;

namespace ExercicioAPIStella.CrossCutting.DependencyInjection
{
    public static class ConfigureServices
    {
        public static void ConfigureDependenciesService(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUsuarioService, UsuarioService>();
        }
    }
}
