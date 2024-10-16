using CSharpEducation.GroupProject.ChatMSG.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CSharpEducation.GroupProject.ChatMSG.DataBase.Configuration
{
  /// <summary>
  /// Контекст базы данных для приложения, включающий чаты и сообщения.
  /// Наследуется от <see cref="IdentityDbContext{UserEntity}"/> для управления пользователями и их сообщениями.
  /// </summary>
  public class ApplicationDbContext : IdentityDbContext<UserEntity>
  {
    /// <summary>
    /// Таблица чатов в базе данных.
    /// </summary>
    public DbSet<ChatEntity> Chats { get; set; }

    /// <summary>
    /// Таблица сообщения в базе данных
    /// </summary>
    public DbSet<MessageEntity> Messages { get; set; }

    //Таблица пользователей создается библиотекой Identity.

    /// <summary>
    /// Настраивает сущности при создании модели базы данных с помощью Fluent API
    /// </summary>
    /// <param name="builder">Построитель модели для настройки сущностей.</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      builder.Entity<ChatEntity>(entity =>
      {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Name).HasMaxLength(50).IsRequired();
        entity.HasMany(m => m.Messages)
              .WithOne(c => c.Chat)
              .HasForeignKey(c => c.ChatId);
        
        entity.HasMany(c => c.Users)
          .WithMany(u => u.Chats)
          .UsingEntity(j => j.ToTable("UserChats"));
      });

      //TODO в отдельном классе
      builder.Entity<MessageEntity>(entity =>
      {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.UserId).IsRequired();
        entity.Property(e => e.ChatId).IsRequired();
        entity.Property(e => e.Content).IsRequired();
        entity.Property(e => e.DateTime).IsRequired();
        entity.HasOne(u => u.User).WithMany(m => m.Messages).HasForeignKey(u => u.UserId);
      });
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
  }
}
