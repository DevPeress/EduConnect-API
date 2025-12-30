using EduConnect.Application.DTO;
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
        [HttpGet("filtro/page/{page}")]
        public async Task<IActionResult> GetRegistros(int page)
        {
            var filtro = new FiltroDTO
            {
                Page = page
            };

            var (registros, total) = await _registroService.GetRegistros(filtro);

            return Ok(new FiltroResponseViewModel<RegistroDTO>
            {
                Dados = registros,
                Total = total
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
