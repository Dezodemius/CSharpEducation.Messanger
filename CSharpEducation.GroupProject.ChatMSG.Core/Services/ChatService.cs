using CSharpEducation.GroupProject.ChatMSG.Application.Abstractions;
using CSharpEducation.GroupProject.ChatMSG.Core.Abstractions;
using CSharpEducation.GroupProject.ChatMSG.Core.Entities;
using CSharpEducation.GroupProject.ChatMSG.Core.Models;

namespace CSharpEducation.GroupProject.ChatMSG.Core.Services
{
  public class ChatService : IChatService
  {
    private IChatRepository<ChatEntity> chatRepository;

    public async Task<IEnumerable<Chat>> GetAll()
    {
      List<Chat> chats = new List<Chat>();
      var entities = chatRepository.GetAll();
      chats = entities.Select(chat => new Chat { Id = chat.Id, Name = chat.Name }).ToList();

      return chats;
    }

    public async Task<Chat> GetChat(int id)
    {
      ChatEntity chat = await chatRepository.Get(id);
      return new Chat() { Id = chat.Id, Name = chat.Name };
    }

    public async Task CreateChat(Chat chat)
    {
      ChatEntity newChat = new ChatEntity() { Name = chat.Name };
      chatRepository.Add(newChat);
    }

    public async Task<List<User>> GetAllChatUsers(int chatId)
    {
      var userEntities = await chatRepository.GetChatUsers(chatId);
      return userEntities.Select(u => new User { Id = u.Id, Name = u.UserName }).ToList();
    }

    public ChatService(IChatRepository<ChatEntity> repository)
    {
      chatRepository = repository;
    }
  }
}