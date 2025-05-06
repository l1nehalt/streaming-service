namespace Streaming_service.Infrastructure.Entities;

public class UserEntity
{
    public long Id { get; set; }
    
    public string Username { get; set; } = string.Empty;
    
    public string Password { get; set; } = string.Empty;
    
    public List<FavoriteEntity>? FavoritesEntities { get; set; }
}