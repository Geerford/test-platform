using System.Web.Mvc;

namespace web_application_mvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Tests()
        {
            return View();
        }

        public ActionResult Statistics()
        {
            return View();
        }
        
        public new ActionResult Profile()
        {
            return View();
        }
    }
}