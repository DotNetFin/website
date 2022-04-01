using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using website.Services.EmailSender;

namespace website.Services.NotifictionService;

public class NotificationService : INotificationService
{
    private IEmailSender _emailSender;
    public NotificationService(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }
    public void GreetNewMember(string email, string token)
    {
        var emailBuilder = new EmailBuilder();
        emailBuilder.WithH1Header("DotNetFin warmly welcome you!");
        emailBuilder.WithH2Header("Thank you for you interest in our community!");
        emailBuilder.WithParagraph("We do believe that .NET is an unique development platform that offers an infinite amount of opportunities. While there are a lot of communities dedicated to .NET in many countires, there are now places in Finland where .NET developers can communicate and collabarate.And we know that there are many of them!");
        emailBuilder.WithParagraph(@$"We kindly ask you to follow the <a href=""https://dotnetfin.com/ConfirmEmail?email={email}&token={token}"">link</a> in order to confirm your email address.");
        emailBuilder.WithParagraph(@"Best regards,<br/>DotNetFin Team<br/><br/>");
        var emailContent = emailBuilder.Build();
        _emailSender.SendEmailAsync("noreply@dotnetfin.com", email, "DotNetFin Community!", "", emailContent);
    }
}
