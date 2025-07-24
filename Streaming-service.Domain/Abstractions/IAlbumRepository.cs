using Streaming_service.Domain.Models;

namespace Streaming_service.Domain.Abstractions;

public interface IAlbumRepository
{
    Task<List<Album>> Get();
    
    Task<Album?> GetById(long id);

    Task<List<Album>> GetByArtist(long artistId);
}