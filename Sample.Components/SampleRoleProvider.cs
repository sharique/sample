using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using Sample.Data;
using NHibernate.Linq;

namespace Sample.Components
{
    class SampleRoleProvider : RoleProvider
    {
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            IList<string> roles = new List<string>();
            using (var session = Database.OpenSession())
            {
                roles = (from x in session.Query<Role>()
                         select x.RoleName).ToList();
            }
            return roles.ToArray();
        }

        public override string[] GetRolesForUser(string username)
        {
            //IList<string> roles = new List<string>();
            //using (var session = Database.OpenSession())
            //{
            //    List<UserInRole> lst = (from x in session.Query<User>()                         
            //             where  x.UserName == username
            //             select x.UserInRoles).ToList();

            //    roles = lst.Select(x => x.Role.RoleName).ToList();
            //}
            //return roles.ToArray();

            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            bool result = false;
            using (var session = Database.OpenSession())
            {
                if (session.Query<Role>().Where(x => x.RoleName == roleName).Count() != 0)
                {
                    result = true;
                }
            }

            return result;
        }
    }
}
