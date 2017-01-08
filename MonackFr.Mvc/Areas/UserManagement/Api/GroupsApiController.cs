using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MonackFr.Mvc.Areas.UserManagement.ViewModels;
using MonackFr.Mvc.Repositories;

namespace MonackFr.Mvc.Areas.UserManagement.Api
{
    public class GroupsApiController : ApiController
    {
        #region private properties

        /// <summary>
        /// The repository
        /// </summary>
        private IGroupRepository _repository;
        
        #endregion //private properties

        #region constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public GroupsApiController()
            : this(new GroupRepository())
        {
        }

        /// <summary>
        /// Constructor with custom repository and authentication object
        /// </summary>
        /// <param name="repository"></param>
        public GroupsApiController(IGroupRepository repository)
        {
            _repository = repository;
        }

        #endregion //constructors

        /// <summary>
        /// Returns groups
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            var mapper = AutoMapperConfig.Mapper;
            var list = mapper.Map<List<Group>>(_repository.GetAll());

            return Ok(list);
        }

        /// <summary>
        /// Returns a group
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Get(int id)
        {
            var mapper = AutoMapperConfig.Mapper;
            var group = mapper.Map<Group>(_repository.GetSingle(g => g.Id == id));

            return Ok(group);
        }

        /// <summary>
        /// Creates group
        /// </summary>
        /// <param name="group"></param>
        public void Post(Group group)
        {
            var mapper = AutoMapperConfig.Mapper;
            _repository.Create(mapper.Map<Entities.Group>(group));
            _repository.Save();
        }

        /// <summary>
        /// Edits group
        /// </summary>
        /// <param name="group"></param>
        public void Put(Group group)
        {
            var mapper = AutoMapperConfig.Mapper;
            var groupToUpdate = _repository.GetSingle(g => g.Id == group.Id);
            mapper.Map(group, groupToUpdate);
            _repository.Edit(groupToUpdate);
            _repository.Save();
        }

        /// <summary>
        /// Deletes group
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            var groupToDelete = _repository.GetSingle(g => g.Id == id);
            _repository.Delete(groupToDelete);
            _repository.Save();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
