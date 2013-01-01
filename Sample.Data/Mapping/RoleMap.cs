using System;
using System.Collections.Generic;
using System.Text;
using FluentNHibernate.Mapping;

namespace Sample.Data
{
    public class RoleMap : ClassMap<Role>
    {
        public RoleMap()
        {
            Table("Roles");
            LazyLoad();
            Id(x => x.RoleId).GeneratedBy.Identity().Column("RoleId");
            Map(x => x.RoleName).Column("RoleName").Not.Nullable().Length(50).Unique();
            Map(x => x.Description).Column("Description").Length(200);
            HasMany(x => x.RolePermissions).KeyColumn("RoleId");
            HasMany(x => x.UserInRoles).KeyColumn("RoleId");
        }
    }
}
