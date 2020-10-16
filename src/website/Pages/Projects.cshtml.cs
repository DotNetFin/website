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

        public IEnumerable<ProjectViewModel> Projects { get; private set; }

        public ProjectsModel(IMemoryCache cache)
        {
            _cache = cache;
        }
        public void OnGet()
        {
            if (_cache.TryGetValue("projects", out var projects))
            {
                Projects = projects as IEnumerable<ProjectViewModel>;
            }
            else
            {
                Projects = new ProjectViewModel[0];
            }
        }
    }
}
