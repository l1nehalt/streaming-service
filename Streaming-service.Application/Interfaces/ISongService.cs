using Microsoft.AspNetCore.Http;
using Streaming_service.Application.DTOs;
using Streaming_service.Domain.Models;

namespace Streaming_service.Application.Interfaces;

public interface ISongService
{
    Task<List<SongDto>> GetSongsAsync();
    
    Task<List<AlbumDto>> GetAlbumsAsync();

    Task<AlbumDto?> GetAlbumByIdAsync(long albumId);

    Task UploadSongAsync(UploadSongDto uploadSongDto);
}