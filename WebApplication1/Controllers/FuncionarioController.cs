using EduConnect.Application.DTO;
using EduConnect.Application.Services;
using EduConnect.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers
{
    [ApiController]
    [Route("api/funcionarios")]
    public class FuncionarioController(FuncionarioService service) : ControllerBase
    {
        private readonly FuncionarioService _funcionarioService = service;

        [HttpGet("filtro/status/{status}/page/{page}")]
        public async Task<IActionResult> GetAllFuncionarios(string status, int page)
        {
            var filtro = new FiltroPessoaDTO
            {
                Status = status,
                Page = page
            };

            var (funcionarios, total) = await _funcionarioService.GetByFilters(filtro);
            if (funcionarios == null)
            {
                return NotFound();
            }

            return Ok(new RetornoFiltro<FuncionarioDTO>
            {
                Dados = funcionarios,
                Total = total
            });
        }

        [HttpGet("{matricula}")]
        public async Task<IActionResult> GetFuncionarioById(int id)
        {
            var funcionarios = await _funcionarioService.GetFuncionarioByIdAsync(id);
            if (funcionarios == null)
            {
                return NotFound();
            }
            return Ok(funcionarios);
        }

        [HttpPost]
        public async Task<IActionResult> AddFuncionario(FuncionarioDTO dto)
        {
            await _funcionarioService.AddFuncionarioAsync(dto);
            return Ok();
        }

        [HttpPut("{matricula}")]
        public async Task<IActionResult> UpdateFuncionario(int id, FuncionarioDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }
            var existingFuncionario = await _funcionarioService.GetFuncionarioByIdAsync(id);
            if (existingFuncionario == null)
            {
                return NotFound();
            }
            await _funcionarioService.UpdateFuncionarioAsync(dto);
            return NoContent();
        }

        [HttpDelete("{matricula}")]
        public async Task<IActionResult> DeleteFuncionario(int id)
        {
            var existingFuncionario = await _funcionarioService.GetFuncionarioByIdAsync(id);
            if (existingFuncionario == null)
            {
                return NotFound();
            }
            await _funcionarioService.DeleteFuncionarioAsync(id);
            return NoContent();
        }
    }
}
