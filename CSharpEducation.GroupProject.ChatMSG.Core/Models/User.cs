namespace CSharpEducation.GroupProject.ChatMSG.Core.Models;

/// <summary>
/// Представляет данные пользователя для работы с сервисом.
/// </summary>
public class User
{
  /// <summary>
  /// Идентификатор пользователя.
  /// </summary>
  public string Id { get; set; }

  /// <summary>
  /// Имя пользователя.
  /// </summary>
  public string Name { get; set; }

  /// <summary>
  /// Список чатов пользователя.
  /// </summary>
  public ICollection<Chat> Chats { get; set; }
}