using System;
using System.Threading.Tasks;

namespace website.Services.EmailSender
{
    public class ConsoleEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string fromEmail, string toEmail, string subject, string plainTextContent, string htmlContent)
        {
            Console.WriteLine($"Sends from {fromEmail} to {toEmail} with subject {subject}");
            Console.WriteLine(htmlContent);
            return Task.CompletedTask;
        }
    }
}
