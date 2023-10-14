using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IMediaService
{
    Task CreateAsync(MediaCreationDto dto);
    Task<ICollection<Media>> GetAsync();
    Task UpdateAsync(MediaUpdateDto dto);
    Task DeleteAsync(int id);
    Task<Media> GetById(int id);
}