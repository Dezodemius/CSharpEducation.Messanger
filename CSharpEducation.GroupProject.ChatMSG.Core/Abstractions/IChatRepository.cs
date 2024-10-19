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
  
  /// <summary>
  /// Получает список пользователей по идентификаторам.
  /// </summary>
  /// <param name="userIds">Список идентификаторов пользователей</param>
  /// <returns>Список пользователей</returns>
  Task<List<UserEntity>> GetUsersByIds(List<string> userIds);
  
  /// <summary>
  /// Удаляет пользователя из чата.
  /// </summary>
  /// <param name="chat">Сущность чата</param>
  /// <param name="user">Сущность пользователя</param>
  /// <returns>Результат удаления</returns>
  Task RemoveUserFromChat(ChatEntity chat, UserEntity user);
}