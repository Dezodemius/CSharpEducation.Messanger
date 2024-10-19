using CSharpEducation.GroupProject.ChatMSG.Core.Abstractions;
using CSharpEducation.GroupProject.ChatMSG.Core.Entities;
using CSharpEducation.GroupProject.ChatMSG.Core.Models;

namespace CSharpEducation.GroupProject.ChatMSG.Core.Services
{
  public class ChatService : IChatService
  {
    private readonly IChatRepository<ChatEntity> _chatRepository;

    public async Task<IEnumerable<Chat>> GetAll()
    {
      List<Chat> chats = new List<Chat>();
      var entities = _chatRepository.GetAll();
      chats = entities.Select(chat => new Chat { Id = chat.Id, Name = chat.Name }).ToList();

      return chats;
    }

    public async Task<Chat> GetChat(int id)
    {
      ChatEntity chat = await _chatRepository.Get(id);
      return new Chat() { Id = chat.Id, Name = chat.Name };
    }

    public async Task<Chat> CreateChat(Chat chat, List<string> userIds)
    {
      var users = await _chatRepository.GetUsersByIds(userIds);

      if (users.Count != userIds.Count)
      {
        throw new Exception("Some users were not found.");
      }

      ChatEntity newChat = new ChatEntity
      {
        Name = chat.Name,
        Users = users
      };

      var chatEntity = await _chatRepository.Add(newChat);

      return new Chat
      {
        Id = chatEntity.Id, Name = chatEntity.Name, Users = chatEntity.Users.Select(
          u => new User() { Id = u.Id, Name = u.UserName }
        ).ToList()
      };
    }

    public async Task<List<User>> GetAllChatUsers(int chatId)
    {
      var userEntities = await _chatRepository.GetChatUsers(chatId);
      return userEntities.Select(u => new User { Id = u.Id, Name = u.UserName }).ToList();
    }

    public async Task<List<Chat>> GetAllUserChats(string userId)
    {
      var chatEntities = await _chatRepository.GetUserChats(userId);
      return chatEntities.Select(chat =>
        new Chat
        {
          Id = chat.Id,
          Name = chat.Name,
          Users = chat.Users.Select(user => new User { Id = user.Id, Name = user.UserName }).ToList()
        }).ToList();
    }

    public async Task<bool> RemoveUserFromChatAsync(int chatId, string userId)
    {
      var chat = await _chatRepository.Get(chatId);
      if (chat == null) return false;

      var user = _chatRepository.GetChatUsers(chatId).Result.FirstOrDefault(u => u.Id == userId);

      await _chatRepository.RemoveUserFromChat(chat, user);

      return true;
    }

    public ChatService(IChatRepository<ChatEntity> repository)
    {
      _chatRepository = repository;
    }
  }
}