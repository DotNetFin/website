using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using website.Services.EmailSender;

namespace website.Services.NotifictionService
{
    public class NotificationService : INotificationService
    {
        private IEmailSender _emailSender;
        public NotificationService(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        public void GreetNewMember(string email)
        {
            _emailSender.SendEmailAsync("noreply@dotnetfin.com", email, "Hey!!!", "", "someHtml");
        }
    }
}
