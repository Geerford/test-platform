using Application.Interfaces;
using Core;
using System.Collections.Generic;
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
        ITaskService taskService;
        IUserTaskService userTaskService;
        ICuratorService curatorService;

        public ProfileController(IUserService userService, IActivityService activityService, IGradeService gradeService,
            ITaskService taskService, IUserTaskService userTaskService, ICuratorService curatorService)
        {
            this.userService = userService;
            this.activityService = activityService;
            this.gradeService = gradeService;
            this.taskService = taskService;
            this.userTaskService = userTaskService;
            this.curatorService = curatorService;
        }

        // GET: Profile
        public ActionResult Index()
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
            List<StudentTaskViewModelPartial> tasks = new List<StudentTaskViewModelPartial>();
            foreach (UserTask task in userTaskService.GetAll().Where(x => x.UserID == user.ID))
            {
                tasks.Add(new StudentTaskViewModelPartial
                {
                    ID = task.ID,
                    Task = taskService.Get(task.TaskID),
                    Answer = task.Answer,
                    Comment = task.Comment,
                    Grade = task.Grade
                });
            }
            GradeActivityViewModel model = new GradeActivityViewModel()
            {
                Grades = gradeService.GetAll().Where(x => x.UserID == user.ID),
                Activities = activityService.GetAll().Where(x => x.UserID == user.ID),
                Tasks = tasks
            };
            return PartialView("_StudentPartial", model);
        }

        [Authorize]
        public ActionResult Curator()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var id = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var user = userService.Get(int.Parse(id));
            var curator = curatorService.GetAll().Where(x => x.User.ID == user.ID).FirstOrDefault();
            if(curator == null)
            {
                return null;
            }
            List<ExtentionTaskViewModel> checkedTasks = new List<ExtentionTaskViewModel>();
            List<ExtentionTaskViewModel> uncheckedTasks = new List<ExtentionTaskViewModel>();
            foreach(var student in curator.Students)
            {
                foreach (UserTask task in userTaskService.GetAll().Where(x => x.UserID == student.ID))
                {
                    if (string.IsNullOrEmpty(task.Grade))
                    {
                        uncheckedTasks.Add(new ExtentionTaskViewModel
                        {
                            ID = task.ID,
                            User = student,
                            Task = taskService.Get(task.TaskID),
                            Answer = task.Answer,
                            Comment = task.Comment,
                            Grade = task.Grade
                        });
                    }
                    else
                    {
                        checkedTasks.Add(new ExtentionTaskViewModel
                        {
                            ID = task.ID,
                            User = student,
                            Task = taskService.Get(task.TaskID),
                            Answer = task.Answer,
                            Comment = task.Comment,
                            Grade = task.Grade
                        });
                    }
                }
            }            
            CuratorProfileViewModel model = new CuratorProfileViewModel()
            {
                Checked = checkedTasks,
                Unchecked = uncheckedTasks
            };
            return PartialView("_CuratorPartial", model);
        }
    }
}