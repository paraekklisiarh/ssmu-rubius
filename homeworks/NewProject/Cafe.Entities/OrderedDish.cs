namespace Cafe.Entities;

public class OrderedDish
{
    /// <summary>
    /// Идентификатор заказанного блюда
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Внешний ключ к сущности "заказ": один:один
    /// </summary>
    public int OrderId { get; set; }
    
    /// <summary>
    /// Заказ, в котором это блюдо
    /// </summary>
    public Order Order { get; set; }
    
    /// <summary>
    /// Внешний ключ к сущности "Блюдо": один:один
    /// </summary>
    public int DishId { get; set; } 
    
    /// <summary>
    /// Данное блюдо
    /// </summary>
    public Dish Dish { get; set; }
    
    /// <summary>
    /// Количество заказанных блюд этого типа
    /// </summary>
    public int Quantity { get; set; }
}