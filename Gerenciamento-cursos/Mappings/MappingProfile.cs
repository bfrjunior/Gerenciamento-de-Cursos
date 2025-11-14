using AutoMapper;
using Gerenciamento_cursos.Dto;
using Gerenciamento_cursos.Model;

namespace Gerenciamento_cursos.Mappings
{
  
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
           
            CreateMap<AlunoDto, AlunoModel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Matriculas, opt => opt.Ignore());

            CreateMap<AlunoModel, AlunoDto>();

          
            CreateMap<CursoDto, CursoModel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Matriculas, opt => opt.Ignore());

            CreateMap<CursoModel, CursoDto>();

          
            CreateMap<MatricularDto, MatriculaModel>()
                .ForMember(dest => dest.DataMatricula, opt => opt.Ignore())
                .ForMember(dest => dest.Aluno, opt => opt.Ignore())
                .ForMember(dest => dest.Curso, opt => opt.Ignore());

            CreateMap<MatriculaModel, MatricularDto>();
        }
    }
}