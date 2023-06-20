using MediatR;

namespace Meetup.Application.Commands.Users.DeleteUser;

public class DeleteUserCommand : IRequest
{
    public required Guid UserId { get; set; }
}