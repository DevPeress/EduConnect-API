using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace EduConnect.MiddleWares;

public static class ApiConfiguration
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Conventions.Add(new RouteTokenTransformerConvention(new PrefixApiTransformer()));
        });
        return services;
    }

    internal class PrefixApiTransformer : IOutboundParameterTransformer
    {
        public string? TransformOutbound(object? value)
        {
            return value != null ? $"api/{value}" : null;
        }
    }
}
