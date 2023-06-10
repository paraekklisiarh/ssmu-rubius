using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cafe.Entities;
using Cafe.Infrastructure;
using Cafe.Services.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Services;

public interface IUserService
{
    public Task<UserDto?> GetUser(int userId);

    public Task<int> CreateUser(UserCreateDto dto);

    public Task<int> CreateAdmin(UserCreateDto dto);
}

public class UserService : IUserService
{
    private readonly AppDbContext _dbContext;

    private readonly IMapper _mapper;

    public UserService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    /// <summary>
    /// Получение информации о пользователе по ID
    /// Запланировано: брать ID из куков
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public Task<UserDto?> GetUser(int userId)
    {
        var user = _dbContext.Users
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(u => u.Id == userId);
        return user;
    }

    /// <summary>
    /// Создание нового пользователя
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<int> CreateUser(UserCreateDto dto)
    {
        var entity = _mapper.Map<User>(dto);
        _dbContext.Users.Add(entity);
        await _dbContext.SaveChangesAsync();

        return entity.Id;
    }

    /// <summary>
    /// Добавление нового администратора
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<int> CreateAdmin(UserCreateDto dto)
    {
        var entity = _mapper.Map<User>(dto);
        entity.Role = Roles.Admin;
        _dbContext.Users.Add(entity);
        await _dbContext.SaveChangesAsync();

        return entity.Id;
    }
}