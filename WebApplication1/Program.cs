using EduConnect.MiddleWares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddApiConfiguration();
builder.Services.AddAuthorizationConfiguration();
builder.Services.AddDependencyInjectionConfiguration(builder.Configuration);
builder.Services.AddCorsConfiguration(builder.Configuration);
builder.Services.AddProblemDetails();
var app = builder.Build();  

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
// Middleware para redirecionar HTTP para HTTPS
//app.UseHttpsRedirection();
// Utilizar CORS
app.UseCors("restrictivePolicy");
// Middleware de autorização
app.UseAuthorization();
// Mapeia controllers para requisições de API
app.MapControllers();

app.Run();
