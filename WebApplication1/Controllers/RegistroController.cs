using EduConnect.Application.DTO;
using EduConnect.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers
{
    [ApiController]
    [Route("api/registros")]
    public class RegistroController(RegistroService service) : ControllerBase
    {
        private readonly RegistroService _registroService = service;
        [HttpGet]
        public async Task<IActionResult> GetAllRegistrosAsync()
        {
            var registros = await _registroService.GetAllRegistrosAsync();
            return Ok(registros);
        }
        [HttpGet("DashBoard")]
        public async Task<IActionResult> GetLastRegistrosAsync()
        {
            var registros = await _registroService.GetLastRegistrosAsync();
            return Ok(registros);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegistroById(Guid id)
        {
            var registro = await _registroService.GetRegistroByIdAsync(id);
            return Ok(registro);
        }
        [HttpPost]
        public async Task<IActionResult> AddRegistro(RegistroDTO dto)
        {
            await _registroService.AddRegistroAsync(dto);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRegistro(Guid id, RegistroDTO dto)
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistro(Guid id)
        {
            await _registroService.DeleteRegistroAsync(id);
            return Ok();
        }
    }
}
