using EduConnect.Application.DTO.Entities;
using EduConnect.Application.Services;
using EduConnect.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers
{
    [ApiController]
    [Route("api/turma")]
    public class TurmaController(TurmaService TurmaService) : ControllerBase
    {
        private readonly TurmaService _turmaService = TurmaService;

        [Authorize(Roles = "Administrador, Funcionario, Professor")]
        [HttpGet("filtro")]
        public async Task<IActionResult> GetAllTurmas([FromQuery] FiltroViewModel viewModel)
        {
            var filtro = new FiltroTurmaDTO
            {
                Turno = viewModel.Turno,
                Status = viewModel.Status,
                Page = viewModel.Page,
                Ano = viewModel.Ano,
                Pesquisa = viewModel.Pesquisa
            };

            var result = await _turmaService.GetByFilters(filtro);
            if (result.IsFailed)
                return BadRequest(result.Errors);

            var (turmas, total) = result.Value;

            return Ok(new FiltroResponseViewModel<TurmaDTO>
            {
                Dados = turmas,
                Total = total
            });
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpGet("validas")]
        public async Task<IActionResult> GetTurmasValidas()
        {
            var turmas = await _turmaService.GetTurmasValidasAsync();
            return Ok(turmas);
        }

        [Authorize(Roles = "Administrador, Funcionario, Professor")]
        [HttpGet("{Registro}")]
        public async Task<IActionResult> GetTurmaById(string Registro)
        {
            var turma = await _turmaService.GetTurmaByIdAsync(Registro);
            if (turma.IsFailed)
                return NotFound();

            return Ok(turma);
        }

        [Authorize(Roles = "Administrador, Funcionario, Professor")]
        [HttpGet("pegarInformativos")]
        public async Task<IActionResult> GetInformativosAlunosAsync()
        {
            var anos = await _turmaService.GetInformativos();
            return Ok(anos);
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpGet("Cadastro")]
        public async Task<IActionResult> GetTurmaByCadastro()
        {
            var turma = await _turmaService.GetLastTurma();
            if (turma.IsFailed)
                return Ok("T000001");

            // Registro vem no formato MA000123
            var atual = turma.Value.Registro;

            // Pega somente os números (6 dígitos)
            var numeros = atual.Substring(2);

            // Converte para int
            var numeroAtual = int.Parse(numeros);

            // Incrementa
            var proximo = numeroAtual + 1;

            // Formata para sempre ter 6 dígitos
            var proximoFormatado = proximo.ToString("D6");

            return Ok("T" + proximoFormatado);
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpPost]
        public async Task<IActionResult> CreateTurma([FromBody] TurmaCadastroDTO turmaDTO)
        {
            var add = await _turmaService.AddTurmaAsync(turmaDTO);
            if (add.IsFailed)
                return BadRequest(add.Errors);

            return Ok();
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpPut("{Registro}")]
        public async Task<IActionResult> UpdateTurma(string Registro, [FromBody] TurmaUpdateDTO turmaDTO)
        {
            if (Registro != turmaDTO.Registro)
                return BadRequest();

            var update = await _turmaService.UpdateTurmaAsync(turmaDTO);
            if (update.IsFailed)
                return BadRequest(update.Errors);

            return Ok();
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpDelete("{Registro}")]
        public async Task<IActionResult> DeleteTurma(string Registro)
        {
            var delete = await _turmaService.DeleteTurmaAsync(Registro);
            if (delete.IsFailed)
                return BadRequest(delete.Errors);

            return Ok();
        }
    }
}
