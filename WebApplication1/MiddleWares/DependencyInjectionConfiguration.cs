using EduConnect.Infra.IoC;

namespace EduConnect.MiddleWares
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            Injection.RegisterServices(services, configuration);
        }
    }
}
