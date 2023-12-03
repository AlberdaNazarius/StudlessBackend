using AutoMapper;
using StudlessBackend.Dto;
using StudlessBackend.Persistence.Models;

namespace StudlessBackend.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<Answer, AnswerDto>().ReverseMap();
        CreateMap<Tag, TagDto>().ReverseMap();
        CreateMap<QuestionDto, Question>();
        CreateMap<Question, QuestionDto>()
            .ForMember(dto => dto.Tags, opt => opt
                    .MapFrom(q => 
                        q.QuestionTags!
                            .Select(qt => qt.Tag)
                            .ToList()));
    }
}