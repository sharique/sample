using System; 
using System.Collections.Generic; 
using System.Text; 
using FluentNHibernate.Mapping;

namespace Sample.Data {
    
    
    public class UserMap : ClassMap<User> {
        
        public UserMap() {
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

			HasMany(x => x.UserInRoles).KeyColumn("UserId").Inverse().Cascade.All();
            HasMany(x => x.Posts).KeyColumn("PostedBy").Inverse().Cascade.All();
			HasMany(x => x.PostsModified).KeyColumn("ModifiedBy").Inverse().Cascade.All();
        }
    }
}
