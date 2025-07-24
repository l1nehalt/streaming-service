using Streaming_service.Domain.Models;

namespace Streaming_service.Domain.Abstractions;

public interface IUserRepository
{
    Task<User?> Create(User user);
    
    Task<bool> CheckPassword(User user, string password);
    
    Task<User?> GetByName(string username);
}