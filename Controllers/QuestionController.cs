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

    [HttpGet("{id:long}")]
    [ProducesResponseType(200, Type = typeof(Question))]
    [ProducesResponseType(404)]
    public IActionResult GetQuestion(long id)
    {
        var result = _mapper.Map<QuestionDto>(_questionRepository.GetQuestion(id));
        
        if (result == null)
            return NotFound($"Question with id = {id} was not found");
        
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
        var result = _questionRepository.AddQuestion(objectToSave, tagId);
        
        if (!result)
            return BadRequest(ModelState);
        
        return Ok("Successfully created");
    }

    [HttpPut("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public IActionResult UpdateQuestion([FromBody] QuestionDto? updatedDtoObject, long id)
    {
        if (updatedDtoObject == null)
            return BadRequest(ModelState);

        if (id != updatedDtoObject.Id)
            return BadRequest(ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var objectToUpdate = _mapper.Map<Question>(updatedDtoObject);
        var result = _questionRepository.UpdateQuestion(objectToUpdate);
        
        if (!result)
            return BadRequest(ModelState);
        
        return NoContent();
    }

    [HttpDelete("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult DeleteQuestion(long id)
    {
        var questionToDelete = _questionRepository.GetQuestion(id);
        
        if (questionToDelete == null)
            return NotFound($"Question with id = {id} don't exist");
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = _questionRepository.DeleteQuestion(questionToDelete);

        if (!result)
            return BadRequest(ModelState);
        
        return NoContent();
    }
}