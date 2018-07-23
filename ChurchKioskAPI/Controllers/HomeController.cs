using System.Collections.Generic;
using System.Web.Http;

namespace ChurchKioskAPI.Controllers
{
    [Authorize]
    public class HomeController : ApiController
    {
        // GET: api/Home
        public IEnumerable<string> Get()
        {
            return new string[] { "I'm a secured resource!" };
        }
    }
}
