using Microsoft.AspNetCore.Http;

namespace Streaming_service.Application.DTOs;

public record UploadSongDto(
    string ArtistName, 
    string Title, 
    IFormFile? File
    );