namespace Cafe.Entities;

public class Order
{
    /// <summary>
    /// Идентификатор заказа
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Внешний ключ к сущности "пользователь"
    /// </summary>
    public int UserId { get; set; } 
    
    /// <summary>
    /// Заказавший пользователь
    /// </summary>
    public User User { get; set; }
    
    /// <summary>
    /// Дата и время заказа
    /// </summary>
    public DateTime OrderDate { get; set; }

    /// <summary>
    /// Статус: новый, в работе, готов, отменен
    /// </summary>
    public OrderStatuses Status { get; set; } = OrderStatuses.New;
    
    /// <summary>
    /// Блюда в этом заказе
    /// </summary>
    public ICollection<OrderedDish> OrderedDishes { get; set; }
}

/// <summary>
/// Статусы заказов
/// </summary>
public enum OrderStatuses
{
    New,
    InProgress,
    Completed,
    Canceled
}