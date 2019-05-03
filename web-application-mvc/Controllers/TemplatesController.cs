using System.Net;
using System.Web.Mvc;
using Application.Interfaces;
using Core;

namespace web_application_mvc.Controllers
{
    [Authorize(Roles = "Администратор")]
    public class TemplatesController : Controller
    {
        ITemplateService templateService;

        public TemplatesController(ITemplateService templateService)
        {
            this.templateService = templateService;
        }

        // GET: Templates
        public ActionResult Index()
        {
            return View(templateService.GetAll());
        }

        // GET: Templates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Template template = templateService.Get(id);
            if (template == null)
            {
                return HttpNotFound();
            }
            return View(template);
        }

        // GET: Templates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Templates/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Description")] Template template)
        {
            if (ModelState.IsValid)
            {
                templateService.Create(template);
                return RedirectToAction("Index");
            }

            return View(template);
        }

        // GET: Templates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Template template = templateService.Get(id);
            if (template == null)
            {
                return HttpNotFound();
            }
            return View(template);
        }

        // POST: Templates/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Description")] Template template)
        {
            if (ModelState.IsValid)
            {
                templateService.Edit(template);
                return RedirectToAction("Index");
            }
            return View(template);
        }

        // GET: Templates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Template template = templateService.Get(id);
            if (template == null)
            {
                return HttpNotFound();
            }
            return View(template);
        }

        // POST: Templates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Template template = templateService.Get(id);
            templateService.Delete(template);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                templateService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}