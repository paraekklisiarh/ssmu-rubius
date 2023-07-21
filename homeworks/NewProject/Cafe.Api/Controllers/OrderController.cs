using Cafe.Services;
using Cafe.Services.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebCafe.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _service;

    public OrderController(IOrderService service)
    {
        _service = service;
    }

    [HttpPost()]
    public async Task<ActionResult<int>> CreateOrder(OrderCreateDto dto)
    {
        try
        {
            return Ok( await _service.CreateOrder(dto));
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }
    
    // Получить информацию о заказе
    [HttpGet("{orderId}")]
    public async Task<ActionResult<OrderDto>> OrderInfo(int orderId)
    {
        var orderDto = await _service.GetOrderInfo(orderId);
        if (orderDto == null)
        {
            return NotFound("Такого заказа не существует");
        }
        
        return Ok(orderDto);
    }
}