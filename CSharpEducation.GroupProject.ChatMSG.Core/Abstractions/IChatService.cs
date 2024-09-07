﻿using CSharpEducation.GroupProject.ChatMSG.Core.Models;

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
  }
}
