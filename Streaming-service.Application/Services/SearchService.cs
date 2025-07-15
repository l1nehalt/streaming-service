using FuzzySharp;
using Streaming_service.Application.DTOs;
using Streaming_service.Application.Interfaces;
using Streaming_service.Domain.Abstractions;


namespace Streaming_service.Application.Services;

public class SearchService : ISearchService
{
    private readonly ISongsRepository _songsRepository;
    private readonly IAlbumsRepository _albumsRepository;
    private readonly IArtistsRepository _artistsRepository;
    private const int MatchLimit = 60;

    public SearchService(ISongsRepository songsRepository, IAlbumsRepository albumsRepository, 
        IArtistsRepository artistsRepository)
    {
        _songsRepository = songsRepository;
        _albumsRepository = albumsRepository;
        _artistsRepository = artistsRepository;
    }

    public async Task<SearchResponse?> SearchAsync(string query)
    {
        var albums = await _albumsRepository.Get();
        var artists = await _artistsRepository.Get();
        var songs = await _songsRepository.Get();
        
        if (albums.Count == 0 || songs.Count == 0 || artists.Count == 0) return null;
        
        var bestAlbumMatch = albums.MaxBy(a => Fuzz.Ratio(a.Title.ToLower(), query.ToLower()));
        var bestArtistMatch = artists.MaxBy(a => Fuzz.Ratio(a.Name.ToLower(), query.ToLower()));
        var bestSongMatch = songs.MaxBy(a => Fuzz.Ratio(a.Title.ToLower(), query.ToLower()));
        
        if (bestAlbumMatch == null || bestArtistMatch == null || bestSongMatch == null) return null;
        
        var albumScore = Fuzz.Ratio(bestAlbumMatch.Title.ToLower(), query.ToLower());
        var songScore = Fuzz.Ratio(bestSongMatch.Title.ToLower(), query.ToLower());
        var artistScore = Fuzz.Ratio(bestArtistMatch.Name.ToLower(), query.ToLower());

        if (albumScore > artistScore && albumScore > songScore && albumScore > MatchLimit)
        {
            return new SearchResponse
            {
                Type = "Album",
                Data = new AlbumResponse
                {
                    Id = bestAlbumMatch.Id,
                    Title = bestAlbumMatch.Title,
                    ArtistName = bestAlbumMatch.Artist.Name,
                    ImagePath = bestAlbumMatch.ImagePath,
                    Songs = bestAlbumMatch.Songs.Select(songsResponse => new SongResponse
                    {
                        Title = songsResponse.Title,
                        AlbumTitle = bestAlbumMatch.Title,
                        ArtistName = bestAlbumMatch.Artist.Name,
                        FeaturingArtists = songsResponse.FeaturingArtists.Select(fa => new ArtistResponse
                        {
                            Id = fa.ArtistId,
                            Name = fa.Artist.Name
                        }).ToList(),
                        ImagePath = songsResponse.ImagePath,
                        FilePath = songsResponse.FilePath,
                    }).ToList()
                }
            };
        }
        
        if (songScore > albumScore && songScore > artistScore && songScore > MatchLimit)
        {
            return new SearchResponse
            {
               Type = "Song",
               Data = new SongResponse
               {
                   Id = bestSongMatch.Id,
                   Title = bestSongMatch.Title,
                   ArtistName = bestSongMatch.Artist.Name,
                   FeaturingArtists = bestSongMatch.FeaturingArtists.Select(fa => new ArtistResponse
                   {
                       Id = fa.ArtistId,
                       Name = fa.Artist.Name
                   }).ToList(),
                   FilePath = bestSongMatch.FilePath,
                   AlbumTitle = bestSongMatch.Album.Title,
                   ImagePath = bestSongMatch.ImagePath,
               }
            };
        }

        if (artistScore > songScore && artistScore > albumScore && artistScore > MatchLimit)
        {
            return new SearchResponse
            {
                Type = "Artist",
                Data = new ArtistResponse
                {
                    Id = bestArtistMatch.Id,
                    Name = bestArtistMatch.Name,
                    ImagePath = bestArtistMatch.ImagePath
                }
            };
        }
        return null;
    }
}