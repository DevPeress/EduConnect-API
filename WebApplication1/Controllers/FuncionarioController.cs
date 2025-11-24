using EduConnect.Application.Services;
using EduConnect.Domain;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers
{
    [ApiController]
    [Route("api/funcionarios")]
    public class FuncionarioController(FuncionarioService service) : ControllerBase
    {
        private readonly FuncionarioService _funcionarioService = service;

        [HttpGet]
        public async Task<IActionResult> GetAllFuncionarios()
        {
            var funcionarios = await _funcionarioService.GetAllFuncionariosAsync();
            return Ok(funcionarios);
        }
        [HttpGet("{matricula}")]
        public async Task<IActionResult> GetFuncionarioById(string matricula)
        {
            var funcionarios = await _funcionarioService.GetFuncionarioByIdAsync(matricula);
            if (funcionarios == null)
            {
                return NotFound();
            }
            return Ok(funcionarios);
        }
        [HttpPost]
        public async Task<IActionResult> AddFuncionario(Funcionario funcionario)
        {
            await _funcionarioService.AddFuncionarioAsync(funcionario);
            return CreatedAtAction(nameof(GetFuncionarioById), new { matricula = funcionario.Registro }, funcionario);
        }
        [HttpPut("{matricula}")]
        public async Task<IActionResult> UpdateFuncionario(string matricula, Funcionario funcionario)
        {
            if (matricula != funcionario.Registro)
            {
                return BadRequest();
            }
            var existingFuncionario = await _funcionarioService.GetFuncionarioByIdAsync(matricula);
            if (existingFuncionario == null)
            {
                return NotFound();
            }
            await _funcionarioService.UpdateFuncionarioAsync(funcionario);
            return NoContent();
        }
        [HttpDelete("{matricula}")]
        public async Task<IActionResult> DeleteFuncionario(string matricula)
        {
            var existingFuncionario = await _funcionarioService.GetFuncionarioByIdAsync(matricula);
            if (existingFuncionario == null)
            {
                return NotFound();
            }
            await _funcionarioService.DeleteFuncionarioAsync(matricula);
            return NoContent();
        }
    }
}
