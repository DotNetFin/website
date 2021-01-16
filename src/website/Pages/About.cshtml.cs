using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using website.Models;

namespace website.Pages
{
    public class AboutModel : PageModel
    {
        private IMemoryCache _cache;

        public int NumberOfMembers { get; private set; }
        public int NumberOfCities { get; private set; }

        public AboutModel(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void OnGet()
        {
            if(_cache.TryGetValue(CacheKeys.MEMBERS_COUNT, out int membersCount)){
                NumberOfMembers = membersCount;
            }
            if (_cache.TryGetValue(CacheKeys.CITIES_COUNT, out int citiesCount))
            {
                NumberOfCities = citiesCount;
            }
        }
    }
}
