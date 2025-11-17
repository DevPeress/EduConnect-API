namespace EduConnect.MiddleWare;

public class CorsConfiguration
{
    public static IServiceCollection AddConfigureCors(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(p =>
        {
            p.AddPolicy("restrictivePolicy", policy =>
            {
                policy.WithOrigins(configuration["ALLOWED_HOSTS"] || "http://localhost:5173")
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials();
            });
        });

        return services;
    }
}
