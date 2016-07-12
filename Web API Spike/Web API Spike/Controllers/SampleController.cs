using System.Web.Http;
using MediatR;
using Web_API_Spike.Commands;
using Web_API_Spike.Configuration;
using Web_API_Spike.Configuration.Filters;

namespace Web_API_Spike.Controllers
{
    [ValidateModelStateFilter]
    [AllowAnonymous]
    [RoutePrefix("api")]
    public class SampleController : ApiController
    {
        private readonly ISettings _settings;
        private readonly IMediator _mediator;

        public SampleController(ISettings settings, IMediator mediator)
        {
            _settings = settings;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("connectionstring")]
        public IHttpActionResult GetTestData()
        {
            return Ok(_settings.GetConnectionString("DefaultConnection"));
        }

        [HttpPost]
        [Route("echo")]
        public IHttpActionResult Echo(TestCommandRequest request)
        {
            var echoMessage = _mediator.Send(request);
            return Ok(new { Message = echoMessage });
        }
    }
}
