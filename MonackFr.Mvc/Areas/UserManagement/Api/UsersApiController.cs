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
        private Repositories.IUserRepository _repository;
        
        #endregion //private properties

        #region constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UsersApiController()
            : this(new Repositories.UserRepository())
        {
        }

        /// <summary>
        /// Constructor with custom repository
        /// </summary>
        /// <param name="repository"></param>
        public UsersApiController(Repositories.IUserRepository repository)
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
            var users = mapper.Map<List<User>>(_repository.GetAll());

            return Ok(users);
        }

        /// <summary>
        /// Returns a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Get(int id)
        {
            var mapper = AutoMapperConfig.Mapper;
            var user = mapper.Map<User>(_repository.GetSingle(u =>u.Id == id));

            return Ok(user);
        }

        /// <summary>
        /// Creates user
        /// </summary>
        /// <param name="user"></param>
        public void Post(User user)
        {
            var mapper = AutoMapperConfig.Mapper;
            _repository.Create(mapper.Map<Entities.User>(user));
            _repository.Save();
        }

        /// <summary>
        /// Edits user
        /// </summary>
        /// <param name="user"></param>
        public void Put(User user)
        {
            var mapper = AutoMapperConfig.Mapper;
            var userToUpdate = _repository.GetSingle(u => u.Id == user.Id);
            mapper.Map(user, userToUpdate);
            _repository.Edit(userToUpdate);
            _repository.Save();
        }

        /// <summary>
        /// Deletes user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            var userToDelete = _repository.GetSingle(u => u.Id == id);
            _repository.Delete(userToDelete);
            _repository.Save();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
