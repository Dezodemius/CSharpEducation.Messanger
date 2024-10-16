using CSharpEducation.GroupProject.ChatMSG.Application.Abstractions;
using CSharpEducation.GroupProject.ChatMSG.Core.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSharpEducation.GroupProject.ChatMSG.Core.Entities
{
  /// <summary>
  /// Сущность пользователя для работы с репозиторием, наследуемый от IdentityUser.
  /// </summary>
  public class UserEntity : IdentityUser
  {
    /// <summary>
    /// Коллекция сообщений, который отправил этот пользователь.
    /// </summary>
    public ICollection<MessageEntity> Messages { get; }

    /// <summary>
    /// Коллекция чатов, в которых состоит пользователь.
    /// </summary>
    public ICollection<ChatEntity> Chats { get; set; }
  }
}