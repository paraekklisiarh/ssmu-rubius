using Cafe.Entities;
using Cafe.Services.Mapper;

namespace Cafe.Services.Dtos;

public class OrderedDishDto : IMapFrom<OrderedDish>
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public DishDto Dish { get; set; }
    public int Quantity { get; set; }
}