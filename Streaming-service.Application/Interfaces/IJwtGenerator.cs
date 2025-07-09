using Streaming_service.Domain.Models;

namespace Streaming_service.Application.Interfaces;

public interface IJwtGenerator
{
    string JwtGenerate(User user);
}