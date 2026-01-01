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

    public async Task<(int, int, int, int)> GetTotalsAsync()
    {
        int totalAlunos = await _context.Alunos.CountAsync(a => a.Deletado == false);
        int totalProfessores = await _context.Professores.CountAsync(p => p.Deletado == false);
        int totalTurmas = await _context.Turmas.CountAsync(t => t.Deletado == false);
        var Presencas = await _context.Presencas.ToListAsync();
        int DecrementoPresencas = Presencas.Count(t => t.Presente == false && t.Justificada == false);
        Presencas = Presencas.Where(t => t.Presente == true || t.Justificada == true).ToList();
        int totalPresenca = Presencas.Count - DecrementoPresencas;
        return (totalAlunos, totalProfessores, totalTurmas, totalPresenca);
    }

    public async Task<(int, int, int, int)> GetAumentoAsync()
    {
        var Alunos = await _context.Alunos.Where(p => p.Deletado == false).ToListAsync();
        int AumentoAlunos = Alunos.Count(a => a.DataMatricula.Month.ToString("00") == MesAtual) - Alunos.Count(a => a.DataMatricula.Month.ToString("00") == MesAnterior);

        var Professores = await _context.Professores.Where(p => p.Deletado == false).ToListAsync();
        int AumentoProfessores = Professores.Count(a => a.Contratacao.Month.ToString("00") == MesAtual) - Professores.Count(a => a.Contratacao.Month.ToString("00") == MesAnterior);

        var Turmas = await _context.Turmas.Where(p => p.Deletado == false).ToListAsync();
        int AumentoTurmas = Turmas.Count(a => a.DataCriacao.Month.ToString("00") == MesAtual) - Turmas.Count(a => a.DataCriacao.Month.ToString("00") == MesAnterior);

        var Prensencas = await _context.Presencas.ToListAsync();
        Prensencas = Prensencas.Where(t => t.Data.Month.ToString("00") == MesAtual || t.Data.Month.ToString("00") == MesAnterior).ToList();
        int DecrementoPresencas = Prensencas.Count(t => (t.Presente == false && t.Justificada == false) && t.Data.Month.ToString("00") == MesAnterior);
        Prensencas = Prensencas.Where(t => (t.Presente == true || t.Justificada == true) && t.Data.Month.ToString("00") == MesAtual).ToList();
        int AumentoPresencas = Prensencas.Count - DecrementoPresencas;

        return (AumentoAlunos, AumentoProfessores, AumentoTurmas, AumentoPresencas);
    }

    public async Task<(double, double, double, double)> GetPorcentagemAsync()
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

        var Prensencas = await _context.Presencas.ToListAsync();
        Prensencas = Prensencas.Where(t => t.Data.Month.ToString("00") == MesAtual || t.Data.Month.ToString("00") == MesAnterior).ToList();
        int decrementoPresenca = Prensencas.Count(t => (t.Presente == false && t.Justificada == false) && t.Data.Month.ToString("00") == MesAnterior);
        Prensencas = Prensencas.Where(t => (t.Presente == true || t.Justificada == true) && t.Data.Month.ToString("00") == MesAtual).ToList();
        int aumentoPresenca = Prensencas.Count - decrementoPresenca;

        return (CalcularPorcentagem(aumentoAluno,decrementoAluno), CalcularPorcentagem(aumentoProfessore, decrementoProfessor), CalcularPorcentagem(aumentoTurma, decrementoTurma), CalcularPorcentagem(aumentoPresenca, decrementoPresenca));
    }
}
