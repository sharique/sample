using System.Collections.Generic;
using System.Text;
using System;

namespace Sample.Data
{
    public class Role
    {
        public Role()
        {
            RolePermissions = new List<RolePermission>();
            UserInRoles = new List<UserInRole>();
        }
        public virtual int RoleId { get; set; }
        public virtual IList<RolePermission> RolePermissions { get; set; }
        public virtual IList<UserInRole> UserInRoles { get; set; }
        public virtual string RoleName { get; set; }
        public virtual string Description { get; set; }
    }
}
