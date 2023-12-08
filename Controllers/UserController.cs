using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudlessBackend.Dto;
using StudlessBackend.Persistence.Models;
using StudlessBackend.Persistence.Repositories;

namespace StudlessBackend.Controllers;

[Route("api")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserController(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }


    [HttpGet("profile/{id:long}")]
    [ProducesResponseType(200, Type = typeof(User))]
    [ProducesResponseType(404)]
    public async Task<ActionResult<UserDto>> GetUser(long id)
    {
        var result = _mapper.Map<UserDto>(await _userRepository.GetUser(id));
        
        if (result == null)
            return NotFound($"User {id} was not found");
        
        return Ok(result);
    }



    [HttpPut("signup")]
    [ProducesResponseType(200, Type = typeof(string))]
    [ProducesResponseType(400)]
    public async Task<ActionResult<string>> AddUser([FromBody] UserDto dto)
    {
        Console.WriteLine(dto);
        Console.WriteLine("add user");
        if(_userRepository.FindByEmail(dto.Email)!=null)
            return BadRequest($"User with email '{dto.Email}' already registered");

        
        var objectToSave = _mapper.Map<User>(dto);
        var result = await _userRepository.AddUser(objectToSave);
        if (!result)
            return BadRequest(ModelState);
        
        return Ok("User was successfully created");
    }

    [HttpGet("users")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
    public async Task<ActionResult<ICollection<UserDto>>> getUsers()
    {
        var result = _mapper.Map<List<UserDto>>(await _userRepository.GetUsers());
        return Ok(result);
    }
    [HttpDelete("delete-user/{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<string>> deleteUser(long id)
    {
        Console.WriteLine("ban user started");
        var userToBan =  _userRepository.FindById(id);
        if(await _userRepository.DeleteUser(userToBan.Id))
            return Ok("user was banned");
        return BadRequest("some error with ban");
    }

}