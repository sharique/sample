using System.Collections.Generic;
using System.Text;
using System;

namespace Sample.Data
{
    public class RolePermission
    {
        public RolePermission() { }

        public virtual int PermissionId { get; set; }
        public virtual int RoleId { get; set; }
        public virtual bool Allow { get; set; }

        public virtual Permission Permission { get; set; }
        public virtual Role Role { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as RolePermission;
            if (t == null)
                return false;
            if (Role == t.Role && Permission == t.Permission)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return (Role.RoleName + "|" + Permission.PermissionName).GetHashCode();
        }
    }
}
