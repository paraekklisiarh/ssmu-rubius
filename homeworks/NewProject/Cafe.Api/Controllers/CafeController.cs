using Cafe.Services;
using Cafe.Services.Dtos;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable once CheckNamespace
namespace WebCafe.Controllers;

[ApiController]
[Route("controller")]
public class CafeController : ControllerBase
{
    private readonly ICafeService _service;

    public CafeController(ICafeService service)
    {
        _service = service;
    }

    [HttpGet()]
    public Task<List<CategoryDto>> Get()
    {
        return _service.GetCategories();
    }

    // Create category
    [HttpPost("category/create{dto}")]
    public async Task<ActionResult<int>> CreateCategory(CategoryCreateDto dto)
    {
        return Ok(await _service.CreateCategory(dto));
    }

    // Create dish
    [HttpPost("dish/create{dto}")]
    public async Task<ActionResult<int>> CreateDish(DishCreateDto dto)
    {
        return Ok(await _service.CreateDish(dto));
    }

    /// <summary>
    /// Добавить блюдо в категорию
    /// </summary>
    /// <param name="dishId"></param>
    /// <param name="categoryId"></param>
    /// <returns></returns>
    [HttpPost("category/addDish{dishId:int}/{categoryId}")]
    public async Task<ActionResult> AddDishToCategory(int dishId, int categoryId)
    {
        try
        {
            await _service.AddDishToCategory(dishId: dishId, categoryId: categoryId);
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok();
    }

    // Show dish
    [HttpGet("dish/{dishId}")]
    public async Task<ActionResult<DishDto>> ShowDish(int dishId)
    {
        var dto = await _service.ShowDish(dishId);
        if (dto == null)
        {
            return NotFound();
        }

        return Ok(dto);
    }

    // Show all dishes in category
    [HttpGet("category/{categoryId}")]
    public async Task<ActionResult<List<DishDto>>> ShowDishesInCategory(int categoryId)
    {
        List<DishDto> dto;
        try
        {
            dto = await _service.ShowCategoryDishes(categoryId);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        // Тут я решил не отправлять 404 потому, что это нецелесообразно.
        // Пустая категория тоже существует - покажем её пустой.
        return Ok(dto);
    }
}