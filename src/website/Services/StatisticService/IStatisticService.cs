using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace website.Services.StatisticService;

public interface IStatisticService
{
    void SetMembersCount();
    void SetMembersCitiesCount();
}
