using System; 
using System.Collections.Generic; 
using System.Text; 
using FluentNHibernate.Mapping;

namespace Sample.Data {
    
    
    public class UserInRoleMap : ClassMap<UserInRole> {
        
        public UserInRoleMap() {
			Table("UserInRoles");
			LazyLoad();
			CompositeId().KeyProperty(x => x.UserId, "UserId").KeyProperty(x => x.RoleId, "RoleId");
			References(x => x.User).Column("UserId");
			References(x => x.Role).Column("RoleId");
        }
    }
}
