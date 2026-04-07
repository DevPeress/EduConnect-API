using EduConnect.Application.DTO.Entities;
using EduConnect.Application.Services;
using EduConnect.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EduConnect.Controllers
{
    [Authorize(Roles = "Administrador")]
    [ApiController]
    [Route("api/disciplinas")]
    public class DisciplinasController(DisciplinasService service) : ControllerBase
    {
        private readonly DisciplinasService _disciplinasService = service;

        [HttpGet("filtro")]
        public async Task<IActionResult> GetDisciplinas([FromQuery] FiltroViewModel viewModel)
        {
            var filtro = new FiltroBaseDTO
            {
                Page = viewModel.Page,
                Pesquisa = viewModel.Pesquisa
            };

            var result = await _disciplinasService.GetDisciplinas(filtro);
            if (result == null)
                return NoContent();

            var (disciplinas, total) = result.Value;

            List<DisciplinasResponseViewModel> lista = [];
            foreach (var disciplina in disciplinas)
            {
                lista.Add(new DisciplinasResponseViewModel
                {
                    Registro = disciplina.Registro,
                    Nome = disciplina.Nome,
                    Descricao = disciplina.Descricao,
                    DataCriacao = disciplina.DataCriacao
                });
            }

            return Ok(new FiltroResponseViewModel<DisciplinasResponseViewModel>
            {
                Total = total,
                Dados = lista
            });
        }

        [HttpGet("pegarDisciplinas")]
        public async Task<IActionResult> GetAllDisciplinas()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            if (id == null || role == null)
                return BadRequest();

            var disciplinas = await _disciplinasService.GetAllDisciplinas(role, id);
            if (disciplinas == null)
                return NoContent();

            List<DisciplinasResponseViewModel> lista = [];
            foreach (var disciplina in disciplinas.Value)
            {
                lista.Add(new DisciplinasResponseViewModel
                {
                    Registro = disciplina.Registro,
                    Nome = disciplina.Nome,
                    Descricao = disciplina.Descricao,
                    DataCriacao = disciplina.DataCriacao
                });
            }

            return Ok(lista);
        }

        [HttpGet("Cadastro")]
        public async Task<IActionResult> GetDisciplinaByCadastro()
        {
            var disciplinas = await _disciplinasService.GetLastDisciplina();
            if (disciplinas.IsFailed)
                return Ok("D000001");
         
            // Registro vem no formato MA000123
            var atual = disciplinas.Value.Registro;

            // Pega somente os números (6 dígitos)
            var numeros = atual.Substring(2);

            // Converte para int
            var numeroAtual = int.Parse(numeros);

            // Incrementa
            var proximo = numeroAtual + 1;

            // Formata para sempre ter 6 dígitos
            var proximoFormatado = proximo.ToString("D6");

            return Ok("D" + proximoFormatado);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDisciplina([FromBody] DisciplinaCadastroDTO DisciplinaDTO)
        {
            var create = await _disciplinasService.CreateDisciplina(DisciplinaDTO);
            if (create == null)
                return BadRequest("Erro ao criar disciplina.");

            return Ok("Disciplina criada com sucesso.");
        }

        [HttpDelete("{Registro}")]
        public async Task<IActionResult> DeleteDisciplina(string Registro)
        {
            var delete = await _disciplinasService.DeleteDisciplina(Registro);
            if (delete == null)
                return NotFound("Disciplina não encontrada.");
          
            return Ok();
        }
    }
}
