using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;

namespace MyPortfolio.Services;

public class ProjectService
{
    private const string ProjectsPath = "data/projects.json";
    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(10);

    private readonly HttpClient _httpClient;
    private IReadOnlyList<Project>? _cachedProjects;
    private DateTime _cachedAtUtc;

    public ProjectService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IReadOnlyList<Project>> GetProjectsAsync()
    {
        if (_cachedProjects != null && DateTime.UtcNow - _cachedAtUtc < CacheDuration)
        {
            return _cachedProjects;
        }

        try
        {
            var data = await _httpClient.GetFromJsonAsync<ProjectData>(ProjectsPath);
            var projects = data?.Projects ?? new List<Project>();

            var validated = ValidateProjects(projects);
            _cachedProjects = validated;
            _cachedAtUtc = DateTime.UtcNow;
            return validated;
        }
        catch
        {
            return Array.Empty<Project>();
        }
    }

    public async Task<Project?> GetProjectByIdAsync(string? id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return null;
        }

        var projects = await GetProjectsAsync();
        return projects.FirstOrDefault(p => p.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
    }

    private static IReadOnlyList<Project> ValidateProjects(IEnumerable<Project> projects)
    {
        var results = new List<Project>();
        var ids = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (var project in projects)
        {
            if (!IsValid(project))
            {
                continue;
            }

            if (!ids.Add(project.Id))
            {
                continue;
            }

            project.Technologies = project.Technologies
                .Where(tech => !string.IsNullOrWhiteSpace(tech))
                .Select(tech => tech.Trim())
                .ToList();

            results.Add(project);
        }

        return results;
    }

    private static bool IsValid(Project project)
    {
        return !string.IsNullOrWhiteSpace(project.Id)
            && !string.IsNullOrWhiteSpace(project.Title)
            && !string.IsNullOrWhiteSpace(project.Summary)
            && !string.IsNullOrWhiteSpace(project.ImageUrl)
            && project.Technologies.Count > 0;
    }
}

public sealed class ProjectData
{
    public List<Project> Projects { get; set; } = new();
}

public class Project
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public List<string> Technologies { get; set; } = new();
    public string? Challenge { get; set; }
    public string? LiveUrl { get; set; }
    public string? RepoUrl { get; set; }
}
