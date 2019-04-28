using Application.Interfaces;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web.Mvc;

namespace web_application_mvc.Controllers
{
    public class ProfileController : Controller
    {
        IUserService service;

        public ProfileController(IUserService service)
        {
            this.service = service;
        }

        // GET: Profile
        [Authorize]
        public ActionResult Index()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var id = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var user = service.Get(int.Parse(id));
            if(user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(user);
        }

        [Authorize]
        public ActionResult Admin()
        {
            return PartialView("_AdminPartial");
        }
    }
}