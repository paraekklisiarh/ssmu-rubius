using Cafe.Entities;
using Cafe.Services.Mapper;

namespace Cafe.Services.Dtos;

public class CategoryDto : IMapFrom<Category>
{
    public int Id { get; set; }
    public string Name { get; set; }
}