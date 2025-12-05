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

    public async Task<List<Aluno>> GetAllAlunosAsync()
    {
        return await _context.Alunos.ToListAsync();
    }

    public async Task<List<Professor>> GetAllProfessoresAsync()
    {
        return await _context.Professores.ToListAsync();
    }

    public async Task<List<Turma>> GetAllTurmasAsync()
    {
        return await _context.Turmas.ToListAsync();
    }

    public async Task<int> GetAumentoAlunos()
    {
        List<Aluno> Alunos = await _context.Alunos.ToListAsync();
        int Aumento = Alunos.Count(a => a.DataMatricula.Month.ToString("00") == MesAtual) - Alunos.Count(a => a.DataMatricula.Month.ToString("00") == MesAnterior);
        return Aumento;
    }

    public async Task<int> GetAumentProfessores()
    {
        List<Professor> Professores = await _context.Professores.ToListAsync();
        int Aumento = Professores.Count(a => a.Contratacao.Month.ToString("00") == MesAtual) - Professores.Count(a => a.Contratacao.Month.ToString("00") == MesAnterior);
        return Aumento;
    }

    public async Task<int> GetAumentTurmas()
    {
        List<Turma> Turmas = await _context.Turmas.ToListAsync();
        int Aumento = Turmas.Count(a => a.DataCriacao.Month.ToString("00") == MesAtual) - Turmas.Count(a => a.DataCriacao.Month.ToString("00") == MesAnterior);
        return Aumento;
    }

    public async Task<double> GetPorcentagemAlunos()
    {
        List<Aluno> alunos = await _context.Alunos.ToListAsync();
        int aumento = alunos.Count(a => a.DataMatricula.Month.ToString("00") == MesAtual);
        int decremento = alunos.Count(a => a.DataMatricula.Month.ToString("00") == MesAnterior);

        if (decremento == 0)
            return aumento > 0 ? 100 : 0;

        if (aumento == 0)
            return 100;

        double porcentagem = ((double)(aumento - decremento) / decremento) * 100;
        return porcentagem;
    }

    public async Task<double> GetPorcentagemProfessores()
    {
        List<Professor> professor = await _context.Professores.ToListAsync();
        int aumento = professor.Count(a => a.Contratacao.Month.ToString("00") == MesAtual);
        int decremento = professor.Count(a => a.Contratacao.Month.ToString("00") == MesAnterior);

        if (decremento == 0)
            return aumento > 0 ? 100 : 0;

        if (aumento == 0)
            return 100;

        double porcentagem = ((double)(aumento - decremento) / decremento) * 100;
        return porcentagem;
    }

    public async Task<double> GetPorcentagemTurmas()
    {
        List<Turma> turma = await _context.Turmas.ToListAsync();
        int aumento = turma.Count(a => a.DataCriacao.Month.ToString("00") == MesAtual);
        int decremento = turma.Count(a => a.DataCriacao.Month.ToString("00") == MesAnterior);

        if (decremento == 0)
            return aumento > 0 ? 100 : 0;

        if (aumento == 0)
            return 100;

        double porcentagem = ((double)(aumento - decremento) / decremento) * 100;
        return porcentagem;
    }
}
