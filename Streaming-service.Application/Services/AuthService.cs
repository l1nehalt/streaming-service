using Streaming_service.Application.DTOs;
using Streaming_service.Application.Interfaces;
using Streaming_service.Domain.Abstractions;
using Streaming_service.Domain.Models;

namespace Streaming_service.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public AuthService(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<AuthDto> RegisterAsync(User user)
    {
        if (await _userRepository.Create(user) == null)
        {
            return AuthDto.Failure("Username is already taken");
        }
        
        return AuthDto.Success(user);
    }

    public async Task<AuthDto> LoginAsync(string username, string password)
    {
        var user = await _userRepository.GetByName(username);

        if (user == null || !await _userRepository.CheckPassword(user, password))
        {
            return AuthDto.Failure("Invalid username or password");
        }
        
        var token = _jwtService.JwtGenerate(user);
        
        return AuthDto.Success(user, token);
    }

    public async Task<UserDto?> GetUserProfile(string username)
    {
        var user = await _userRepository.GetByName(username);

        if (user == null) return null;

        var result = new UserDto
        {
            Id = user.Id,
            Name = user.Username
        };
        
        return result;
    }
}