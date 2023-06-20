using Meetup.Application.Interfaces;
using Meetup.Models;
using Microsoft.EntityFrameworkCore;

namespace Meetup.Persistence.Repositories.Repositories;

public class UserRepository : IUserRepository
{
    private readonly MeetUpDbContext _context;

    public UserRepository(MeetUpDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetUserById(Guid id)
    {
        return await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
    }

    public async Task<bool> UserExist(Guid id)
    {
        return await _context.Users.AnyAsync(user => user.Id == id);
    }

    public async Task<User?> GetUserByName(string name)
    {
        return await _context.Users.FirstOrDefaultAsync(user => name == user.FirstName + " " + user.LastName);
    }

    public async Task AddUser(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task UpdateUser(User user)
    {
        var updateUser = await GetUserById(user.Id);

        if (updateUser is null)
            return;
            
        updateUser.FirstName = user.FirstName;
        updateUser.LastName = user.LastName;
        updateUser.PasswordHash = user.PasswordHash;
    }

    public async Task DeleteUser(Guid id)
    {
        var user = await GetUserById(id);
        if (user is null)
            return;
        
        _context.Users.Remove(user);
    }
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}