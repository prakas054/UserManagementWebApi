using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserManagement.Models
{
    public class Membership
    {
        public string _ApplicationId { get; set; }
        public string _UserId { get; set; }
        public string _Password { get; set; }
        public string _PasswordFormat { get; set; }
        public string _PasswordSalt { get; set; }
        public string _IsApproved { get; set; }
        public string _IsLockedOut { get; set; }
        public string _CreateDate { get; set; }
        public string _LastLoginDate { get; set; }
        public string _LastPasswordChangedDate { get; set; }
        public string _LastLockoutDate { get; set; }
        public string _FailedPasswordAttemptCount { get; set; }
        public string _FailedPasswordAttemptWindowStart { get; set; }
        public string _FailedPasswordAnswerAttemptCount { get; set; }
        public string _FailedPasswordAnswerAttemptWindowStart { get; set; }
    }
}