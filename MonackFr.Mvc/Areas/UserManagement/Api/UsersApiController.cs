using System.Collections.Generic;
using System.Web.Http;
using System.Net.Http;
using MonackFr.Mvc.Areas.UserManagement.ViewModels;
using MonackFr.Security;
using System.Net;

namespace MonackFr.Mvc.Areas.UserManagement.Api
{
    public class UsersApiController : ApiController
    {
        #region private properties

        /// <summary>
        /// The repository
        /// </summary>
        private MonackFr.Mvc.Repositories.IUserRepository _repository;

        /// <summary>
        /// Authentication object
        /// </summary>
        private IAuthentication _authentication;

        #endregion //private properties

        #region constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UsersApiController()
            : this(new MonackFr.Mvc.Repositories.UserRepository(), new Authentication())
        {
        }

        /// <summary>
        /// Constructor with custom repository and authentication object
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="authentication"></param>
        public UsersApiController(MonackFr.Mvc.Repositories.IUserRepository repository, IAuthentication authentication)
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
            var lijst = mapper.Map<List<User>>(_repository.GetAll());

            return Ok(lijst);
        }

        public void Post(User user)
        {
            var mapper = AutoMapperConfig.Mapper;
            _repository.Create(mapper.Map<Entities.User>(user));
            _repository.Save();
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            var mapper = AutoMapperConfig.Mapper;
            var userToDelete = _repository.GetSingle(u => u.Id == id);
            _repository.Delete(userToDelete);
            _repository.Save();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
