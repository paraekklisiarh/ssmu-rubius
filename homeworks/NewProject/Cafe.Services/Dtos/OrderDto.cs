using Cafe.Entities;
using Cafe.Services.Mapper;

namespace Cafe.Services.Dtos;

public class OrderDto : IMapFrom<Order>
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; }
    public ICollection<OrderedDishDto> OrderedDishes { get; set; }
}