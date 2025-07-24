using FuzzySharp;
using Streaming_service.Application.DTOs;
using Streaming_service.Application.Interfaces;
using Streaming_service.Domain.Abstractions;


namespace Streaming_service.Application.Services;

public class SearchService : ISearchService
{
    private readonly ISongRepository _songRepository;
    private readonly IAlbumRepository _albumRepository;
    private readonly IArtistRepository _artistRepository;
    private const int MatchLimit = 60;

    public SearchService(ISongRepository songRepository, IAlbumRepository albumRepository, 
        IArtistRepository artistRepository)
    {
        _songRepository = songRepository;
        _albumRepository = albumRepository;
        _artistRepository = artistRepository;
    }

    public async Task<SearchDto?> SearchAsync(string query)
    {
        var albums = await _albumRepository.Get();
        var artists = await _artistRepository.Get();
        var songs = await _songRepository.Get();
        
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
            return new SearchDto
            {
                Type = "Album",
                Data = new AlbumDto
                {
                    Id = bestAlbumMatch.Id,
                    Title = bestAlbumMatch.Title,
                    ArtistName = bestAlbumMatch.Artist.Name,
                    ImagePath = bestAlbumMatch.ImagePath,
                    Songs = bestAlbumMatch.Songs.Select(songsResponse => new SongDto
                    {
                        Title = songsResponse.Title,
                        AlbumTitle = bestAlbumMatch.Title,
                        ArtistName = bestAlbumMatch.Artist.Name,
                        FeaturingArtists = songsResponse.FeaturingArtists.Select(fa => new ArtistDto
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
            return new SearchDto
            {
               Type = "Song",
               Data = new SongDto
               {
                   Id = bestSongMatch.Id,
                   Title = bestSongMatch.Title,
                   ArtistName = bestSongMatch.Artist.Name,
                   FeaturingArtists = bestSongMatch.FeaturingArtists.Select(fa => new ArtistDto
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
            return new SearchDto
            {
                Type = "Artist",
                Data = new ArtistDto
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