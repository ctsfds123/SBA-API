using ProjecManagement.BusinessLayer.Interface;
using ProjecManagement.BusinessLayer.ViewModel;
using System.Web.Http;

namespace ProjecManagement.Services.Controllers
{
    [RoutePrefix("Api/TaskDetail")]
    public class TasksController : ApiController
    {
        readonly ITaskDetailsBL _taskBusiness;
        public TasksController(ITaskDetailsBL taskBusiness)
        {
            _taskBusiness = taskBusiness;
        }

        [HttpGet]
        [Route("parent-tasks")]
        public IHttpActionResult GetAllParentTasks()
        {
            var models = _taskBusiness.GetAllParentTaskDetails();
            return Ok(models);
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetById(int id)
        {
            var task = _taskBusiness.GetDetailsById(id);
            return Ok(task);
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var tasks = _taskBusiness.GetAllTaskDetails();
            return Ok(tasks);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Save(TaskViewModel model)
        {
            _taskBusiness.SaveTaskDetils(model);
            return Ok();
        }

        [HttpPost]
        [Route("complete")]
        public IHttpActionResult Complete(TaskViewModel model)
        {
            _taskBusiness.Complete(model);

            var tasks = _taskBusiness.GetAllTaskDetails();
            return Ok(tasks); ;
        }
    }
}
