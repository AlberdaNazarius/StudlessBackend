using AutoMapper;
using StudlessBackend.Dtos;
using StudlessBackend.Persistence.Models;

namespace StudlessBackend.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Question, QuestionDto>();
    }
}