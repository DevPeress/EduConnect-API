using EduConnect.Application.DTO;
using EduConnect.Application.Services;
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
        [HttpGet]
        public async Task<IActionResult> GetAllRegistrosAsync()
        {
            var registros = await _registroService.GetAllRegistrosAsync();
            return Ok(registros);
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpGet("DashBoard")]
        public async Task<IActionResult> GetLastRegistrosAsync()
        {
            var registros = await _registroService.GetLastRegistrosAsync();
            return Ok(registros);
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegistroById(int id)
        {
            var registro = await _registroService.GetRegistroByIdAsync(id);
            return Ok(registro);
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpPost]
        public async Task<IActionResult> AddRegistro(RegistroDTO dto)
        {
            await _registroService.AddRegistroAsync(dto);
            return Ok();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRegistro(int id, RegistroDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }
            var existingRegistro = await _registroService.GetRegistroByIdAsync(id);
            if (existingRegistro == null)
            {
                return NotFound();
            }
            await _registroService.UpdateRegistroAsync(dto);
            return Ok();
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
