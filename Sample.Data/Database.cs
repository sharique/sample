using System;
//using System.Configuration;
using System.Reflection;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Cfg;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

namespace Sample.Data
{
    public class Database
    {
        private static ISessionFactory sessionFactory = null;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (sessionFactory == null)
                {
                    sessionFactory = Fluently
                    .Configure()
                    .Database(MySQLConfiguration.Standard.ConnectionString(c => c.FromConnectionStringWithKey("SampleApp")))
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Database>())
                    .ExposeConfiguration(BuildSchema)
                    .BuildSessionFactory();
                }
                return sessionFactory;
            }
        }

        private static void BuildSchema(Configuration config)
        {
            new SchemaUpdate(config).Execute(false, true);
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}

