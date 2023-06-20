namespace Meetup.Application.Dtos;

public class UserLoginDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Password { get; set; }
}