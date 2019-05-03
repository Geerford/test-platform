using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Application.Interfaces;
using Core;
using web_application_mvc.App_Start;

namespace web_application_mvc.Controllers
{
    [Authorize(Roles = "Администратор")]
    public class CoursesController : Controller
    {
        ICourseService courseService;
        ISectionService sectionService;

        public CoursesController(ICourseService courseService, ISectionService sectionService)
        {
            this.courseService = courseService;
            this.sectionService = sectionService;
        }

        // GET: Courses
        public ActionResult Index()
        {
            return View(courseService.GetAll());
        }

        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = courseService.Get(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            ViewBag.SectionID = new SelectList(sectionService.GetAll(), "ID", "Description");
            return View();
        }

        // POST: Courses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Description,Link,SectionID")] Course course, 
            HttpPostedFileBase filedata = null)
        {
            if (ModelState.IsValid)
            {
                string fileName = "";
                if (filedata != null)
                {
                    string path = System.Web.HttpContext.Current.Server.MapPath("~/Content/Uploads/"), 
                        fileEx = Path.GetExtension(filedata.FileName);
                    fileName = System.Guid.NewGuid().ToString() + fileEx;
                    filedata.SaveAs(path + fileName);
                }
                course.Link = fileName;
                courseService.Create(course);
                return RedirectToAction("Index");
            }

            ViewBag.SectionID = new SelectList(sectionService.GetAll(), "ID", "Description", course.SectionID);
            return View(course);
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = courseService.Get(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.SectionID = new SelectList(sectionService.GetAll(), "ID", "Description", course.SectionID);
            return View(course);
        }

        // POST: Courses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Description,Link,SectionID")] Course course,
            HttpPostedFileBase filedata = null)
        {
            if (ModelState.IsValid)
            {
                string fileName = "";
                if (filedata != null)
                {
                    string path = System.Web.HttpContext.Current.Server.MapPath("~/Content/Uploads/"),
                        fileEx = Path.GetExtension(filedata.FileName);
                    if (System.IO.File.Exists(path + course.Link))
                    {
                        System.IO.File.Delete(path + course.Link);
                    }
                    fileName = System.Guid.NewGuid().ToString() + fileEx;
                    filedata.SaveAs(path + fileName);
                }
                course.Link = fileName;
                courseService.Edit(course);
                return RedirectToAction("Index");
            }
            ViewBag.SectionID = new SelectList(sectionService.GetAll(), "ID", "Description", course.SectionID);
            return View(course);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = courseService.Get(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = courseService.Get(id);
            courseService.Delete(course);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                courseService.Dispose();
                sectionService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}