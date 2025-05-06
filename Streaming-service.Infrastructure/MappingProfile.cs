using AutoMapper;
using Streaming_service.Infrastructure.Entities;


namespace Streaming_service.Infrastructure;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<FavoriteEntity, Domain.Models.Favorite>();
        CreateMap<SongEntity, Domain.Models.Song>();
        CreateMap<AlbumEntity, Domain.Models.Album>();
        CreateMap<ArtistEntity, Domain.Models.Artist>();
    }
}