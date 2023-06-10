using Cafe.Services;
using Cafe.Services.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebCafe.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<UserDto>> GetUserInfo(int userId)
    {
        var user = await _service.GetUser(userId);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost("{dto}")]
    public Task<int> Create([FromBody] UserCreateDto dto)
    {
        return _service.CreateUser(dto);
    }

    [HttpPost("createadmin/{dto}")]
    public Task<int> CreateAdmin([FromBody] UserCreateDto dto)
    {
        return _service.CreateAdmin(dto);
    }
}