using AutoMapper;
using MediatR;
using Meetup.Application.Dtos;
using Meetup.Application.Interfaces;

namespace Meetup.Application.Commands.Users.GetUsers;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserDetailsDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDetailsDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllUsers();
        return _mapper.Map<IEnumerable<UserDetailsDto>>(users);
    }
}