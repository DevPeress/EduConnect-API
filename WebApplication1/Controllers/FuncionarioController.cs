using EduConnect.Application.DTO.Entities;
using EduConnect.Application.Services;
using EduConnect.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers
{
    [ApiController]
    [Route("api/funcionarios")]
    public class FuncionarioController(FuncionarioService service) : ControllerBase
    {
        private readonly FuncionarioService _funcionarioService = service;

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpGet("filtro/selecionada/{selecionada}/status/{status}/page/{page}/ano/{ano}")]
        public async Task<IActionResult> GetAllFuncionarios(string selecionada, string status, int page, string ano)
        {
            var filtro = new FiltroPessoaDTO
            {
                Categoria = selecionada,
                Status = status,
                Page = page,
                Ano = ano
            };

            var (funcionarios, total) = await _funcionarioService.GetByFilters(filtro);

            var funcionarioDTO = new List<FuncionarioResponseViewModel>(
                funcionarios.Select(f => new FuncionarioResponseViewModel
                {
                    Registro = f.Registro,
                    Nome = f.Nome,
                    Nasc = f.Nasc,
                    Cargo = f.Cargo,
                    Departamento = f.Departamento,
                    DataAdmissao = f.DataAdmissao,
                    Status = f.Status,
                    Foto = f.Foto,
                    Telefone = f.Telefone
                })
            );

            return Ok(new FiltroResponseViewModel<FuncionarioResponseViewModel>
            {
                Dados = funcionarioDTO,
                Total = total
            });
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpGet("{matricula}")]
        public async Task<IActionResult> GetFuncionarioById(string Registro)
        {
            var funcionarios = await _funcionarioService.GetFuncionarioByIdAsync(Registro);
            if (funcionarios == null)
            {
                return NotFound();
            }
            return Ok(funcionarios);
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpGet("pegarInformativos")]
        public async Task<IActionResult> GetInformativosFuncionariosAsync()
        {
            var (deparamentos, anos) = await _funcionarioService.GetInformativos();
            return Ok(new
            {
                Anos = anos,
                Departamentos = deparamentos
            });
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
        public async Task<IActionResult> AddFuncionario([FromBody] FuncionarioDTO FuncionarioDTO)
        {
            await _funcionarioService.AddFuncionarioAsync(FuncionarioDTO);
            return Ok();
        }

        [Authorize(Roles = "Administrador)")]
        [HttpPut("{Registro}")]
        public async Task<IActionResult> UpdateFuncionario(string Registro, [FromBody] FuncionarioDTO FuncionarioDTO)
        {
            if (Registro != FuncionarioDTO.Registro)
            {
                return BadRequest();
            }
            var existingFuncionario = await _funcionarioService.GetFuncionarioByIdAsync(Registro);
            if (existingFuncionario == null)
            {
                return NotFound();
            }
            await _funcionarioService.UpdateFuncionarioAsync(FuncionarioDTO);
            return NoContent();
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("{Registro}")]
        public async Task<IActionResult> DeleteFuncionario(string Registro)
        {
            var existingFuncionario = await _funcionarioService.GetFuncionarioByIdAsync(Registro);
            if (existingFuncionario == null)
            {
                return NotFound();
            }
            await _funcionarioService.DeleteFuncionarioAsync(Registro);
            return NoContent();
        }
    }
}
