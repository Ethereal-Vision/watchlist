namespace Domain.DTOs;

public class MediaCreationDto
{
    public string Title { get; }
    public string Type { get; }
    public int NumberOfEpisodes { get; }
    
    //Types:
    public const string Movie = "Movie";
    public const string TvShow = "TV Show";

    public MediaCreationDto(string title, string type, int numberOfEpisodes)
    {
        Title = title;
        Type = type;
        NumberOfEpisodes = numberOfEpisodes;
    }
}