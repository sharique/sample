using System;

namespace Sample.Data
{
	public class Setting
	{
		public Setting ()
		{
		}
		
		public virtual int SettingId {get; set;}		
		public virtual string SettingName {get; set;}
		public virtual string SettingValue {get; set;}
		public virtual string Description { get; set; }
	}
}

