using System; 
using System.Collections.Generic; 
using System.Text; 
using FluentNHibernate.Mapping;

namespace Sample.Data {
    
    
    public class RolePermissionMap : ClassMap<RolePermission> {
        
        public RolePermissionMap() {
			Table("RolePermissions");
			LazyLoad();
			Map(x => x.Allow).Column("Allow").Not.Nullable();
			
			CompositeId().KeyProperty(x => x.PermissionId, "PermissionId").KeyProperty(x => x.RoleId, "RoleId");
			References(x => x.Permission).Column("PermissionId");
			References(x => x.Role).Column("RoleId");			
        }
    }
}
