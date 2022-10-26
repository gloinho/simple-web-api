using ExercicioAPIStella.Data.Repositories;
using ExercicioAPIStella.Domain.Interfaces;

namespace ExercicioAPIStella.CrossCutting.DependencyInjection
{
    public static class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUsuarioRepository, UsuarioRepository>();
        }
    }
}
