using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using website.Models;

namespace website.Services.GitHubService
{
    public class GitHubService
    {
        private IMemoryCache _cache;
        private readonly IHttpClientFactory _clientFactory;
        public GitHubService(IMemoryCache cache, IHttpClientFactory httpClientFactory)
        {
            _cache = cache;
            _clientFactory = httpClientFactory;
        }

        public async Task SetProjects()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/orgs/DotNetFin/repos");

            var client = _clientFactory.CreateClient();
            client.BaseAddress = new Uri("https://api.github.com/");
            // Github API versioning
            client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.inertia-preview+json");
            client.DefaultRequestHeaders.Add("User-Agent", "DotNetFin");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var retrievedProjects = await JsonSerializer.DeserializeAsync<IEnumerable<ProjectViewModel>>(responseStream);
                _cache.Set("projects", retrievedProjects, DateTimeOffset.UtcNow.AddHours(4));
            }
        }
    }
}
