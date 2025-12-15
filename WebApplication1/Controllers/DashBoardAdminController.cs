using EduConnect.Application.DTO;
using EduConnect.Application.Services;
using EduConnect.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers
{
    [ApiController]
    [Route("api/dashboardadmin")]
    public class DashBoardAdminController(DashBoardAdminService service) : ControllerBase
    {
        private readonly DashBoardAdminService _dashBoardAdminService = service;

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpGet("Cards")]
        public async Task<IActionResult> GetCardsData()
        {
            var totalAlunos = await _dashBoardAdminService.GetTotalAlunosAsync();
            var totalProfessores = await _dashBoardAdminService.GetTotalProfessoresAsync();
            var totalTurmas = await _dashBoardAdminService.GetTotalTurmasAsync();
            var (aumentoAlunos, aumentoProfessores, aumentoTurmas) = await _dashBoardAdminService.GetAumentoAsync();
            var (porcentagemAlunos, porcentagemProfessores, porcentagemTurmas) = await _dashBoardAdminService.GetPorcentagemAsync();
            return Ok(new List<CardsAdminResponseViewModel>
            {
                new CardsAdminResponseViewModel
                {
                    Dado = "Alunos",
                    Total = totalAlunos,
                    Aumento = aumentoAlunos,
                    Porcentagem = porcentagemAlunos
                },
                new CardsAdminResponseViewModel
                {
                    Dado = "Professores",
                    Total = totalProfessores,
                    Aumento = aumentoProfessores,
                    Porcentagem = porcentagemProfessores
                },
                new CardsAdminResponseViewModel
                {
                    Dado = "Turmas",
                    Total = totalTurmas,
                    Aumento = aumentoTurmas,
                    Porcentagem = porcentagemTurmas
                }
            });
        }
    }
}
