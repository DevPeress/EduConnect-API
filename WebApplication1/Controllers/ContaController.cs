using EduConnect.Application.DTO;
using EduConnect.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers
{
    [ApiController]
    [Route("api/contas")]
    public class ContaController(ContaService service, JWTService jwt) : ControllerBase
    {
        private readonly ContaService _contaService = service;
        private readonly JWTService _jwtService = jwt;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] ContaDTO contaDto)
        {
            var conta = await _contaService.GetConta(contaDto.Registro, contaDto.Senha);
            if (conta == null)
            {
                return Unauthorized("Credenciais inválidas.");
            }

            var token = _jwtService.GenerateToken(1,"Admin",contaDto.Lembrar);
            Response.Cookies.Append("auth", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,       // Requer HTTPS
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddHours(2)
            });

            return Ok("Login bem-sucedido.");
        }

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
