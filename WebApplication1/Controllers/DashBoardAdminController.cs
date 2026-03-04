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
            var (totalAlunos, totalProfessores, totalTurmas, totalPresenca) = await _dashBoardAdminService.GetTotalsAsync();
            var (aumentoAlunos, aumentoProfessores, aumentoTurmas, aumentoPresenca) = await _dashBoardAdminService.GetAumentoAsync();
            var (porcentagemAlunos, porcentagemProfessores, porcentagemTurmas, porcentagemPresenca) = await _dashBoardAdminService.GetPorcentagemAsync();
            return Ok(new List<CardsAdminResponseViewModel>
            {
                new()
                {
                    Dado = "Alunos",
                    Total = totalAlunos,
                    Aumento = aumentoAlunos,
                    Porcentagem = porcentagemAlunos
                },
                new()
                {
                    Dado = "Professores",
                    Total = totalProfessores,
                    Aumento = aumentoProfessores,
                    Porcentagem = porcentagemProfessores
                },
                new()
                {
                    Dado = "Turmas",
                    Total = totalTurmas,
                    Aumento = aumentoTurmas,
                    Porcentagem = porcentagemTurmas
                },
                new() {
                    Dado = "Presença",
                    Total = totalPresenca,
                    Aumento = aumentoPresenca,
                    Porcentagem = porcentagemPresenca
                }
            });
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpGet("Atividades")]
        public async Task<IActionResult> GetAtividadesData()
        {
            var atividades = await _dashBoardAdminService.GetAtividadesAsync();
            List<AtividadesDashBoardResponseViewModel> resposta = [];

            foreach (var atividade in atividades)
            {
                resposta.Add(new AtividadesDashBoardResponseViewModel
                {
                    Tipo = atividade.Action,
                    Dado = atividade.Detalhes,
                    Horario = (int)(DateTime.Now - atividade.CreatedAt).TotalMinutes,
                });
            }

            return Ok(atividades);
        }
    }
}
