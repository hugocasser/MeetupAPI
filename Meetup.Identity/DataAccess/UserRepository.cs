using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace Meetup.Report.DataAccess;

public class UserRepository
{
    public List<User> TestUsers;

    public UserRepository()
    {
        TestUsers = new List<User>();
        TestUsers.Add(new User()
        {
            UserName = "11",
            Password = "11"
        });
        TestUsers.Add(new User()
        {
            UserName = "12",
            Password = "12"
        });
    }

    public User GetUser(string username)
    {
        try
        {
            return TestUsers.First(user => user.UserName.Equals(username));
        }
        catch
        {
            return null;
        }
    }
}