using EduConnect.Application.Common.Auditing;
using EduConnect.Application.DTO.Entities;
using EduConnect.Application.Services;
using EduConnect.Domain.Enums;
using EduConnect.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers
{
    [ApiController]
    [Route("api/alunos")]
    public class AlunoController(AlunoService service) : ControllerBase
    {
        private readonly AlunoService _alunoService = service;

        [Authorize(Roles = "Administrador, Funcionario, Professor")]
        [HttpGet("filtro/selecionada/{categoria}/status/{status}/page/{page}/ano/{ano}")]
        public async Task<IActionResult> GetAlunos(string categoria, string status, int page, string ano)
        {
            var filtro = new FiltroPessoaDTO
            {
                Categoria = categoria,
                Status = status,
                Page = page,
                Ano = ano
            };

            var (alunos, total) = await _alunoService.GetByFilters(filtro);

            return Ok(new FiltroResponseViewModel<AlunoDTO>
            {
                Dados = alunos,
                Total = total
            }
            );
        }

        [Authorize(Roles = "Administrador, Funcionario, Professor")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAlunoById(int id)
        {
            var aluno = await _alunoService.GetAlunoByIdAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }
            return Ok(aluno);
        }

        [Authorize(Roles = "Administrador, Funcionario, Professor")]
        [HttpGet("pegarInformativos")]
        public async Task<IActionResult> GetInformativosAlunosAsync()
        {
            var anos = await _alunoService.GetInformativos();
            return Ok(anos);
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpGet("Cadastro")]
        public async Task<IActionResult> GetAlunosByCadastro()
        {
            var aluno = await _alunoService.GetLastAluno();
            if (aluno == null)
            {
                return Ok("A000001");
            }
            // Registro vem no formato MA000123
            var atual = aluno.Registro;

            // Pega somente os números (6 dígitos)
            var numeros = atual.Substring(2);

            // Converte para int
            var numeroAtual = int.Parse(numeros);

            // Incrementa
            var proximo = numeroAtual + 1;

            // Formata para sempre ter 6 dígitos
            var proximoFormatado = proximo.ToString("D6");

            return Ok("A" + proximoFormatado);
        }

        [HttpPost]
        public async Task<IActionResult> AddAluno([FromBody] AlunoDTO dto)
        {
            await _alunoService.AddAlunoAsync(dto);

            HttpContext.SetAudit(
                AuditAction.Create,
                "Aluno",
                dto.Id,
                $"Aluno {dto.Nome} criado."
            );

            return Ok();
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAluno(int id, AlunoDTO dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var existingAluno = await _alunoService.GetAlunoByIdAsync(id);
            if (existingAluno == null)
                return NotFound();

            await _alunoService.UpdateAlunoAsync(dto);
            return NoContent();
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAluno(int id)
        {
            var existingAluno = await _alunoService.GetAlunoByIdAsync(id);
            if (existingAluno == null)
                return NotFound();

            await _alunoService.DeleteAlunoAsync(id);
            return NoContent();
        }
    }
}
