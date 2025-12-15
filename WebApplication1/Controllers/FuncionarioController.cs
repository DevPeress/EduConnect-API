using EduConnect.Application.DTO;
using EduConnect.Application.Services;
using EduConnect.Domain.Entities;
using EduConnect.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers
{
    [ApiController]
    [Route("api/funcionarios")]
    public class FuncionarioController(FuncionarioService service, ContaService conta) : ControllerBase
    {
        private readonly FuncionarioService _funcionarioService = service;
        private readonly ContaService _contaService = conta;

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpGet("filtro/status/{status}/page/{page}")]
        public async Task<IActionResult> GetAllFuncionarios(string status, int page)
        {
            var filtro = new FiltroDTO
            {
                Status = status,
                Page = page
            };

            var (funcionarios, total) = await _funcionarioService.GetByFilters(filtro);

            return Ok(new FiltroResponseViewModel<FuncionarioDTO>
            {
                Dados = funcionarios,
                Total = total
            });
        }

        [Authorize(Roles = "Administrador, Funcionario")]
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

        [Authorize(Roles = "Administrador")]
        [HttpGet("Cadastro")]
        public async Task<IActionResult> GetLastFuncionarioAsync()
        {
            var funcionario = await _funcionarioService.GetLastFuncionarioAsync();
            if (funcionario == null)
            {
                return Ok("F000001");
            }

            // Registro vem no formato FO000123
            var atual = funcionario.Registro;

            // Pega somente os números (6 dígitos)
            var numeros = atual.Substring(2);

            // Converte para int
            var numeroAtual = int.Parse(numeros);

            // Incrementa
            var proximo = numeroAtual + 1;

            // Formata para sempre ter 6 dígitos
            var proximoFormatado = proximo.ToString("D6");

            return Ok("F" + proximoFormatado);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> AddFuncionario(FuncionarioDTO dto)
        {
            await _funcionarioService.AddFuncionarioAsync(dto);
            var conta = new ContaDTO
            {
                Registro = dto.Registro,
                Senha = "Teste",
                Cargo = "Funcionario"
            };
            await _contaService.AddContaAsync(conta);
            return Ok();
        }

        [Authorize(Roles = "Administrador)")]
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

        [Authorize(Roles = "Administrador")]
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
