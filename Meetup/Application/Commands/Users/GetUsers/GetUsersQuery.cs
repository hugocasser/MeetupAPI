using MediatR;
using Meetup.Application.Dtos;

namespace Meetup.Application.Commands.Users.GetUsers;

public class GetUsersQuery : IRequest<IEnumerable<UserDetailsDto>>
{
    
}