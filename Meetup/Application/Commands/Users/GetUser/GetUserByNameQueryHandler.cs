using MediatR;
using Meetup.Application.Interfaces;
using Meetup.Extensions;
using Meetup.Models;

namespace Meetup.Application.Commands.Users.GetUser;

public class GetUserByNameQueryHandler : IRequestHandler<GetUserByNameQuery, User>
{
    private readonly IUserRepository _userRepository;

    public GetUserByNameQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> Handle(GetUserByNameQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByName(request.Name);
        if (user is null)
            throw new NotFoundException<User>(nameof(request.Name));
        
        return user;
    }
}