using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudlessBackend.Dtos;
using StudlessBackend.Persistence.Models;
using StudlessBackend.Persistence.Repositories;

namespace StudlessBackend.Controllers;

[Route("api/question")]
[ApiController]
public class QuestionController : ControllerBase
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IMapper _mapper;

    public QuestionController(IQuestionRepository questionRepository, IMapper mapper)
    {
        _questionRepository = questionRepository;
        _mapper = mapper;
    }

    [HttpGet("questions")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Question>))]
    public IActionResult GetQuestions()
    {
        var questions = _mapper.Map<List<QuestionDto>>(_questionRepository.GetQuestions());
        return Ok(questions);
    }
}