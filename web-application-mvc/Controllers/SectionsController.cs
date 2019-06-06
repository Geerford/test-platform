using System.Net;
using System.Web.Mvc;
using Application.Interfaces;
using Core;

namespace web_application_mvc.Controllers
{
    //[Authorize(Roles = "Администратор")]
    public class SectionsController : Controller
    {
        ISectionService sectionService;
        ICourseService courseService;
        ITestService testService;

        public SectionsController(ISectionService sectionService, ICourseService courseService, ITestService testService)
        {
            this.sectionService = sectionService;
            this.courseService = courseService;
            this.testService = testService;
        }

        // GET: Sections
        [Authorize(Roles = "Администратор")]
        public ActionResult Index()
        {
            return View(sectionService.GetAll());
        }

        // GET: Sections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = sectionService.Get(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // GET: Sections/Create
        [Authorize(Roles = "Администратор")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sections/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Администратор")]
        public ActionResult Create([Bind(Include = "ID,Description")] Section section)
        {
            if (ModelState.IsValid)
            {
                sectionService.Create(section);
                return RedirectToAction("Index");
            }
            return View(section);
        }

        // GET: Sections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = sectionService.Get(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // POST: Sections/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Администратор")]
        public ActionResult Edit([Bind(Include = "ID,Description")] Section section)
        {
            if (ModelState.IsValid)
            {
                sectionService.Edit(section);
                return RedirectToAction("Index");
            }
            return View(section);
        }

        // GET: Sections/Delete/5
        [Authorize(Roles = "Администратор")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = sectionService.Get(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // POST: Sections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Администратор")]
        public ActionResult DeleteConfirmed(int id)
        {
            Section section = sectionService.Get(id);
            sectionService.Delete(section);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                sectionService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}