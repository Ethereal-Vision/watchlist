namespace Domain.DTOs;

public class MediaUpdateDto
{
    public int Id { get; }
    public string Title { get; set; }
    public double? AvgRating { get; set; }
    public double? ErnestRating { get; set; }
    public double? ViktorRating { get; set; }
    public string? Status { get; set; }
    public string Type { get; set; }
    public int? NumberOfEpisodes { get; set; }
    public int? CurrentEpisode { get; set; }

    public MediaUpdateDto(string title, int id, string type)
    {
        Id = id;
        Type = type;
        Title = title;
    }
}