using System;
using System.Configuration;
using System.Reflection;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Cfg;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

namespace Sample.Data
{
	public class NHHelper
	{
		public NHHelper ()
		{
			this.SessionFactory = CreateSessionFactory();
		}
		
		public string ConnectionStringName {
			get;
			set;
		}
		
		public ISessionFactory SessionFactory {
			get;
			set;
		}
		
		private ISessionFactory CreateSessionFactory ()
		{
			string provider=string.Empty;
			string constr = string.Empty;
			
			if (ConnectionStringName==String.Empty || ConnectionStringName == null) {
				provider= ConfigurationManager.ConnectionStrings [0].ProviderName;			
				constr= ConfigurationManager.ConnectionStrings [0].ConnectionString;
			}
			else{
				provider= ConfigurationManager.ConnectionStrings [ConnectionStringName].ProviderName;			
				constr= ConfigurationManager.ConnectionStrings [ConnectionStringName].ConnectionString;
			}
			
			ISessionFactory factory = null;
			switch (provider) {
			case "MySql.Data.MySqlClient" :
				factory = Fluently.Configure ()
				.Database (MySQLConfiguration.Standard
					.ConnectionString (c => c.FromConnectionStringWithKey ("MySql")))
					.Mappings (m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
					.ExposeConfiguration (cfg =>
				{
					SchemaExport schemaExport = new SchemaExport (cfg);
					schemaExport.Drop (true, true);
					schemaExport.Create (true, true);
				})
					.BuildSessionFactory ();
				
				break;
			case "System.Data.SqlClient":
				factory = Fluently.Configure ()
				.Database (MsSqlConfiguration.MsSql2008
					.ConnectionString (c => c.FromConnectionStringWithKey ("SQLExpress")))
					.Mappings (m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
					.ExposeConfiguration (cfg =>
				{
					SchemaExport schemaExport = new SchemaExport (cfg);
					schemaExport.Drop (true, true);
					schemaExport.Create (true, true);
				})
					.BuildSessionFactory ();
				
				break;
			default:
				break;
			}
			return factory;
		}
	}
}

