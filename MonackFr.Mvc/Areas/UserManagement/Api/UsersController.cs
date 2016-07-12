using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MonackFr.Mvc.Areas.UserManagement.ViewModels;

namespace MonackFr.Mvc.Areas.UserManagement.Api
{
    public class UsersController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            List<User> lijst = new List<User>
            {
                new User()
                {
                    UserName = "User1"
                },
                new User()
                {
                    UserName = "User2"
                }
            };

            return Ok(lijst);
        }
    }
}
