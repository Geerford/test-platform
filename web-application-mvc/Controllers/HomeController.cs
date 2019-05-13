using Application.Interfaces;
using Core;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web.Mvc;
using web_application_mvc.Models;

namespace web_application_mvc.Controllers
{
    public class HomeController : Controller
    {
        IUserService userService;

        public HomeController(IUserService userService)
        {
            this.userService = userService;
        }

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

        public ActionResult Contacts()
        {
            return View();
        }

        public ActionResult Safety()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var id = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            User user = null;
            if(id != null)
            {
                user = userService.Get(int.Parse(id.Value));
            }
            if (user == null)
            {
                return View();
            }
            if (user.Safety == false)
            {
                return View(new SafetyViewModel
                {
                    Status = false
                });
            }
            return View(new SafetyViewModel
            {
                Status = true
            });
        }

        [Authorize]
        [HttpPost]
        public ActionResult Safety(SafetyViewModel model)
        {
            if (model.Status)
            {
                var identity = (ClaimsIdentity)User.Identity;
                var id = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                var user = userService.Get(int.Parse(id));
                if (user == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                user.Safety = true;
                userService.Edit(user);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}