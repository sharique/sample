using System.Collections.Generic;
using System.Text;
using System;

namespace Sample.Data
{
    public class UserInRole
    {
        public UserInRole() { }
        public virtual int UserId { get; set; }
        public virtual int RoleId { get; set; }
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as UserInRole;
            if (t == null)
                return false;
            if (Role == t.Role && User == t.User)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return (Role.RoleName + "|" + User.UserName).GetHashCode();
        }
    }
}
