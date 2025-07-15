using Streaming_service.Application.DTOs;
using Streaming_service.Domain.Models;

namespace Streaming_service.Application.Interfaces;

public interface IArtistsService
{
    Task<ArtistResponse?> GetByIdAsync(int id);
}