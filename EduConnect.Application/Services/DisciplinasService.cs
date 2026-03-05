using EduConnect.Application.DTO.Entities;
using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using FluentResults;

namespace EduConnect.Application.Services
{
    public class DisciplinasService(IDisciplinasRepository disciplinasRepository)
    {
        private readonly IDisciplinasRepository _disciplinasRepository = disciplinasRepository;

        public async Task<Result<(IEnumerable<Disciplinas>, int TotalRegistro)>> GetDisciplinas(FiltroBaseDTO FiltroDTO)
        {
            var filtro = new FiltroDisciplinas
            {
                Page = FiltroDTO.Page,
                Pesquisa = FiltroDTO.Pesquisa
            };

            return await _disciplinasRepository.GetDisciplinas(filtro);
        }

        public async Task<Result<List<Disciplinas>>> GetAllDisciplinas()
        {
            return await _disciplinasRepository.GetAllDisciplinas();
        }

        public async Task<Result<Disciplinas>> GetLastDisciplina()
        {
            var disciplina = await _disciplinasRepository.GetLastDisciplina();
            if (disciplina == null) 
                return Result.Fail("Nenhuma disciplina encontrada.");

            return disciplina;
        }

        public async Task<Result<bool>> CreateDisciplina(DisciplinaCadastroDTO DisciplinaDTO)
        {
            var disciplinaExisting = await _disciplinasRepository.GetDisciplinaById(DisciplinaDTO.Registro);
            if (disciplinaExisting == null)
                return Result.Fail("Disciplina não encontrada.");

            var disciplina = new Disciplinas
            {
                Registro = DisciplinaDTO.Registro,
                Nome = DisciplinaDTO.Nome,
                Descricao = DisciplinaDTO.Descricao,
                DataCriacao = DateOnly.FromDateTime(DateTime.Now)
            };

            return await _disciplinasRepository.CreateDisciplina(disciplina);
        }

        public async Task<Result<bool>> DeleteDisciplina(string Registro)
        {
            var disciplina = await _disciplinasRepository.GetDisciplinaById(Registro);
            if (disciplina == null)
                return Result.Fail("Disciplina não encontrada.");

            return await _disciplinasRepository.DeleteDisciplina(disciplina);
        }
    }
}
