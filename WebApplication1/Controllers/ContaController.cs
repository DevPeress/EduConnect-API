using EduConnect.Application.DTO;
using EduConnect.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EduConnect.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class ContaController(ContaService service, JWTService jwt) : ControllerBase
    {
        private readonly ContaService _contaService = service;
        private readonly JWTService _jwtService = jwt;

        [Authorize]
        [HttpGet("usuario")]
        public IActionResult Get()
        {
            var id = User.FindFirst("Registro")?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var nome = User.FindFirst("Nome")?.Value;
            var foto = User.FindFirst("Foto")?.Value;

            return Ok(new { id, role, nome, foto });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] ContaDTO contaDto)
        {
            var conta = await _contaService.GetConta(contaDto.Registro, contaDto.Senha);
            if (conta == null)
            {
                return Unauthorized("Credenciais inválidas.");
            }

            var token = _jwtService.GenerateToken(contaDto.Registro, conta.Cargo, "Fabrício Peres", conta.Foto, contaDto.Lembrar);
            Response.Cookies.Append("auth", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,       // Requer HTTPS
                SameSite = SameSiteMode.Lax,
                Expires = DateTimeOffset.UtcNow.AddHours(2)
            });

            return Ok("Login bem-sucedido.");
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("auth");
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteConta(int id)
        {
            await _contaService.DeleteContaAsync(id);
            return Ok("Conta deletada com sucesso.");
        }
    }
}
