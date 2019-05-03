using System.Net;
using System.Web.Mvc;
using Application.Interfaces;
using Core;

namespace web_application_mvc.Controllers
{
    [Authorize(Roles = "Администратор")]
    public class UsersController : Controller
    {
        IUserService userService;
        ICuratorService curatorService;
        IGroupService groupService;
        IRoleService roleService;

        public UsersController(IUserService userService, ICuratorService curatorService, IGroupService groupService,
            IRoleService roleService)
        {
            this.userService = userService;
            this.curatorService = curatorService;
            this.groupService = groupService;
            this.roleService = roleService;
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
            ViewBag.CurrentCuratorID = new SelectList(curatorService.GetAll(), "ID", "User.Surname");
            ViewBag.GroupID = new SelectList(groupService.GetAll(), "ID", "Description");
            ViewBag.RoleID = new SelectList(roleService.GetAll(), "ID", "Value");
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Surname,Midname,Email,Password,Phone,Status,RoleID,CurrentCuratorID,GroupID")] User user)
        {
            if (ModelState.IsValid)
            {
                userService.Create(user);
                return RedirectToAction("Index");
            }
            ViewBag.CurrentCuratorID = new SelectList(curatorService.GetAll(), "ID", "User.Surname", user.CurrentCuratorID);
            ViewBag.GroupID = new SelectList(groupService.GetAll(), "ID", "Description", user.GroupID);
            ViewBag.RoleID = new SelectList(roleService.GetAll(), "ID", "Value", user.RoleID);
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
            ViewBag.CurrentCuratorID = new SelectList(curatorService.GetAll(), "ID", "User.Surname", user.CurrentCuratorID);
            ViewBag.GroupID = new SelectList(groupService.GetAll(), "ID", "Description", user.GroupID);
            ViewBag.RoleID = new SelectList(roleService.GetAll(), "ID", "Value", user.RoleID);
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Surname,Midname,Email,Password,Phone,Status,RoleID,CurrentCuratorID,GroupID")] User user)
        {
            if (ModelState.IsValid)
            {
                userService.Edit(user);
                return RedirectToAction("Index");
            }
            ViewBag.CurrentCuratorID = new SelectList(curatorService.GetAll(), "ID", "User.Surname", user.CurrentCuratorID);
            ViewBag.GroupID = new SelectList(groupService.GetAll(), "ID", "Description", user.GroupID);
            ViewBag.RoleID = new SelectList(roleService.GetAll(), "ID", "Value", user.RoleID);
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
                roleService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}