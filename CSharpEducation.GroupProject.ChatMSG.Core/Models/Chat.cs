namespace CSharpEducation.GroupProject.ChatMSG.Core.Models
{
  /// <summary>
  /// Сущность чата для работы с сервисом.
  /// </summary>
  public class Chat
  {
    /// <summary>
    /// Идентификатор чата
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Имя чата.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Пользователи чата.
    /// </summary>
    public ICollection<User> Users { get; set; }
  }
}
