using AutoMapper;
using Streaming_service.Domain.Models;
using Streaming_service.Infrastructure.Entities;

namespace Streaming_service.Infrastructure;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<FavoriteEntity, Favorite>()
            .ForMember(dest => dest.Song, opt
                => opt.MapFrom(src => src.SongEntity));
        CreateMap<SongEntity, Song>()
            .ForMember(dest => dest.Artist, opt 
                => opt.MapFrom(src => src.ArtistEntity))
            .ForMember(dest => dest.Album, opt 
            => opt.MapFrom(src => src.AlbumEntity));
        
        CreateMap<AlbumEntity, Album>();
        CreateMap<ArtistEntity, Artist>();
    }
}