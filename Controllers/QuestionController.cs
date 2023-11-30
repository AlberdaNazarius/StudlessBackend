using Microsoft.AspNetCore.Mvc;
using StudlessBackend.Persistence.Models;
using StudlessBackend.Persistence.Repositories;

namespace StudlessBackend.Controllers;

[Route("api/question")]
[ApiController]
public class QuestionController : ControllerBase
{
    private readonly IQuestionRepository _questionRepository;
    
    public QuestionController(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    [HttpGet("questions")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Question>))]
    public IActionResult GetQuestions()
    {
        var questions = _questionRepository.GetQuestions();
        return Ok(questions);
    }
}