using EduConnect.Application.Services;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.Data.Context;
using EduConnect.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EduConnect.Infra.IoC
{
    public class Injection
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EduContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IAlunoRepository, AlunoRepository>();
            services.AddScoped<AlunoService>();
            services.AddScoped<IProfessorRepository, ProfessorRepository>();
            services.AddScoped<ProfessorService>();
            services.AddScoped<IDashboardAdminRepository, DashBoardAdminRepository>();
            services.AddScoped<DashBoardAdminService>();
            services.AddScoped<IRegistroRepository, RegistroRepository>();
            services.AddScoped<RegistroService>();
            services.AddScoped<IFinanceiroRepository, FinanceiroRepository>();
            services.AddScoped<FinanceiroService>();
            services.AddScoped<IContaRepository, ContaRepository>();
            services.AddScoped<ContaService>();
        }
    }
}
