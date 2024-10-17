namespace CSharpEducation.GroupProject.ChatMSG.Web.Contracts;

public record class CreateChatRequest(string Name, List<string> UserIds);