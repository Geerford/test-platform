using System.Net;
using System.Web.Mvc;
using Application.Interfaces;
using Core;

namespace web_application_mvc.Controllers
{
    public class QuestionsController : Controller
    {
        IQuestionService questionService;
        ITestService testService;
        ITypeService typeService;

        public QuestionsController(IQuestionService questionService, ITestService testService, ITypeService typeService)
        {
            this.questionService = questionService;
            this.testService = testService;
            this.typeService = typeService;
        }

        // GET: Questions
        public ActionResult Index()
        {
            return View(questionService.GetAll());
        }

        // GET: Questions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = questionService.Get(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // GET: Questions/Create
        public ActionResult Create()
        {
            ViewBag.TestID = new SelectList(testService.GetAll(), "ID", "Title");
            ViewBag.TypeID = new SelectList(typeService.GetAll(), "ID", "Description");
            return View();
        }

        // POST: Questions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Description,TestID,TypeID")] Question question)
        {
            if (ModelState.IsValid)
            {
                questionService.Create(question);
                return RedirectToAction("Index");
            }
            ViewBag.TestID = new SelectList(testService.GetAll(), "ID", "Title", question.TestID);
            ViewBag.TypeID = new SelectList(typeService.GetAll(), "ID", "Description", question.TypeID);
            return View(question);
        }

        // GET: Questions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = questionService.Get(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            ViewBag.TestID = new SelectList(testService.GetAll(), "ID", "Title", question.TestID);
            ViewBag.TypeID = new SelectList(typeService.GetAll(), "ID", "Description", question.TypeID);
            return View(question);
        }

        // POST: Questions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Description,TestID,TypeID")] Question question)
        {
            if (ModelState.IsValid)
            {
                questionService.Edit(question);
                return RedirectToAction("Index");
            }
            ViewBag.TestID = new SelectList(testService.GetAll(), "ID", "Title", question.TestID);
            ViewBag.TypeID = new SelectList(typeService.GetAll(), "ID", "Description", question.TypeID);
            return View(question);
        }

        // GET: Questions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = questionService.Get(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = questionService.Get(id);
            questionService.Delete(question);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                questionService.Dispose();
                testService.Dispose();
                typeService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}