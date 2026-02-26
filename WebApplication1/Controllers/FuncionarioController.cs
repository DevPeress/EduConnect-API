using EduConnect.Application.DTO.Entities;
using EduConnect.Application.Services;
using EduConnect.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EduConnect.Controllers
{
    [ApiController]
    [Route("api/funcionarios")]
    public class FuncionarioController(FuncionarioService service) : ControllerBase
    {
        private readonly FuncionarioService _funcionarioService = service;

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpGet("filtro")]
        public async Task<IActionResult> GetAllFuncionarios([FromQuery] FiltroViewModel viewModel)
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            if (id == null || role == null)
                return BadRequest();

            var filtro = new FiltroPessoaDTO
            {
                Categoria = viewModel.Selecionada,
                Status = viewModel.Status,
                Page = viewModel.Page,
                Ano = viewModel.Ano,
                Pesquisa = viewModel.Pesquisa
            };

            var result = await _funcionarioService.GetByFilters(filtro, id, role);
            if (result.IsFailed)
                return NotFound();

            var (funcionarios, total) = result.Value;

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
        [HttpGet("{Registro}")]
        public async Task<IActionResult> GetFuncionarioById(string Registro)
        {
            var funcionarios = await _funcionarioService.GetFuncionarioByIdAsync(Registro);
            if (funcionarios.IsFailed)
                 return NotFound();
            
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
            if (funcionario.IsFailed)
                return Ok("F000001");
     
            // Registro vem no formato FO000123
            var atual = funcionario.Value.Registro;

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
        public async Task<IActionResult> AddFuncionario([FromBody] FuncionarioCadastroDTO FuncionarioDTO)
        {
            await _funcionarioService.AddFuncionarioAsync(FuncionarioDTO);
            return Ok();
        }

        [Authorize(Roles = "Administrador)")]
        [HttpPut("{Registro}")]
        public async Task<IActionResult> UpdateFuncionario(string Registro, [FromBody] FuncionarioUpdateDTO FuncionarioDTO)
        {
            if (Registro != FuncionarioDTO.Registro)
                return BadRequest();
          
            var existingFuncionario = await _funcionarioService.GetFuncionarioByIdAsync(Registro);
            if (existingFuncionario.IsFailed)
                return NotFound();
            
            await _funcionarioService.UpdateFuncionarioAsync(FuncionarioDTO, existingFuncionario.Value.DataAdmissao);
            return NoContent();
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("{Registro}")]
        public async Task<IActionResult> DeleteFuncionario(string Registro)
        {
            var existingFuncionario = await _funcionarioService.GetFuncionarioByIdAsync(Registro);
            if (existingFuncionario.IsFailed)
                return NotFound();
           
            await _funcionarioService.DeleteFuncionarioAsync(Registro);
            return NoContent();
        }
    }
}
