using Application.Interfaces;
using Core;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        ITypeService typeService;
        ITaskService taskService;
        IUserTaskService userTaskService;

        public TestController(ITestService testService, IQuestionService questionService, IGradeService gradeService, 
            IUserService userService, IGroupSectionService groupSectionService, ISectionService sectionService,
            ITypeService typeService, ITaskService taskService, IUserTaskService userTaskService)
        {
            this.testService = testService;
            this.questionService = questionService;
            this.gradeService = gradeService;
            this.userService = userService;
            this.groupSectionService = groupSectionService;
            this.sectionService = sectionService;
            this.typeService = typeService;
            this.taskService = taskService;
            this.userTaskService = userTaskService;
        }

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
            foreach (var item in sections)
            {
                foreach(var grade in user.Grades)
                {
                    foreach(var test in item.Tests)
                    {
                        if (grade.TestID != test.ID )
                        {
                            tests.Add(test);
                        }
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
                        IsCorrect = a.Desctiption.ToLower().Equals(answser.Answer.ToLower())
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
                var grade = new Grade
                {
                    TestID = question.TestID,
                    UserID = user.ID,
                    Value = mark
                };

                gradeService.Create(grade);
            }
            return Json(new { result = finalResultQuiz }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Администратор")]
        public ActionResult Create()
        {
            ViewBag.SectionID = new SelectList(sectionService.GetAll(), "ID", "Description");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Администратор")]
        public ActionResult Create(TestModel test)
        {
            List<Question> questions = new List<Question>();
            foreach(var question in test.Questions)
            {
                Type type;
                if (question.Type.Equals("enter"))
                {
                    type = typeService.GetAll().Where(x => x.Status.Equals("Ввод ответа")).FirstOrDefault();
                }
                else
                {
                    type = typeService.GetAll().Where(x => x.Status.Equals("Выбор ответа")).FirstOrDefault();
                }
                List<Answer> answers = new List<Answer>();
                foreach(var answer in question.Answers)
                {
                    answers.Add(new Answer
                    {
                        Desctiption = answer.Answer,
                        Correct = answer.IsCorrect
                    });
                }
                questions.Add(new Question
                {
                    Description = question.Description,
                    TypeID = type.ID,
                    Answers = answers
                });
            }
            testService.Create(new Test
            {
                Description = test.Description,
                Questions = questions,
                SectionID = int.Parse(test.Section),
                Title = test.Title
            });
            ViewBag.SectionID = new SelectList(sectionService.GetAll(), "ID", "Description");
            return View();
        }

        [Authorize(Roles = "Администратор")]
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
            TestModel model = new TestModel
            {
                ID = test.ID,
                Description = test.Description,
                Section = test.Section.Description,
                Title = test.Title,
                Questions = test.Questions.Select(x => new QuestionViewModel()
                {
                    Description = x.Description,
                    Answers = x.Answers.Select(y => new AnswerViewModel()
                    {
                        Answer = y.Desctiption,
                        IsCorrect = y.Correct
                    }).ToList(),
                    Type = x.Type.Status
                }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Администратор")]
        public ActionResult Edit(TestModel test)
        {
            if (test.ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Test item = testService.Get(test.ID);
            if (item == null)
            {
                return HttpNotFound();
            }
            testService.Delete(item);
            List<Question> questions = new List<Question>();
            foreach (var question in test.Questions)
            {
                Type type;
                if (question.Type.Equals("enter"))
                {
                    type = typeService.GetAll().Where(x => x.Status.Equals("Ввод ответа")).FirstOrDefault();
                }
                else
                {
                    type = typeService.GetAll().Where(x => x.Status.Equals("Выбор ответа")).FirstOrDefault();
                }
                List<Answer> answers = new List<Answer>();
                foreach (var answer in question.Answers)
                {
                    answers.Add(new Answer
                    {
                        Desctiption = answer.Answer,
                        Correct = answer.IsCorrect
                    });
                }
                questions.Add(new Question
                {
                    Description = question.Description,
                    TypeID = type.ID,
                    Answers = answers
                });
            }
            testService.Create(new Test
            {
                Description = test.Description,
                Questions = questions,
                SectionID = int.Parse(test.Section),
                Title = test.Title
            });
            ViewBag.SectionID = new SelectList(sectionService.GetAll(), "ID", "Description");
            return View(test);
        }

        [Authorize]
        public ActionResult Task()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var id = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var user = userService.Get(int.Parse(id));
            List<GroupSection> groupSections = new List<GroupSection>();
            if (user.GroupID != null)
            {
                groupSections.AddRange(groupSectionService.GetAll().Where(x => x.GroupID == user.GroupID));
            }
            //Section by group
            List<Section> sections = new List<Section>();
            foreach (var item in sectionService.GetAll())
            {
                for (int i = 0; i < groupSections.Count; i++)
                {
                    if (item.ID == groupSections[i].SectionID)
                    {
                        sections.Add(item);
                        groupSections.Remove(groupSections[i]);
                    }
                }
            }
            List<Task> tasks = new List<Task>();
            foreach (var item in sections)
            {
                foreach (var task in item.Tasks)
                {
                    var past = userTaskService.GetAll().Where(x => x.TaskID == item.ID && x.UserID == user.ID)
                        .FirstOrDefault();
                    if (past == null && task.SectionID == item.ID)
                    {
                        tasks.Add(task);
                    }
                }
            }

            ChooseTaskViewModel model = new ChooseTaskViewModel
            {
                DropList = tasks.Select(q => new SelectListItem
                {
                    Text = q.Description,
                    Value = q.ID.ToString()

                }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Task(ChooseTaskViewModel task)
        {
            Task choosed = taskService.GetAll().Where(x => x.ID == task.ID).FirstOrDefault();
            if (choosed != null)
            {
                return RedirectToAction("Laboratory", new { id = choosed.ID });
            }
            return View();
        }

        [Authorize]
        public ActionResult Laboratory(int id)
        {
            Task task = taskService.GetAll().Where(x => x.ID == id).FirstOrDefault();
            if (task != null)
            {
                return View(new StudentTaskViewModel
                {
                    ID = task.ID,
                    Task = task.Description
                });
            }
            return RedirectToAction("Task");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Laboratory(StudentTaskViewModel model)
        {
            Task task = taskService.GetAll().Where(x => x.ID == model.ID).FirstOrDefault();
            if (task != null)
            {
                var identity = (ClaimsIdentity)User.Identity;
                var id = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                userTaskService.Create(new UserTask
                {
                    TaskID = model.ID,
                    UserID = int.Parse(id),
                    Answer = model.Answer
                });
                return RedirectToAction("Task");
            }
            return View();
        }
    }    
}