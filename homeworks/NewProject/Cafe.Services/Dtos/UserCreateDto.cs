using Cafe.Entities;
using Cafe.Services.Mapper;

namespace Cafe.Services.Dtos;

public class UserCreateDto : IMapTo<User>
{
    public string Username { get; set; }
    
    public string Email { get; set; }
    
    public string Password { get; set; }
}