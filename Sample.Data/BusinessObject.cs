using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace Sample.Data
{
    public class BusinessObject<T>
    {
        public virtual void Save()
        {
            ISession session = Database.OpenSession();
            if (!this.IsPersisted)
                session.Save(this);
            else
                session.Merge(this);

            session.Flush();
        }

        public virtual void Delete()
        {
            ISession session = Database.OpenSession();
            session.Delete(this);
            session.Flush();
        }

        public virtual T Clone()
        {
            return this.Clone();
        }

        /// <summary>
        /// Returns a valid populated object or a null reference        
        /// </summary>        
        public static T Get(int id)
        {
            if (id <= 0)
                return default(T);
            else
            {
                ISession session = Database.OpenSession();
                return session.Get<T>(id);
            }
        }

        private int _id;

        public virtual int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public virtual bool IsPersisted
        {
            get
            {
                return this.ID > 0;
            }
        }
    }

}
