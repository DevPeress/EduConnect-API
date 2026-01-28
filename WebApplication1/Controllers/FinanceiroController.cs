using EduConnect.Application.DTO.Entities;
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
                // Corrigido: aguarda a Task para obter o Aluno
                if (f == null || f.Aluno == null || f.Aluno.Registro == null)
                    continue;
              
                var aluno = _alunoService.GetAlunoByIdAsync(f.Aluno.Registro).Result;
                if (aluno.IsFailed)
                    continue;
                
                // Verifica o Status do Pagamento
                var verificarStatus = f.Pago ? "Pago" : f.Cancelado ? "Cancelado" : f.DataVencimento < DateOnly.FromDateTime(DateTime.Now) ? "Atrasado" : "Pendente";
                var dto = new FinanceiroDTO
                {
                    Registro = f.Registro,
                    Categoria = f.Categoria,
                    Valor = f.Valor,
                    DataVencimento = f.DataVencimento,
                    DataPagamento = f.DataPagamento,
                    Aluno = aluno.Value.Nome,
                    Foto = aluno.Value.Foto,
                    Nasc = aluno.Value.Nasc,
                    Status = verificarStatus,
                    Mes = f.DataVencimento.ToString("MMMM")
                };
                financeiroDTOs.Add(dto);
            }
            return financeiroDTOs;
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpGet("Dashboard")] 
        public async Task<IActionResult> TestGetFinanceiros()
        {
            var result = await _financeiroService.GetDashBoard();
            if (result.IsFailed)
                return NotFound();

            var (totalRecebido, totalPendente, totalAtrasado) = result.Value;

            return Ok(new List<CardsFinanceiroResponseViewModel>
            {
                new()
                {
                    Dado = "Recebido",
                    Total = totalRecebido
                },
                new()
                {
                    Dado = "Pendente",
                    Total = totalPendente
                },
                new()
                {
                    Dado = "Atrasado",
                    Total = totalAtrasado
                },
                new()
                {
                    Dado = "Total",
                    Total = totalAtrasado + totalPendente + totalRecebido
                }
            });
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpGet("filtro")]
        public async Task<IActionResult> GetByFilters([FromQuery] FiltroViewModel viewModel)
        {
            var filtro = new FiltroFinanceiroDTO
            {
                Categoria = viewModel.Categoria,
                Status = viewModel.Status,
                Meses = viewModel.Data,
                Page = viewModel.Page,
                Pesquisa = viewModel.Pesquisa
            };

            var result = await _financeiroService.GetByFilters(filtro);
            if (result.IsFailed)
                return NotFound();

            var (financeiros, total) = result.Value;
            var financeiroDTOs = Filtro(financeiros);

            return Ok(new FiltroResponseViewModel<FinanceiroDTO>
                {
                    Total = total,
                    Dados = financeiroDTOs
                }
            );
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpGet("aluno/{Registro}")]
        public async Task<IActionResult> GetByAlunoId(string Registro)
        {
            var financeiros = await _financeiroService.GetByAlunoId(Registro);
            if (financeiros.IsFailed)
                return NotFound();

            var financeiroDTOs = Filtro(financeiros.Value);

            return Ok(financeiroDTOs);
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpGet("{Registro}")]
        public async Task<IActionResult> GetById(string Registro)
        {
            // Pega o Financeiro pelo Id e adiciona o Nome do Aluno e o Status no DTO
            var financeiro = await _financeiroService.GetById(Registro);
            if (financeiro.IsFailed)
                return NotFound();
        
            var aluno = _alunoService.GetAlunoByIdAsync(financeiro.Value.Aluno.Registro).Result;
            if (aluno.IsFailed)
                return NotFound();

            var verificarStatus = financeiro.Value.Pago ? "Pago" : financeiro.Value.Cancelado ? "Cancelado" : financeiro.Value.DataVencimento < DateOnly.FromDateTime(DateTime.Now) ? "Atrasado" : "Pendente";

            var dto = new FinanceiroDTO
            {
                Registro = financeiro.Value.Registro,
                Categoria = financeiro.Value.Categoria,
                Valor = financeiro.Value.Valor,
                DataVencimento = financeiro.Value.DataVencimento,
                DataPagamento = financeiro.Value.DataPagamento,
                Aluno = aluno.Value.Nome,
                Foto = aluno.Value.Foto,
                Nasc = aluno.Value.Nasc,
                Status = verificarStatus,
                Mes = financeiro.Value.DataVencimento.ToString("MMMM")
            };

            return Ok(dto);
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpPost]
        public async Task<IActionResult> AddFinanceiro(FinanceiroCadastroDTO FinanceiroDTO)
        {
            var add = await _financeiroService.AddFinanceiroAsync(FinanceiroDTO);
            if (add.IsFailed)
                return BadRequest();

            return Ok();
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpPut("{Registro}")]
        public async Task<IActionResult> UpdateFinanceiro(string Registro, FinanceiroUpdateDTO FinanceiroDTO)
        {
            if (Registro != FinanceiroDTO.Registro)
                return BadRequest();

            var update = await _financeiroService.UpdateFinanceiroAsync(FinanceiroDTO);
            if (update.IsFailed)
                return NotFound();

            return Ok();
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpDelete("{Registro}")]
        public async Task<IActionResult> DeleteFinanceiro(string Registro)
        {
            var delete = await _financeiroService.DeleteFinanceiroAsync(Registro);
            if (delete.IsFailed)
                return NotFound();

            return Ok();
        }
    }
}
