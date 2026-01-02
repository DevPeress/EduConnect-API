using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.Data.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Infra.Data.Repositories;

public class DashBoardAdminRepository(EduContext context) : IDashboardAdminRepository
{
    private readonly EduContext _context = context;
    private readonly int MesAtual = DateTime.Now.Month;
    private readonly int MesAnterior = DateTime.Now.AddMonths(-1).Month;
    private readonly int AnoAtual = DateTime.Now.Year;

    private double CalcularPorcentagem(int aumento, int decremento)
    {
        if (decremento == 0)
            return aumento > 0 ? 100 : 0;

        if (aumento == 0)
            return 100;

        return ((double)(aumento - decremento) / decremento) * 100;
    }

    public async Task<(int, int, int, int)> GetTotalsAsync()
    {
        // executa a SP e traz para memória como lista
        var resultList = await _context.GetTotalDashBoard
            .FromSqlRaw("EXEC sp_GetTotalDashBoard")
            .AsNoTracking()
            .ToListAsync(); // async e funciona com SP não-composable

        var result = resultList.First(); // pega a primeira linha


        return (
            result.TotalAlunos,
            result.TotalProfessores,
            result.TotalTurmas,
            result.TotalPresencas
        );
    }

    public async Task<(int, int, int, int)> GetAumentoAsync()
    {
        // executa a SP e traz para memória como lista
        var resultList = await _context.GetAumentoDashBoard
            .FromSqlRaw("EXEC sp_GetAumentoDashBoard @MesAtual, @MesAnterior, @Ano",
                new SqlParameter("@MesAtual", MesAtual),
                new SqlParameter("@MesAnterior", MesAnterior),
                new SqlParameter("@Ano", AnoAtual))
            )
            .AsNoTracking()
            .ToListAsync(); // async e funciona com SP não-composable

        var result = resultList.First(); // pega a primeira linha


        return (
            result.AumentoAlunos,
            result.AumentoProfessores,
            result.AumentoTurmas,
            result.AumentoPresencas
        );
    }

    public async Task<(double, double, double, double)> GetPorcentagemAsync()
    {
        var resultList = await _context.GetPorcentagemDashBoard
           .FromSqlRaw("EXEC sp_GetPorcentagem @MesAtual, @MesAnterior, @Ano",
               new SqlParameter("@MesAtual", MesAtual),
               new SqlParameter("@MesAnterior", MesAnterior),
               new SqlParameter("@Ano", AnoAtual))
            )
            .AsNoTracking()
            .ToListAsync(); // async e funciona com SP não-composable

        var result = resultList.First(); // pega a primeira linha

        return (CalcularPorcentagem(result.AumentoAluno,result.DecrementoAluno), CalcularPorcentagem(result.AumentoProfessor, result.DecrementoProfessor), CalcularPorcentagem(result.AumentoTurma, result.DecrementoTurma), CalcularPorcentagem(result.AumentoPresenca, result.DecrementoPresenca));
    }
}
