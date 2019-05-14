using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Application.Interfaces;
using Core;
using web_application_mvc.Models;

namespace web_application_mvc.Controllers
{
    [Authorize]
    public class FeedbackController : Controller
    {
        IUserTaskService userTaskService;
        IUserService userService;
        ITaskService taskService;
        ISectionService sectionService;

        public FeedbackController(IUserTaskService userTaskService, IUserService userService, ITaskService taskService,
            ISectionService sectionService)
        {
            this.userService = userService;
            this.userTaskService = userTaskService;
            this.taskService = taskService;
            this.sectionService = sectionService;
        }

        // GET: Feedback
        [Authorize(Roles = "Администратор")]
        public ActionResult Index()
        {
            List<ExtentionTaskViewModel> model = userTaskService.GetAll().Select(x => new ExtentionTaskViewModel
            {
                ID = x.ID,
                Answer = x.Answer,
                Comment = x.Comment,
                Grade = x.Grade,
                Task = taskService.Get(x.TaskID),
                User = userService.Get(x.UserID)
            }).ToList();
            return View(model);
        }

        // GET: Feedback/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTask userTask = userTaskService.GetAll().Where(x => x.ID == id).FirstOrDefault();
            if(userTask == null)
            {
                return HttpNotFound();
            }
            var task = taskService.Get(userTask.TaskID);
            ExtentionTaskViewModel model = new ExtentionTaskViewModel
            {
                ID = userTask.ID,
                Answer = userTask.Answer,
                Comment = userTask.Comment,
                Grade = userTask.Grade,
                Task = task,
                Section = task.Section,
                User = userService.Get(userTask.UserID)
            };
            return View(model);
        }

        // GET: Feedback/Create
        public ActionResult Create(int? id)
        {
            UserTask userTask = userTaskService.GetAll().Where(x => x.ID == id).FirstOrDefault();
            Task task = taskService.Get(userTask.TaskID);
            User user = userService.Get(userTask.UserID);
            ExtentionTaskViewModel model = new ExtentionTaskViewModel
            {
                ID = userTask.ID,
                Answer = userTask.Answer,
                Comment = userTask.Comment,
                Grade = userTask.Grade,
                Section = task.Section,
                Task = task,
                User = user,
                SectionID = task.Section.ID,
                TaskID = task.ID,
                UserID = user.ID
            };

            ViewBag.Grade = new SelectList(new List<string>()
            {
                "Отлично",
                "Хорошо",
                "Удовлетворительно",
                "Неудовлетворительно"
            }, "ID");
            return View(model);
        }

        // POST: Feedback/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ExtentionTaskViewModel task)
        {
            if (ModelState.IsValid)
            {
                userTaskService.Delete(userTaskService.GetAll().Where(x => x.ID == task.ID).FirstOrDefault());
                userTaskService.Create(new UserTask
                {
                    Answer = task.Answer,
                    Comment = task.Comment,
                    Grade = task.Grade,
                    TaskID = (int)task.TaskID,
                    UserID = (int)task.UserID
                });
                return RedirectToAction("Index", "Profile");
            }
            ViewBag.Grade = new SelectList(new List<string>()
            {
                "Отлично",
                "Хорошо",
                "Удовлетворительно",
                "Неудовлетворительно"
            }, "ID", task.Grade);
            return View(task);
        }

        // GET: Feedback/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTask userTask = userTaskService.GetAll().Where(x => x.ID == id).FirstOrDefault();
            if (userTask == null)
            {
                return HttpNotFound();
            }
            var task = taskService.Get(userTask.TaskID);
            ExtentionTaskViewModel model = new ExtentionTaskViewModel
            {
                ID = userTask.ID,
                Answer = userTask.Answer,
                Comment = userTask.Comment,
                Grade = userTask.Grade,
                Task = task,
                Section = task.Section,
                User = userService.Get(userTask.UserID),
                SectionID = task.Section.ID,
                TaskID = task.ID,
                UserID = userTask.UserID
            };
            ViewBag.SectionID = new SelectList(sectionService.GetAll(), "ID", "Description", model.Section.ID);
            if(model.Grade != null)
            {
                ViewBag.Grade = new SelectList(new List<string>()
                {
                    "Отлично",
                    "Хорошо",
                    "Удовлетворительно",
                    "Неудовлетворительно"
                }, model.Grade);
            }
            else
            {
                ViewBag.Grade = new SelectList(new List<string>()
                {
                    "Отлично",
                    "Хорошо",
                    "Удовлетворительно",
                    "Неудовлетворительно"
                }, "ID");
            }            
            return View(model);
        }

        // POST: Feedback/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ExtentionTaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserTask userTask = new UserTask
                {
                    ID = model.ID,
                    TaskID = (int)model.TaskID,
                    UserID = (int)model.UserID,
                    Answer = model.Answer,
                    Comment = model.Comment,
                    Grade = model.Grade
                };
                
                userTaskService.Edit(userTask);
                return RedirectToAction("Index", "Profile");
            }
            ViewBag.SectionID = new SelectList(sectionService.GetAll(), "ID", "Description", model.Section.ID);
            if (model.Grade != null)
            {
                ViewBag.Grade = new SelectList(new List<string>()
                {
                    "Отлично",
                    "Хорошо",
                    "Удовлетворительно",
                    "Неудовлетворительно"
                }, "ID", "Description", model.Grade);
            }
            else
            {
                ViewBag.Grade = new SelectList(new List<string>()
                {
                    "Отлично",
                    "Хорошо",
                    "Удовлетворительно",
                    "Неудовлетворительно"
                }, "ID");
            }
            return View(model);
        }

        // GET: Feedback/Delete/5
        [Authorize(Roles = "Администратор")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTask userTask = userTaskService.GetAll().Where(x => x.ID == id).FirstOrDefault();
            if (userTask == null)
            {
                return HttpNotFound();
            }
            var task = taskService.Get(userTask.TaskID);
            ExtentionTaskViewModel model = new ExtentionTaskViewModel
            {
                ID = userTask.ID,
                Answer = userTask.Answer,
                Comment = userTask.Comment,
                Grade = userTask.Grade,
                Task = task,
                Section = task.Section,
                User = userService.Get(userTask.UserID),
                SectionID = task.Section.ID,
                TaskID = task.ID,
                UserID = userTask.UserID
            };
            return View(model);
        }

        // POST: Feedback/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Администратор")]
        public ActionResult DeleteConfirmed(int id)
        {
            UserTask userTask = userTaskService.GetAll().Where(x => x.ID == id).FirstOrDefault();
            userTaskService.Delete(userTask);
            return RedirectToAction("Index", "Profile");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                userTaskService.Dispose();
                userService.Dispose();
                taskService.Dispose();
                sectionService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}