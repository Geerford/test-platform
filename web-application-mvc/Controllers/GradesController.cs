using System.Linq;
using System.Net;
using System.Web.Mvc;
using Application.Interfaces;
using Core;

namespace web_application_mvc.Controllers
{
    [Authorize(Roles = "Администратор")]
    public class GradesController : Controller
    {
        IGradeService gradeService;
        ITestService testService;
        IUserService userService;

        public GradesController(IGradeService gradeService, ITestService testService, IUserService userService)
        {
            this.gradeService = gradeService;
            this.testService = testService;
            this.userService = userService;
        }
        
        // GET: Grades
        public ActionResult Index()
        {
            return View(gradeService.GetAll());
        }

        // GET: Grades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grade grade = gradeService.Get(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            return View(grade);
        }

        // GET: Grades/Create
        public ActionResult Create()
        {
            ViewBag.TestID = new SelectList(testService.GetAll(), "ID", "Title");
            ViewBag.UserID = new SelectList(userService.GetAll().Where(x => x.Role.Value.Equals("Студент")), "ID", "Name");
            return View();
        }

        // POST: Grades/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Value,UserID,TestID")] Grade grade)
        {
            if (ModelState.IsValid)
            {
                gradeService.Create(grade);
                return RedirectToAction("Index");
            }

            ViewBag.TestID = new SelectList(testService.GetAll(), "ID", "Title", grade.TestID);
            ViewBag.UserID = new SelectList(userService.GetAll().Where(x => x.Role.Value.Equals("Студент")), "ID", "Name");
            return View(grade);
        }

        // GET: Grades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grade grade = gradeService.Get(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            ViewBag.TestID = new SelectList(testService.GetAll(), "ID", "Title", grade.TestID);
            ViewBag.UserID = new SelectList(userService.GetAll().Where(x => x.Role.Value.Equals("Студент")), "ID", "Name");
            return View(grade);
        }

        // POST: Grades/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Value,UserID,TestID")] Grade grade)
        {
            if (ModelState.IsValid)
            {
                gradeService.Edit(grade);
                return RedirectToAction("Index");
            }
            ViewBag.TestID = new SelectList(testService.GetAll(), "ID", "Title", grade.TestID);
            ViewBag.UserID = new SelectList(userService.GetAll().Where(x => x.Role.Value.Equals("Студент")), "ID", "Name");
            return View(grade);
        }

        // GET: Grades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grade grade = gradeService.Get(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            return View(grade);
        }

        // POST: Grades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Grade grade = gradeService.Get(id);
            gradeService.Delete(grade);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                gradeService.Dispose();
                testService.Dispose();
                userService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}