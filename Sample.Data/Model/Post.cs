using FluentNHibernate.Mapping;
using System;

namespace Sample.Data
{
    /// <summary>
    /// Description of Post.
    /// </summary>
    public class Post
    {
        public Post()
        {
        }

        // table fields
        public virtual int PostId { get; set; }
        public virtual string PostTitle { get; set; }
        public virtual string Body { get; set; }
        public virtual DateTime PostOn { get; set; }
        public virtual DateTime? ModifiedOn { get; set; }
        public virtual int PostedBy { get; set; }
        public virtual int? ModifiedBy { get; set; }
        // relation objects
        public virtual User PostedByUser { get; set; }
        public virtual User ModifiedByUser { get; set; }
        //public virtual PostType Type { get; set; }
    }

    public class PostMap : ClassMap<Post>
    {
        public PostMap()
        {
            Table("Posts");
            LazyLoad();

            Id(x => x.PostId).GeneratedBy.Identity().Column("PostId").Not.Nullable();
            Map(x => x.PostTitle).Not.Nullable();
            Map(x => x.Body);
            Map(x => x.PostOn).Not.Nullable();
            Map(x => x.PostedBy);
            Map(x => x.ModifiedBy);
            Map(x => x.ModifiedOn);

            References(x => x.PostedByUser).Column("PostedBy");
            References(x => x.ModifiedByUser).Column("ModifiedBy");
            //References(x=>x.Type).Column("PostTypeId");
        }
    }
}
