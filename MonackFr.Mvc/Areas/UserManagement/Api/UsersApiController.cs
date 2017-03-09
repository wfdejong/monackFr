using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Net.Http;
using MonackFr.Mvc.Areas.UserManagement.ViewModels;
using System.Net;
using MonackFr.Mvc.Repositories;

namespace MonackFr.Mvc.Areas.UserManagement.Api
{
    public class UsersApiController : ApiController
    {
        #region private properties

        /// <summary>
        /// The user repository
        /// </summary>
        private IUserRepository _userRepository;

        /// <summary>
        /// The group repository
        /// </summary>
        private IGroupRepository _groupRepository;

        #endregion //private properties

        #region constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UsersApiController()
            : this(new UserRepository(), new GroupRepository())
        {
        }

        /// <summary>
        /// Constructor with custom userRepository
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="groupRepository"></param>
        public UsersApiController(IUserRepository userRepository, IGroupRepository groupRepository)
        {
            _userRepository = userRepository;
            _groupRepository = groupRepository;
        }

        #endregion //constructors

        /// <summary>
        /// Retrurns all users
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            var mapper = AutoMapperConfig.Mapper;
            var users = mapper.Map<List<User>>(_userRepository.GetAll().ToList());
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
            var entityUser = _userRepository.GetSingle(u => u.Id == id);
            
            var user = mapper.Map<User>(entityUser);
            user.Groups = mapper.Map<IEnumerable<Group>>(_groupRepository.GetAll().ToList());

            foreach (Group group in user.Groups)
                group.Selected = entityUser.Groups.Select(g => g.Name).Contains(group.Name);

            return Ok(user);
        }

        /// <summary>
        /// Creates user
        /// </summary>
        /// <param name="user"></param>
        public void Post(User user)
        {
            var mapper = AutoMapperConfig.Mapper;
            _userRepository.Create(mapper.Map<Entities.User>(user));
            _userRepository.Save();
        }

        /// <summary>
        /// Edits user
        /// </summary>
        /// <param name="user"></param>
        public void Put(User user)
        {
            var mapper = AutoMapperConfig.Mapper;
            var userToUpdate = _userRepository.GetSingle(u => u.Id == user.Id);
            mapper.Map(user, userToUpdate);
            _userRepository.Edit(userToUpdate);

            _userRepository.RemoveAllGroupsFromUser(userToUpdate);

            var selectedGroups = from g2 in user.Groups where g2.Selected select g2.Name;
            var groups = from g in _groupRepository.GetAll()
                         where selectedGroups.Contains(g.Name)
                         select g;
            _userRepository.AddGroupsToUser(userToUpdate, groups.ToArray());
            
            _userRepository.Save();
        }

        /// <summary>
        /// Deletes user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            var userToDelete = _userRepository.GetSingle(u => u.Id == id);
            _userRepository.Delete(userToDelete);
            _userRepository.Save();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
