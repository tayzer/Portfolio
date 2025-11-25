using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace MyPortfolio.Services;

/// <summary>
/// Service to fetch markdown content from a GitHub repository
/// </summary>
public class GitHubKnowledgeBaseService
{
    private readonly HttpClient _httpClient;
    private readonly string _owner = "tayzer";
    private readonly string _repo = "KnowledgeBase.Dev";
    private readonly string _branch = "main";
    
    // Cache to avoid excessive API calls
    private readonly Dictionary<string, (DateTime CachedAt, List<GitHubContentItem> Items)> _contentsCache = new();
    private readonly Dictionary<string, (DateTime CachedAt, string Content)> _fileCache = new();
    private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(10);

    public GitHubKnowledgeBaseService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Gets the contents of a directory in the repository
    /// </summary>
    /// <param name="path">Path relative to repository root (empty string for root)</param>
    public async Task<List<GitHubContentItem>> GetContentsAsync(string path = "")
    {
        // Check cache first
        if (_contentsCache.TryGetValue(path, out var cached) && 
            DateTime.UtcNow - cached.CachedAt < _cacheDuration)
        {
            return cached.Items;
        }

        try
        {
            var apiUrl = string.IsNullOrEmpty(path)
                ? $"https://api.github.com/repos/{_owner}/{_repo}/contents"
                : $"https://api.github.com/repos/{_owner}/{_repo}/contents/{path}";
            
            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            request.Headers.Add("User-Agent", "MyPortfolio-BlazorApp");
            request.Headers.Add("Accept", "application/vnd.github+json");
            
            var response = await _httpClient.SendAsync(request);
            
            if (!response.IsSuccessStatusCode)
            {
                return new List<GitHubContentItem>();
            }

            var items = await response.Content.ReadFromJsonAsync<List<GitHubContentItem>>();
            
            if (items != null)
            {
                // Filter to only show markdown files and directories
                var filteredItems = items
                    .Where(i => i.Type == "dir" || (i.Type == "file" && i.Name.EndsWith(".md", StringComparison.OrdinalIgnoreCase)))
                    .OrderByDescending(i => i.Type == "dir") // Folders first
                    .ThenBy(i => i.Name)
                    .ToList();
                
                // Cache the results
                _contentsCache[path] = (DateTime.UtcNow, filteredItems);
                
                return filteredItems;
            }
        }
        catch (Exception)
        {
            // Silently fail and return empty list
        }

        return new List<GitHubContentItem>();
    }

    /// <summary>
    /// Gets the raw markdown content of a file
    /// </summary>
    /// <param name="path">Path to the file relative to repository root</param>
    public async Task<string> GetFileContentAsync(string path)
    {
        // Check cache first
        if (_fileCache.TryGetValue(path, out var cached) && 
            DateTime.UtcNow - cached.CachedAt < _cacheDuration)
        {
            return cached.Content;
        }

        try
        {
            // Use raw.githubusercontent.com for direct file access
            var rawUrl = $"https://raw.githubusercontent.com/{_owner}/{_repo}/{_branch}/{path}";
            
            var content = await _httpClient.GetStringAsync(rawUrl);
            
            // Cache the result
            _fileCache[path] = (DateTime.UtcNow, content);
            
            return content;
        }
        catch (Exception ex)
        {
            return $"Error loading file: {ex.Message}";
        }
    }

    /// <summary>
    /// Clears the cache (useful for manual refresh)
    /// </summary>
    public void ClearCache()
    {
        _contentsCache.Clear();
        _fileCache.Clear();
    }
}

/// <summary>
/// Represents an item (file or directory) from GitHub API
/// </summary>
public class GitHubContentItem
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("path")]
    public string Path { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty; // "file" or "dir"

    [JsonPropertyName("size")]
    public long Size { get; set; }

    [JsonPropertyName("download_url")]
    public string? DownloadUrl { get; set; }

    [JsonPropertyName("html_url")]
    public string? HtmlUrl { get; set; }

    /// <summary>
    /// Gets a display-friendly name (without .md extension)
    /// </summary>
    public string DisplayName => Type == "file" && Name.EndsWith(".md", StringComparison.OrdinalIgnoreCase)
        ? Name[..^3]
        : Name;

    /// <summary>
    /// Returns true if this is a directory
    /// </summary>
    public bool IsDirectory => Type == "dir";

    /// <summary>
    /// Returns true if this is a markdown file
    /// </summary>
    public bool IsMarkdown => Type == "file" && Name.EndsWith(".md", StringComparison.OrdinalIgnoreCase);
}