using Cafe.Entities;
using Cafe.Services.Mapper;

namespace Cafe.Services.Dtos;

public class CategoryCreateDto : IMapTo<Category>
{
    public string Name { get; set; }
}