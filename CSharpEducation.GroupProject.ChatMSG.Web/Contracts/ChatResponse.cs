using CSharpEducation.GroupProject.ChatMSG.Core.Models;

namespace CSharpEducation.GroupProject.ChatMSG.Web.Contracts
{
  public record class ChatResponse(int Id, string Name, List<User> Users);
}