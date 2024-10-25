using AutoMapper;
using MediatR;
using TaskManagement.Application.Interfaces.Auth;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Features.User.Commands.Register;

public class RegisterUserCommandHandler(
    IMapper mapper,
    IUserRepository userRepository,
    IPasswordHasherService passwordHasher) 
    : IRequestHandler<RegisterUserCommand, Guid>
{
    private readonly IMapper _mapper = mapper;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasherService _passwordHasher = passwordHasher;

    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var hashedPassword = _passwordHasher.Generate(request.Password);

        var user = _mapper.Map<Domain.Entities.User>(request);

        await _userRepository.CreateAsync(user);

        return user.Id;
    }
}