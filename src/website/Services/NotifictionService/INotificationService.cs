using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace website.Services.NotifictionService;

public interface INotificationService
{
    void GreetNewMember(string email, string token);
}
