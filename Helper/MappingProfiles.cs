using AutoMapper;
using StudlessBackend.Dto;
using StudlessBackend.Persistence.Models;

namespace StudlessBackend.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, UserDto>()
            .ForMember(dto => dto.AnswersId, opt => opt
                .MapFrom(user => user.Answers.Select(answer => answer.Id).ToArray()))
            .ForMember(dto => dto.QuestionsId, opt => opt
                .MapFrom(user => user.Questions.Select(question => question.Id).ToArray()))
            .ReverseMap(); 
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