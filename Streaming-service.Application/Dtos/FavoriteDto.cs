namespace Streaming_service.Application.DTOs;

public class FavoriteDto
{
    public List<FavoriteSongDto>? FavoriteSongs { get; set; }
    
    public List<FavoriteAlbumDto>? FavoriteAlbums { get; set; }
}