using System.Net;
using System.Web.Mvc;
using Application.Interfaces;

namespace web_application_mvc.Controllers
{
    public class TypesController : Controller
    {
        ITypeService typeService;

        public TypesController(ITypeService typeService)
        {
            this.typeService = typeService;
        }

        // GET: Types
        public ActionResult Index()
        {
            return View(typeService.GetAll());
        }

        // GET: Types/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Core.Type type = typeService.Get(id);
            if (type == null)
            {
                return HttpNotFound();
            }
            return View(type);
        }

        // GET: Types/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Types/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Desription,Status")] Core.Type type)
        {
            if (ModelState.IsValid)
            {
                typeService.Create(type);
                return RedirectToAction("Index");
            }

            return View(type);
        }

        // GET: Types/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Core.Type type = typeService.Get(id);
            if (type == null)
            {
                return HttpNotFound();
            }
            return View(type);
        }

        // POST: Types/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Desription,Status")] Core.Type type)
        {
            if (ModelState.IsValid)
            {
                typeService.Edit(type);
                return RedirectToAction("Index");
            }
            return View(type);
        }

        // GET: Types/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Core.Type type = typeService.Get(id);
            if (type == null)
            {
                return HttpNotFound();
            }
            return View(type);
        }

        // POST: Types/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Core.Type type = typeService.Get(id);
            typeService.Delete(type);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                typeService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}