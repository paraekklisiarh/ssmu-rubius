namespace Cafe.Entities;

public class User
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Username { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Роль пользвателя: клиент или администратор
    /// </summary>
    public Roles Role { get; set; } = Roles.Client;
}

public enum Roles
{
    Client,
    Admin
}