using EduConnect.Application.DTO.Entities;
using EduConnect.Application.Services;
using EduConnect.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EduConnect.Controllers
{
    [ApiController]
    [Route("api/notas")]
    public class NotasController(NotasService service) : ControllerBase
    {
        private readonly NotasService _notasService = service;

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

            var result = await _notasService.GetByFilters(filtro, id, role);
            if (result.IsFailed)
                return BadRequest(result.Errors);

            var (notas, total) = result.Value;

            return Ok(new FiltroResponseViewModel<NotasDTO>
            {
                Dados = notas,
                Total = total
            }
            );
        }

        [Authorize(Roles = "Administrador, Funcionario, Professor")]
        [HttpGet("{Registro}")]
        public async Task<IActionResult> GetNotasById(int Registro)
        {
            var aluno = await _notasService.GetNotasByIdAsync(Registro);
            if (aluno.IsFailed) return NotFound();

            return Ok(aluno);
        }

        [Authorize(Roles = "Administrador, Funcionario, Professor")]
        [HttpGet("pegarInformativos")]
        public async Task<IActionResult> GetInformativosNotasAsync()
        {
            var (anos, salas) = await _notasService.GetInformativos();
            return Ok(new 
            {
                Anos = anos,
                Salas = salas
            });
        }


        [HttpPost]
        public async Task<IActionResult> AddAluno([FromBody] NotasCadastroDTO NotaDTO)
        {
            await _notasService.AddNotaAsync(NotaDTO);

            return Ok();
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpPut("{Registro}")]
        public async Task<IActionResult> UpdateAluno(int Registro, [FromBody] NotasUpdateDTO NotaDTO)
        {
            if (Registro != NotaDTO.Registro)
                return BadRequest();

            var existingNota = await _notasService.GetNotasByIdAsync(Registro);
            if (existingNota.IsFailed)
                return NotFound();

            var update = await _notasService.UpdateNotaAsync(NotaDTO);
            if (update.IsFailed)
                return BadRequest(update.Errors);

            return Ok();
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpDelete("{Registro}")]
        public async Task<IActionResult> DeleteAluno(int Registro)
        {
            var existingAluno = await _notasService.GetNotasByIdAsync(Registro);
            if (existingAluno.IsFailed)
                return NotFound();

            var update = await _notasService.DeleteNotaAsync(Registro);
            if (update.IsFailed)
                return BadRequest(update.Errors);

            return Ok();
        }
    }
}
