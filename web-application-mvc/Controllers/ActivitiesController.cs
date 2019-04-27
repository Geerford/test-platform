using System.Net;
using System.Web.Mvc;
using Application.Interfaces;
using Core;

namespace web_application_mvc.Controllers
{
    public class ActivitiesController : Controller
    {
        IActivityService activityService;
        IUserService userService;

        public ActivitiesController(IActivityService activityService, IUserService userService)
        {
            this.activityService = activityService;
            this.userService = userService;
        }

        // GET: Activities
        public ActionResult Index()
        {
            return View(activityService.GetAll());
        }

        // GET: Activities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = activityService.Get(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // GET: Activities/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(userService.GetAll(), "ID", "Name");
            return View();
        }

        // POST: Activities/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Date,UserID")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                activityService.Create(activity);
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(userService.GetAll(), "ID", "Name", activity.UserID);
            return View(activity);
        }

        // GET: Activities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = activityService.Get(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(userService.GetAll(), "ID", "Name", activity.UserID);
            return View(activity);
        }

        // POST: Activities/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Date,UserID")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                activityService.Edit(activity);
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(userService.GetAll(), "ID", "Name", activity.UserID);
            return View(activity);
        }

        // GET: Activities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = activityService.Get(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Activity activity = activityService.Get(id);
            activityService.Delete(activity);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                activityService.Dispose();
                userService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}