using MediatR;
using Meetup.Application.Dtos;

namespace Meetup.Application.Commands.Users.UpdateUser;

public class UpdateUserCommand : IRequest<UserDetailsDto>
{
    public required Guid Id { get; set; }
    public string? LastName { get; set; }
    public string? FirstName { get; set; }
    public string? Password { get; set; }
}