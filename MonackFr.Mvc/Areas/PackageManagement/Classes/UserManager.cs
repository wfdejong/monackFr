using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace MonackFr.Mvc.Areas.PackageManagement
{
    public class UserManager : IUserManager
    {
        void IUserManager.CreateUser(string username, string password)
        {
            Membership.CreateUser(username, password);
        }
        void IUserManager.AddUserToAllRoles(string username)
        {
            string[] roles = Roles.GetAllRoles();

            if (roles.Length > 0)
            {
                Roles.AddUserToRoles("admin", roles);
            }
        }
    }
}