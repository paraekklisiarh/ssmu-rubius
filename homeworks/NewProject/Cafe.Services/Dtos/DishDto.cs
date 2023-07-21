using Cafe.Entities;
using Cafe.Services.Mapper;

namespace Cafe.Services.Dtos;

public class DishDto : IMapFrom<Dish>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public ICollection<CategoryDto>? Categories { get; set; }
}