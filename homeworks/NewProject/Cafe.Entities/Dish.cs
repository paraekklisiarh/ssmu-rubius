namespace Cafe.Entities;

public class Dish
{
    /// <summary>
    /// 
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Стоимость блюда
    /// </summary>
    public decimal Price { get; set; }
    
    /// <summary>
    /// Категории, к которым относится данное блюдо. Многие:многие
    /// </summary>
    public ICollection<Category> Categories { get; set; }

    public Dish()
    {
        Categories = new List<Category>();
    }
}