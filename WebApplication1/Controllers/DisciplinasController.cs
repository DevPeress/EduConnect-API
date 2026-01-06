using EduConnect.Application.Services;
using EduConnect.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers
{
    [ApiController]
    [Route("api/disciplinas")]
    public class DisciplinasController(DisciplinasService service) : ControllerBase
    {
        private readonly DisciplinasService _disciplinasService = service;

        [Authorize(Roles = "Administrador")]
        [HttpGet("pegarDisciplinas")]
        public async Task<IActionResult> GetAllDisciplinas()
        {
            var disciplinas = await _disciplinasService.GetAllDisciplinas();
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
            return Ok(lista);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet("Cadastro")]
        public async Task<IActionResult> GetDisciplinaByCadastro()
        {
            var disciplinas = await _disciplinasService.GetLastDisciplina();
            if (disciplinas == null)
            {
                return Ok("D000001");
            }
            // Registro vem no formato MA000123
            var atual = disciplinas.Registro;

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
    }
}
