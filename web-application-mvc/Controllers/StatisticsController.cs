using Application.Interfaces;
using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web.Mvc;
using web_application_mvc.Models;

namespace web_application_mvc.Controllers
{
    public class StatisticsController : Controller
    {
        IUserService userService;
        IActivityService activityService;
        IGradeService gradeService;

        public StatisticsController(IUserService userService, IActivityService activityService, IGradeService gradeService)
        {
            this.userService = userService;
            this.activityService = activityService;
            this.gradeService = gradeService;
        }

        // GET: Statistics
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Report(int? id)
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
            ReportViewModel model = new ReportViewModel()
            {
                User = user,
                Activities = activityService.GetAll().Where(x => x.UserID == id),
                Grades = gradeService.GetAll().Where(x => x.UserID == id)
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Search(string query)
        {
            ViewBag.Query = query;
            List<User> result = new List<User>();
            foreach (var item in userService.GetAll())
            {
                if(PropertiesThatContainText(item, query))
                {
                    result.Add(item);
                }
            }
            return View(result);
        }

        public static bool PropertiesThatContainText<T>(T obj, string text, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
               .Where(p => p.PropertyType == typeof(string) && p.CanRead);
            foreach (PropertyInfo prop in properties)
            {
                string propVal = (string)prop.GetValue(obj, null);
                if (string.Equals(text, propVal, comparison)) return true;
            }
            return false;
        }
    }
}