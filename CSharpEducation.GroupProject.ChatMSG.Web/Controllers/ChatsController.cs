using CSharpEducation.GroupProject.ChatMSG.Core.Abstractions;
using CSharpEducation.GroupProject.ChatMSG.Core.Models;
using CSharpEducation.GroupProject.ChatMSG.Web.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSharpEducation.GroupProject.ChatMSG.Web.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ChatsController : Controller
  {
    private IChatService chatService;

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<ChatResponse>>> GetAll()
    {
      var chats = await chatService.GetAll();
      var response = chats.Select(c => new ChatResponse(c.Id, c.Name, c.Users.ToList()));
      return Ok(response);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ChatResponse>> Create([FromBody] CreateChatRequest chatRequest)
    {
      if (chatRequest == null || string.IsNullOrEmpty(chatRequest.Name) || chatRequest.UserIds == null ||
          !chatRequest.UserIds.Any())
      {
        return BadRequest("Invalid chat data or users.");
      }

      Chat newChat = new Chat
      {
        Name = chatRequest.Name
      };

      var chat = await chatService.CreateChat(newChat, chatRequest.UserIds);
      return Ok(new ChatResponse(chat.Id, chat.Name, chat.Users.ToList()));
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<ChatResponse>> Get([FromRoute] int id)
    {
      Chat newChat = await chatService.GetChat(id);
      return Ok(newChat);
    }

    [Authorize]
    [HttpGet("ChatUsers/{id}")]
    public async Task<ActionResult<List<User>>> GetAllChatUsers([FromRoute] int id)
    {
      var allChatUsers = await chatService.GetAllChatUsers(id);
      return Ok(allChatUsers);
    }

    [Authorize]
    [HttpGet("UserChats/{userId}")]
    public async Task<ActionResult<List<Chat>>> GetAllUserChats([FromRoute] string userId)
    {
      var allUserChats = await chatService.GetAllUserChats(userId);
      return Ok(allUserChats);
    }

    [Authorize]
    [HttpDelete("RemoveUser/{chatId}/{userId}")]
    public async Task<IActionResult> RemoveUserFromChat([FromRoute] int chatId, [FromRoute] string userId)
    {
      var result = await chatService.RemoveUserFromChatAsync(chatId, userId);
      if (!result)
      {
        return NotFound(new { message = "Chat or user not found" });
      }

      return NoContent();
    }

    public ChatsController(IChatService service, IMessageService messageService)
    {
      chatService = service;
    }
  }
}