using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.Data.Context;
using EduConnect.Infra.Data.Migrations;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Infra.Data.Repositories;

public class DashBoardAdminRepository(EduContext context) : IDashboardAdminRepository
{
    private readonly EduContext _context = context;
    private readonly int MesAtual = DateTime.Now.Month;
    private readonly int MesAnterior = DateTime.Now.AddMonths(-1).Month;
    private readonly int AnoAtual = DateTime.Now.Year;

    private static double CalcularPorcentagem(int aumento, int decremento)
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
                new SqlParameter("@Ano", AnoAtual)
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
               new SqlParameter("@Ano", AnoAtual)
            )
            .AsNoTracking()
            .ToListAsync(); // async e funciona com SP não-composable

        var result = resultList.First(); // pega a primeira linha

        return (CalcularPorcentagem(result.AumentoAluno,result.DecrementoAluno), CalcularPorcentagem(result.AumentoProfessor, result.DecrementoProfessor), CalcularPorcentagem(result.AumentoTurma, result.DecrementoTurma), CalcularPorcentagem(result.AumentoPresenca, result.DecrementoPresenca));
    }

    public async Task<List<Registro>> GetAtividadesAsync(string cargo, string id)
    {
        var query = _context.Registros.AsNoTracking().Where(p => p.Deletado == false);

        if (cargo == "Professor")
        {
            query = query.Where(p => p.UserRole == "Professor" && p.UserId == id);
        }
        else if (cargo == "Aluno")
        {
            query = query.Where(p => p.UserRole == "Aluno" && p.UserId == id);
        }
        else if (cargo == "Funcionário")
        {
            query = query.Where(p => p.UserRole == "Funcionário" && p.UserId == id);
        }

        var result = await query
            .OrderByDescending(p => p.CreatedAt)
            .Take(4)
            .ToListAsync();

        return result;
    }

    public async Task<(int[], int[])> GetGraficoAsync(string cargo, string id)
    {
        if (cargo == "Administrador" || cargo == "Funcionário")
        {
            var notas = await _context.Notas.ToListAsync();
            List<int> notasValor = [];

            foreach (var valor in notas) {
                notasValor.Add(valor.Nota);
            }

            int[] notasArray = [.. notasValor];
            int[] notasMedia7 = [.. Enumerable.Repeat(7, notasArray.Length)];

            return (notasArray, notasMedia7);
        } 
        else if (cargo == "Professor")
        {
            List<int> notasValor = [];
            var alunosProfessor = await _context.Turmas.Where(d => d.ProfessorResponsavel == id).Select(d => d.Alunos).Distinct().ToListAsync();
            foreach(var aluno in alunosProfessor)
            {
                var notas = await _context.Notas.Where(d => d.Aluno == aluno).ToListAsync();
                foreach(var valor in notas)
                {
                    notasValor.Add(valor.Nota);
                }
            }

            int[] notasArray = [.. notasValor];
            int[] notasMedia7 = [.. Enumerable.Repeat(7, notasArray.Length)];

            return (notasArray, notasMedia7);
        }
        else
        {
            List<int> notasValor = [];
            var notas = await _context.Notas.Where(d => d.Aluno.Registro == id).ToListAsync();
            foreach (var valor in notas)
            {
                notasValor.Add(valor.Nota);
            }

            int[] notasArray = [.. notasValor];
            int[] notasMedia7 = [.. Enumerable.Repeat(7, notasArray.Length)];

            return (notasArray, notasMedia7);
        }
    }
}
