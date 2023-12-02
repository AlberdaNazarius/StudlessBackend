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
    public async Task<IActionResult> GetQuestions()
    {
        var result = _mapper.Map<List<QuestionDto>>(await _questionRepository.GetQuestions());
        return Ok(result);
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(200, Type = typeof(Question))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetQuestion(long id)
    {
        var result = _mapper.Map<QuestionDto>(await _questionRepository.GetQuestion(id));
        
        if (result == null)
            return NotFound($"Question with id: {id} was not found");
        
        return Ok(result);
    }

    [HttpPost("{questionId:long}/addTag")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> AddTag(long questionId, [FromQuery] long tagId)
    {
        var question = await _questionRepository.GetQuestion(questionId);
        
        if (question == null)
            return NotFound($"Question with id: {questionId} was not found");
        
        var result = await _questionRepository.AddTag(questionId, tagId);
        
        if (!result)
            return BadRequest(ModelState);
        
        return Ok("Tag was successfully added");
    }

    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> AddQuestion([FromBody] QuestionDto? dto)
    {
        if (dto == null)
            return BadRequest(ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
   
        var objectToSave = _mapper.Map<Question>(dto);
        var result = await _questionRepository.AddQuestion(objectToSave);
        
        if (!result)
            return BadRequest(ModelState);
        
        return Ok("Question was successfully created");
    }

    [HttpPut("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> UpdateQuestion([FromBody] QuestionDto? updatedDtoObject, long id)
    {
        if (updatedDtoObject == null)
            return BadRequest(ModelState);

        if (id != updatedDtoObject.Id)
            return BadRequest(ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var objectToUpdate = _mapper.Map<Question>(updatedDtoObject);
        var result = await _questionRepository.UpdateQuestion(objectToUpdate);
        
        if (!result)
            return BadRequest(ModelState);
        
        return NoContent();
    }

    [HttpDelete("{id:long}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteQuestion(long id)
    {
        var questionToDelete = await _questionRepository.GetQuestion(id);
        
        if (questionToDelete == null)
            return NotFound($"Question with id: {id} don't exist");
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _questionRepository.DeleteQuestion(questionToDelete);

        if (!result)
            return BadRequest(ModelState);
        
        return NoContent();
    }
}