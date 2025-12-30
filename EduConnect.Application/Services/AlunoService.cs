using EduConnect.Application.Common.Auditing;
using EduConnect.Application.DTO;
using EduConnect.Domain.Entities;
using EduConnect.Domain.Enums;
using EduConnect.Domain.Interfaces;

namespace EduConnect.Application.Services;

public class AlunoService(IAlunoRepository repo, AuditContext context)
{
    private readonly IAlunoRepository _alunoRepository = repo;
    private readonly AuditContext _auditContext = context;

    public async Task<(List<AlunoDTO>, int TotalRegistro)> GetByFilters(FiltroDTO filtrodto)
    {
        var filtro = new Filtro
        {
            Page = filtrodto.Page,
            Categoria = filtrodto.Categoria,
            Status = filtrodto.Status
        };

        var (alunos, total) = await _alunoRepository.GetByFilters(filtro);

        List<AlunoDTO> alunosDTO = [];
        foreach(var aluno in alunos)
        {
            var dto = new AlunoDTO(aluno)
            {
                Registro = aluno.Registro,
                Nome = aluno.Nome,
                Email = aluno.Email,
                Telefone = aluno.Telefone,
                Status = aluno.Status,
                Nasc = aluno.Nasc,
                Endereco = aluno.Endereco,
                Cpf = aluno.Cpf,
                ContatoEmergencia = aluno.ContatoEmergencia,
                Foto = aluno.Foto
            };
            alunosDTO.Add(dto);
        }

        return (alunosDTO, total);
    }

    public async Task<Aluno?> GetAlunoByIdAsync(int id)
    {
        return await _alunoRepository.GetByIdAsync(id);
    }

    public async Task<Aluno?> GetLastAluno()
    {
        return await _alunoRepository.GetLastPessoaAsync();
    }

    public async Task AddAlunoAsync(AlunoDTO dto)
    {
        var aluno = new Aluno
        {
            Nome = dto.Nome,
            Email = dto.Email,
            Telefone = dto.Telefone,
            Status = dto.Status,
            Nasc = dto.Nasc,
            Endereco = dto.Endereco,
            Cpf = dto.Cpf,
            ContatoEmergencia = dto.ContatoEmergencia,
            Registro = dto.Registro,
            Turma = dto.Turma,
            Media = dto.Media,
            DataMatricula = dto.DataMatricula,
            Foto = dto.Foto
        };
        await _alunoRepository.AddAsync(aluno);
        _auditContext.Set(
           action: AuditAction.Create,
           entity: "Aluno",
           entityId: dto.Registro,
           details: "Criação dos dados do aluno"
       );
    }

    public async Task UpdateAlunoAsync(AlunoDTO dto)
    {
        var aluno = new Aluno
        {
            Nome = dto.Nome,
            Email = dto.Email,
            Telefone = dto.Telefone,
            Status = dto.Status,
            Nasc = dto.Nasc,
            Endereco = dto.Endereco,
            Cpf = dto.Cpf,
            ContatoEmergencia = dto.ContatoEmergencia,
            Registro = dto.Registro,
            Turma = dto.Turma,
            Media = dto.Media,
            DataMatricula = dto.DataMatricula,
            Foto = dto.Foto
        };
        await _alunoRepository.UpdateAsync(aluno);
        _auditContext.Set(
           action: AuditAction.Update,
           entity: "Aluno",
           entityId: dto.Registro,
           details: "Atualização dos dados do aluno"
       );
    }

    public async Task DeleteAlunoAsync(int id)
    {
        await _alunoRepository.DeleteAsync(id);
        _auditContext.Set(
            action: AuditAction.Delete,
            entity: "Aluno",
            entityId: id,
            details: "Deletado os dados do aluno"
        );
    }
}
