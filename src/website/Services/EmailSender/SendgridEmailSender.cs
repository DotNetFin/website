using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace website.Services.EmailSender
{
    public class SendgridEmailSender : IEmailSender
    {
        private readonly ILogger<SendgridEmailSender> _logger;
        public SendgridEmailSender(ILogger<SendgridEmailSender> logger)
        {
            _logger = logger;
        }

        public async Task SendEmailAsync(string fromEmail, string toEmail, string subject, string plainTextContent, string htmlContent)
        {
            try
            {
                var apiKey = Environment.GetEnvironmentVariable("DOTNETFIN_SENDGRID_KEY");
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress(fromEmail, "DotNetFin");
                var to = new EmailAddress(toEmail);
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var clientResponse = client.SendEmailAsync(msg);

                #region sends email to my email
                var toMyPersonalEmail = new EmailAddress("diyaz.yakubov@dotnetfin.com");//sorry guys, I need it!
                var msgToMe = MailHelper.CreateSingleEmail(from, toMyPersonalEmail, "new member!!!", $"new Member: {toEmail}", "");
                var myResponse = await client.SendEmailAsync(msgToMe);
                #endregion

                _ = await clientResponse;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
