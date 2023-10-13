using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class MediaLogic : IMediaLogic
{
    private readonly IMediaDao _mediaDao;

    public MediaLogic(IMediaDao mediaDao)
    {
        _mediaDao = mediaDao;
    }

    public async Task<Media> CreateAsync(MediaCreationDto dto)
    {
        ValidateMediaCreationDto(dto);
        Media? media = null;
        string? type = dto.Type;
        if (type.Equals(Media.Anime))
            media = new Anime(dto.NumberOfEpisodes, dto.Title);
        if (type.Equals(Media.Movie))
            media = new Movie(dto.Title);
        if (type.Equals(Media.TvShow))
            media = new TvShow(dto.NumberOfEpisodes, dto.Title);
        if (media == null || string.IsNullOrEmpty(type))
            throw new Exception("Media has a wrong type!");
        media.Type = type;
        Media created = await _mediaDao.CreateAsync(media);
        return created;
    }

    public async Task UpdateAsync(MediaUpdateDto dto)
    {
        Media? existing = await _mediaDao.GetById(dto.Id);
        if (existing == null)
        {
            throw new Exception($"Media with ID {dto.Id} does not exist!");
        }
        ValidateMediaUpdateDto(dto);
        string type = dto.Type;
        string title = dto.Title;
        double avgRating = dto.AvgRating ?? existing.AvgRating;
        double ernestRating = dto.ErnestRating ?? existing.ErnestRating;
        double viktorRating = dto.ViktorRating ?? existing.ViktorRating;
        string status = dto.Status ?? existing.Status;
        int numberOfEpisodes = 0;
        int currentEpisode = 0;
        if(existing is Anime)
        {
            Anime anime = (Anime)existing;
            numberOfEpisodes = dto.NumberOfEpisodes ?? anime.NumberOfEpisodes;
            currentEpisode = dto.CurrentEpisode ?? anime.CurrentEpisode;
        }

        if (existing is TvShow)
        {
            TvShow tvShow = (TvShow)existing;
            numberOfEpisodes = dto.NumberOfEpisodes ?? tvShow.NumberOfEpisodes;
            currentEpisode = dto.CurrentEpisode ?? tvShow.CurrentEpisode;
        }

        Media updatedMedia = null;
        if (type == MediaCreationDto.Movie)
        {
            updatedMedia = new Movie(title)
            {
                Id = dto.Id,
                AvgRating = avgRating,
                ErnestRating = ernestRating,
                ViktorRating = viktorRating,
                Status = status,
                Type = Media.Movie
            };
        }
        
        if (type == MediaCreationDto.TvShow)
        {
            updatedMedia = new TvShow(numberOfEpisodes,title)
            {
                Id = dto.Id,
                AvgRating = avgRating,
                ErnestRating = ernestRating,
                ViktorRating = viktorRating,
                Status = status,
                Type = Media.TvShow,
                CurrentEpisode = currentEpisode
            };
        }
        
        if (type == MediaCreationDto.Anime)
        {
            updatedMedia = new Anime(numberOfEpisodes,title)
            {
                Id = dto.Id,
                AvgRating = avgRating,
                ErnestRating = ernestRating,
                ViktorRating = viktorRating,
                Status = status,
                Type = Media.Anime,
                CurrentEpisode = currentEpisode
            };
        }
        
        if (updatedMedia == null)
        {
            throw new Exception("Something went wrong with creation!");
        }

        await _mediaDao.UpdateAsync(updatedMedia);
    }

    private void ValidateMediaCreationDto(MediaCreationDto dto)
    {
        if (string.IsNullOrEmpty(dto.Title))
            throw new Exception("Title cannot be empty!");
        if (dto.Type == MediaCreationDto.Anime || dto.Type == MediaCreationDto.TvShow)
            if (dto.NumberOfEpisodes <= 0)
                throw new Exception($"Number of episodes is either 0, not specified or an invalid number!");
    }

    private void ValidateMediaUpdateDto(MediaUpdateDto dto)
    {
        if (string.IsNullOrEmpty(dto.Title))
            throw new Exception("Title cannot be empty!");
        if (dto.Type == MediaCreationDto.Anime || dto.Type == MediaCreationDto.TvShow)
            if (dto.NumberOfEpisodes <= 0)
                throw new Exception($"Number of episodes is either 0, not specified or an invalid number!");
        if (dto.CurrentEpisode > dto.NumberOfEpisodes || dto.CurrentEpisode < 0)
            throw new Exception("Current episode is either an invalid number or above the Media's number of episodes!");
        if (dto.ViktorRating < 0 || dto.ViktorRating > 10 || dto.ErnestRating < 0 || dto.ErnestRating > 10)
            throw new Exception("Rating can only be between 0 and 10");
        if (!dto.Type.Equals(Media.Anime) && !dto.Type.Equals(Media.TvShow) &&
            !dto.Type.Equals(Media.Movie))
            throw new Exception("Media type is invalid!");
    }
}