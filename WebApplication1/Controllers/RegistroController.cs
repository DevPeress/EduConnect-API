using EduConnect.Application.DTO.Entities;
using EduConnect.Application.Services;
using EduConnect.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers
{
    [ApiController]
    [Route("api/registros")]
    public class RegistroController(RegistroService service) : ControllerBase
    {
        private readonly RegistroService _registroService = service;

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpGet("filtro/page/{page}/pesquisa/{pesquisa}")]
        public async Task<IActionResult> GetRegistros(int page, string pesquisa)
        {
            var filtro = new FiltroRegistroDTO
            {
                Ano = "",
                Categoria = "",
                Status = "",
                Page = page,
                Pesquisa = pesquisa
            };

            var (registros, total) = await _registroService.GetRegistros(filtro);

            return Ok(new FiltroResponseViewModel<RegistroDTO>
            {
                Dados = registros,
                Total = total
            });
        }

        [HttpGet("DashBoard")]
        public async Task<IActionResult> GetDashBoard()
        {
            return Ok(new List<DashBoardRegistroResponseViewModel>
            {
                new() {
                     Tipo = "Registros",
                    Dado = "Total de Registros",
                    Horario = 10
                },
                new() {
                     Tipo = "Registros",
                    Dado = "Total de Registros",
                    Horario = 10
                }
            });
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegistroById(int id)
        {
            var registro = await _registroService.GetRegistroByIdAsync(id);
            return Ok(registro);
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistro(int id)
        {
            await _registroService.DeleteRegistroAsync(id);
            return Ok();
        }
    }
}
