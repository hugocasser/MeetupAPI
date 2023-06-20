using MediatR;
using Meetup.Application.Interfaces;
using Meetup.Models;

namespace Meetup.Application.Commands.Users.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User()
        {
            Id = Guid.NewGuid(),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            LastName = request.LastName,
            FirstName = request.FirstName
        };

        await _userRepository.AddUser(user);
        await _userRepository.SaveChangesAsync();
        return user;
    }
}