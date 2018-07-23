using System.Web.Mvc;

namespace ChurchKioskAPI.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Go to not-authorized view
        /// </summary>
        /// <returns></returns>
        public ActionResult NotAuthorized()
        {
            return View();
        }
    }
}
