using AutoMapper;
using EduConnect.Application.DTO.Entities;
using EduConnect.Domain.Entities;

namespace EduConnect.Infra.IoC
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Aluno, AlunoDTO>().ReverseMap();
            CreateMap<ContaDTO, Conta>().ReverseMap();
            CreateMap<DisciplinaCadastroDTO, Disciplinas>().ReverseMap();
            CreateMap<FinanceiroDTO, Financeiro>().ReverseMap();
            CreateMap<FinanceiroCadastroDTO, Financeiro>().ReverseMap();
            CreateMap<FinanceiroUpdateDTO, Financeiro>().ReverseMap();
            CreateMap<FuncionarioDTO, Funcionario>().ReverseMap();
            CreateMap<FuncionarioCadastroDTO, Funcionario>().ReverseMap();
            CreateMap<FuncionarioUpdateDTO, Funcionario>().ReverseMap();
            CreateMap<ProfessorDTO, Professor>().ReverseMap();
            CreateMap<ProfessorCadastroDTO, Professor>().ReverseMap();
            CreateMap<ProfessorUpdateDTO, Professor>().ReverseMap();
            CreateMap<RegistroDTO, Registro>().ReverseMap();
            CreateMap<Turma, TurmaDTO>()
            .ForMember(dest => dest.Professor,
                opt => opt.MapFrom(src => src.ProfessorResponsavel))
            .ForMember(dest => dest.Horario,
                opt => opt.MapFrom(src => $"{src.Inicio} - {src.Fim}"));
            CreateMap<TurmaDTO, Turma>();
            CreateMap<TurmaCadastroDTO, Turma>().ReverseMap();
            CreateMap<TurmaUpdateDTO, Turma>().ReverseMap();
            CreateMap<NotasDTO, Notas>().ReverseMap();
            CreateMap<NotasCadastroDTO, Notas>().ReverseMap();
            CreateMap<NotasUpdateDTO, Notas>().ReverseMap();
        }
    }
}