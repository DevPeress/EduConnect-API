using EduConnect.Application.DTO.Entities;
using EduConnect.Application.Services;
using EduConnect.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace EduConnect.Controllers
{
    [Authorize(Roles = "Administrador, Funcionario")]
    [ApiController]
    [Route("api/professores")]
    public class ProfessorController(ProfessorService service) : ControllerBase
    {
        private readonly ProfessorService _professorService = service;

        [HttpGet("filtro")]
        public async Task<IActionResult> GetAllProfessor([FromQuery] FiltroViewModel viewModel)
        {
            var filtro = new FiltroPessoaDTO
            {
                Categoria = viewModel.Selecionada,
                Status = viewModel.Status,
                Page = viewModel.Page,
                Ano = viewModel.Ano,
                Pesquisa = viewModel.Pesquisa
            };

            var result = await _professorService.GetByFilters(filtro);
            if (result.IsFailed)
                return BadRequest(result.Errors);

            var (professores, total) = result.Value;
            return Ok(new FiltroResponseViewModel<ProfessorDTO>
            {
                Dados = professores,
                Total = total
            });
        }

        [HttpGet("pegarInformativos")]
        public async Task<IActionResult> GetInformativosAlunosAsync()
        {
            var (anos, salas) = await _professorService.GetInformativos();
            return Ok(new
            {
                Anos = anos,
                Salas = salas
            });
        }

        [HttpGet("{Registro}")]
        public async Task<IActionResult> GetProfessorById(string Registro)
        {
            var professores = await _professorService.GetProfessorByIdAsync(Registro);
            if (professores.IsFailed)
                return NotFound();
        
            return Ok(professores);
        }

        [HttpGet("Cadastro")]
        public async Task<IActionResult> GetLastProfessorAsync()
        {
            var professor = await _professorService.GetLastProfessorAsync();
            if (professor.IsFailed)
                return Ok("P000001");
         
            // Registro vem no formato PO000123
            var atual = professor.Value.Registro;

            // Pega somente os números (6 dígitos)
            var numeros = atual.Substring(2);

            // Converte para int
            var numeroAtual = int.Parse(numeros);

            // Incrementa
            var proximo = numeroAtual + 1;

            // Formata para sempre ter 6 dígitos
            var proximoFormatado = proximo.ToString("D6");

            return Ok("P" + proximoFormatado);
        }

        [HttpPost]
        public async Task<IActionResult> AddProfessor([FromBody] ProfessorCadastroDTO ProfessorDTO)
        {
            await _professorService.AddProfessorAsync(ProfessorDTO);
            return Ok();
        }

        [HttpPut("{Registro}")]
        public async Task<IActionResult> UpdateProfessor(string Registro, [FromBody] ProfessorUpdateDTO ProfessorDTO)
        {
            if (Registro != ProfessorDTO.Registro)
                return BadRequest();
         
            var existingProfessor = await _professorService.GetProfessorByIdAsync(Registro);
            if (existingProfessor.IsFailed)
                return NotFound();
         
            await _professorService.UpdateProfessorAsync(ProfessorDTO, existingProfessor.Value.Contratacao);
            return NoContent();
        }

        [HttpDelete("{Registro}")]
        public async Task<IActionResult> DeleteProfessor(string Registro)
        {
            var existingProfessor = await _professorService.GetProfessorByIdAsync(Registro);
            if (existingProfessor.IsFailed)
                return NotFound();
         
            await _professorService.DeleteProfessorAsync(Registro);
            return NoContent();
        }
    }
}
