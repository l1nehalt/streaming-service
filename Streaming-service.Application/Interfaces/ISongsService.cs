using Streaming_service.Application.DTOs;
using Streaming_service.Domain.Models;

namespace Streaming_service.Application.Interfaces;

public interface ISongsService
{
    Task<List<SongResponse>> GetSongsAsync();
    
    Task<List<AlbumResponse>> GetAlbumsAsync();
}