using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using website.Models;

namespace website.Pages
{
    public class ProjectsModel : PageModel
    {
        private IMemoryCache _cache;
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<IndexModel> _logger;

        public IEnumerable<ProjectViewModel> Projects { get; private set; }

        public ProjectsModel(IMemoryCache cache, IHttpClientFactory httpClientFactory, ILogger<IndexModel> logger)
        {
            _cache = cache;
            _clientFactory = httpClientFactory;
            _logger = logger;
        }
        public async Task OnGetAsync()
        {
            try
            {
                if (_cache.TryGetValue("projects", out var projects))
                {
                    Projects = projects as IEnumerable<ProjectViewModel>;
                }
                else
                {
                    var request = new HttpRequestMessage(HttpMethod.Get, "/orgs/DotNetFin/repos");

                    var client = _clientFactory.CreateClient("github");

                    var response = await client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        using var responseStream = await response.Content.ReadAsStreamAsync();
                        var retrievedProjects = await JsonSerializer.DeserializeAsync<IEnumerable<ProjectViewModel>>(responseStream);
                        _cache.Set("projects", retrievedProjects, DateTimeOffset.UtcNow.AddHours(3));
                        Projects = retrievedProjects;
                    }
                    else
                    {
                        Projects = new ProjectViewModel[0];
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
