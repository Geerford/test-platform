using System.Net;
using System.Web.Mvc;
using Application.Interfaces;
using Core;

namespace web_application_mvc.Controllers
{
    public class ReportsController : Controller
    {
        IReportService reportService;
        IUserService userService;

        public ReportsController(IReportService reportService, IUserService userService)
        {
            this.reportService = reportService;
            this.userService = userService;
        }

        // GET: Reports
        public ActionResult Index()
        {
            return View(reportService.GetAll());
        }

        // GET: Reports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = reportService.Get(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // GET: Reports/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(userService.GetAll(), "ID", "Name");
            return View();
        }

        // POST: Reports/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Link")] Report report)
        {
            if (ModelState.IsValid)
            {
                reportService.Create(report);
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(userService.GetAll(), "ID", "Name", report.ID);
            return View(report);
        }

        // GET: Reports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = reportService.Get(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(userService.GetAll(), "ID", "Name", report.ID);
            return View(report);
        }

        // POST: Reports/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Link")] Report report)
        {
            if (ModelState.IsValid)
            {
                reportService.Edit(report);
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(userService.GetAll(), "ID", "Name", report.ID);
            return View(report);
        }

        // GET: Reports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = reportService.Get(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // POST: Reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Report report = reportService.Get(id);
            reportService.Delete(report);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                reportService.Dispose();
                userService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}