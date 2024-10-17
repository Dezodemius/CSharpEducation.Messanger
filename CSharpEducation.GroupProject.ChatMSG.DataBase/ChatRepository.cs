using CSharpEducation.GroupProject.ChatMSG.Application.Abstractions;
using CSharpEducation.GroupProject.ChatMSG.Core.Abstractions;
using CSharpEducation.GroupProject.ChatMSG.Core.Entities;
using CSharpEducation.GroupProject.ChatMSG.DataBase.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CSharpEducation.GroupProject.ChatMSG.DataBase
{
  public class ChatRepository<T> : IChatRepository<T> where T : ChatEntity
  {
    private readonly ApplicationDbContext _appDbContext;
    private readonly DbSet<T> _dbSet;

    public async Task<T> Get(int id)
    {
      return await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
    }

    public IQueryable<T> GetAll(bool includeDeleted)
    {
      var query = _dbSet.AsQueryable();

      if (!includeDeleted)
      {
        query = query.Where(e => e.IsDeleted == false);
      }

      return query;
    }

    public async Task<T> Add(T entity)
    {
      await _dbSet.AddAsync(entity);
      await _appDbContext.SaveChangesAsync();

      return entity;
    }

    public async Task Update(T entity)
    {
      _appDbContext.Entry(entity).State = EntityState.Modified;
      await _appDbContext.SaveChangesAsync();
    }

    public async Task Remove(T entity)
    {
      entity.IsDeleted = true;
      _appDbContext.Entry(entity).State = EntityState.Modified;
      await _appDbContext.SaveChangesAsync();
    }

    public async Task HardRemove(T entity)
    {
      _dbSet.Remove(entity);
      await _appDbContext.SaveChangesAsync();
    }

    public async Task<List<UserEntity>> GetChatUsers(int chatId)
    {
      var chatWithUsers = await _dbSet
        .Include(c => c.Users)
        .FirstOrDefaultAsync(c => c.Id == chatId);

      if (chatWithUsers == null)
      {
        return new List<UserEntity>();
      }

      return chatWithUsers.Users.ToList();
    }

    public async Task<List<ChatEntity>> GetUserChats(string userId)
    {
      var user = await _appDbContext.Users
        .Include(u => u.Chats)
        .ThenInclude(c => c.Users)
        .FirstOrDefaultAsync(u => u.Id == userId);

      if (user == null)
      {
        return new List<ChatEntity>();
      }

      return user.Chats.ToList();
    }

    public async Task<List<UserEntity>> GetUsersByIds(List<string> userIds)
    {
      return await _appDbContext.Users
        .Where(u => userIds.Contains(u.Id))
        .ToListAsync();
    }

    public ChatRepository(ApplicationDbContext appDbContext)
    {
      _appDbContext = appDbContext;
      _dbSet = appDbContext.Set<T>();
    }
  }
}