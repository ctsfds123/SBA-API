using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using ProjecManagement.BusinessLayer.ViewModel;
using ProjecManagement.ErrorHandler;
using ProjecManagement.BusinessLayer.Implementation;
using ProjecManagement.Business.Interface;

namespace ProjecManagement.Services.Controllers
{
    [RoutePrefix("Api/UserDetail")]
    public class UserDetailController : ApiController
    {
        readonly IUserDetailsBL UserDetailsBL;
        readonly IErrorLog _logger;
        public UserDetailController(IUserDetailsBL _UserDetailsBL, IErrorLog logger)
        {
            UserDetailsBL = _UserDetailsBL;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<UserDetailsViewModel> GetAll()
        {
            return UserDetailsBL.GetAllUserDetailsRecord();
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetById(int id)
        {
            var user = UserDetailsBL.GetAllUserDetailsRecord().FirstOrDefault(e => e.UserId == id);
            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Save(UserDetailsViewModel model)
        {
            UserDetailsBL.SaveUserDetailsRecord(model);
            return Ok();
        }

        [HttpPost]
        [Route("delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            UserDetailsBL.DeleteUserDetailsRecordByUserId(id);
            return Ok();
        }
    }
}
