using System.Collections.Generic;
using System.Linq;

namespace MyPortfolio.Services;

public class ProjectService
{
    private readonly List<Project> _projects = new()
    {
        new Project
        {
            Id = "project-1",
            Title = "BeatSwirl",
            Summary = "A song queuing app leveraging music apis (e.g. Spotify) built with Blazor and .NET.",
            ImageUrl = "images/BeatApp.jpg",
            Technologies = new List<string> { "Blazor WASM", ".NET 8" },
            Challenge = "The challenge was getting used to Blazor, as it was my first Blazor site",
            LiveUrl = "https://beat.bitswirl.co.uk"
        },
        new Project
        {
            Id = "project-2",
            Title = "Smart Comparer",
            Summary = "A Blazor WASM site and .NET library for A/B comparisons supporting both JSON and XML.",
            ImageUrl = "images/Compare.jpg",
            Technologies = new List<string> { "ASP.NET Core" },
            Challenge = "Trying to do something new for A/B comparisons and making it efficient and intuitive... ",
            RepoUrl = "https://github.com/tayzer/SmartObjectComparer.Net"
        }
    };

    public List<Project> GetProjects() => _projects;
    public Project? GetProjectById(string id) => _projects.FirstOrDefault(p => p.Id == id);
}

public class Project
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Summary { get; set; }
    public string ImageUrl { get; set; }
    public List<string> Technologies { get; set; }
    public string Challenge { get; set; }
    public string LiveUrl { get; set; }
    public string RepoUrl { get; set; }
}
