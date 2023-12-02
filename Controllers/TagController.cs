using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudlessBackend.Dto;
using StudlessBackend.Persistence.Models;
using StudlessBackend.Persistence.Repositories;

namespace StudlessBackend.Controllers;

[Route("api/tag")]
[ApiController]
public class TagController: ControllerBase
{
    private readonly ITagRepository _tagRepository;
    private readonly IMapper _mapper;

    public TagController(ITagRepository tagRepository, IMapper mapper)
    {
        _tagRepository = tagRepository;
        _mapper = mapper;
    }

    [HttpGet("tags")]
    [ProducesResponseType(200)]
    public IActionResult GetTags()
    {
        var result = _mapper.Map<List<TagDto>>(_tagRepository.GetTags().Result);
        return Ok(result);
    }
    
    [HttpGet("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public IActionResult GetTag(long id)
    {
        var result = _mapper.Map<TagDto>(_tagRepository.GetTag(id).Result);
        
        if (result == null)
            return NotFound($"Tag with id: {id} was not found");
        
        return Ok(result);
    }
    
    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult AddTag([FromBody] TagDto? dto)
    {
        if (dto == null)
            return BadRequest(ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
   
        var objectToSave = _mapper.Map<Tag>(dto);
        var result = _tagRepository.AddTag(objectToSave).Result;
        
        if (!result)
            return BadRequest(ModelState);
        
        return Ok("Successfully created");
    }
    
    [HttpDelete("{id:long}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult DeleteTag(long id)
    {
        var tagToDelete = _tagRepository.GetTag(id).Result;
        
        if (tagToDelete == null)
            return NotFound($"Question with id: {id} don't exist");
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = _tagRepository.DeleteTag(tagToDelete).Result;

        if (!result)
            return BadRequest(ModelState);
        
        return NoContent();
    }
}