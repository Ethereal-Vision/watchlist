namespace Domain.Models;

public class Media
{
    public int Id { get; set; }
    public string Title { get; set; }
    public double AvgRating { get; set; }
    public double ErnestRating { get; set; }
    public double ViktorRating { get; set; }
    public string Status { get; set; }

    public Media(string title)
    {
        Title = title;
        AvgRating = 0;
        ErnestRating = 0;
        ViktorRating = 0;
        Status = "Not Started";
    }
}