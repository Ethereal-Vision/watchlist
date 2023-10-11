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
        Media media = null;
        if (dto.Type == MediaCreationDto.Anime)
            media = new Anime(dto.NumberOfEpisodes, dto.Title);
        if (dto.Type == MediaCreationDto.Movie)
            media = new Movie(dto.Title);
        if (dto.Type == MediaCreationDto.TvShow)
            media = new TvShow(dto.NumberOfEpisodes, dto.Title);
        if (media == null)
            throw new Exception("Media has a wrong type!");
        Media created = await _mediaDao.CreateAsync(media);
        return created;
    }

    public void ValidateMediaCreationDto(MediaCreationDto dto)
    {
        if (string.IsNullOrEmpty(dto.Title))
            throw new Exception("Title cannot be empty!");
        if (dto.Type == MediaCreationDto.Anime || dto.Type == MediaCreationDto.TvShow)
            if (dto.NumberOfEpisodes <= 0)
                throw new Exception($"Number of episodes is either 0, not specified or an invalid number!");
    }
}