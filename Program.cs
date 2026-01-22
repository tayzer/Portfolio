using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MyPortfolio;
using MyPortfolio.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<ProfileService>();
builder.Services.AddScoped<HomeContentService>();
builder.Services.AddScoped<GitHubKnowledgeBaseService>();

await builder.Build().RunAsync();
