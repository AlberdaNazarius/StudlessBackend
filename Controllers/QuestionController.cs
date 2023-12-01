using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudlessBackend.Dto;
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
        var result = _mapper.Map<List<QuestionDto>>(_questionRepository.GetQuestions());
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(Question))]
    public IActionResult GetQuestion(long id)
    {
        var result = _mapper.Map<QuestionDto>(_questionRepository.GetQuestion(id));
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult AddQuestion([FromBody] QuestionDto? dto, [FromQuery] long tagId)
    {
        if (dto == null)
            return BadRequest(ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
   
        var objectToSave = _mapper.Map<Question>(dto);
        _questionRepository.AddQuestion(objectToSave, tagId);
        return Ok("Successfully created");
    }
}