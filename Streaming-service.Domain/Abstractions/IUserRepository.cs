using Streaming_service.Domain.Models;

namespace Streaming_service.Domain.Abstractions;

public interface IUserRepository
{
    Task<User?> CreateUser(User user);
    
    Task<bool> CheckPassword(User user, string password);
    
    Task<User?> GetUserByName(string username);
}