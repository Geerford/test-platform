using System.Net;
using System.Web.Mvc;
using Application.Interfaces;
using Core;

namespace web_application_mvc.Controllers
{
    [Authorize(Roles = "Администратор")]
    public class AnswersController : Controller
    {
        IAnswerService answerService;
        IQuestionService questionService;

        public AnswersController(IAnswerService answerService, IQuestionService questionService)
        {
            this.answerService = answerService;
            this.questionService = questionService;
        }

        // GET: Answers
        public ActionResult Index()
        {
            return View(answerService.GetAll());
        }

        // GET: Answers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answer answer = answerService.Get(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            return View(answer);
        }

        // GET: Answers/Create
        public ActionResult Create()
        {
            ViewBag.QuestionID = new SelectList(questionService.GetAll(), "ID", "Description");
            return View();
        }

        // POST: Answers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Desctiption,Correct,QuestionID")] Answer answer)
        {
            if (ModelState.IsValid)
            {
                answerService.Create(answer);
                return RedirectToAction("Index");
            }

            ViewBag.QuestionID = new SelectList(questionService.GetAll(), "ID", "Description", answer.QuestionID);
            return View(answer);
        }

        // GET: Answers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answer answer = answerService.Get(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionID = new SelectList(questionService.GetAll(), "ID", "Description", answer.QuestionID);
            return View(answer);
        }

        // POST: Answers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Desctiption,Correct,QuestionID")] Answer answer)
        {
            if (ModelState.IsValid)
            {
                answerService.Edit(answer);
                return RedirectToAction("Index");
            }
            ViewBag.QuestionID = new SelectList(questionService.GetAll(), "ID", "Description", answer.QuestionID);
            return View(answer);
        }

        // GET: Answers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answer answer = answerService.Get(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            return View(answer);
        }

        // POST: Answers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Answer answer = answerService.Get(id);
            answerService.Delete(answer);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                answerService.Dispose();
                questionService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}