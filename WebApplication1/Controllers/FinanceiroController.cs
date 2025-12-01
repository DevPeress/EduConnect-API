using EduConnect.Application.DTO;
using EduConnect.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers
{
    [ApiController]
    [Route("api/financeiro")]
    public class FinanceiroController(FinanceiroService service, AlunoService alunoService) : ControllerBase
    {
        private readonly FinanceiroService _financeiroService = service;
        private readonly AlunoService _alunoService = alunoService;
        [HttpGet]
        public async Task<IActionResult> GetAllFinanceiros()
        {
            var financeiros = await _financeiroService.GetAllFinanceirosAsync();
            var financeiroDTOs = new List<FinanceiroDTO>();

            foreach (var f in financeiros)
            {
                var aluno = await _alunoService.GetAlunoByIdAsync(f.AlunoId);
                if (aluno == null)
                {
                    continue;
                }
                var dto = new FinanceiroDTO
                {
                    Registro = f.Registro,
                    AlunoId = f.AlunoId,
                    Categoria = f.Categoria,
                    Valor = f.Valor,
                    DataVencimento = f.DataVencimento,
                    Pago = f.Pago,
                    DataPagamento = f.DataPagamento,
                    Cancelado = f.Cancelado,
                    Aluno = aluno.Nome,
                    Nasc = aluno.Nasc
                };
                financeiroDTOs.Add(dto);
            }

            return Ok(financeiroDTOs);
        }
        [HttpGet("aluno/{alunoId}")]
        public async Task<IActionResult> GetByAlunoId(Guid alunoId)
        {
            var financeiros = await _financeiroService.GetByAlunoId(alunoId);
            return Ok(financeiros);
        }
        [HttpGet("categoria/{categoria}")]
        public async Task<IActionResult> GetByCategoria(string categoria)
        {
            var financeiros = await _financeiroService.GetByCategoria(categoria);
            return Ok(financeiros);
        }
        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetByStatus(string status)
        {
            var financeiros = await _financeiroService.GetByStatus(status);
            return Ok(financeiros);
        }
        [HttpGet("daterange")]
        public async Task<IActionResult> GetByDateRange([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate)
        {
            var financeiros = await _financeiroService.GetByDateRange(startDate, endDate);
            return Ok(financeiros);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
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
        public async Task<IActionResult> UpdateFinanceiro(Guid id, FinanceiroDTO dto)
        {
            if (id != dto.Id)
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
        public async Task<IActionResult> DeleteFinanceiro(Guid id)
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
