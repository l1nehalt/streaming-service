using Streaming_service.Application.DTOs;
using Streaming_service.Application.Interfaces;
using Streaming_service.Domain.Abstractions;
using Streaming_service.Domain.Models;

namespace Streaming_service.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtGenerator _jwtGenerator;

    public AuthService(IUserRepository userRepository, IJwtGenerator jwtGenerator)
    {
        _userRepository = userRepository;
        _jwtGenerator = jwtGenerator;
    }

    public async Task<AuthResponse> RegisterAsync(User user)
    {
        if (await _userRepository.CreateUser(user) == null)
        {
            return AuthResponse.Failure("Username is already taken");
        }
        
        return AuthResponse.Success(user);
    }

    public async Task<AuthResponse> LoginAsync(string username, string password)
    {
        var user = await _userRepository.GetUserByName(username);

        if (user == null || !await _userRepository.CheckPassword(user, password))
        {
            return AuthResponse.Failure("Invalid username or password");
        }
        
        var token = _jwtGenerator.JwtGenerate(user);
        
        return AuthResponse.Success(user, token);
    }
}