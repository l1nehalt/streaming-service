using Streaming_service.Application.DTOs;
using Streaming_service.Domain.Models;

namespace Streaming_service.Application.Interfaces;

public interface IArtistService
{
    Task<ArtistDto?> GetByIdAsync(long id);
}