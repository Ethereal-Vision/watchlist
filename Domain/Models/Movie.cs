namespace Domain.Models;

[Serializable]
public class Movie : Media
{
    public Movie(string title) : base(title)
    {
        
    }
}