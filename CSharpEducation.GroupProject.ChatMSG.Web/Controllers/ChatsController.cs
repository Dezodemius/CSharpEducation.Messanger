using CSharpEducation.GroupProject.ChatMSG.Core.Abstractions;
using CSharpEducation.GroupProject.ChatMSG.Core.Entities;
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
    public async Task<ActionResult<ChatResponse>> Create([FromBody] ChatRequest chatRequest)
    {
      Chat newChat = new Chat() { Name = chatRequest.Name };
      await chatService.CreateChat(newChat);
      return Ok(newChat);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<ChatResponse>> Get([FromRoute] int id)
    {
      Chat newChat = await chatService.GetChat(id);
      return Ok(newChat);
    }

    [Authorize]
    [HttpGet("GetChatUsers/{id}")]
    public async Task<ActionResult<List<User>>> GetAllChatUsers([FromRoute] int id)
    {
      var allChatUsers = await chatService.GetAllChatUsers(id);
      return Ok(allChatUsers);
    }

    public ChatsController(IChatService service, IMessageService messageService)
    {
      chatService = service;
    }
  }
}