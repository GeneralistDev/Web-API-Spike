using System.Web.Http;
using Web_API_Spike.Configuration;

namespace Web_API_Spike.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api")]
    public class SampleController : ApiController
    {
        private readonly ISettings _settings;

        public SampleController(ISettings settings)
        {
            _settings = settings;
        }

        [HttpGet]
        [Route("connectionstring")]
        public IHttpActionResult GetTestData()
        {
            return Ok(_settings.GetConnectionString("DefaultConnection"));
        }
    }
}
