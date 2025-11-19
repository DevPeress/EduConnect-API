namespace EduConnect.MiddleWares;

public static class CorsConfiguration
{
    public static IServiceCollection AddCorsConfiguration(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddCors(p =>
        {
            p.AddPolicy("restrictivePolicy", policy =>
            {
                policy.WithOrigins("http://localhost:5173")
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials();
            });
        });

        return services;
    }
}
