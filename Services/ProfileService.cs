using System;
using System.Net.Http.Json;

namespace MyPortfolio.Services;

public sealed class ProfileService
{
    private const string ProfilePath = "data/profile.json";
    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(10);

    private readonly HttpClient _httpClient;
    private Profile? _cachedProfile;
    private DateTime _cachedAtUtc;

    public ProfileService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Profile?> GetProfileAsync()
    {
        if (_cachedProfile != null && DateTime.UtcNow - _cachedAtUtc < CacheDuration)
        {
            return _cachedProfile;
        }

        try
        {
            var profile = await _httpClient.GetFromJsonAsync<Profile>(ProfilePath);
            if (profile == null || !IsValid(profile))
            {
                return null;
            }

            _cachedProfile = profile;
            _cachedAtUtc = DateTime.UtcNow;
            return profile;
        }
        catch
        {
            return null;
        }
    }

    private static bool IsValid(Profile profile)
    {
        return !string.IsNullOrWhiteSpace(profile.Email)
            && !string.IsNullOrWhiteSpace(profile.GitHubUrl)
            && !string.IsNullOrWhiteSpace(profile.LinkedInUrl)
            && !string.IsNullOrWhiteSpace(profile.ResumeUrl);
    }
}

public sealed class Profile
{
    public string Email { get; set; } = string.Empty;
    public string GitHubUrl { get; set; } = string.Empty;
    public string LinkedInUrl { get; set; } = string.Empty;
    public string ResumeUrl { get; set; } = string.Empty;
    public string ResumeFileName { get; set; } = "Resume.pdf";
}