using Microsoft.EntityFrameworkCore;
using Streaming_service.Domain.Abstractions;
using Streaming_service.Domain.Models;

namespace Streaming_service.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly StreamingDbContext _context;

    public UserRepository(StreamingDbContext context)
    {
        _context = context;
    }

    public async Task<User?> CreateUser(User user)
    {
        if (await _context.Users.AnyAsync(a => a.Username == user.Username))
        {
            return null;
        }
        
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        
        return user;
    }

    public async Task<bool> CheckPassword(User user, string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, user.Password);
    }

    public async Task<User?> GetUserByName(string username)
    {
        var user = await _context.Users.FirstOrDefaultAsync(a => a.Username == username);
        
        if (user == null) return null;
        
        return user;
    }
}