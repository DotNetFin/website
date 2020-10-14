using System;
using System.Threading.Tasks;

namespace website.Services.EmailSender
{
    public class ConsoleEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string content)
        {
            Console.WriteLine(email, content);
            return Task.CompletedTask;
        }
    }
}
