using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cafe.Entities;
using Cafe.Infrastructure;
using Cafe.Services.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Services;

public interface IOrderService
{
    public Task<int> CreateOrder(OrderCreateDto dto);

    public Task<OrderDto?> GetOrderInfo(int orderId);
}

public class OrderService : IOrderService
{
    private readonly AppDbContext _dbContext;

    private readonly IMapper _mapper;

    public OrderService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<int> CreateOrder(OrderCreateDto dto)
    {
        if (dto.OrderedDishes.Count == 0)
        {
            throw new ArgumentException("Пустой заказ");
        }
        
        var newOrder = new Order
        {
            OrderDate = DateTime.Now,
            UserId = dto.UserId,
            OrderedDishes = new List<OrderedDish>()
        };
        
        foreach (var dishCreateDto in dto.OrderedDishes)
        {
            newOrder.OrderedDishes.Add(_mapper.Map<OrderedDish>(dishCreateDto));
        }

        _dbContext.Orders.Add(newOrder);

        await _dbContext.SaveChangesAsync();

        return newOrder.Id;
    }

    public async Task<OrderDto?> GetOrderInfo(int orderId)
    {
        return await _dbContext.Orders.ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }
}