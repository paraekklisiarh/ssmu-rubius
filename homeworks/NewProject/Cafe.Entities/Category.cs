namespace Cafe.Entities;

public class Category
{
    /// <summary>
    /// 
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Блюда, относящиеся к данной категории. Многие:многим
    /// </summary>
    public ICollection<Dish> Dishes { get; set; }

    public Category()
    {
        Dishes = new List<Dish>();
    }
}