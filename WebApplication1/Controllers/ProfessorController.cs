using EduConnect.Application.DTO;
using EduConnect.Application.Services;
using EduConnect.Domain.Entities;
using EduConnect.Infra.CrossCutting.Utils;
using EduConnect.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers
{
    [ApiController]
    [Route("api/professores")]
    public class ProfessorController(ProfessorService service, ContaService conta) : ControllerBase
    {
        private readonly ProfessorService _professorService = service;
        private readonly ContaService _contaService = conta;

        [HttpGet("filtro/selecionada/{selecionada}/status/{status}/page/{page}")]
        public async Task<IActionResult> GetAllProfessor(string selecionada, string status, int page)
        {
            var filtro = new FiltroDTO
            {
                Categoria = selecionada,
                Status = status,
                Page = page
            };

            var (professores, total) = await _professorService.GetByFilters(filtro);

            return Ok(new FiltroResponseViewModel<ProfessorDTO>
            {
                Dados = professores,
                Total = total
            });
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
                return Ok("P000001");
            }
            // Registro vem no formato PO000123
            var atual = professor.Registro;

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
