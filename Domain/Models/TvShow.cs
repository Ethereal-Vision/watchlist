using System.Text.Json.Serialization;

namespace Domain.Models;

[Serializable]
public class TvShow : Media
{
    [JsonInclude]
    public int NumberOfEpisodes { get; set; }
    [JsonInclude]
    public int CurrentEpisode { get; set; }


    public TvShow(int numberOfEpisodes, string title) : base(title)
    {
        NumberOfEpisodes = numberOfEpisodes;
        CurrentEpisode = 0;
    }
}