using EduConnect.Application.DTO;
using EduConnect.Application.Services;
using EduConnect.Domain.Entities;
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
        public async Task<IActionResult> GetProfessorById(int id)
        {
            var professores = await _professorService.GetProfessorByIdAsync(id);
            if (professores == null)
            {
                return NotFound();
            }
            return Ok(professores);
        }
        [HttpGet("Cadastro")]
        public async Task<IActionResult> GetLastProfessorAsync()
        {
            var professor = await _professorService.GetLastProfessorAsync();
            if (professor == null)
            {
                return NotFound();
            }
            var matricula = int.Parse(professor.Registro.Substring(2));
            var novaMatricula = (matricula + 1).ToString();
            return Ok("PO" + novaMatricula);
        }
        [HttpPost]
        public async Task<IActionResult> AddProfessor(ProfessorDTO dto)
        {
            await _professorService.AddProfessorAsync(dto);
            return Ok();
        }
        [HttpPut("{matricula}")]
        public async Task<IActionResult> UpdateProfessor(int id, ProfessorDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }
            var existingProfessor = await _professorService.GetProfessorByIdAsync(id);
            if (existingProfessor == null)
            {
                return NotFound();
            }
            await _professorService.UpdateProfessorAsync(dto);
            return NoContent();
        }
        [HttpDelete("{matricula}")]
        public async Task<IActionResult> DeleteProfessor(int id)
        {
            var existingProfessor = await _professorService.GetProfessorByIdAsync(id);
            if (existingProfessor == null)
            {
                return NotFound();
            }
            await _professorService.DeleteProfessorAsync(id);
            return NoContent();
        }
    }
}
