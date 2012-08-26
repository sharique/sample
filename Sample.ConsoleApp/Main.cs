using System;
using System.Linq;
using Sample.Data;

namespace Sample.ConsoleApp
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Hello World!");
			var setting = Repository.Instance.GetSetting("installed");
			Console.WriteLine(" installed :" + setting.First().SettingValue);
			//Console.ReadLine();
		}
	}
}
