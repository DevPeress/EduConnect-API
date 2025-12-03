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
            var financeiroDTOs = new List<FinanceiroDTO>();
            foreach (var f in lista)
            {
                var aluno = _alunoService.GetAlunoByIdAsync(f.AlunoId).Result;
                if (aluno == null)
                {
                    continue;
                }
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
            var financeiros = await _financeiroService.GetAllFinanceirosAsync();
            var financeiroDTOs = Filtro(financeiros.ToList());

            return Ok(financeiroDTOs);
        }
        [HttpGet("DashBoard")]
        public async Task<IActionResult> GetBashBoard()
        {
            decimal recebido = await _financeiroService.GetRecebidosAsync();
            decimal pendente = await _financeiroService.GetPendentesAsync();
            decimal atrasado = await _financeiroService.GetAtrasadosAsync();
            return Ok(new List<CardsFinanceiroDTO>
            {
                new CardsFinanceiroDTO
                {
                    Dado = "Recebido",
                    Number = recebido
                },
                new CardsFinanceiroDTO
                {
                    Dado = "Pendente",
                    Number = pendente
                },
                new CardsFinanceiroDTO
                {
                    Dado = "Atrasado",
                    Number = atrasado
                },
                new CardsFinanceiroDTO
                {
                    Dado = "Total",
                    Number = recebido + atrasado + pendente
                }
            });
        }
        [HttpGet("Filtro/categoria/{categoria}/status/{status}/data/{data}/page/{page}")]
        public async Task<IActionResult> GetByFilters(string categoria, string status, string data, int page)
        {
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
            var financeiros = await _financeiroService.GetByAlunoId(alunoId);
            return Ok(financeiros);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var financeiro = await _financeiroService.GetById(id);
            if (financeiro == null)
            {
                return NotFound();
            }
            return Ok(financeiro);
        }
        [HttpPost]
        public async Task<IActionResult> AddFinanceiro(FinanceiroDTO dto)
        {
            await _financeiroService.AddFinanceiroAsync(dto);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFinanceiro(int id, FinanceiroDTO dto)
        {
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
