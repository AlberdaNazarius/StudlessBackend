using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudlessBackend.Dto;
using StudlessBackend.Persistence.Models;
using StudlessBackend.Persistence.Repositories;

namespace StudlessBackend.Controllers;

[Route("api/topic")]
[ApiController]
public class TopicController : ControllerBase
{
    private readonly ITopicRepository _topicRepository;
    private readonly IQuestionRepository _questionRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<TopicController> _logger;

    public TopicController(ITopicRepository topicRepository, IQuestionRepository questionRepository, IMapper mapper, ILogger<TopicController> logger)
    {
        _topicRepository = topicRepository;
        _questionRepository = questionRepository;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet("topics")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ICollection<TopicDto>>> GetTopics()
    {
        var result = _mapper.Map<List<TopicDto>>(await _topicRepository.GetTopics());
        return Ok(result);
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TopicDto>> GetTopic(long id)
    {
        var result = _mapper.Map<TopicDto>(await _topicRepository.GetTopic(id));

        if (result == null)
            return NotFound($"{typeof(Topic).Name} with id: {id} was not found");

        return Ok(result);
    }

    [HttpGet("{id:long}/question/recent")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Question>> GetRecentQuestion(long id)
    {
        var topic = await _topicRepository.GetTopic(id);

        if (topic == null)
            return NotFound($"No {typeof(Topic).Name} with id: {id} was not found");

        var questions = topic.Questions;

        if (questions == null || !questions.Any())
            return NotFound($"No questions in {typeof(Topic).Name} with id: {id} was not found");

        var result = questions.OrderByDescending(q => q.AskedDate).First();
        result = (await _questionRepository.GetQuestion(result.Id))!;

        return Ok(_mapper.Map<QuestionDto>(result));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TopicDto>> AddTopic([FromBody] TopicDto dto)
    {
        var topic = _mapper.Map<Topic>(dto);
        var result = await _topicRepository.AddTopic(topic);

        if (!result)
            return BadRequest(ModelState);

        return CreatedAtAction(nameof(GetTopic), new { id = topic.Id }, _mapper.Map<TopicDto>(topic));
    }

    [HttpPost("{id:long}/question/{questionId:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Question>> AddQuestionToTopic(long id, long questionId)
    {
        var topic = await _topicRepository.GetTopic(id);

        if (topic == null)
            return NotFound($"No {typeof(Topic).Name} with id: {id} was not found");

        var question = await _questionRepository.GetQuestion(questionId);

        if (question == null)
            return NotFound($"No {typeof(Question).Name} with id: {questionId} was not found");

        var result = await _topicRepository.AddQuestionToTopic(topic, question);

        if (!result)
            return BadRequest(ModelState);

        return Ok($"Successfully added {typeof(Question).Name} with id: {questionId} to {typeof(Topic).Name}");
    }


    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteTopic(long id)
    {
        var topicToDelete = await _topicRepository.GetTopic(id);

        if (topicToDelete == null)
            return NotFound($"{typeof(Topic).Name} with id: {id} don't exist");

        var result = await _topicRepository.DeleteTopic(topicToDelete);

        if (!result)
            return BadRequest(ModelState);

        return NoContent();
    }
}