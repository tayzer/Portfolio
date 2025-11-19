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
            Title = "Awesome E-commerce App",
            Summary = "A full-stack e-commerce site built with Blazor and .NET.",
            ImageUrl = "images/project1.png",
            Technologies = new List<string> { "Blazor WASM", ".NET 8", "Entity Framework Core", "Stripe" },
            Challenge = "The main challenge was implementing a secure payment gateway...",
            LiveUrl = "https://your-live-site.com",
            RepoUrl = "https://github.com/YOUR_USERNAME/project-1"
        },
        new Project
        {
            Id = "project-2",
            Title = "Task Management API",
            Summary = "A RESTful API for managing tasks and projects.",
            ImageUrl = "images/project2.png",
            Technologies = new List<string> { "ASP.NET Core", "xUnit", "Docker", "PostgreSQL" },
            Challenge = "Implementing efficient database indexing for search... ",
            LiveUrl = "https://your-api-docs.com",
            RepoUrl = "https://github.com/YOUR_USERNAME/project-2"
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
