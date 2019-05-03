using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Application.Interfaces;
using Core;

namespace web_application_mvc.Controllers
{
    [Authorize(Roles = "Администратор")]
    public class CuratorsController : Controller
    {
        ICuratorService curatorService;
        IUserService userService;

        public CuratorsController(ICuratorService curatorService, IUserService userService)
        {
            this.curatorService = curatorService;
            this.userService = userService;
        }

        // GET: Curators
        public ActionResult Index()
        {
            return View(curatorService.GetAll());
        }

        // GET: Curators/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curator curator = curatorService.Get(id);
            if (curator == null)
            {
                return HttpNotFound();
            }
            ViewBag.Students = curator.Students;
            return View(curator);
        }

        // GET: Curators/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(userService.GetAll().Where(x => !x.Role.Value.Equals("Студент")), "ID", "Name");
            ViewBag.Students = userService.GetAll().Where(x => x.CurrentCuratorID == null && x.Role.Value.Equals("Студент")).ToList();
            return View();
        }

        // POST: Curators/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection formval, [Bind(Include = "ID")] Curator curator)
        {
            if (ModelState.IsValid)
            {
                List<string> students = new List<string>();
                if (formval["students"] != null)
                {
                    students = formval["students"].Split(',').ToList();
                }
                if (students.Count > 0)
                {
                    foreach (var studentID in formval["students"].Split(',').ToList())
                    {
                        User user = userService.Get(int.Parse(studentID));
                        user.CurrentCurator = curator;
                        userService.Edit(user);
                    }
                }
                else
                {
                    curatorService.Create(curator);
                }
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(userService.GetAll(), "ID", "Name", curator.ID);
            ViewBag.Students = userService.GetAll().Where(x => x.CurrentCuratorID == null && x.Role.Value.Equals("Студент")).ToList();
            return View(curator);
        }

        // GET: Curators/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curator curator = curatorService.Get(id);
            if (curator == null)
            {
                return HttpNotFound();
            }
            ViewBag.Users = userService.GetAll().Where(x => x.CurrentCuratorID == null && x.Role.Value.Equals("Студент")).ToList();
            ViewBag.Students = curator.Students;
            ViewBag.ID = new SelectList(userService.GetAll(), "ID", "Name", curator.ID);
            return View(curator);
        }

        // POST: Curators/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection formval, [Bind(Include = "ID")] Curator curator)
        {
            if (ModelState.IsValid)
            {
                List<string> students = new List<string>(), users = new List<string>();
                if (formval["students"] != null)
                {
                    students = formval["students"].Split(',').ToList();
                }
                if (formval["users"] != null)
                {
                    users = formval["users"].Split(',').ToList();
                }
                List<User> all = userService.GetAll().Where(x => x.CurrentCuratorID == curator.ID).ToList();
                foreach (var item in all)
                {
                    item.CurrentCuratorID = null;
                    userService.Edit(item);
                }
                if (students.Count > 0 || users.Count > 0)
                {
                    if (students.Count > 0)
                    {
                        foreach (var studentID in students)
                        {
                            User user = userService.Get(int.Parse(studentID));
                            user.CurrentCuratorID = curator.ID;
                            userService.Edit(user);
                        }
                    }
                    if (users.Count > 0)
                    {
                        foreach (var studentID in users)
                        {
                            User user = userService.Get(int.Parse(studentID));
                            user.CurrentCuratorID = curator.ID;
                            userService.Edit(user);
                        }
                    }
                }
                else
                {
                    curatorService.Edit(curator);
                }
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(userService.GetAll(), "ID", "Name", curator.ID);
            ViewBag.Users = userService.GetAll().Where(x => x.CurrentCuratorID == null && x.Role.Value.Equals("Студент")).ToList();
            ViewBag.Students = curator.Students;
            return View(curator);
        }

        // GET: Curators/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curator curator = curatorService.Get(id);
            if (curator == null)
            {
                return HttpNotFound();
            }
            return View(curator);
        }

        // POST: Curators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Curator curator = curatorService.Get(id);
            curatorService.Delete(curator);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                curatorService.Dispose();
                userService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}