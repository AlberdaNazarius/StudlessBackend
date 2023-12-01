using AutoMapper;
using StudlessBackend.Dto;
using StudlessBackend.Persistence.Models;

namespace StudlessBackend.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Question, QuestionDto>();
        CreateMap<QuestionDto, Question>();
        
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
        
        CreateMap<Answer, AnswerDto>();
        CreateMap<AnswerDto, Answer>();

        CreateMap<Tag, TagDto>();
        CreateMap<TagDto, Tag>();
    }
}