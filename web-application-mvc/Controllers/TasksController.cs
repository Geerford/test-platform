using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Application.Interfaces;
using Core;
using web_application_mvc.Models;

namespace web_application_mvc.Controllers
{
    [Authorize(Roles = "Администратор")]
    public class TasksController : Controller
    {
        ITaskService taskService;
        ISectionService sectionService;
        IUserTaskService userTaskService;
        IUserService userService;

        public TasksController(ITaskService taskService, ISectionService sectionService,
            IUserTaskService userTaskService, IUserService userService)
        {
            this.taskService = taskService;
            this.sectionService = sectionService;
            this.userTaskService = userTaskService;
            this.userService = userService;
        }

        // GET: Tasks
        public ActionResult Index()
        {
            return View(taskService.GetAll());
        }

        // GET: Tasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = taskService.Get(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            List<UserTask> userTask = userTaskService.GetAll().Where(x => x.TaskID == task.ID).ToList();

            AdminTaskViewModel model = new AdminTaskViewModel
            {
                ID = task.ID,
                Description = task.Description,
                Section = task.Section,
                Students = userTaskService.GetAll().Where(x => x.TaskID == task.ID)
                    .Select(x => new AdminInnerTaskModel
                    {
                        Answer = x.Answer,
                        Comment = x.Comment,
                        Grade = x.Grade,
                        User = userService.Get(x.UserID)
                    })
            };
            return View(model);
        }

        // GET: Tasks/Create
        public ActionResult Create()
        {
            ViewBag.SectionID = new SelectList(sectionService.GetAll(), "ID", "Description");
            return View();
        }

        // POST: Tasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Description,SectionID")] Task task)
        {
            if (ModelState.IsValid)
            {
                taskService.Create(task);
                return RedirectToAction("Index");
            }
            ViewBag.SectionID = new SelectList(sectionService.GetAll(), "ID", "Description", task.SectionID);
            return View(task);
        }

        // GET: Tasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = taskService.Get(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            ViewBag.SectionID = new SelectList(sectionService.GetAll(), "ID", "Description", task.SectionID);
            return View(task);
        }

        // POST: Tasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Description,SectionID")] Task task)
        {
            if (ModelState.IsValid)
            {
                taskService.Edit(task);
                return RedirectToAction("Index");
            }
            ViewBag.SectionID = new SelectList(sectionService.GetAll(), "ID", "Description", task.SectionID);
            return View(task);
        }

        // GET: Tasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = taskService.Get(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            List<UserTask> userTask = userTaskService.GetAll().Where(x => x.TaskID == task.ID).ToList();

            AdminTaskViewModel model = new AdminTaskViewModel
            {
                ID = task.ID,
                Title = task.Title,
                Description = task.Description,
                Section = task.Section,
                Students = userTaskService.GetAll().Where(x => x.TaskID == task.ID)
                    .Select(x => new AdminInnerTaskModel
                    {
                        Answer = x.Answer,
                        Comment = x.Comment,
                        Grade = x.Grade,
                        User = userService.Get(x.UserID)
                    })
            };
            return View(model);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Task task = taskService.Get(id);
            taskService.Delete(task);
            List<UserTask> tasks = new List<UserTask>();
            foreach(var item in userTaskService.GetAll().Where(x => x.TaskID == task.ID))
            {
                tasks.Add(item);
            }
            foreach(var item in tasks)
            {
                userTaskService.Delete(item);
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                taskService.Dispose();
                sectionService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
