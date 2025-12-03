using EduConnect.Application.DTO;
using EduConnect.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurmaController(TurmaService TurmaService) : ControllerBase
    {
        private readonly TurmaService _turmaService = TurmaService;
        [HttpGet]
        public async Task<IActionResult> GetAllTurmas()
        {
            var turmas = await _turmaService.GetAllTurmasAsync();
            return Ok(turmas);
        }
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
        [HttpPost]
        public async Task<IActionResult> CreateTurma(TurmaDTO turmaDTO)
        {
            await _turmaService.AddTurmaAsync(turmaDTO);
            return Ok();
        }
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTurma(int id)
        {
            await _turmaService.DeleteTurmaAsync(id);
            return NoContent();
        }
    }
}
