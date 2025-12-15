using EduConnect.Application.DTO;
using EduConnect.Application.Services;
using EduConnect.Domain.Entities;
using EduConnect.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers
{
    [ApiController]
    [Route("api/financeiro")]
    public class FinanceiroController(FinanceiroService service, AlunoService alunoService) : ControllerBase
    {
        private readonly FinanceiroService _financeiroService = service;
        private readonly AlunoService _alunoService = alunoService;
        private List<FinanceiroDTO> Filtro(List<Financeiro> lista)
        {
            // Recebe a Lista do Financeiro e adiciona o Nome do Aluno e o Status no DTO
            var financeiroDTOs = new List<FinanceiroDTO>();
            foreach (var f in lista)
            {
                var aluno = _alunoService.GetAlunoByIdAsync(f.AlunoId).Result;
                if (aluno == null)
                {
                    continue;
                }
                // Verifica o Status do Pagamento
                var verificarStatus = f.Pago ? "Pago" : f.Cancelado ? "Cancelado" : f.DataVencimento < DateOnly.FromDateTime(DateTime.Now) ? "Atrasado" : "Pendente";
                var dto = new FinanceiroDTO(f)
                {
                    Aluno = aluno.Nome,
                    Nasc = aluno.Nasc,
                    Status = verificarStatus
                };
                financeiroDTOs.Add(dto);
            }
            return financeiroDTOs;
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpGet("Dashboard")] 
        public async Task<IActionResult> TestGetFinanceiros()
        {
            var (totalRecebido, totalPendente, totalAtrasado) = await _financeiroService.GetDashBoard();
            return Ok(new List<CardsFinanceiroResponseViewModel>
            {
                new CardsFinanceiroResponseViewModel()
                {
                    Dado = "Recebido",
                    Total = totalRecebido
                },
                new CardsFinanceiroResponseViewModel()
                {
                    Dado = "Pendente",
                    Total = totalPendente
                },
                new CardsFinanceiroResponseViewModel()
                {
                    Dado = "Atrasado",
                    Total = totalAtrasado
                },
                new CardsFinanceiroResponseViewModel()
                {
                    Dado = "Total",
                    Total = totalAtrasado + totalPendente + totalRecebido
                }
            });
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpGet("filtro/categoria/{categoria}/status/{status}/data/{data}/page/{page}")]
        public async Task<IActionResult> GetByFilters(string categoria, string status, string data, int page)
        {
            var filtro = new FiltroDTO
            {
                Categoria = categoria,
                Status = status,
                Data = data,
                Page = page
            };

            var (financeiros, total) = await _financeiroService.GetByFilters(filtro);
            var financeiroDTOs = Filtro(financeiros.ToList());

            return Ok(new FiltroResponseViewModel<FinanceiroDTO>
                {
                    Total = total,
                    Dados = financeiroDTOs
            }
            );
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpGet("aluno/{alunoId}")]
        public async Task<IActionResult> GetByAlunoId(int alunoId)
        {
            // Pega os Financeiros pelo Id do Aluno e adiciona o Nome do Aluno e o Status no DTO
            var financeiros = await _financeiroService.GetByAlunoId(alunoId);
            var financeiroDTOs = Filtro(financeiros.ToList());

            return Ok(financeiroDTOs);
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // Pega o Financeiro pelo Id e adiciona o Nome do Aluno e o Status no DTO
            var financeiro = await _financeiroService.GetById(id);
            if (financeiro == null)
            {
                return NotFound();
            }
            var aluno = _alunoService.GetAlunoByIdAsync(financeiro.AlunoId).Result;
            if (aluno == null)
            {
                return NotFound();
            }
            // Verifica o Status do Pagamento
            var verificarStatus = financeiro.Pago ? "Pago" : financeiro.Cancelado ? "Cancelado" : financeiro.DataVencimento < DateOnly.FromDateTime(DateTime.Now) ? "Atrasado" : "Pendente";
            var financeiroDTO = new FinanceiroDTO(financeiro)
            {
                Aluno = aluno.Nome,
                Nasc = aluno.Nasc,
                Status = verificarStatus
            };
            return Ok(financeiroDTO);
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpPost]
        public async Task<IActionResult> AddFinanceiro(FinanceiroDTO dto)
        {
            var aluno = _alunoService.GetAlunoByIdAsync(dto.AlunoId).Result;
            if (aluno == null)
            {
                return NotFound();
            }
            // Adiciona um novo Financeiro
            await _financeiroService.AddFinanceiroAsync(dto);
            return Ok();
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFinanceiro(int id, FinanceiroDTO dto)
        {
            // Atualiza um Financeiro existente
            if (id != dto.Registro)
            {
                return BadRequest();
            }
            var existingFinanceiro = await _financeiroService.GetById(id);
            if (existingFinanceiro == null)
            {
                return NotFound();
            }
            await _financeiroService.UpdateFinanceiroAsync(dto);
            return NoContent();
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFinanceiro(int id)
        {
            // Deleta um Financeiro existente
            var existingFinanceiro = await _financeiroService.GetById(id);
            if (existingFinanceiro == null)
            {
                return NotFound();
            }
            await _financeiroService.DeleteFinanceiroAsync(id);
            return NoContent();
        }
    }
}
