using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IMediaLogic
{
    Task<Media> CreateAsync(MediaCreationDto dto);
    Task UpdateAsync(MediaUpdateDto dto);
    Task DeleteAsync(int id);
    Task<Media> GetByIdAsync(int id);
    Task<IEnumerable<Media>> GetAsync();
}