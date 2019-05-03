using Application.Interfaces;
using Core;
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
        IGroupSectionService groupSectionService;
        ISectionService sectionService;

        public TestController(ITestService testService, IQuestionService questionService, IGradeService gradeService, 
            IUserService userService, IGroupSectionService groupSectionService, ISectionService sectionService)
        {
            this.testService = testService;
            this.questionService = questionService;
            this.gradeService = gradeService;
            this.userService = userService;
            this.groupSectionService = groupSectionService;
            this.sectionService = sectionService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            TestVM quiz = new TestVM();
            var identity = (ClaimsIdentity)User.Identity;
            var id = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var user = userService.Get(int.Parse(id));
            List<GroupSection> groupSections = new List<GroupSection>();
            if(user.GroupID != null)
            {
                groupSections.AddRange(groupSectionService.GetAll().Where(x => x.GroupID == user.GroupID));
            }
            //Section by group
            List<Section> sections = new List<Section>();
            foreach(var item in sectionService.GetAll())
            {
                for(int i = 0; i < groupSections.Count; i++)
                {
                    if(item.ID == groupSections[i].SectionID)
                    {
                        sections.Add(item);
                        groupSections.Remove(groupSections[i]);
                    }
                }
            }
            List<Test> tests = new List<Test>();
            foreach(var item in testService.GetAll())
            {
                for(int i = 0; i < sections.Count; i++)
                {
                    if(item.SectionID == sections[i].ID)
                    {
                        tests.Add(item);
                        sections.Remove(sections[i]);
                    }
                }
            }

            quiz.Questions = tests.Select(q => new SelectListItem
            {
                Text = q.Title,
                Value = q.ID.ToString()

            }).ToList();

            return View(quiz);
        }

        [HttpPost]
        public ActionResult Index(TestVM quiz)
        {
            TestVM quizSelected = testService.GetAll().Where(t => t.ID == quiz.ID).Select(q => new TestVM
            {
                ID = q.ID,
                Name = q.Title,

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
            TestVM quizSelected = Session["SelectedQuiz"] as TestVM;
            IQueryable<QuestionVM> questions = null;

            if (quizSelected != null)
            {
                questions = questionService.GetAll().Where(x => x.TestID == quizSelected.ID)
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
            return View(questions);
        }

        [HttpPost]
        public ActionResult Work(List<AnswersVM> resultQuiz)
        {
            List<AnswersVM> finalResultQuiz = new List<AnswersVM>();

            foreach (AnswersVM answser in resultQuiz)
            {
                if(answser.Answer != null)
                {
                    AnswersVM result = questionService.GetAll().Where(x => x.ID == answser.QuestionID)
                        .FirstOrDefault().Answers.Where(d => d.Correct).Select(a => new AnswersVM
                    {
                        QuestionID = a.ID,
                        Answer = a.Desctiption,
                        IsCorrect = a.Desctiption.Equals(answser.Answer)
                    }).FirstOrDefault();

                    finalResultQuiz.Add(result);
                }
                else
                {
                    AnswersVM result = questionService.GetAll().Where(x => x.ID == answser.QuestionID)
                    .FirstOrDefault().Answers.Where(d => d.Correct).Select(a => new AnswersVM
                    {
                        QuestionID = a.ID,
                        Answer = a.Desctiption,
                        IsCorrect = false
                    }).FirstOrDefault();

                    finalResultQuiz.Add(result);
                }
            }
            int count = 0, correct = 0;
            foreach(var item in finalResultQuiz)
            {
                ++count;
                if (item.IsCorrect)
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
                double mark = (double)correct / count;
                var grade = new Core.Grade
                {
                    TestID = question.TestID,
                    UserID = user.ID,
                    Value = mark
                };

                gradeService.Create(grade);
            }
            return Json(new { result = finalResultQuiz }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "Администратор")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Администратор")]
        public ActionResult Create(List<Question> questions)
        {
            return View();
        }
    }    
}