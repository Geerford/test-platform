using Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;
using web_application_mvc.Models;

namespace web_application_mvc.Controllers
{
    [Authorize]
    public class TestController : Controller
    {

        ITestService testService;
        IQuestionService questionService;
        IGradeService gradeService;
        IUserService userService;

        public TestController(ITestService testService, IQuestionService questionService, IGradeService gradeService, IUserService userService)
        {
            this.testService = testService;
            this.questionService = questionService;
            this.gradeService = gradeService;
            this.userService = userService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            QuizVM quiz = new QuizVM();
            quiz.ListOfQuizz = testService.GetAll().Select(q => new SelectListItem
            {
                Text = q.Title,
                Value = q.ID.ToString()

            }).ToList();

            return View(quiz);
        }

        [HttpPost]
        public ActionResult Index(QuizVM quiz)
        {
            QuizVM quizSelected = testService.GetAll().Where(t => t.ID == quiz.QuizID).Select(q => new QuizVM
            {
                QuizID = q.ID,
                QuizName = q.Title,

            }).FirstOrDefault();

            if (quizSelected != null)
            {
                Session["SelectedQuiz"] = quizSelected;

                return RedirectToAction("Work");
            }

            return View();
        }

        [HttpGet] 
        public ActionResult Work()
        {
            QuizVM quizSelected = Session["SelectedQuiz"] as QuizVM;
            IQueryable<QuestionVM> questions = null;

            if (quizSelected != null)
            {
                questions = questionService.GetAll().Where(x => x.TestID == quizSelected.QuizID)
                    .Select(q => new QuestionVM
                    {
                        QuestionID = q.ID,
                        QuestionText = q.Description,
                        Choices = q.Answers.Select(c => new ChoiceVM
                        {
                            ChoiceID = c.ID,
                            ChoiceText = c.Desctiption
                        }).ToList(),
                        QuestionType = q.Type.Status
                    }).AsQueryable();
                

            }
            var y = questions.ToList(); //
            return View(questions);
        }

        [HttpPost]
        public ActionResult Work(List<QuizAnswersVM> resultQuiz)
        {
            List<QuizAnswersVM> finalResultQuiz = new List<QuizAnswersVM>();

            foreach (QuizAnswersVM answser in resultQuiz)
            {
                if(answser.AnswerQ != null)
                {
                    QuizAnswersVM result = questionService.GetAll().Where(x => x.ID == answser.QuestionID)
                        .FirstOrDefault().Answers.Where(d => d.Correct).Select(a => new QuizAnswersVM
                    {
                        QuestionID = a.ID,
                        AnswerQ = a.Desctiption,
                        isCorrect = a.Desctiption.Equals(answser.AnswerQ)
                    }).FirstOrDefault();

                    finalResultQuiz.Add(result);
                }
                else
                {
                    QuizAnswersVM result = questionService.GetAll().Where(x => x.ID == answser.QuestionID)
                    .FirstOrDefault().Answers.Where(d => d.Correct).Select(a => new QuizAnswersVM
                    {
                        QuestionID = a.ID,
                        AnswerQ = a.Desctiption,
                        isCorrect = false
                    }).FirstOrDefault();

                    finalResultQuiz.Add(result);
                }
            }
            int count = 0, correct = 0;
            foreach(var item in finalResultQuiz)
            {
                ++count;
                if (item.isCorrect)
                {
                    ++correct;
                }
            }
            var question = questionService.GetAll().Where(x => x.ID == finalResultQuiz[0].QuestionID).FirstOrDefault();
            if(question != null)
            {
                var identity = (ClaimsIdentity)User.Identity;
                var id = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                var user = userService.Get(int.Parse(id));
                double mark = correct / count;
                var grade = new Core.Grade
                {
                    //Test = question.Test,
                    TestID = question.TestID,
                   // User = user,
                    UserID = user.ID,
                    Value = mark
                };

                gradeService.Create(grade);
            }
            return Json(new { result = finalResultQuiz }, JsonRequestBehavior.AllowGet);
        }
    }    
}