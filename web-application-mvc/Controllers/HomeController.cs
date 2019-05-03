using System.Web.Mvc;
using web_application_mvc.App_Start;

namespace web_application_mvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Tests()
        {
            return View();
        }

        [Authorize(Roles = "Администратор")]
        public ActionResult Statistics()
        {
            return View();
        }

        [Authorize]
        public new ActionResult Profile()
        {
            return View();
        }
    }
}