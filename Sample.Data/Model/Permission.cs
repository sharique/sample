using System.Collections.Generic; 
using System.Text; 
using System; 


namespace Sample.Data {
    
    public class Permission {
        public Permission() {
			RolePermissions = new List<RolePermission>();
        }
        public virtual int PermissionId { get; set; }
        public virtual IList<RolePermission> RolePermissions { get; set; }
        public virtual string PermissionName { get; set; }
        public virtual string Description { get; set; }
    }
}
