using ExercicioAPIStella.CrossCutting.DependencyInjection;

namespace ExercicioAPIStella.IoC
{
    public static class NativeInjectorBootStraper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            ConfigureServices.ConfigureDependenciesService(services);
            ConfigureRepository.ConfigureDependenciesRepository(services);
        }
    }
}
