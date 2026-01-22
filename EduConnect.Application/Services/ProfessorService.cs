using EduConnect.Application.DTO.Entities;
using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;

namespace EduConnect.Application.Services;

public class ProfessorService(IProfessorRepository repo)
{
    private readonly IProfessorRepository _professorRepository = repo;

    public async Task<(List<ProfessorDTO>, int TotalRegistro)> GetByFilters(FiltroPessoaDTO filtrodto)
    {
        var filtro = new FiltroPessoa
        {
            Page = filtrodto.Page,
            Categoria = filtrodto.Categoria,
            Status = filtrodto.Status,
            Ano = filtrodto.Ano,
            Pesquisa = filtrodto.Pesquisa
        };

        var (professores, total) = await _professorRepository.GetByFilters(filtro);
        List<ProfessorDTO> professoresDTO = professores.Select(professores => new ProfessorDTO(professores)
        {
            Registro = professores.Registro,
            Nome = professores.Nome,
            Email = professores.Email,
            Telefone = professores.Telefone,
            Status = professores.Status,
            Nasc = professores.Nasc,
            Endereco = professores.Endereco,
            Cpf = professores.Cpf,
            ContatoEmergencia = professores.ContatoEmergencia,
            Foto = professores.Foto
        }).ToList();
       
        return (professoresDTO, total);
    }

    public async Task<(List<string>, List<string>?)> GetInformativos()
    {
        return await _professorRepository.GetInformativos();
    }

    public async Task<Professor?> GetProfessorByIdAsync(string Registro)
    {
        return await _professorRepository.GetByIdAsync(Registro);
    }

    public async Task<Professor?> GetLastProfessorAsync()
    {
        return await _professorRepository.GetLastPessoaAsync();
    }

    public async Task AddProfessorAsync(ProfessorCadastroDTO ProfessorDTO)
    {
        var professor = new Professor
        {
            Registro = ProfessorDTO.Registro,
            Nome = ProfessorDTO.Nome,
            Email = ProfessorDTO.Email,
            Telefone = ProfessorDTO.Telefone,
            Status = ProfessorDTO.Status,
            Nasc = ProfessorDTO.Nasc,
            Endereco = ProfessorDTO.Endereco,
            Cpf = ProfessorDTO.CPF,
            NomeEmergencia = ProfessorDTO.NomeEmergencia,
            ContatoEmergencia = ProfessorDTO.ContatoEmergencia,
            Turmas = [],
            Foto = ProfessorDTO.Foto,
            Formacao = ProfessorDTO.Formacao,
            Contratacao = DateOnly.FromDateTime(DateTime.Now),
            Salario = 0m
        };
        await _professorRepository.AddAsync(professor);
    }

    public async Task UpdateProfessorAsync(ProfessorUpdateDTO ProfessorDTO, DateOnly DataContrato)
    {
        var disciplinas = await _professorRepository.GetDisciplinasByProfessorAsync(ProfessorDTO.Registro);
        ICollection<ProfessorDisciplina> disciplinasDoProfessor = disciplinas != null! ? disciplinas : [];
         
        var turmas = await _professorRepository.GetTurmasByProfessorAsync(ProfessorDTO.Registro);
        ICollection<Turma> turmasDoProfessor = turmas != null! ? turmas : [];

        var professor = new Professor
        {
            Registro = ProfessorDTO.Registro,
            Nome = ProfessorDTO.Nome,
            Email = ProfessorDTO.Email,
            Telefone = ProfessorDTO.Telefone,
            Status = ProfessorDTO.Status,
            Nasc = ProfessorDTO.Nasc,
            Endereco = ProfessorDTO.Endereco,
            Cpf = ProfessorDTO.CPF,
            NomeEmergencia  = ProfessorDTO.NomeEmergencia,
            ContatoEmergencia = ProfessorDTO.ContatoEmergencia,
            Turmas = turmasDoProfessor,
            ProfessorDisciplinas = disciplinasDoProfessor,
            Foto = ProfessorDTO.Foto,
            Formacao = ProfessorDTO.Formacao,
            Contratacao = DataContrato,
            Salario = ProfessorDTO.Salario
        };
        await _professorRepository.UpdateAsync(professor);
    }

    public async Task DeleteProfessorAsync(string Registro)
    {
        await _professorRepository.DeleteAsync(Registro);
    }
}
