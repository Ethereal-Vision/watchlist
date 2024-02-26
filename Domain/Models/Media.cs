namespace Domain.Models;

[Serializable]
public class Media
{
    public int Id { get; set; }
    public string Title { get; set; }
    public double AvgRating { get; set; }
    public double ErnestRating { get; set; }
    public double ViktorRating { get; set; }
    public string Status { get; set; }
    public string Type { get; set; }
    
    public const string Movie = "Movie";
    public const string TvShow = "TV Show";

    public const string NotStarted = "Not Started";
    public const string CurrentlyWatching = "Currently Watching";
    public const string Completed = "Completed";

    public Media(string title)
    {
        Title = title;
        AvgRating = 0;
        ErnestRating = 0;
        ViktorRating = 0;
        Status = "Not Started";
        Type = "";
    }
}