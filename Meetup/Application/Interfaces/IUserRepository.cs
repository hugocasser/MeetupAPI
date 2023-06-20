using Meetup.Models;

namespace Meetup.Application.Interfaces;

public interface IUserRepository
{
    public Task<IEnumerable<User>> GetAllUsers();
    public Task<User?> GetUserById(Guid id);
    public Task<bool> UserExist(Guid id);
    public Task<User?> GetUserByName(string username);
    public Task AddUser(User modelUser);
    public Task UpdateUser(User modelUser);
    public Task DeleteUser(Guid id);
    public Task SaveChangesAsync();
}