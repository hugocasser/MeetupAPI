using MediatR;
using Meetup.Models;

namespace Meetup.Application.Commands.Users.GetUser;

public class GetUserByNameQuery : IRequest<User>
{
    public required string Name { get; set; }
}