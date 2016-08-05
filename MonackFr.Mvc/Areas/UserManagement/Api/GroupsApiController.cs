using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using MonackFr.Mvc.Areas.UserManagement.ViewModels;
using MonackFr.Mvc.Repositories;
using MonackFr.Security;

namespace MonackFr.Mvc.Areas.UserManagement.Api
{
    public class GroupsApiController : ApiController
    {
        #region private properties

        /// <summary>
        /// The repository
        /// </summary>
        private IGroupRepository _repository;

        /// <summary>
        /// Authentication object
        /// </summary>
        private IAuthentication _authentication;

        #endregion //private properties

        #region constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public GroupsApiController()
            : this(new GroupRepository(), new Authentication())
        {
        }

        /// <summary>
        /// Constructor with custom repository and authentication object
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="authentication"></param>
        public GroupsApiController(IGroupRepository repository, IAuthentication authentication)
        {
            _repository = repository;
            _authentication = authentication;
        }

        #endregion //constructors

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            var mapper = AutoMapperConfig.Mapper;
            var lijst = mapper.Map<List<Group>>(_repository.GetAll());

            return Ok(lijst);
        }
    }
}
