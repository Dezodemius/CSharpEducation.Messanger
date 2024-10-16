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

    public async Task CreateChat(Chat chat)
    {
      ChatEntity newChat = new ChatEntity() { Name = chat.Name };
      _chatRepository.Add(newChat);
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

    public ChatService(IChatRepository<ChatEntity> repository)
    {
      _chatRepository = repository;
    }
  }
}