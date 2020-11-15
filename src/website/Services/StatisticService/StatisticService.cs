using LiteDB;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using website.Models;

namespace website.Services.StatisticService
{
    public class StatisticService : IStatisticService
    {
        private readonly LiteDatabase _db;
        private IMemoryCache _cache;

        public StatisticService(LiteDatabase db, IMemoryCache cache)
        {
            _db = db;
            _cache = cache;
        }

        public void SetMembersCitiesCount()
        {
            var members = _db.GetCollection<Member>("members");
            int countOfCities = members.FindAll().GroupBy(p => p.City).Count();
            _cache.Set(CacheKeys.CITIES_COUNT, countOfCities, DateTimeOffset.UtcNow.AddDays(1));
        }

        public void SetMembersCount()
        {
            var members = _db.GetCollection("members");
            int countOfMembers = members.Count();
            _cache.Set(CacheKeys.MEMBERS_COUNT, countOfMembers, DateTimeOffset.UtcNow.AddDays(1));
        }
    }
}
