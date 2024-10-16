using CSharpEducation.GroupProject.ChatMSG.Core.Models;

namespace CSharpEducation.GroupProject.ChatMSG.Core.Abstractions
{
  /// <summary>
  /// Интерфейс сервиса для управления чатами
  /// </summary>
  public interface IChatService
  {
    /// <summary>
    /// Получает все чаты.
    /// </summary>
    /// <returns>Список всех существующих чатов.</returns>
    Task<IEnumerable<Chat>> GetAll();

    /// <summary>
    /// Получает чат по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор чата</param>
    /// <returns>Возвращает обьект нового чата.</returns>
    Task<Chat> GetChat(int id);

    /// <summary>
    /// Создает чат 
    /// </summary>
    /// <param name="chat"></param>
    /// <returns></returns>
    Task CreateChat(Chat chat);
    
    /// <summary>
    /// Получает всех пользователей чата
    /// </summary>
    /// <param name="chatId">Идентификатор чата</param>
    /// <returns>Список пользователей чата</returns>
    Task<List<User>> GetAllChatUsers(int chatId);
    
    /// <summary>
    /// Получает список всех чатов пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <returns>Список чатов пользователя</returns>
    Task<List<Chat>> GetAllUserChats(string userId);
  }
}
