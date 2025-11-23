using EduConnect.Application.Services;
using EduConnect.Domain;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers
{
    [ApiController]
    [Route("api/professores")]
    public class ProfessorController(ProfessorService service) : ControllerBase
    {
        private readonly ProfessorService _professorService = service;

        [HttpGet]
        public async Task<IActionResult> GetAllProfessor()
        {
            var professores = await _professorService.GetAllProfessorAsync();
            return Ok(professores);
        }
        [HttpGet("{matricula}")]
        public async Task<IActionResult> GetProfessorById(string matricula)
        {
            var professores = await _professorService.GetProfessorByIdAsync(matricula);
            if (professores == null)
            {
                return NotFound();
            }
            return Ok(professores);
        }
        [HttpPost]
        public async Task<IActionResult> AddProfessor(Professor professor)
        {
            await _professorService.AddProfessorAsync(professor);
            return CreatedAtAction(nameof(GetProfessorById), new { matricula = professor.Registro }, professor);
        }
        [HttpPut("{matricula}")]
        public async Task<IActionResult> UpdateProfessor(string matricula, Professor professor)
        {
            if (matricula != professor.Registro)
            {
                return BadRequest();
            }
            var existingProfessor = await _professorService.GetProfessorByIdAsync(matricula);
            if (existingProfessor == null)
            {
                return NotFound();
            }
            await _professorService.UpdateProfessorAsync(professor);
            return NoContent();
        }
        [HttpDelete("{matricula}")]
        public async Task<IActionResult> DeleteAluno(string matricula)
        {
            var existingAluno = await _professorService.GetProfessorByIdAsync(matricula);
            if (existingAluno == null)
            {
                return NotFound();
            }
            await _professorService.DeleteProfessorAsync(matricula);
            return NoContent();
        }
    }
}
