using EduConnect.Application;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers
{
    [ApiController]
    [Route("api/alunos")]
    public class AlunoController(AlunoService service) : ControllerBase
    {
        private readonly AlunoService _alunoService = service;

        [HttpGet]
        public async Task<IActionResult> GetAllAlunos()
        {
            var alunos = await _alunoService.GetAllAlunosAsync();
            return Ok(alunos);
        }
        [HttpGet("{matricula}")]
        public async Task<IActionResult> GetAlunoById(string matricula)
        {
            var aluno = await _alunoService.GetAlunoByIdAsync(matricula);
            if (aluno == null)
            {
                return NotFound();
            }
            return Ok(aluno);
        }
        [HttpPost]
        public async Task<IActionResult> AddAluno([FromBody] EduConnect.Domain.Aluno aluno)
        {
            await _alunoService.AddAlunoAsync(aluno);
            return CreatedAtAction(nameof(GetAlunoById), new { matricula = aluno.Matricula }, aluno);
        }
        [HttpPut("{matricula}")]
        public async Task<IActionResult> UpdateAluno(string matricula, [FromBody] EduConnect.Domain.Aluno aluno)
        {
            if (matricula != aluno.Registro)
            {
                return BadRequest();
            }
            var existingAluno = await _alunoService.GetAlunoByIdAsync(matricula);
            if (existingAluno == null)
            {
                return NotFound();
            }
            await _alunoService.UpdateAlunoAsync(aluno);
            return NoContent();
        }
        [HttpDelete("{matricula}")]
        public async Task<IActionResult> DeleteAluno(string matricula)
        {
            var existingAluno = await _alunoService.GetAlunoByIdAsync(matricula);
            if (existingAluno == null)
            {
                return NotFound();
            }
            await _alunoService.DeleteAlunoAsync(matricula);
            return NoContent();
        }
    }
}
