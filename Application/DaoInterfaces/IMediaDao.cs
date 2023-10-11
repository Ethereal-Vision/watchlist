using Domain.Models;

namespace Application.DaoInterfaces;

public interface IMediaDao
{
    Task<Media> CreateAsync(Media media);
    Task UpdateAsync(Media media);
    Task DeleteAsync(int id);
    Task<Media> GetById(int id);
}