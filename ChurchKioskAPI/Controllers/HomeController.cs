using System.Collections.Generic;
using System.Web.Http;

namespace ChurchKioskAPI.Controllers
{
    [Authorize]
    public class HomeController : ApiController
    {
        // GET: api/Home
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Get() : Method of HomeController!" };
        }

        [HttpGet]
        public IEnumerable<string> GetById(string id)
        {
            return new string[] { "GetById(string id) : Method of HomeController!" };
        }
    }
}
