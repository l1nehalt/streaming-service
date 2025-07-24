using Streaming_service.Application.DTOs;
using Streaming_service.Domain.Models;

namespace Streaming_service.Application.Interfaces;

public interface IAuthService
{
    Task<AuthDto> RegisterAsync(User user);
    
    Task<AuthDto> LoginAsync(string username, string password);

    Task<UserDto?> GetUserProfile(string username);
}