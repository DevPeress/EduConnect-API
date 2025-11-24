using EduConnect.Application.DTO;
using EduConnect.Application.Services;
using EduConnect.Domain;
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
        [HttpGet("Cadastro")]
        public async Task<IActionResult> GetAlunosByCadastro()
        {
            var aluno = await _alunoService.GetLastAluno();
            if (aluno == null)
            {
                return NotFound();
            }
            var matricula = int.Parse(aluno.Registro.Substring(2));
            var novaMatricula = (matricula + 1).ToString();
            return Ok("MA" + novaMatricula);
        }
        [HttpPost]
        public async Task<IActionResult> AddAluno(AlunoDTO dto)
        {
            await _alunoService.AddAlunoAsync(dto);
            return Ok();
        }
        [HttpPut("{matricula}")]
        public async Task<IActionResult> UpdateAluno(string matricula, AlunoDTO dto)
        {
            if (matricula != dto.Registro)
            {
                return BadRequest();
            }
            var existingAluno = await _alunoService.GetAlunoByIdAsync(matricula);
            if (existingAluno == null)
            {
                return NotFound();
            }
            await _alunoService.UpdateAlunoAsync(dto);
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
