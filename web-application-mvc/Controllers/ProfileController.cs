using Application.Interfaces;
using Core;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web.Mvc;
using web_application_mvc.Models;

namespace web_application_mvc.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        IUserService userService;
        IActivityService activityService;
        IGradeService gradeService;

        public ProfileController(IUserService userService, IActivityService activityService, IGradeService gradeService)
        {
            this.userService = userService;
            this.activityService = activityService;
            this.gradeService = gradeService;
        }

        // GET: Profile
        public ActionResult Index()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var id = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var user = userService.Get(int.Parse(id));
            if(user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(user);
        }

        public ActionResult Edit()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var id = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var user = userService.Get(int.Parse(id));
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                userService.Edit(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [Authorize(Roles = "Администратор")]
        public ActionResult Admin()
        {
            return PartialView("_AdminPartial");
        }

        [Authorize(Roles = "Студент")]
        public ActionResult Student()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var id = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var user = userService.Get(int.Parse(id));
            GradeActivityViewModel model = new GradeActivityViewModel()
            {
                Grades = gradeService.GetAll().Where(x => x.UserID == user.ID),
                Activities = activityService.GetAll().Where(x => x.UserID == user.ID)
            };
            return PartialView("_StudentPartial", model);
        }
    }
}