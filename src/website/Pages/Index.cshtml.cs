using System;
using System.Text.RegularExpressions;
using Hangfire;
using LiteDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using website.Models;
using website.Services.NotifictionService;

namespace website.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public bool IsSubscribed { get; set; }
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            var cookieValue = Request.Cookies["isMember"];
            if (bool.TrueString == cookieValue)
            {
                IsSubscribed = true;
            }
        }

        public IActionResult OnPost([FromServices] LiteDatabase db)
        {
            try
            {
                string email = Request.Form["email"].ToString();
                if (string.IsNullOrWhiteSpace(email))
                    throw new ArgumentNullException("Email couldn't be empty!");

                if (!Regex.IsMatch(email, @"^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$"))
                    throw new ArgumentException("Email is incorrect!");


                var members = db.GetCollection<Member>("members");
                var member = members.FindOne(p => p.Email == email);
                if (member == null)
                {
                    var newMember = new Member(email);
                    members.Insert(newMember);
                    Response.Cookies.Append("isMember", bool.TrueString);
                    IsSubscribed = true;
                    var jobId = BackgroundJob.Enqueue<INotificationService>(sender => sender.GreetNewMember(email, newMember.Token));
                }

                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return Page();
            }
        }
    }
}
