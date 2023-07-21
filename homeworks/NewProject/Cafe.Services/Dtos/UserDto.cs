using Cafe.Entities;
using Cafe.Services.Mapper;

namespace Cafe.Services.Dtos;

public class UserDto : IMapFrom<User>
{
    public int Id { get; set; }
    
    public string Username { get; set; }
    
    public string Email { get; set; }
    
    public Roles Role { get; set; }
}