using Application.Interfaces;
using Core;
using System.Net;
using System.Web.Mvc;

namespace web_application_mvc.Controllers
{
    public class UsersController : Controller
    {
        IUserService userService;
        ICuratorService curatorService;
        IGroupService groupService;

        public UsersController(IUserService userService, ICuratorService curatorService, IGroupService groupService)
        {
            this.userService = userService;
            this.curatorService = curatorService;
            this.groupService = groupService;
        }

        // GET: Users
        public ActionResult Index()
        {
            return View(userService.GetAll());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = userService.Get(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.CurrentCuratorID = new SelectList(curatorService.GetAll(), "ID", "ID");
            ViewBag.GroupID = new SelectList(groupService.GetAll(), "ID", "Description");
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Surname,Midname,Username,Password,Phone,Email,Status,CurrentCuratorID,GroupID")] User user)
        {
            if (ModelState.IsValid)
            {
                userService.Create(user);
                return RedirectToAction("Index");
            }
            ViewBag.CurrentCuratorID = new SelectList(curatorService.GetAll(), "ID", "ID", user.CurrentCuratorID);
            ViewBag.GroupID = new SelectList(groupService.GetAll(), "ID", "Description", user.GroupID);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = userService.Get(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.CurrentCuratorID = new SelectList(curatorService.GetAll(), "ID", "ID", user.CurrentCuratorID);
            ViewBag.GroupID = new SelectList(groupService.GetAll(), "ID", "Description", user.GroupID);
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Surname,Midname,Username,Password,Phone,Email,Status,CurrentCuratorID,GroupID")] User user)
        {
            if (ModelState.IsValid)
            {
                userService.Edit(user);
                return RedirectToAction("Index");
            }
            ViewBag.CurrentCuratorID = new SelectList(curatorService.GetAll(), "ID", "ID", user.CurrentCuratorID);
            ViewBag.GroupID = new SelectList(groupService.GetAll(), "ID", "Description", user.GroupID);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = userService.Get(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = userService.Get(id);
            userService.Delete(user);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                userService.Dispose();
                curatorService.Dispose();
                groupService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}