using Application.DaoInterfaces;
using Domain.Models;

namespace FileData.DAOs;

public class MediaFileDao : IMediaDao
{
    private readonly FileContext _context;

    public MediaFileDao(FileContext context)
    {
        _context = context;
    }

    public Task<Media> CreateAsync(Media media)
    {
        int id = 1;
        if (_context.Medias != null && _context.Medias.Any())
        {
            id = _context.Medias.Max(m => m.Id);
            id++;
        }

        media.Id = id;
        _context.Medias.Add(media);
        _context.SaveChanges();
        return Task.FromResult(media);
    }

    public Task UpdateAsync(Media media)
    {
        Media? existing = _context.Medias?.FirstOrDefault(m => media.Id == m.Id);
        if (existing == null)
        {
            throw new Exception($"A Media with ID {media.Id} does not exist!");
        }

        _context.Medias?.Remove(existing);
        _context.Medias?.Add(media);
        _context.SaveChanges();
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        Media? existing = _context.Medias?.FirstOrDefault(m => m.Id == id);
        if (existing == null)
            throw new Exception($"A Media with ID {id} does not exist!");
        _context.Medias?.Remove(existing);
        _context.SaveChanges();
        return Task.CompletedTask;
    }

    public Task<Media> GetById(int id)
    {
        Media? existing = _context.Medias?.FirstOrDefault(m=>m.Id == id);
        if (existing == null)
            throw new Exception($"A Media with ID {id} does not exist!");
        return Task.FromResult(existing);
    }

    public Task<IEnumerable<Media>> GetAsync()
    {
        IEnumerable<Media>? medias = _context.Medias?.AsEnumerable();
        return Task.FromResult(medias);
    }
}