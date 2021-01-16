using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
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
            if (_cache.TryGetValue(CacheKeys.GITHUB_PROJECTS, out var projects))
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
