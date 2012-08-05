using System.Collections.Generic;
using System.Text;
using System;

namespace Sample.Data
{
    public class User
    {
        public User()
        {
            UserInRoles = new List<UserInRole>();
            Posts = new List<Post>();
        }
        public virtual int UserId { get; set; }

        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual string Email { get; set; }
        public virtual System.Nullable<bool> isApproved { get; set; }
        public virtual System.Nullable<bool> isLockedOut { get; set; }
        public virtual string PasswordQuestion { get; set; }
        public virtual string PasswordAnswer { get; set; }
        public virtual System.Nullable<System.DateTime> LastActivityDate { get; set; }
        public virtual System.Nullable<System.DateTime> LastLoginDate { get; set; }
        public virtual System.Nullable<System.DateTime> LastPasswordChangedDate { get; set; }
        public virtual System.Nullable<System.DateTime> CreationDate { get; set; }
        public virtual System.Nullable<bool> isOnline { get; set; }
        public virtual System.Nullable<System.DateTime> LastLockedOutDate { get; set; }
        public virtual System.Nullable<int> FailedPasswordAttemptCount { get; set; }
        public virtual System.Nullable<System.DateTime> FailedPasswordAttemptWindowStart { get; set; }
        public virtual System.Nullable<int> FailedPasswordAnswerAttemptCount { get; set; }
        public virtual System.Nullable<System.DateTime> FailedPasswordAnswerAttemptWindowStart { get; set; }
        public virtual string Comment { get; set; }

        public virtual IList<UserInRole> UserInRoles { get; set; }
        public virtual IList<Post> Posts { get; set; }
        public virtual IList<Post> PostsModified { get; set; }
    }
}
