using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonackFr.Mvc.Areas.UserManagement
{
    public interface IUserManager
    {
        void CreateUser(string username, string password);
        void AddUserToAllRoles(string username);
    }
}