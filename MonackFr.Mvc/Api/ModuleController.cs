using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using MonackFr.Mvc.Repositories;

namespace MonackFr.Mvc.Api
{
    public class ModuleController : ApiController
    {
        private ModuleRepository _moduleRepository = null;

        /// <summary>
        /// Constructor
        /// </summary>
        public ModuleController() : base()
        {
            _moduleRepository = new ModuleRepository();

            //Add disposables			
            //AddDisposable(_moduleRepository as IDisposable);
        }

        /// <summary>
        /// Returns all IModules
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            IEnumerable<ViewModels.Module> modules = Mapper.Map<IEnumerable<ViewModels.Module>>(_moduleRepository.GetAll());
            return Ok(modules);
        }
    }
}
