using System.Net;
using System.Web.Mvc;
using Application.Interfaces;
using Core;

namespace web_application_mvc.Controllers
{
    public class CuratorsController : Controller
    {
        ICuratorService curatorService;
        IUserService userService;

        public CuratorsController(ICuratorService curatorService, IUserService userService)
        {
            this.curatorService = curatorService;
            this.userService = userService;
        }

        // GET: Curators
        public ActionResult Index()
        {
            return View(curatorService.GetAll());
        }

        // GET: Curators/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curator curator = curatorService.Get(id);
            if (curator == null)
            {
                return HttpNotFound();
            }
            return View(curator);
        }

        // GET: Curators/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(userService.GetAll(), "ID", "Name");
            return View();
        }

        // POST: Curators/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID")] Curator curator)
        {
            if (ModelState.IsValid)
            {
                curatorService.Create(curator);
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(userService.GetAll(), "ID", "Name", curator.ID);
            return View(curator);
        }

        // GET: Curators/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curator curator = curatorService.Get(id);
            if (curator == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(userService.GetAll(), "ID", "Name", curator.ID);
            return View(curator);
        }

        // POST: Curators/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID")] Curator curator)
        {
            if (ModelState.IsValid)
            {
                curatorService.Edit(curator);
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(userService.GetAll(), "ID", "Name", curator.ID);
            return View(curator);
        }

        // GET: Curators/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curator curator = curatorService.Get(id);
            if (curator == null)
            {
                return HttpNotFound();
            }
            return View(curator);
        }

        // POST: Curators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Curator curator = curatorService.Get(id);
            curatorService.Delete(curator);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                curatorService.Dispose();
                userService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}