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

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpGet]
        public async Task<IActionResult> GetAllDisciplinas()
        {
            var disciplinas = await _disciplinasService.GetAllDisciplinas();
            List<DisciplinasResponseViewModel> lista = [];
            foreach (var disciplina in disciplinas)
            {
                lista.Add(new DisciplinasResponseViewModel
                {
                    Registro = disciplina.Registro,
                    Nome = disciplina.Nome
                });
            }
            return Ok(lista);
        }
    }
}
