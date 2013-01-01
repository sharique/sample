using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Data
{
    public class Project
    {
        public virtual int ProjectId { get; set; }
        public virtual string ProjectName { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual string Status { get; set; }
        public virtual int CreatedBy { get; set; }
        public virtual int AssignedTo { get; set; }

        /* references */
        public virtual User CreatedByUser { get; set; }
        public virtual User AssignedToUser { get; set; }
    }

    public class ProjectMap : ClassMap<Project>
    {
        public ProjectMap()
        {
            Table("Projects");
            LazyLoad();
            Id(x => x.ProjectId).GeneratedBy.Identity().Column("ProjectId");
            Map(x => x.ProjectName).Column("ProjectName").Not.Nullable().Length(50).Unique();
            Map(x => x.Description).Column("Description").Length(200);
            Map(x => x.CreateDate).Column("CreateDate");
            Map(x => x.StartDate).Column("StartDate");
            Map(x => x.EndDate).Column("EndDate");
            Map(x => x.Status).Column("Status");
            Map(x => x.CreatedBy).Column("CreatedBy");
            Map(x => x.AssignedTo).Column("AssignedTo");

            References(x => x.CreatedByUser).Column("CreatedBy");
            References(x => x.AssignedToUser).Column("AssignedTo");
        }
    }
}
