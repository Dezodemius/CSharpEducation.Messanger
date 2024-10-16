using CSharpEducation.GroupProject.ChatMSG.Application.Abstractions;
using CSharpEducation.GroupProject.ChatMSG.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSharpEducation.GroupProject.ChatMSG.Core.Entities
{
  /// <summary>
  /// Класс, представляющий сущность чата для работы с репозиторием.
  /// </summary>
  public class ChatEntity : BaseEntity
  {
    /// <summary>
    /// Имя чата.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Коллекция сообщений чата.
    /// </summary>
    public ICollection<MessageEntity> Messages { get; set; }

    /// <summary>
    /// Коллекция пользователей.
    /// </summary>
    public ICollection<UserEntity> Users { get; set; }
  }
}