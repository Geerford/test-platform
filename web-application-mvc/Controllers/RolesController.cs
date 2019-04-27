using System.Net;
using System.Web.Mvc;
using Application.Interfaces;
using Core;

namespace web_application_mvc.Controllers
{
    public class RolesController : Controller
    {
        IRoleService roleService;
        IUserService userService;

        public RolesController(IRoleService roleService, IUserService userService)
        {
            this.roleService = roleService;
            this.userService = userService;
        }

        // GET: Roles
        public ActionResult Index()
        {
            return View(roleService.GetAll());
        }

        // GET: Roles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = roleService.Get(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(userService.GetAll(), "ID", "Name");
            return View();
        }

        // POST: Roles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Description,Value")] Role role)
        {
            if (ModelState.IsValid)
            {
                roleService.Create(role);
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(userService.GetAll(), "ID", "Name", role.ID);
            return View(role);
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = roleService.Get(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(userService.GetAll(), "ID", "Name", role.ID);
            return View(role);
        }

        // POST: Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Description,Value")] Role role)
        {
            if (ModelState.IsValid)
            {
                roleService.Edit(role);
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(userService.GetAll(), "ID", "Name", role.ID);
            return View(role);
        }

        // GET: Roles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = roleService.Get(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Role role = roleService.Get(id);
            roleService.Delete(role);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                roleService.Dispose();
                userService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}