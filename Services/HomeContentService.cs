using System;
using System.Collections.Generic;
using System.Net.Http.Json;

namespace MyPortfolio.Services;

public sealed class HomeContentService
{
    private const string HomeContentPath = "data/home.json";
    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(10);

    private readonly HttpClient _httpClient;
    private HomeContent? _cached;
    private DateTime _cachedAtUtc;

    public HomeContentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<HomeContent?> GetHomeContentAsync()
    {
        if (_cached != null && DateTime.UtcNow - _cachedAtUtc < CacheDuration)
        {
            return _cached;
        }

        try
        {
            var content = await _httpClient.GetFromJsonAsync<HomeContent>(HomeContentPath);
            if (content == null || !IsValid(content))
            {
                return null;
            }

            _cached = content;
            _cachedAtUtc = DateTime.UtcNow;
            return content;
        }
        catch
        {
            return null;
        }
    }

    private static bool IsValid(HomeContent content)
    {
        return !string.IsNullOrWhiteSpace(content.HeroImageUrl)
            && !string.IsNullOrWhiteSpace(content.HeroImageAlt)
            && !string.IsNullOrWhiteSpace(content.HeroGreeting)
            && !string.IsNullOrWhiteSpace(content.HeroName)
            && content.HeroParagraphs.Count > 0
            && !string.IsNullOrWhiteSpace(content.CtaText)
            && !string.IsNullOrWhiteSpace(content.CtaHref)
            && !string.IsNullOrWhiteSpace(content.GoalTitle)
            && !string.IsNullOrWhiteSpace(content.GoalText);
    }
}

public sealed class HomeContent
{
    public string HeroImageUrl { get; set; } = string.Empty;
    public string HeroImageAlt { get; set; } = string.Empty;
    public string HeroGreeting { get; set; } = string.Empty;
    public string HeroName { get; set; } = string.Empty;
    public List<string> HeroParagraphs { get; set; } = new();
    public string CtaText { get; set; } = string.Empty;
    public string CtaHref { get; set; } = string.Empty;
    public string GoalTitle { get; set; } = string.Empty;
    public string GoalText { get; set; } = string.Empty;
}