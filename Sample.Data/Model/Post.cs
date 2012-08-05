/*
 * Created by SharpDevelop.
 * User: sharique
 * Date: 1/17/2012
 * Time: 1:11 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sample.Data
{
	/// <summary>
	/// Description of Post.
	/// </summary>
	public class Post
	{
		public Post ()
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
}
