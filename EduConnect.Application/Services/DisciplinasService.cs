using EduConnect.Application.DTO.Entities;
using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;

namespace EduConnect.Application.Services
{
    public class DisciplinasService(IDisciplinasRepository disciplinasRepository)
    {
        private readonly IDisciplinasRepository _disciplinasRepository = disciplinasRepository;

        public async Task<(IEnumerable<Disciplinas>, int TotalRegistro)> GetDisciplinas(FiltroBaseDTO FiltroDTO)
        {
            var filtro = new FiltroBase
            {
                Page = FiltroDTO.Page
            };
            return await _disciplinasRepository.GetDisciplinas(filtro);
        }

        public async Task<List<Disciplinas>> GetAllDisciplinas()
        {
            return await _disciplinasRepository.GetAllDisciplinas();
        }

        public async Task<Disciplinas?> GetLastDisciplina()
        {
            return await _disciplinasRepository.GetLastDisciplina();
        }

        public async Task<Disciplinas> CreateDisciplina(DisciplinaCadastroDTO DisciplinaDTO)
        {
            var disciplina = new Disciplinas
            {
                Registro = DisciplinaDTO.Registro,
                Nome = DisciplinaDTO.Nome,
                Descricao = DisciplinaDTO.Descricao,
                DataCriacao = DateOnly.FromDateTime(DateTime.Now)
            };
            return await _disciplinasRepository.CreateDisciplina(disciplina);
        }

        public async Task DeleteDisciplina(string Registro)
        {
            await _disciplinasRepository.DeleteDisciplina(Registro);
        }
    }
}
