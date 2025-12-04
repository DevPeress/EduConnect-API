using EduConnect.Application.DTO;
using EduConnect.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers
{
    [ApiController]
    [Route("api/alunos")]
    public class AlunoController(AlunoService service) : ControllerBase
    {
        private readonly AlunoService _alunoService = service;

        [HttpGet]
        public async Task<IActionResult> GetAlunos(string categoria, string status, string data, int page)
        {
            var filtro = new FiltroPessoaDTO
            {
                Categoria = categoria,
                Status = status,
                Data = data,
                Page = page
            };

            var (alunos, total) = await _alunoService.GetByFilters(filtro);
            if (alunos == null)
            {
                return NotFound();
            }

            return Ok(alunos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAlunoById(int id)
        {
            var aluno = await _alunoService.GetAlunoByIdAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }
            return Ok(aluno);
        }

        [HttpGet("Cadastro")]
        public async Task<IActionResult> GetAlunosByCadastro()
        {
            var aluno = await _alunoService.GetLastAluno();
            if (aluno == null)
            {
                return Ok("MA000001");
            }
            // Registro vem no formato MA000123
            var atual = aluno.Registro;

            // Pega somente os números (6 dígitos)
            var numeros = atual.Substring(2);

            // Converte para int
            var numeroAtual = int.Parse(numeros);

            // Incrementa
            var proximo = numeroAtual + 1;

            // Formata para sempre ter 6 dígitos
            var proximoFormatado = proximo.ToString("D6");

            return Ok("MA" + proximoFormatado);
        }

        [HttpPost]
        public async Task<IActionResult> AddAluno([FromBody] AlunoDTO dto)
        {
            await _alunoService.AddAlunoAsync(dto);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAluno(int id, AlunoDTO dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var existingAluno = await _alunoService.GetAlunoByIdAsync(id);
            if (existingAluno == null)
                return NotFound();

            await _alunoService.UpdateAlunoAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAluno(int id)
        {
            var existingAluno = await _alunoService.GetAlunoByIdAsync(id);
            if (existingAluno == null)
                return NotFound();

            await _alunoService.DeleteAlunoAsync(id);
            return NoContent();
        }
    }
}
