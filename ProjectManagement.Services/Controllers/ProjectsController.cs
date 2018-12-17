using ProjecManagement.BusinessLayer.Interface;
using ProjecManagement.BusinessLayer.ViewModel;
using ProjecManagement.ErrorHandler;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ProjecManagement.Services.Controllers
{
    [RoutePrefix("Api/ProjectDetail")]
    public class ProjectsController : ApiController
    {
        readonly IProjectDetailsBL ProjectDetailsBL;
        readonly IErrorLog _logger;
        readonly IProjectDetailsBL _projectBusiness;
        public ProjectsController(IProjectDetailsBL _ProjectDetailsBL)
        {
            ProjectDetailsBL = _ProjectDetailsBL;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<ProjectViewModel> GetAll()
        {
            return ProjectDetailsBL.GetAllProjectDetail();
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetById(int id)
        {
            var user = ProjectDetailsBL.GetAllProjectDetail().FirstOrDefault(e => e.ProjectId == id);
            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Save(ProjectViewModel model)
        {
            ProjectDetailsBL.SaveProjectDetails(model);
            return Ok();
        }

        [HttpPost]
        [Route("delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            ProjectDetailsBL.DeleteProjectDetail(id);
            return Ok();
        }
    }
}
