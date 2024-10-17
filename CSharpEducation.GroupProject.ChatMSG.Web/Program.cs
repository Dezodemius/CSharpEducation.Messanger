using CSharpEducation.GroupProject.ChatMSG.Application.Abstractions;
using CSharpEducation.GroupProject.ChatMSG.Core.Abstractions;
using CSharpEducation.GroupProject.ChatMSG.Core.Entities;
using CSharpEducation.GroupProject.ChatMSG.Core.Services;
using CSharpEducation.GroupProject.ChatMSG.DataBase;
using CSharpEducation.GroupProject.ChatMSG.DataBase.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CSharpEducation.GroupProject.ChatMSG.Web
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      // Add services to the container.

      builder.Services.AddScoped<IChatRepository<ChatEntity>, ChatRepository<ChatEntity>>();
      builder.Services.AddScoped<IRepository<MessageEntity>, MessageRepository<MessageEntity>>();
      builder.Services.AddScoped<IChatService, ChatService>();
      builder.Services.AddScoped<IMessageService, MessageService>();
      builder.Services.AddControllers();
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen();

      builder.Services.AddIdentity<UserEntity, IdentityRole>(options =>
        {
          options.Password.RequireDigit = false;
          options.Password.RequireLowercase = false;
          options.Password.RequireUppercase = false;
          options.Password.RequireNonAlphanumeric = false;
          options.Password.RequiredLength = 6;
          options.User.RequireUniqueEmail = false;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders()
        .AddApiEndpoints();

      builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(
          builder.Configuration.GetConnectionString("ConnectionString")));
      var app = builder.Build();

      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
      }

      app.UseStaticFiles();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseHttpsRedirection();

      app.MapControllers();

      app.Run();
    }
  }
}