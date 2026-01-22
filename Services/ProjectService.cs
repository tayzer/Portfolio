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
            Title = "SyncIt",
            Summary = "My ongoing Blazor side project: a Spotify-integrated queuing app I built end-to-end (useful for parties or running).",
            ImageUrl = "images/BeatApp.jpg",
            Technologies = new List<string> { "Blazor WASM", ".NET 8" },
            Challenge = "With this being my first proper Blazor project, the main challenge was building familiarity with Blazor.",
            LiveUrl = "https://syncityo.com"
        },
        new Project
        {
            Id = "project-2",
            Title = "Smart Comparer",
            Summary = "My ongoing open-source project: a .NET library for efficient A/B object comparisons across JSON/XML. I maintain it as a reusable library and keep tightening the diffing logic and developer ergonomics.",
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
