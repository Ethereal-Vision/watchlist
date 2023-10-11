namespace Domain.Models;

public class TvShow : Media
{
    public int NumberOfEpisodes { get; set; }
    public int CurrentEpisode { get; set; }


    public TvShow(int numberOfEpisodes, string title) : base(title)
    {
        NumberOfEpisodes = numberOfEpisodes;
        CurrentEpisode = 0;
    }
}