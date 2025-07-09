using Streaming_service.Application.DTOs;
using Streaming_service.Domain.Models;

namespace Streaming_service.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(User user);
    Task<AuthResponse> LoginAsync(string username, string password);
}