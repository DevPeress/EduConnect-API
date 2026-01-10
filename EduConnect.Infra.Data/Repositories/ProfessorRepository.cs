using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.CrossCutting.Utils;
using EduConnect.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Infra.Data.Repositories;

public class ProfessorRepository(EduContext context) : IProfessorRepository
{
    private readonly EduContext _context = context;

    private IQueryable<Professor> QueryFiltroProfessor(FiltroPessoa filtro)
    {
        var query = _context.Professores.AsNoTracking().Where(p => p.Deletado == false);

        if (filtro.Pesquisa != "" && filtro.Pesquisa.Length > 2)
        {
            string pesquisa = filtro.Pesquisa;
            query = query.Where(dados =>
                dados.Nome.Contains(pesquisa, StringComparison.OrdinalIgnoreCase) ||
                dados.Registro.Contains(pesquisa, StringComparison.OrdinalIgnoreCase) ||
                dados.Email.Contains(pesquisa, StringComparison.OrdinalIgnoreCase) ||
                dados.Telefone.Contains(pesquisa, StringComparison.OrdinalIgnoreCase) ||
                dados.Formacao.Contains(pesquisa, StringComparison.OrdinalIgnoreCase) ||
                dados.Turmas.Any(turma => turma.Nome.Contains(pesquisa, StringComparison.OrdinalIgnoreCase) ||
                dados.ProfessorDisciplinas.Any(pd => pd.Disciplina.Nome.Contains(pesquisa, StringComparison.OrdinalIgnoreCase)) ||
                dados.ProfessorDisciplinas.Any(pd => pd.Disciplina.Registro.Contains(pesquisa, StringComparison.OrdinalIgnoreCase))
            );
        }

        if (filtro.Ano != null && filtro.Ano != "Todos os Anos")
        {
            int anoLetivo = int.Parse(filtro.Ano);
            query = query.Where(dados => dados.Contratacao.Year == anoLetivo);
        }

        if (filtro.Status != null && filtro.Status != "Todos os Status")
        {
            query = query.Where(dados => dados.Status == filtro.Status);
        }

        if (filtro.Categoria != null && filtro.Categoria != "Todas as Salas")
        {
            query = query.Where(dados => dados.Turmas.Any(turma => turma.Nome == filtro.Categoria));
        }

        return query;
    }

    public async Task<(IEnumerable<Professor>, int TotalRegistro)> GetByFilters(FiltroPessoa filtro)
    {
        var query = QueryFiltroProfessor(filtro);
        var total = await query.CountAsync();

        query = query.Skip(filtro.Offset).Take(6);
        var result = await query.ToListAsync();

        return (result, total);
    }

    public async Task<(List<string>, List<string>)> GetInformativos()
    {
        var anos = await _context.Professores
            .Where(a => a.Deletado == false)
            .Select(a => a.Contratacao.Year.ToString())
            .Distinct()
            .ToListAsync();

        var salas = await _context.Professores
            .Where(a => a.Deletado == false)
            .SelectMany(a => a.Turmas.Select(t => t.Nome))
            .Distinct()
            .ToListAsync();

        return (anos, salas);
    }

    public async Task<List<ProfessorDisciplina>?> GetDisciplinasByProfessorAsync(string Registro)
    {
        return await _context.ProfessorDisciplinas.Where(p => p.Professor.Registro == Registro).ToListAsync();
    }

    public async Task<List<Turma>?> GetTurmasByProfessorAsync(string Registro)
    {
        return await _context.Turmas.Where(t => t.Professor.Registro == Registro).ToListAsync();
    }

    public async Task<Professor?> GetByIdAsync(string Registro)
    {
        return await _context.Professores.Where(p => p.Deletado == false).FirstOrDefaultAsync(a => a.Registro == Registro);
    }

    public async Task<Professor?> GetLastPessoaAsync()
    {
        return await _context.Professores.Where(p => p.Deletado == false)
        .OrderBy(a => a.Registro)
        .LastOrDefaultAsync();
    }

    public async Task AddAsync(Professor professor)
    {
        var conta = new Conta
        {
            Registro = professor.Registro,
            Senha = SegurancaManager.GerarSenha(),
            Cargo = "Aluno"
        };
        await _context.Contas.AddAsync(conta);
        await _context.SaveChangesAsync();

        professor.ContaId = conta.Id;
        professor.Conta = conta;

        await _context.Professores.AddAsync(professor);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Professor professor)
    {
        _context.Professores.Update(professor);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string Registro)
    {
        var professor = await GetByIdAsync(Registro);
        if (professor != null)
        {
            await Task.Run(() =>
            {
                professor.Deletado = false;
                _context.Professores.Update(professor);
                _context.SaveChanges();
            });
        }
    }
}
