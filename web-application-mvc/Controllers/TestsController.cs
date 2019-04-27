using System.Net;
using System.Web.Mvc;
using Application.Interfaces;
using Core;

namespace web_application_mvc.Controllers
{
    public class TestsController : Controller
    {
        ITestService testService;
        ISectionService sectionService;

        public TestsController(ITestService testService, ISectionService sectionService)
        {
            this.testService = testService;
            this.sectionService = sectionService;
        }

        // GET: Tests
        public ActionResult Index()
        {
            return View(testService.GetAll());
        }

        // GET: Tests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Test test = testService.Get(id);
            if (test == null)
            {
                return HttpNotFound();
            }
            return View(test);
        }

        // GET: Tests/Create
        public ActionResult Create()
        {
            ViewBag.SectionID = new SelectList(sectionService.GetAll(), "ID", "Description");
            return View();
        }

        // POST: Tests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Description,SectionID")] Test test)
        {
            if (ModelState.IsValid)
            {
                testService.Create(test);
                return RedirectToAction("Index");
            }

            ViewBag.SectionID = new SelectList(sectionService.GetAll(), "ID", "Description", test.SectionID);
            return View(test);
        }

        // GET: Tests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Test test = testService.Get(id);
            if (test == null)
            {
                return HttpNotFound();
            }
            ViewBag.SectionID = new SelectList(sectionService.GetAll(), "ID", "Description", test.SectionID);
            return View(test);
        }

        // POST: Tests/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Description,SectionID")] Test test)
        {
            if (ModelState.IsValid)
            {
                testService.Edit(test);
                return RedirectToAction("Index");
            }
            ViewBag.SectionID = new SelectList(sectionService.GetAll(), "ID", "Description", test.SectionID);
            return View(test);
        }

        // GET: Tests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Test test = testService.Get(id);
            if (test == null)
            {
                return HttpNotFound();
            }
            return View(test);
        }

        // POST: Tests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Test test = testService.Get(id);
            testService.Delete(test);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                testService.Dispose();
                sectionService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}