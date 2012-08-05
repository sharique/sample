using System; 
using System.Collections.Generic; 
using System.Text; 
using FluentNHibernate.Mapping;

namespace Sample.Data {
    
    
    public class PermissionMap : ClassMap<Permission> {
        
        public PermissionMap() {
			Table("Permissions");
			LazyLoad();
			Id(x => x.PermissionId).GeneratedBy.Identity().Column("PermissionId");
			Map(x => x.PermissionName).Column("PermissionName").Not.Nullable().Length(50);
			Map(x => x.Description).Column("Description").Length(250);
			HasMany(x => x.RolePermissions).KeyColumn("PermissionId");
        }
    }
}
