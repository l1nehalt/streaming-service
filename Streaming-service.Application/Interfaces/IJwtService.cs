using Streaming_service.Domain.Models;

namespace Streaming_service.Application.Interfaces;

public interface IJwtService
{
    string JwtGenerate(User user);
}