using System.Net;
using System.Web.Mvc;
using Application.Interfaces;
using Core;

namespace web_application_mvc.Controllers
{
    public class GroupsController : Controller
    {
        IGroupService groupService;

        public GroupsController(IGroupService groupService)
        {
            this.groupService = groupService;
        }

        // GET: Groups
        public ActionResult Index()
        {
            return View(groupService.GetAll());
        }

        // GET: Groups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = groupService.Get(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Description,University,Department,Start,End")] Group group)
        {
            if (ModelState.IsValid)
            {
                groupService.Create(group);
                return RedirectToAction("Index");
            }
            return View(group);
        }

        // GET: Groups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = groupService.Get(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Description,University,Department,Start,End")] Group group)
        {
            if (ModelState.IsValid)
            {
                groupService.Edit(group);
                return RedirectToAction("Index");
            }
            return View(group);
        }

        // GET: Groups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = groupService.Get(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Group group = groupService.Get(id);
            groupService.Delete(group);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                groupService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}