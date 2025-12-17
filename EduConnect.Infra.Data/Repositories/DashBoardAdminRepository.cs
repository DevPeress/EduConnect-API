using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Infra.Data.Repositories;

public class DashBoardAdminRepository(EduContext context) : IDashboardAdminRepository
{
    private readonly EduContext _context = context;
    private readonly string MesAtual = DateOnly.FromDateTime(DateTime.Now).ToString("MM");
    private readonly string MesAnterior = DateOnly.FromDateTime(DateTime.Now.AddMonths(-1)).ToString("MM");
    
    private double CalcularPorcentagem(int aumento, int decremento)
    {
        if (decremento == 0)
            return aumento > 0 ? 100 : 0;

        if (aumento == 0)
            return 100;

        return ((double)(aumento - decremento) / decremento) * 100;
    }

    public async Task<(int, int, int)> GetTotalsAsync()
    {
        int totalAlunos = await _context.Alunos.CountAsync(a => a.Deletado == false);
        int totalProfessores = await _context.Professores.CountAsync(p => p.Deletado == false);
        int totalTurmas = await _context.Turmas.CountAsync(t => t.Deletado == false);
        return (totalAlunos, totalProfessores, totalTurmas);
    }

    public async Task<(int, int, int)> GetAumentoAsync()
    {
        List<Aluno> Alunos = await _context.Alunos.Where(p => p.Deletado == false).ToListAsync();
        int AumentoAlunos = Alunos.Count(a => a.DataMatricula.Month.ToString("00") == MesAtual) - Alunos.Count(a => a.DataMatricula.Month.ToString("00") == MesAnterior);

        List<Professor> Professores = await _context.Professores.Where(p => p.Deletado == false).ToListAsync();
        int AumentoProfessores = Professores.Count(a => a.Contratacao.Month.ToString("00") == MesAtual) - Professores.Count(a => a.Contratacao.Month.ToString("00") == MesAnterior);

        List<Turma> Turmas = await _context.Turmas.Where(p => p.Deletado == false).ToListAsync();
        int AumentoTurmas = Turmas.Count(a => a.DataCriacao.Month.ToString("00") == MesAtual) - Turmas.Count(a => a.DataCriacao.Month.ToString("00") == MesAnterior);

        return (AumentoAlunos, AumentoProfessores, AumentoTurmas);
    }

    public async Task<(double, double, double)> GetPorcentagemAsync()
    {
        List<Aluno> alunos = await _context.Alunos.Where(p => p.Deletado == false).ToListAsync();
        int aumentoAluno = alunos.Count(a => a.DataMatricula.Month.ToString("00") == MesAtual);
        int decrementoAluno = alunos.Count(a => a.DataMatricula.Month.ToString("00") == MesAnterior);

        List<Professor> professor = await _context.Professores.Where(p => p.Deletado == false).ToListAsync();
        int aumentoProfessore = professor.Count(a => a.Contratacao.Month.ToString("00") == MesAtual);
        int decrementoProfessor = professor.Count(a => a.Contratacao.Month.ToString("00") == MesAnterior);

        List<Turma> turma = await _context.Turmas.Where(p => p.Deletado == false).ToListAsync();
        int aumentoTurma = turma.Count(a => a.DataCriacao.Month.ToString("00") == MesAtual);
        int decrementoTurma = turma.Count(a => a.DataCriacao.Month.ToString("00") == MesAnterior);

        return (CalcularPorcentagem(aumentoAluno,decrementoAluno), CalcularPorcentagem(aumentoProfessore, decrementoProfessor), CalcularPorcentagem(aumentoTurma, decrementoTurma));
    }
}
