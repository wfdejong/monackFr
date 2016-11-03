using System.Collections.Generic;
using System.Web.Http;
using MonackFr.Mvc.Areas.UserManagement.ViewModels;
using MonackFr.Mvc.Repositories;

namespace MonackFr.Mvc.Areas.UserManagement.Api
{
    public class RolesApiController : ApiController
    {
        #region private properties

        /// <summary>
        /// The repository
        /// </summary>
        private IRoleRepository _repository;

        #endregion //private properties

        #region constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public RolesApiController() : this(new RoleRepository())
        {
        }

        /// <summary>
        /// Constructor with custom repository
        /// </summary>
        /// <param name="repository"></param>
        public RolesApiController(IRoleRepository repository)
        {
            _repository = repository;
        }

        #endregion //constructors

        /// <summary>
        /// Retrurns all users
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            var mapper = AutoMapperConfig.Mapper;
            var users = mapper.Map<List<Role>>(_repository.GetAll());

            return Ok(users);
        }
    }
}