using System;
using LiteDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using website.Models;

namespace website.Pages
{
    public class ConfirmEmailModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly LiteDatabase _db;

        public bool IsConfirmed { get; set; }

        [BindProperty]
        public MemberInfoViewModel MemberInfo { get; set; }

        public ConfirmEmailModel(LiteDatabase db, ILogger<IndexModel> logger)
        {
            _db = db;
            _logger = logger;
        }

        public void OnGet([FromQuery] string email, [FromQuery] string token)
        {
            try
            {
                var members = _db.GetCollection<Member>("members");
                var member = members.FindOne(p => p.Email == email);
                if (member == null)
                    throw new ApplicationException($"There is no memeber with the given email: {email}");
                if (member.TryConfirmMembership(token))
                {
                    IsConfirmed = true;
                    members.Update(member);
                }
                else
                {
                    throw new ApplicationException("Token is not valid!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                IsConfirmed = false;
            }
        }

        public IActionResult OnPost(
            [FromQuery] string email,
            [FromQuery] string token
            )
        {
            try
            {
                var members = _db.GetCollection<Member>("members");
                var member = members.FindOne(p => p.Email == email);
                if (member == null)
                    throw new ApplicationException($"There is no memeber with the given email: {email}");

                if (member.TryConfirmMembership(token))
                {
                    member.FullName = MemberInfo.FullName;
                    member.City = MemberInfo.City;
                    member.KeyTechSkills = MemberInfo.KeyTechSkills;
                    members.Update(member);
                }
                else
                {
                    throw new ApplicationException("Token is not valid!");
                }
                return Redirect("/");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Page();
            }
        }
    }
}
