namespace EduConnect.MiddleWares;

public static class AuthorizationConfiguration
{
    public static IServiceCollection AddAuthorizationConfiguration(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminPolicy", policy =>
                policy.RequireRole("Admin"));
            options.AddPolicy("UserPolicy", policy =>
                policy.RequireRole("User", "Admin"));
        });
        return services;
    }
}
