using CSharpEducation.GroupProject.ChatMSG.Application.Abstractions;
using CSharpEducation.GroupProject.ChatMSG.Core.Entities;

namespace CSharpEducation.GroupProject.ChatMSG.Core.Abstractions;

public interface IChatRepository<T> : IRepository<T> where T : BaseEntity
{
  /// <summary>
  /// Получает список всех пользователей чата.
  /// </summary>
  /// <param name="chatId">Идентификатор чата</param>
  /// <returns>Список пользователей чата</returns>
  Task<List<UserEntity>> GetChatUsers(int chatId);
  
  /// <summary>
  /// Получает список чатов пользователя.
  /// </summary>
  /// <param name="userId">Идентификатор пользователя</param>
  /// <returns>Список чатов пользователя</returns>
  Task<List<ChatEntity>> GetUserChats(string userId);
}