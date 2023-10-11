namespace Domain.Models;

public class Anime : Media
{
    public int NumberOfEpisodes { get; set; }
    public int CurrentEpisode { get; set; }


    public Anime(int numberOfEpisodes, string title) : base(title)
    {
        NumberOfEpisodes = numberOfEpisodes;
        CurrentEpisode = 0;
    }
}