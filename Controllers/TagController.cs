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
    public async Task<ActionResult<ICollection<TagDto>>> GetTags()
    {
        var result = _mapper.Map<List<TagDto>>(await _tagRepository.GetTags());
        return Ok(result);
    }
    
    [HttpGet("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<TagDto>> GetTag(long id)
    {
        var result = _mapper.Map<TagDto>(await _tagRepository.GetTag(id));
        
        if (result == null)
            return NotFound($"Tag with id: {id} was not found");
        
        return Ok(result);
    }
    
    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<string>> AddTag([FromBody] TagDto? dto)
    {
        if (dto == null)
            return BadRequest(ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
   
        var objectToSave = _mapper.Map<Tag>(dto);
        var result = await _tagRepository.AddTag(objectToSave);
        
        if (!result)
            return BadRequest(ModelState);
        
        return Ok("Tag was successfully created");
    }
    
    [HttpDelete("{id:long}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<NoContentResult>> DeleteTag(long id)
    {
        var tagToDelete = await _tagRepository.GetTag(id);
        
        if (tagToDelete == null)
            return NotFound($"Question with id: {id} don't exist");
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _tagRepository.DeleteTag(tagToDelete);

        if (!result)
            return BadRequest(ModelState);
        
        return NoContent();
    }
}