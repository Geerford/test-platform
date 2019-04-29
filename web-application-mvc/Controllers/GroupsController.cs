using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Application.Interfaces;
using Core;

namespace web_application_mvc.Controllers
{
    public class GroupsController : Controller
    {
        IGroupService groupService;
        IUserService userService;

        public GroupsController(IGroupService groupService, IUserService userService)
        {
            this.groupService = groupService;
            this.userService = userService;
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
            ViewBag.Students = group.Students;
            return View(group);
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            ViewBag.Students = userService.GetAll().Where(x => x.GroupID == null).ToList();
            return View();
        }

        // POST: Groups/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection formval, [Bind(Include = "ID,Description,University,Department,Start,End")] Group group)
        {
            if (group.Start > group.End)
            {
                ModelState.AddModelError("End", "Начальная дата должна быть меньше конечной");
            }
            if (ModelState.IsValid)
            {
                List<string> students = new List<string>();
                if (formval["students"] != null)
                {
                    students = formval["students"].Split(',').ToList();
                }
                if(students.Count > 0)
                {
                    foreach (var studentID in formval["students"].Split(',').ToList())
                    {
                        User user = userService.Get(int.Parse(studentID));
                        user.Group = group;
                        userService.Edit(user);
                    }
                }
                else
                {
                    groupService.Create(group);
                }
                return RedirectToAction("Index");
            }
            ViewBag.Students = userService.GetAll().Where(x => x.GroupID == null).ToList();
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
            ViewBag.Users = userService.GetAll().Where(x => x.GroupID == null).ToList();
            ViewBag.Students = group.Students;
            return View(group);
        }

        // POST: Groups/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection formval, [Bind(Include = "ID,Description,University,Department,Start,End")] Group group)
        {
            if (ModelState.IsValid)
            {
                List<string> students = new List<string>(), users = new List<string>();
                if (formval["students"] != null)
                {
                    students = formval["students"].Split(',').ToList();
                }
                if(formval["users"] != null)
                {
                    users = formval["users"].Split(',').ToList();
                }
                List<User> all = userService.GetAll().Where(x => x.GroupID == group.ID).ToList();
                foreach (var item in all)
                {
                    item.GroupID = null;
                    userService.Edit(item);
                }
                if (students.Count > 0 || users.Count > 0)
                {
                    if (students.Count > 0)
                    {
                        foreach (var studentID in students)
                        {
                            User user = userService.Get(int.Parse(studentID));
                            user.GroupID = group.ID;
                            userService.Edit(user);
                        }
                    }
                    if (users.Count > 0)
                    {
                        foreach (var studentID in users)
                        {
                            User user = userService.Get(int.Parse(studentID));
                            user.GroupID = group.ID;
                            userService.Edit(user);
                        }
                    }
                }
                else
                {
                    groupService.Edit(group);
                }
                return RedirectToAction("Index");
            }
            ViewBag.Users = userService.GetAll().Where(x => x.GroupID == null).ToList();
            ViewBag.Students = group.Students;
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