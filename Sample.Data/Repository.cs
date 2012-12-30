using System;
using System.Linq;
using NHibernate;
using NHibernate.Linq;

namespace Sample.Data
{
    public sealed class Repository
    {
        private ISession session = null;

        private static readonly Repository _instance = new Repository();

        public static Repository Instance
        {
            get { return _instance; }
        }

        public IQueryable<T> GetAll<T>() //where T : IBusinessBase
        {
            return session.Query<T>();
        }

        public bool Save<T>(T item)
        {
            bool result = false;
            using (var transaction = session.BeginTransaction())
            {
                try
                {
                    session.SaveOrUpdate(item);
                    transaction.Commit();
                    result = true;
                }
                catch
                {
                    transaction.Rollback();
                }
            }
            return result;
        }

        public bool Delete<T>(T item)
        {
            bool result = false;
            using (var transaction = session.BeginTransaction())
            {
                try
                {
                    session.Delete(item);
                    transaction.Commit();
                    result = true;
                }
                catch
                {
                    transaction.Rollback();
                }
            }
            return result;
        }

        public IQueryable<Setting> GetSetting(string keyname)
        {
            var value = from x in session.Query<Setting>()
                        where x.SettingName == keyname
                        select x;
            return value;
        }

        public string GetSettingValue(string keyname)
        {
            var value = from x in session.Query<Setting>()
                        where x.SettingName == keyname
                        select x.SettingValue;
            return value.FirstOrDefault();
        }

        public bool SaveSetting(string settingName, string settingValue)
        {
            Setting setting = (from x in session.Query<Setting>()
                               where x.SettingName == settingName
                               select x).SingleOrDefault();
            if (setting == null || setting.SettingId == 0)
            {
                setting = new Setting();
                setting.SettingName = settingName;
            }
            setting.SettingValue = settingValue;

            return Save<Setting>(setting);
        }

        private Repository()
        {
            session = Database.OpenSession();
        }
    }
}

