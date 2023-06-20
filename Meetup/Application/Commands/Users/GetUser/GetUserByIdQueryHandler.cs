using AutoMapper;
using MediatR;
using Meetup.Application.Dtos;
using Meetup.Application.Interfaces;
using Meetup.Extensions;
using Meetup.Models;

namespace Meetup.Application.Commands.Users.GetUser;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDetailsDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDetailsDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.UserExist(request.Id))
            throw new NotFoundException<User>(nameof(request.Id));

        var user = await _userRepository.GetUserById(request.Id);
        return _mapper.Map<UserDetailsDto>(user);
    }
}