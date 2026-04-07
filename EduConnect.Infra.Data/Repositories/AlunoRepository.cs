using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;

namespace EduConnect.Infra.Data.Repositories;

public class AlunoRepository(EduContext context) : IAlunoRepository
{
    private readonly EduContext _context = context;

    private IQueryable<Aluno> QueryFiltroAluno(FiltroPessoa filtro, string id, string cargo)
    {
        var query = _context.Alunos.AsNoTracking().Where(p => p.Deletado == false);

        if (filtro.Pesquisa != "Todos")
        {
            var pesquisa = $"%{filtro.Pesquisa}%";

            query = query.Where(dados =>
                EF.Functions.Like(dados.Nome, pesquisa) ||
                EF.Functions.Like(dados.Registro, pesquisa) ||
                EF.Functions.Like(dados.Email, pesquisa) ||
                EF.Functions.Like(dados.Cpf, pesquisa)
            );
        }

        if (filtro.Ano != null && filtro.Ano != "Todos os Anos")
        {
            int anoLetivo = int.Parse(filtro.Ano);
            query = query.Where(dados => dados.DataMatricula.Year == anoLetivo);
        }
        
        if (filtro.Status != null && filtro.Status != "Todos os Status")
        {
            query = query.Where(dados => dados.Status == filtro.Status);
        }

        if (filtro.Categoria != null && filtro.Categoria != "Todas as Salas")
        {
            query = query.Where(dados => dados.Turma!.Nome == filtro.Categoria);
        }

        if (cargo == "Administrador" || cargo == "Funcionário")
        {
            return query;
        }
        else
        {
            query = query.Where(dados => dados.Turma!.ProfessorResponsavel == id);
            return query;
        }
    }

    public async Task<(List<Aluno>, int TotalRegistro)> GetByFilters(FiltroPessoa filtro, string id, string cargo)
    {
        var query = QueryFiltroAluno(filtro, id, cargo);
        var total = await query.CountAsync();

        query = query.Skip(filtro.Offset).Take(6);
        var result = await query.ToListAsync(); 

        return (result, total);
    }

    public async Task<(List<string>, List<string>)> GetInformativos()
    {
        var anos = await _context.Alunos
            .Where(a => a.Deletado == false)
            .Select(a => a.DataMatricula.Year.ToString())
            .Distinct()
            .ToListAsync();
        var salas = await _context.Alunos
            .Where(a => a.Deletado == false)
            .Select(a => a.Turma!.Nome)
            .Distinct()
            .ToListAsync();

        return (anos, salas);
    }

    public async Task<Aluno?> GetByIdAsync(string Registro)
    {
        return await _context.Alunos.FirstOrDefaultAsync(a => a.Registro == Registro) ?? null;
    }

    public async Task<Aluno?> GetLastPessoaAsync()
    {
        return await _context.Alunos
            .OrderBy(a => a.Id)
            .LastOrDefaultAsync();
    }

    public async Task<bool> AddAsync(Aluno alunoAdd, Conta conta)
    {
        await _context.Contas.AddAsync(conta);
        await _context.SaveChangesAsync();

        alunoAdd.ContaId = conta.Id;
        alunoAdd.Conta = conta;

        await _context.Alunos.AddAsync(alunoAdd);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateAsync(Aluno alunoUpdate)
    {
        _context.Alunos.Update(alunoUpdate);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Aluno aluno)
    {
        aluno.Deletado = true;
        _context.Alunos.Update(aluno);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<byte[]> GetBoletimAsync(string Registro)
    {
        var aluno = await _context.Alunos
         .FirstOrDefaultAsync(a => a.Registro == Registro) ?? throw new Exception("Aluno não encontrado");

        var turma = await _context.Turmas
            .Include(t => t.TurmaDisciplinas)
            .FirstOrDefaultAsync(t => t.Registro == aluno.TurmaRegistro) ?? throw new Exception("Turma não encontrada");

        var notasAluno = await _context.Notas
            .Where(n => n.AlunoRegistro == aluno.Id)
            .ToListAsync();

        // 🔥 Aqui está o ponto importante
        var disciplinas = turma.TurmaDisciplinas.Select(td =>
        {
            var nota = notasAluno
                .FirstOrDefault(n => n.MateriaId == td.Id);

            return new DisciplinaNota
            {
                Nome = td.Disciplina.Nome,
                Nota = nota?.Nota
            };
        }).ToList();

        var boletim = new Boletim
        {
            NomeAluno = aluno.Nome,
            Turma = turma.Nome,
            Disciplinas = disciplinas
        };

        return GerarPdf(boletim);
    }

    public static byte[] GerarPdf(Boletim boletim)
    {
        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(30);

                page.Header()
                    .Text("Boletim Escolar")
                    .FontSize(20)
                    .Bold()
                    .AlignCenter();

                page.Content().Column(col =>
                {
                    col.Item().Text($"Aluno: {boletim.NomeAluno}");
                    col.Item().Text($"Turma: {boletim.Turma}");

                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.ConstantColumn(80);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Disciplina").Bold();
                            header.Cell().Text("Nota").Bold();
                        });

                        foreach (var d in boletim.Disciplinas)
                        {
                            table.Cell().Text(d.Nome);
                            table.Cell().Text(d.Nota?.ToString("F1") ?? "-");
                        }
                    });
                });

                page.Footer()
                    .AlignCenter()
                    .Text($"Gerado em {DateTime.Now:dd/MM/yyyy}");
            });
        }).GeneratePdf();
    }
}
