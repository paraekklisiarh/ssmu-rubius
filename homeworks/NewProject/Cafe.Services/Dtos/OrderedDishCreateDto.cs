using Cafe.Entities;
using Cafe.Services.Mapper;

namespace Cafe.Services.Dtos;

public class OrderedDishCreateDto : IMapTo<OrderedDish>
{
    public int DishId { get; set; }
    public int Quantity { get; set; }
}