using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudlessBackend.Dto;
using StudlessBackend.Persistence.Models;
using StudlessBackend.Persistence.Repositories;

namespace StudlessBackend.Controllers;

[Route("api")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public AdminController(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    [HttpGet("admin")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
    public async Task<ActionResult<ICollection<UserDto>>> admin()
    {
        var result = _mapper.Map<List<UserDto>>(await _userRepository.GetUsers());
        return Ok(result);
    }
    [HttpDelete("admin/user/ban/{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<string>> banUser(long id)
    {
        Console.WriteLine("ban user started");
        var userToBan =  _userRepository.FindById(id);
        if(await _userRepository.DelteUser(userToBan))
            return Ok("user was banned");
        return BadRequest("some error with ban");
    }

}