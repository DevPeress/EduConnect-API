using EduConnect.Application.DTO.Entities;
using EduConnect.Application.Services;
using EduConnect.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers
{
    [ApiController]
    [Route("api/turma")]
    public class TurmaController(TurmaService TurmaService) : ControllerBase
    {
        private readonly TurmaService _turmaService = TurmaService;

        [Authorize(Roles = "Administrador, Funcionario, Professor")]
        [HttpGet("filtro/turno/{turno}/status/{status}/page/{page}/ano/{ano}")]
        public async Task<IActionResult> GetAllTurmas(string turno, string status, int page, string ano)
        {
            var filtro = new FiltroTurmaDTO
            {
                Turno = turno,
                Status = status,
                Page = page,
                Ano = ano
            };

            var (turmas, total) = await _turmaService.GetByFilters(filtro);

            return Ok(new FiltroResponseViewModel<TurmaDTO>
            {
                Dados = turmas,
                Total = total
            });
        }

        [Authorize(Roles = "Administrador, Funcionario, Professor")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTurmaById(int id)
        {
            var turma = await _turmaService.GetTurmaByIdAsync(id);
            if (turma == null)
            {
                return NotFound();
            }
            return Ok(turma);
        }

        [Authorize(Roles = "Administrador, Funcionario, Professor")]
        [HttpGet("pegarInformativos")]
        public async Task<IActionResult> GetInformativosAlunosAsync()
        {
            var anos = await _turmaService.GetInformativos();
            return Ok(anos);
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpPost]
        public async Task<IActionResult> CreateTurma(TurmaDTO turmaDTO)
        {
            await _turmaService.AddTurmaAsync(turmaDTO);
            return Ok();
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTurma(int id, TurmaDTO turmaDTO)
        {
            if (id != turmaDTO.Registro)
            {
                return BadRequest();
            }
            await _turmaService.UpdateTurmaAsync(turmaDTO);
            return NoContent();
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTurma(int id)
        {
            await _turmaService.DeleteTurmaAsync(id);
            return NoContent();
        }
    }
}
