using AutoMapper;
using Streaming_service.Domain.Models;
using Streaming_service.Infrastructure.Entities;

namespace Streaming_service.Infrastructure;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<FavoriteEntity, Favorite>();
        CreateMap<SongEntity, Song>();
        CreateMap<AlbumEntity, Album>();
        CreateMap<ArtistEntity, Artist>();
    }
}