using Application.Interfaces;
using Core;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace web_application_mvc.Controllers
{
    public class HomeController : Controller
    {
        IUserService service;

        public HomeController(IUserService service)
        {
            this.service = service;
        }

        public ActionResult Index()
        {
            List<User> items = service.GetAll().ToList();
            return View(items);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}