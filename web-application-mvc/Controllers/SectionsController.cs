using System.Net;
using System.Web.Mvc;
using Application.Interfaces;
using Core;

namespace web_application_mvc.Controllers
{
    public class SectionsController : Controller
    {
        ISectionService sectionService;

        public SectionsController(ISectionService sectionService)
        {
            this.sectionService = sectionService;
        }

        // GET: Sections
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sections/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
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