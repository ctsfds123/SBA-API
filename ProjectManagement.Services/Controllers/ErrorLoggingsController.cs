using ProjecManagement.BusinessLayer.ViewModel;
using ProjecManagement.ErrorHandler;
using System.Web.Http;

namespace ProjecManagement.Services.Controllers
{
    [RoutePrefix("Api/ErrorLog")]
    public class ErrorLoggingsController : ApiController
    {
        readonly IErrorLog _logger;
        public ErrorLoggingsController(IErrorLog logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Save([FromBody]ErrorLog request)
        {
            if (string.Equals(request.LogType, "info"))
                _logger.Informations(request.Message);
            else
                _logger.ErrorLogstr(request.Message);

            return Ok();
        }
    }
}
