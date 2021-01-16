using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace website.Models
{
    public class Member
    {
        private Member() { }
        public Member(string email)
        {
            Email = email;
            BecameMemberAt = DateTime.UtcNow;
            Token = Guid.NewGuid().ToString();
        }

        public LiteDB.ObjectId Id { get; set; }

        public string Email { get; set; }
        public DateTime BecameMemberAt { get; private set; }
        public bool IsConfirmedMember { get; private set; }
        public string Token { get; private set; }

        public bool TryConfirmMembership(string token)
        {
            if (Token == token)
            {
                IsConfirmedMember = true;
                return true;
            }
            return false;
        }

        public string FullName { get; set; }
        public string City { get; set; }
        public string KeyTechSkills { get; set; }
    }
}
