using EduConnect.Application.DTO.Entities;
using EduConnect.Application.Services;
using EduConnect.Domain.Entities;
using EduConnect.Infra.Data.Migrations;
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
        private List<FinanceiroResponseViewModel> Filtro(List<Financeiro> lista)
        {
            // Recebe a Lista do Financeiro e adiciona o Nome do Aluno e o Status no DTO
            var financeiroDTOs = new List<FinanceiroResponseViewModel>();
            foreach (var f in lista)
            {
                // Corrigido: aguarda a Task para obter o Aluno
                if (f == null || f.Aluno == null || f.Aluno.Registro == null)
                {
                    continue;
                }
                var aluno = _alunoService.GetAlunoByIdAsync(f.Aluno.Registro).Result;
                if (aluno == null)
                {
                    continue;
                }
                // Verifica o Status do Pagamento
                var verificarStatus = f.Pago ? "Pago" : f.Cancelado ? "Cancelado" : f.DataVencimento < DateOnly.FromDateTime(DateTime.Now) ? "Atrasado" : "Pendente";
                var dto = new FinanceiroResponseViewModel
                {
                    Registro = f.Registro,
                    Categoria = f.Categoria,
                    Valor = f.Valor,
                    DataVencimento = f.DataVencimento,
                    DataPagamento = f.DataPagamento,
                    Aluno = aluno.Nome,
                    Foto = aluno.Foto,
                    Nasc = aluno.Nasc,
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
            var (totalRecebido, totalPendente, totalAtrasado) = await _financeiroService.GetDashBoard();
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
        [HttpGet("filtro/categoria/{categoria}/status/{status}/data/{data}/page/{page}")]
        public async Task<IActionResult> GetByFilters(string categoria, string status, string data, int page)
        {
            var filtro = new FiltroFinanceiroDTO
            {
                Categoria = categoria,
                Status = status,
                Meses = data,
                Page = page
            };

            var (financeiros, total) = await _financeiroService.GetByFilters(filtro);
            var financeiroDTOs = Filtro(financeiros.ToList());

            return Ok(new FiltroResponseViewModel<FinanceiroResponseViewModel>
                {
                    Total = total,
                    Dados = financeiroDTOs
            }
            );
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpGet("aluno/{alunoId}")]
        public async Task<IActionResult> GetByAlunoId(string Registro)
        {
            // Pega os Financeiros pelo Id do Aluno e adiciona o Nome do Aluno e o Status no DTO
            var financeiros = await _financeiroService.GetByAlunoId(Registro);
            var financeiroDTOs = Filtro(financeiros.ToList());

            return Ok(financeiroDTOs);
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpGet("{Registro}")]
        public async Task<IActionResult> GetById(string Registro)
        {
            // Pega o Financeiro pelo Id e adiciona o Nome do Aluno e o Status no DTO
            var financeiro = await _financeiroService.GetById(Registro);
            if (financeiro == null)
            {
                return NotFound();
            }
            var aluno = _alunoService.GetAlunoByIdAsync(financeiro.Aluno.Registro).Result;
            if (aluno == null)
            {
                return NotFound();
            }
            // Verifica o Status do Pagamento
            var verificarStatus = financeiro.Pago ? "Pago" : financeiro.Cancelado ? "Cancelado" : financeiro.DataVencimento < DateOnly.FromDateTime(DateTime.Now) ? "Atrasado" : "Pendente";
            var dto = new FinanceiroResponseViewModel
            {
                Registro = financeiro.Registro,
                Categoria = financeiro.Categoria,
                Valor = financeiro.Valor,
                DataVencimento = financeiro.DataVencimento,
                DataPagamento = financeiro.DataPagamento,
                Aluno = aluno.Nome,
                Foto = aluno.Foto,
                Nasc = aluno.Nasc,
                Status = verificarStatus,
                Mes = financeiro.DataVencimento.ToString("MMMM")
            };
            return Ok(dto);
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpPost]
        public async Task<IActionResult> AddFinanceiro(FinanceiroCadastroDTO FinanceiroDTO)
        {
            var aluno = _alunoService.GetAlunoByIdAsync(FinanceiroDTO.Registro).Result;
            if (aluno == null)
            {
                return NotFound();
            }
            // Adiciona um novo Financeiro
            await _financeiroService.AddFinanceiroAsync(FinanceiroDTO);
            return Ok();
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpPut("{Registro}")]
        public async Task<IActionResult> UpdateFinanceiro(string Registro, FinanceiroUpdateDTO FinanceiroDTO)
        {
            // Atualiza um Financeiro existente
            if (Registro != FinanceiroDTO.Registro)
            {
                return BadRequest();
            }
            var existingFinanceiro = await _financeiroService.GetById(Registro);
            if (existingFinanceiro == null)
            {
                return NotFound();
            }
            await _financeiroService.UpdateFinanceiroAsync(FinanceiroDTO);
            return NoContent();
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        [HttpDelete("{Registro}")]
        public async Task<IActionResult> DeleteFinanceiro(string Registro)
        {
            // Deleta um Financeiro existente
            var existingFinanceiro = await _financeiroService.GetById(Registro);
            if (existingFinanceiro == null)
            {
                return NotFound();
            }
            await _financeiroService.DeleteFinanceiroAsync(Registro);
            return NoContent();
        }
    }
}
