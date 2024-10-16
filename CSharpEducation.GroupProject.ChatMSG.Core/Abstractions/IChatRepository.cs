using CSharpEducation.GroupProject.ChatMSG.Application.Abstractions;
using CSharpEducation.GroupProject.ChatMSG.Core.Entities;

namespace CSharpEducation.GroupProject.ChatMSG.Core.Abstractions;

public interface IChatRepository<T> : IRepository<T> where T : BaseEntity
{
  Task<List<UserEntity>> GetChatUsers(int chatId);
}