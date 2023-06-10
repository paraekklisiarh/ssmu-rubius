using Cafe.Entities;
using Cafe.Services.Mapper;

namespace Cafe.Services.Dtos;

/// <summary>
/// 
/// </summary>
public class OrderCreateDto : IMapTo<Order>
{
    public int UserId { get; set; }
    public ICollection<OrderedDishCreateDto>? OrderedDishes { get; set; }
}