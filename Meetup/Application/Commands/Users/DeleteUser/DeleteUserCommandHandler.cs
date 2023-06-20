using MediatR;
using Meetup.Application.Interfaces;
using Meetup.Extensions;
using Meetup.Models;

namespace Meetup.Application.Commands.Users.DeleteUser;

public class DeleteUserCommandHandler: IRequestHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.UserExist(request.UserId))
            throw new NotFoundException<User>(nameof(request.UserId));
        
        await _userRepository.DeleteUser(request.UserId);
        await _userRepository.SaveChangesAsync();
    } 
}