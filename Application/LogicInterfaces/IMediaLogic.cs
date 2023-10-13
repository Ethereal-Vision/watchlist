using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IMediaLogic
{
    Task<Media> CreateAsync(MediaCreationDto dto);
    Task UpdateAsync(MediaUpdateDto dto);
}