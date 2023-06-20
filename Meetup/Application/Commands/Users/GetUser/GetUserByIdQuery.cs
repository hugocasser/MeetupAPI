using MediatR;
using Meetup.Application.Dtos;

namespace Meetup.Application.Commands.Users.GetUser;

public class GetUserByIdQuery: IRequest<UserDetailsDto>
{
    public required Guid Id { get; set; }
}