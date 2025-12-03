using EduConnect.Application.DTO;
using EduConnect.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers
{
    [ApiController]
    [Route("api/dashboardadmin")]
    public class DashBoardAdminController(DashBoardAdminService service) : ControllerBase
    {
        private readonly DashBoardAdminService _dashBoardAdminService = service;
        [HttpGet("Cards")]
        public async Task<IActionResult> GetCardsData()
        {
            var totalAlunos = await _dashBoardAdminService.GetTotalAlunosAsync();
            var totalProfessores = await _dashBoardAdminService.GetTotalProfessoresAsync();
            var (aumentoAlunos, aumentoProfessores) = await _dashBoardAdminService.GetAumentoAsync();
            var (porcentagemAlunos, porcentagemProfessores) = await _dashBoardAdminService.GetPorcentagemAsync();
            return Ok(new List<CardsAdminDto>
            {
                new CardsAdminDto                 {
                    Dado = "Alunos",
                    Total = totalAlunos,
                    Aumento = aumentoAlunos,
                    Porcentagem = porcentagemAlunos
                },
                new CardsAdminDto
                {
                    Dado = "Professores",
                    Total = totalProfessores,
                    Aumento = aumentoProfessores,
                    Porcentagem = porcentagemProfessores
                }
            });
        }
    }
}
