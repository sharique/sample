using System;
using FluentNHibernate.Mapping;

namespace Sample.Data
{
	public class SettingMap :ClassMap<Setting>
	{
		public SettingMap ()
		{
			Table("Settings");
			LazyLoad();
			Id(x=> x.SettingId).GeneratedBy.Identity().Column("SettingId");
			Map(x=> x.SettingName).Unique().Not.Nullable().Column("SettingName").Length(255);
			Map(x=> x.SettingValue).Not.Nullable().Length(255);
			Map(x=>x.Description);
		}
	}
}

