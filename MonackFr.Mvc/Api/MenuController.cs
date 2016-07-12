using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MonackFr.Mvc.Api
{
    public class MenuController : ApiController
    {

        /// <summary>
        /// Returns menu items of a module
        /// </summary>
        /// <param name="systemName"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Get(string systemName)
        {
            return Ok(ModuleKeeper.Instance.GetModule(systemName).GetMenu());
        }
    }
}
