/*
 * Created by SharpDevelop.
 * User: sharique
 * Date: 1/17/2012
 * Time: 1:23 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using FluentNHibernate.Mapping;
using Sample.Data;

namespace Sample.Data.Mapping
{
	/// <summary>
	/// Description of PostMap.
	/// </summary>
	public class PostMap : ClassMap<Post>
	{
		public PostMap()
		{
			Table("Posts");
			LazyLoad();
			
			Id(x=>x.PostId).GeneratedBy.Identity().Column("PostId").Not.Nullable();
			Map(x=> x.PostTitle).Not.Nullable();
			Map(x=> x.Body);
			Map(x=> x.PostOn).Not.Nullable();
			Map(x=> x.PostedBy);
			Map(x=> x.ModifiedBy);
			Map(x=> x.ModifiedOn);
			
            References(x=>x.PostedByUser).Column("PostedBy");
            References(x=>x.ModifiedByUser).Column("ModifiedBy");
			//References(x=>x.Type).Column("PostTypeId");
		}
	}
}
