using EduConnect.Application.Common.Auditing;
using EduConnect.Application.DTO.Entities;
using EduConnect.Application.Services;
using EduConnect.Domain.Enums;
using EduConnect.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EduConnect.Controllers
{
    [ApiController]
    [Route("api/alunos")]
    public class AlunoController(AlunoService service) : ControllerBase
    {
        private readonly AlunoService _alunoService = service;

        [Authorize(Roles = "Administrador, Funcionario, Professor")]
        [HttpGet("filtro")]
        public async Task<IActionResult> GetAlunos([FromQuery] FiltroViewModel viewModel)
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            if (id == null || role == null)
                return BadRequest();

            var filtro = new FiltroPessoaDTO
            {
                Categoria = viewModel.Categoria,
                Status = viewModel.Status,
                Page = viewModel.Page,
                Ano = viewModel.Ano,
                Pesquisa = viewModel.Pesquisa
            };

            var result = await _alunoService.GetByFilters(filtro, id, role);
            if (result.IsFailed)
                return BadRequest(result.Errors);

            var (alunos, total) = result.Value;

            return Ok(new FiltroResponseViewModel<AlunoDTO>
            {
                Dados = alunos,
                Total = total
            }
            );
        }

        [Authorize(Roles = "Administrador, Funcionario, Professor")]
        [HttpGet("{Registro}")]
        public async Task<IActionResult> GetAlunoById(string Registro)
        {
            var aluno = await _alunoService.GetAlunoByIdAsync(Registro);
            if (aluno.IsFailed) return NotFound();

            return Ok(aluno.Value);
        }

        [Authorize(Roles = "Administrador, Funcionario, Professor")]
        [HttpGet("pegarInformativos")]
        public async Task<IActionResult> GetInformativosAlunosAsync()
        {
            var result = await _alunoService.GetInformativos();
            if (result.IsFailed)
               return NoContent();

            var (anos, salas) = result.Value;

            return Ok(new 
            {
                Anos = anos,
                Salas = salas
            });
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpGet("Cadastro")]
        public async Task<IActionResult> GetAlunosByCadastro()
        {
            var aluno = await _alunoService.GetLastAluno();
            if (aluno.IsFailed)
                return Ok("A000001");
          
            // Registro vem no formato MA000123
            var atual = aluno.Value.Registro;

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

        [Authorize(Roles = "Aluno, Professor, Administrador, Funcionario")]
        [HttpGet("boletim/{Registro}")]
        public async Task<IActionResult> GetBoletim(string Registro)
        {
            var existingAluno = await _alunoService.GetAlunoByIdAsync(Registro);
            if (existingAluno.IsFailed)
                return NotFound();

            var boletim = await _alunoService.GetBoletimAsync(Registro);
            if (boletim.IsFailed)
                return BadRequest(boletim.Errors);

            return File(
                boletim.Value,
                "application/pdf",
                $"boletim_{existingAluno.Value.Nome}.pdf"
            );
        }

        [HttpPost]
        public async Task<IActionResult> AddAluno([FromBody] AlunoCadastroDTO AlunoDTO)
        {
            await _alunoService.AddAlunoAsync(AlunoDTO);

            HttpContext.SetAudit(
                AuditAction.Create,
                "Aluno",
                AlunoDTO.Registro,
                $"Aluno {AlunoDTO.Nome} criado."
            );

            return Ok();
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpPut("{Registro}")]
        public async Task<IActionResult> UpdateAluno(string Registro, [FromBody] AlunoUpdateDTO AlunoDTO)
        {
            if (Registro != AlunoDTO.Registro)
                return BadRequest();

            var existingAluno = await _alunoService.GetAlunoByIdAsync(Registro);
            if (existingAluno.IsFailed)
                return NotFound();

            var update = await _alunoService.UpdateAlunoAsync(AlunoDTO, existingAluno.Value.DataMatricula, existingAluno.Value.Media);
            if (update.IsFailed)
                return BadRequest(update.Errors);

            return Ok();
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpDelete("{Registro}")]
        public async Task<IActionResult> DeleteAluno(string Registro)
        {
            var existingAluno = await _alunoService.GetAlunoByIdAsync(Registro);
            if (existingAluno.IsFailed)
                return NotFound();

            var update = await _alunoService.DeleteAlunoAsync(Registro);
            if (update.IsFailed)
                return BadRequest(update.Errors);

            return Ok();
        }
    }
}
