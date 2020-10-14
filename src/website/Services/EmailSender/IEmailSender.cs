using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace website.Services.EmailSender
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string content);
    }
}
