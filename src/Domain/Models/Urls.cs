namespace Domain.Models;

public class Urls
{
    public Urls() { }

    public Urls(string[] paths)
    {
        Paths = paths;
    }

    public string[] Paths { get; set; }
}