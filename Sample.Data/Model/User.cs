using System.Collections.Generic;
using System.Text;
using System;
using FluentNHibernate.Mapping;

namespace Sample.Data
{
    public class User
    {
        public User()
        {
            UserInRoles = new List<UserInRole>();
            Posts = new List<Post>();
        }
        public virtual int UserId { get; set; }

        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual string Email { get; set; }
        public virtual System.Nullable<bool> isApproved { get; set; }
        public virtual System.Nullable<bool> isLockedOut { get; set; }
        public virtual string PasswordQuestion { get; set; }
        public virtual string PasswordAnswer { get; set; }
        public virtual System.Nullable<System.DateTime> LastActivityDate { get; set; }
        public virtual System.Nullable<System.DateTime> LastLoginDate { get; set; }
        public virtual System.Nullable<System.DateTime> LastPasswordChangedDate { get; set; }
        public virtual System.Nullable<System.DateTime> CreationDate { get; set; }
        public virtual System.Nullable<bool> isOnline { get; set; }
        public virtual System.Nullable<System.DateTime> LastLockedOutDate { get; set; }
        public virtual System.Nullable<int> FailedPasswordAttemptCount { get; set; }
        public virtual System.Nullable<System.DateTime> FailedPasswordAttemptWindowStart { get; set; }
        public virtual System.Nullable<int> FailedPasswordAnswerAttemptCount { get; set; }
        public virtual System.Nullable<System.DateTime> FailedPasswordAnswerAttemptWindowStart { get; set; }
        public virtual string Comment { get; set; }

        public virtual IList<UserInRole> UserInRoles { get; set; }
        public virtual IList<Post> Posts { get; set; }
        public virtual IList<Post> PostsModified { get; set; }
        public virtual IList<Project> Projects { get; set; }
        public virtual IList<Project> ProjectsAssigned { get; set; }
    }

    public class UserMap : ClassMap<User>
    {

        public UserMap()
        {
            Table("Users");
            LazyLoad();
            Id(x => x.UserId).GeneratedBy.Identity().Column("UserId");
            Map(x => x.UserName).Column("UserName").Not.Nullable().Length(50).Unique();
            Map(x => x.Password).Column("Password").Not.Nullable().Length(50);
            Map(x => x.Email).Column("Email").Not.Nullable().Length(50).Unique();
            Map(x => x.isApproved).Column("isApproved");
            Map(x => x.isLockedOut).Column("isLockedOut");
            Map(x => x.PasswordQuestion).Column("PasswordQuestion").Length(250);
            Map(x => x.PasswordAnswer).Column("PasswordAnswer").Length(255);
            Map(x => x.LastActivityDate).Column("LastActivityDate");
            Map(x => x.LastLoginDate).Column("LastLoginDate");
            Map(x => x.LastPasswordChangedDate).Column("LastPasswordChangedDate");
            Map(x => x.CreationDate).Column("CreationDate");
            Map(x => x.isOnline).Column("isOnline");
            Map(x => x.LastLockedOutDate).Column("LastLockedOutDate");
            Map(x => x.FailedPasswordAttemptCount).Column("FailedPasswordAttemptCount");
            Map(x => x.FailedPasswordAttemptWindowStart).Column("FailedPasswordAttemptWindowStart");
            Map(x => x.FailedPasswordAnswerAttemptCount).Column("FailedPasswordAnswerAttemptCount");
            Map(x => x.FailedPasswordAnswerAttemptWindowStart).Column("FailedPasswordAnswerAttemptWindowStart");
            Map(x => x.Comment).Column("Comment").Length(255);

            /* Referenced items */
            HasMany(x => x.UserInRoles).KeyColumn("UserId").Inverse().Cascade.All();
            HasMany(x => x.Posts).KeyColumn("PostedBy").Inverse().Cascade.All();
            HasMany(x => x.PostsModified).KeyColumn("ModifiedBy").Inverse().Cascade.All();
            HasMany(x => x.Projects).KeyColumn("CreatedBy").Inverse().Cascade.All();
            HasMany(x => x.ProjectsAssigned).KeyColumn("AssignedTo").Inverse().Cascade.All();
            
        }
    }
}
