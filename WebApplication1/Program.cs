using EduConnect.Application;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.Data.Context;
using EduConnect.Infra.Data.Repositories;
using EduConnect.MiddleWares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddApiConfiguration();
builder.Services.AddAuthorizationConfiguration();
builder.Services.AddCorsConfiguration(builder.Configuration);
builder.Services.AddProblemDetails();
builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
builder.Services.AddScoped<AlunoService>();
builder.Services.AddScoped<IProfessorRepository, ProfessorRepository>();
builder.Services.AddScoped<ProfessorService>();

builder.Services.AddDbContext<EduContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();  

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
// Middleware para redirecionar HTTP para HTTPS
app.UseHttpsRedirection();
// Utilizar CORS
app.UseCors("CorsPolicy");
// Middleware de autorização
app.UseAuthorization();
// Mapeia controllers para requisições de API
app.MapControllers();

app.Run();
