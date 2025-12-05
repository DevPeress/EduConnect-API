using EduConnect.Application.DTO;
using EduConnect.Application.Services;
using EduConnect.Domain.Entities;
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

        [HttpGet]
        public async Task<IActionResult> GetAllFinanceiros()
        {
            // Pega todos os Financeiros e adiciona o Nome do Aluno e o Status no DTO
            var financeiros = await _financeiroService.GetAllFinanceirosAsync();
            var financeiroDTOs = Filtro(financeiros.ToList());

            return Ok(financeiroDTOs);
        }

        [HttpGet("Dashboard")] 
        public async Task<IActionResult> TestGetFinanceiros()
        {
            var (totalRecebido, totalPendente, totalAtrasado) = await _financeiroService.GetDashBoard();
            return Ok(new List<CardsFinanceiroDTO>
            {
                new CardsFinanceiroDTO()
                {
                    Dado = "Recebido",
                    Total = totalRecebido
                },
                new CardsFinanceiroDTO()
                {
                    Dado = "Pendente",
                    Total = totalPendente
                },
                new CardsFinanceiroDTO()
                {
                    Dado = "Atrasado",
                    Total = totalAtrasado
                },
                new CardsFinanceiroDTO()
                {
                    Dado = "Total",
                    Total = totalAtrasado + totalPendente + totalRecebido
                }
            });
        }

        [HttpGet("filtro/categoria/{categoria}/status/{status}/data/{data}/page/{page}")]
        public async Task<IActionResult> GetByFilters(string categoria, string status, string data, int page)
        {
            // Pega os Financeiros pelos Filtros e adiciona o Nome do Aluno e o Status no DTO
            var filtro = new FinanceiroFiltroDTO
            {
                Categoria = categoria,
                Status = status,
                Data = data,
                Page = page
            };

            var (financeiros, total) = await _financeiroService.GetByFilters(filtro);
            if (financeiros == null || !financeiros.Any())
            {
                return NotFound();
            }
            var financeiroDTOs = Filtro(financeiros.ToList());

            return Ok(new RetornoFiltro<FinanceiroDTO>
                {
                    Total = total,
                    Dados = financeiroDTOs
            }
            );
        }

        [HttpGet("aluno/{alunoId}")]
        public async Task<IActionResult> GetByAlunoId(int alunoId)
        {
            // Pega os Financeiros pelo Id do Aluno e adiciona o Nome do Aluno e o Status no DTO
            var financeiros = await _financeiroService.GetByAlunoId(alunoId);
            var financeiroDTOs = Filtro(financeiros.ToList());

            return Ok(financeiroDTOs);
        }

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
