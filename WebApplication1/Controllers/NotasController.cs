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

        [Authorize(Roles = "Administrador, Funcionario, Professor, Aluno")]
        [HttpGet("filtro")]
        public async Task<IActionResult> GetNotas([FromQuery] FiltroViewModel viewModel)
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
            var nota = await _notasService.GetNotasByIdAsync(Registro);
            if (nota.IsFailed) return NotFound();

            return Ok(nota.Value);
        }

        [Authorize(Roles = "Administrador, Funcionario, Professor")]
        [HttpGet("pegarInformativos")]
        public async Task<IActionResult> GetInformativosNotasAsync()
        {
            var result = await _notasService.GetInformativos();
            if (result.IsFailed) 
                return BadRequest();

            var (anos, salas) = result.Value;

            return Ok(new 
            {
                Anos = anos,
                Salas = salas
            });
        }


        [HttpPost]
        public async Task<IActionResult> AddNota([FromBody] NotasCadastroDTO NotaDTO)
        {
            await _notasService.AddNotaAsync(NotaDTO);

            return Ok();
        }

        [Authorize(Roles = "Administrador, Funcionario, Professor")]
        [HttpPut("{Registro}")]
        public async Task<IActionResult> UpdateNota(int Registro, [FromBody] NotasUpdateDTO NotaDTO)
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

        [Authorize(Roles = "Administrador, Funcionario, Professor")]
        [HttpDelete("{Registro}")]
        public async Task<IActionResult> DeleteNota(int Registro)
        {
            var existingNota = await _notasService.GetNotasByIdAsync(Registro);
            if (existingNota.IsFailed)
                return NotFound();

            var update = await _notasService.DeleteNotaAsync(Registro);
            if (update.IsFailed)
                return BadRequest(update.Errors);

            return Ok();
        }
    }
}
