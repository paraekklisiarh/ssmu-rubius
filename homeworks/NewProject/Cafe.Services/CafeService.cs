using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cafe.Entities;
using Cafe.Infrastructure;
using Cafe.Services.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Services;

public interface ICafeService
{
    Task<List<CategoryDto>> GetCategories();

    Task<int> CreateDish(DishCreateDto dto);

    Task<int> CreateCategory(CategoryCreateDto dto);

    Task AddDishToCategory(int dishId, int categoryId);

    Task<DishDto?> ShowDish(int dishId);

    Task<List<DishDto>> ShowCategoryDishes(int categoryId);

}
public class CafeService: ICafeService
{
    private readonly AppDbContext _dbContext;

    private readonly IMapper _mapper;

    public CafeService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public Task<List<CategoryDto>> GetCategories()
    {
        return _dbContext.Categories
            .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    /// <summary>
    /// Создать блюдо
    /// </summary>
    /// <param name="dto"></param>
    public async Task<int> CreateDish(DishCreateDto dto)
    {
        var dish = _mapper.Map<Dish>(dto);
        await _dbContext.AddAsync(dish);
        await _dbContext.SaveChangesAsync();
        return dish.Id;
    }

    /// <summary>
    /// Создать категорию блюд
    /// </summary>
    /// <param name="dto"></param>
    public async Task<int> CreateCategory(CategoryCreateDto dto)
    {
        var newCategory = _mapper.Map<Category>(dto);
        await _dbContext.Categories.AddAsync(newCategory);
        await _dbContext.SaveChangesAsync();
        return newCategory.Id;
    }

    /// <summary>
    /// Добавить блюдо в категорию
    /// </summary>
    /// <param name="dishId"></param>
    /// <param name="categoryId"></param>
    public async Task AddDishToCategory(int dishId, int categoryId)
    {
        var dish = await _dbContext.Dishes.FirstOrDefaultAsync(d => d.Id == dishId);
        if (dish == null)
        {
            throw new ArgumentException("Такого блюда не существует.");
        }

        var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
        if (category == null)
        {
            throw new ArgumentException("Такой категории не существует.");
        }

        category.Dishes.Add(dish);

        await _dbContext.SaveChangesAsync();
        
    }

    /// <summary>
    /// Показать информацию о блюде
    /// </summary>
    /// <param name="dishId"></param>
    /// <returns></returns>
    public async Task<DishDto?> ShowDish(int dishId)
    {
        return await _dbContext.Dishes
            .ProjectTo<DishDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(d => d.Id == dishId);
    }

    /// <summary>
    /// Показать все блюда в категории
    /// </summary>
    /// <param name="categoryId"></param>
    /// <returns></returns>
    public async Task<List<DishDto>> ShowCategoryDishes(int categoryId)
    {
        if (! await _dbContext.Categories.AnyAsync(c => c.Id == categoryId))
        {
            throw new ArgumentException("Такой категории не существует");
        }

        var categoriesWithDishes = await _dbContext.Categories
            .Include(category => category.Dishes)
            .FirstOrDefaultAsync(category => category.Id == categoryId);

        //var category = await _dbContext.Categories
        //    .FirstOrDefaultAsync(c => c.Id == categoryId);
        var dishes = categoriesWithDishes.Dishes;

        return _mapper.Map<List<DishDto>>(dishes);
    }
}

