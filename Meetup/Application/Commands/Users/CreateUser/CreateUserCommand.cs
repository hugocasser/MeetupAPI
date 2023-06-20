using MediatR;
using Meetup.Models;

namespace Meetup.Application.Commands.Users.CreateUser;

public class CreateUserCommand : IRequest<User>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Password { get; set; }
}