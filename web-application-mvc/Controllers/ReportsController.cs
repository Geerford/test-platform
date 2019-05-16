using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Application.Interfaces;
using Core;
using iTextSharp.text;
using iTextSharp.text.pdf;
using web_application_mvc.Models;

namespace web_application_mvc.Controllers
{
    [Authorize(Roles = "Администратор")]
    public class ReportsController : Controller
    {
        IReportService reportService;
        IUserService userService;
        ITemplateService templateService;
        IGradeService gradeService;
        IActivityService activityService;
        IGroupService groupService;
        IReportQAService reportQAService;
        ITestService testService;
        ITaskService taskService;
        IUserTaskService userTaskService;
        ISectionService sectionService;
        IGroupSectionService groupSectionService;

        public ReportsController(IReportService reportService, IUserService userService, ITemplateService templateService,
            IGradeService gradeService, IActivityService activityService, IGroupService groupService, IReportQAService reportQAService,
            ITestService testService, ISectionService sectionService, IGroupSectionService groupSectionService, ITaskService taskService,
            IUserTaskService userTaskService)
        {
            this.reportService = reportService;
            this.userService = userService;
            this.templateService = templateService;
            this.gradeService = gradeService;
            this.activityService = activityService;
            this.taskService = taskService;
            this.groupService = groupService;
            this.reportQAService = reportQAService;
            this.testService = testService;
            this.sectionService = sectionService;
            this.groupSectionService = groupSectionService;
            this.userTaskService = userTaskService;
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
            Dictionary<string, string> templates = new Dictionary<string, string>();
            foreach(var item in templateService.GetAll())
            {
                templates.Add(item.Description, "");
            }
            ReportTemplateViewModel model = new ReportTemplateViewModel
            {
                Templates = templates
            };
            ViewBag.ID = new SelectList(userService.GetAll().Where(x => x.Role.Value.Equals("Студент")), "ID", "Surname");
            return View(model);
        }

        // POST: Reports/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReportTemplateViewModel report)
        {
            if (ModelState.IsValid)
            {
                string path = System.Web.HttpContext.Current.Server.MapPath("~/Content/Uploads/");
                if (System.IO.File.Exists(path + report.ReportLink))
                {
                    System.IO.File.Delete(path + report.ReportLink);
                }
                
                var student = userService.Get(report.ID);
                List<Grade> grades = gradeService.GetAll().Where(x => x.UserID == report.ID).ToList();
                
                List<Test> tests = new List<Test>();
                foreach (var section in groupSectionService.GetAll().Where(x => x.GroupID == student.GroupID))
                {
                    tests.AddRange(testService.GetAll().Where(x => x.SectionID == section.SectionID));
                }
                double gradeAVG = grades.Select(y => y.Value).Sum() / tests.Count;
                
                List<UserTask> taskGrades = userTaskService.GetAll().Where(x => x.UserID == report.ID).ToList();
                List<Task> tasks = new List<Task>();
                foreach (var section in groupSectionService.GetAll().Where(x => x.GroupID == student.GroupID))
                {
                    tasks.AddRange(taskService.GetAll().Where(x => x.SectionID == section.SectionID));
                }
                double gradeTaskAVG = taskGrades.Select(y => 
                {
                    switch (y.Grade)
                    {
                        case "Отлично":
                            return 5;
                        case "Хорошо":
                            return 4;
                        case "Удовлетворительно":
                            return 3;
                        case "Неудовлетворительно":
                            return 2;
                        default:
                            return 0;
                    }
                }).Sum() / tasks.Count;

                double period = (student.Group.End - student.Group.Start).TotalDays;
                int count = activityService.GetAll().Where(x => x.User.ID == report.ID).Count();
                double percent = 0;
                if (period > 0)
                {
                    percent = (count / period) * 100;
                }
                Report result = new Report
                {
                    Link = CreatePDF(student, gradeAVG, gradeTaskAVG, percent, report.Templates)
                };
                var oldReport = student.Report;
                student.Report = result;
                userService.Edit(student);
                if(oldReport != null && oldReport.ID != 0)
                {
                    reportService.Delete(oldReport);
                }
                foreach (var item in reportQAService.GetAll().Where(x => x.ReportID == report.ReportID).ToList())
                {
                    reportQAService.Delete(item);
                }
                foreach (var item in report.Templates)
                {
                    reportQAService.Create(new ReportQA
                    {
                        Description = item.Value,
                        ReportID = result.ID,
                        TemplateID = templateService.GetAll().Where(x => x.Description.Equals(item.Key)).FirstOrDefault().ID
                    });
                }
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(userService.GetAll().Where(x => x.Role.Value.Equals("Студент")), "ID", "Surname");
            return View(report);
        }


        // GET: Reports/CreateGroup
        public ActionResult CreateGroup()
        {
            ViewBag.ID = new SelectList(groupService.GetAll(), "ID", "Description");
            return View();
        }

        // POST: Reports/CreateGroup
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGroup(ReportGroupViewModel report)
        {
            if (ModelState.IsValid)
            {
                string path = System.Web.HttpContext.Current.Server.MapPath("~/Content/Uploads/");
                if (System.IO.File.Exists(path + report.ReportLink))
                {
                    System.IO.File.Delete(path + report.ReportLink);
                }

                var group = groupService.Get(report.ID);

                List<ReportStudents> students = new List<ReportStudents>();
                foreach(var student in group.Students)
                {
                    List<Grade> grades = gradeService.GetAll().Where(x => x.UserID == student.ID).ToList();
                    List<Test> tests = new List<Test>();
                    foreach (var section in groupSectionService.GetAll().Where(x => x.GroupID == student.GroupID))
                    {
                        tests.AddRange(testService.GetAll().Where(x => x.SectionID == section.SectionID));
                    }
                    double gradeAVG = grades.Select(y => y.Value).Sum() / tests.Count;
                    List<UserTask> taskGrades = userTaskService.GetAll().Where(x => x.UserID == student.ID).ToList();
                    List<Task> tasks = new List<Task>();
                    foreach (var section in groupSectionService.GetAll().Where(x => x.GroupID == student.GroupID))
                    {
                        tasks.AddRange(taskService.GetAll().Where(x => x.SectionID == section.SectionID));
                    }
                    double gradeTaskAVG = taskGrades.Select(y =>
                    {
                        switch (y.Grade)
                        {
                            case "Отлично":
                                return 1;
                            case "Хорошо":
                                return 0.75;
                            case "Удовлетворительно":
                                return 0.5;
                            case "Неудовлетворительно":
                                return 0.25;
                            default:
                                return 0;
                        }
                    }).Sum() / tasks.Count;

                    students.Add(new ReportStudents
                    {
                        Student = student,
                        TestAVG = gradeAVG,
                        TaskAVG = gradeTaskAVG,
                        SUM = (gradeAVG + gradeTaskAVG) / 2
                    });
                }

                Report result = new Report
                {
                    Link = CreatePDF(group, students.OrderByDescending(x => x.SUM).ToList())
                };
                var oldReport = group.Report;
                group.Report = result;
                groupService.Edit(group);
                if (oldReport != null && oldReport.ID != 0)
                {
                    reportService.Delete(oldReport);
                }
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(groupService.GetAll(), "ID", "Description");
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
            Dictionary<string, string> templates = new Dictionary<string, string>();
            foreach (var item in templateService.GetAll())
            {
                templates.Add(item.Description, "");
            }
            ReportTemplateViewModel model = new ReportTemplateViewModel
            {
                ID = report.User.ID,
                ReportID = report.ID,
                ReportLink = report.Link,
                Templates = templates
            };
            ViewBag.ID = new SelectList(userService.GetAll().Where(x => x.Role.Value.Equals("Студент")), "ID", "Surname");
            return View(model);
        }

        // POST: Reports/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ReportTemplateViewModel report)
        {
            if (ModelState.IsValid)
            {
                string path = System.Web.HttpContext.Current.Server.MapPath("~/Content/Uploads/");
                if (System.IO.File.Exists(path + report.ReportLink))
                {
                    System.IO.File.Delete(path + report.ReportLink);
                }
                var student = userService.Get(report.ID);
                //TEST GRADES
                List<Grade> grades = gradeService.GetAll().Where(x => x.UserID == report.ID).ToList();
                List<Test> tests = new List<Test>();
                foreach (var section in groupSectionService.GetAll().Where(x => x.GroupID == student.GroupID))
                {
                    tests.AddRange(testService.GetAll().Where(x => x.SectionID == section.SectionID));
                }
                double gradeAVG = grades.Select(y => y.Value).Sum() / tests.Count;
                //TASK GRADES
                List<UserTask> taskGrades = userTaskService.GetAll().Where(x => x.UserID == report.ID).ToList();
                List<Task> tasks = new List<Task>();
                foreach (var section in groupSectionService.GetAll().Where(x => x.GroupID == student.GroupID))
                {
                    tasks.AddRange(taskService.GetAll().Where(x => x.SectionID == section.SectionID));
                }
                double gradeTaskAVG = taskGrades.Select(y =>
                {
                    switch (y.Grade)
                    {
                        case "Отлично":
                            return 5;
                        case "Хорошо":
                            return 4;
                        case "Удовлетворительно":
                            return 3;
                        case "Неудовлетворительно":
                            return 2;
                        default:
                            return 0;
                    }
                }).Sum() / tasks.Count;

                double period = (student.Group.End - student.Group.Start).TotalDays;
                int count = activityService.GetAll().Where(x => x.User.ID == report.ID).Count();
                double percent = 0;
                if (period > 0)
                {
                    percent = (count / period) * 100;
                }
                Report result = new Report
                {
                    ID = report.ReportID,
                    Link = CreatePDF(student, gradeAVG, gradeTaskAVG, percent, report.Templates)
                };
                student.Report = result;
                userService.Edit(student);
                reportService.Delete(reportService.Get(report.ReportID));
                foreach (var item in reportQAService.GetAll().Where(x => x.ReportID == report.ReportID).ToList())
                {
                    reportQAService.Delete(item);
                }
                foreach (var item in report.Templates)
                {
                    reportQAService.Create(new ReportQA
                    {
                        Description = item.Value,
                        ReportID = result.ID,
                        TemplateID = templateService.GetAll().Where(x => x.Description.Equals(item.Key)).FirstOrDefault().ID
                    });
                }
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(userService.GetAll().Where(x => x.Role.Value.Equals("Студент")), "ID", "Surname");
            return View(report);
        }

        // GET: Reports/Edit/5
        public ActionResult EditGroup(int? id)
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
            ReportGroupViewModel model = new ReportGroupViewModel
            {
                ID = report.Group.ID,
                ReportID = report.ID,
                ReportLink = report.Link
            };
            ViewBag.ID = new SelectList(groupService.GetAll(), "ID", "Description");
            return View(model);
        }

        // POST: Reports/EditGroup/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditGroup(ReportGroupViewModel report)
        {
            if (ModelState.IsValid)
            {
                string path = System.Web.HttpContext.Current.Server.MapPath("~/Content/Uploads/");
                if (System.IO.File.Exists(path + report.ReportLink))
                {
                    System.IO.File.Delete(path + report.ReportLink);
                }

                var group = groupService.Get(report.ID);
                List<ReportStudents> students = new List<ReportStudents>();
                foreach (var student in group.Students)
                {
                    List<Grade> grades = gradeService.GetAll().Where(x => x.UserID == student.ID).ToList();
                    List<Test> tests = new List<Test>();
                    foreach (var section in groupSectionService.GetAll().Where(x => x.GroupID == student.GroupID))
                    {
                        tests.AddRange(testService.GetAll().Where(x => x.SectionID == section.SectionID));
                    }
                    double gradeAVG = grades.Select(y => y.Value).Sum() / tests.Count;
                    List<UserTask> taskGrades = userTaskService.GetAll().Where(x => x.UserID == student.ID).ToList();
                    List<Task> tasks = new List<Task>();
                    foreach (var section in groupSectionService.GetAll().Where(x => x.GroupID == student.GroupID))
                    {
                        tasks.AddRange(taskService.GetAll().Where(x => x.SectionID == section.SectionID));
                    }
                    double gradeTaskAVG = taskGrades.Select(y =>
                    {
                        switch (y.Grade)
                        {
                            case "Отлично":
                                return 1;
                            case "Хорошо":
                                return 0.75;
                            case "Удовлетворительно":
                                return 0.5;
                            case "Неудовлетворительно":
                                return 0.25;
                            default:
                                return 0;
                        }
                    }).Sum() / tasks.Count;

                    students.Add(new ReportStudents
                    {
                        Student = student,
                        TestAVG = gradeAVG,
                        TaskAVG = gradeTaskAVG,
                        SUM = (gradeAVG + gradeTaskAVG) / 2
                    });
                }

                Report result = new Report
                {
                    Link = CreatePDF(group, students.OrderByDescending(x => x.SUM).ToList())
                };
                var oldReport = group.Report;
                group.Report = result;
                groupService.Edit(group);
                if (oldReport != null && oldReport.ID != 0)
                {
                    reportService.Delete(oldReport);
                }
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(groupService.GetAll(), "ID", "Description");
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
                templateService.Dispose();
                gradeService.Dispose();
                activityService.Dispose();
                groupService.Dispose();
                reportQAService.Dispose();
                testService.Dispose();
                sectionService.Dispose();
                groupSectionService.Dispose();
                taskService.Dispose();
                userTaskService.Dispose();
            }
            base.Dispose(disposing);
        }

        private string CreatePDF(User student, double grade, double gradeTask, double percent, Dictionary<string, string> templates)
        {
            var document = new Document(PageSize.A4, 15, 15 , 20, 15);
            string filename = System.Guid.NewGuid().ToString() + ".pdf";
            string path = System.Web.HttpContext.Current.Server.MapPath("~/Content/Uploads/") + filename;
            BaseFont font = BaseFont.CreateFont(System.Web.HttpContext.Current.Server.MapPath("~/fonts/times-new-roman.ttf"),
                BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            var fontBase = new iTextSharp.text.Font(font, 14, iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black));
            var fontHeader = new iTextSharp.text.Font(font, 14, iTextSharp.text.Font.BOLD, new BaseColor(Color.Black));


            PdfWriter.GetInstance(document, new FileStream(path, FileMode.Create));
            document.Open();
            
            document.Add(new Paragraph(new Phrase($"Отчет по практике с {student.Group.Start.ToShortDateString()} по {student.Group.End.ToShortDateString()}", fontBase))
            {
                Alignment = Element.ALIGN_CENTER
            });
            document.Add(new Paragraph(5, "\u00a0"));
            document.Add(new Paragraph(new Phrase($"Руководитель {student.CurrentCurator.User.Surname} " +
                $"{student.CurrentCurator.User.Name} {student.CurrentCurator.User.Midname}", fontBase))
            {
                Alignment = Element.ALIGN_RIGHT,
            });
            document.Add(new Paragraph(new Phrase($"{student.CurrentCurator.User.Role.Value}", fontBase))
            {
                Alignment = Element.ALIGN_RIGHT,
            });
            document.Add(new Paragraph(new Phrase($"Студент {student.Surname} {student.Name} {student.Midname}",
                fontBase))
            {
                Alignment = Element.ALIGN_RIGHT,
            });
            document.Add(new Paragraph(new Phrase($"группы {student.Group.Description}",
                fontBase))
            {
                Alignment = Element.ALIGN_RIGHT,
            });
            PdfPTable table = new PdfPTable(2)
            {             
                WidthPercentage = 90,
            };
            table.DefaultCell.BorderWidth = 0;
            table.DefaultCell.HasBorder(iTextSharp.text.Rectangle.NO_BORDER);
            PdfPCell cellHeader1 = new PdfPCell(new Phrase("Критерий", fontHeader))
            {
                Padding = 2,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                BorderColor = BaseColor.WHITE,
                BorderWidth = 0
            };
            table.AddCell(cellHeader1);
            PdfPCell cellHeader2 = new PdfPCell(new Phrase("Отзыв", fontHeader))
            {
                Padding = 2,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                BorderColor = BaseColor.WHITE,
                BorderWidth = 0
            };
            table.AddCell(cellHeader2);
            foreach (var template in templates)
            {
                PdfPCell cellTemplate = new PdfPCell(new Phrase(template.Key, fontBase))
                {
                    Padding = 10,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    BorderColor = BaseColor.WHITE,
                    BorderWidth = 0
                };
                table.AddCell(cellTemplate);
                PdfPCell cellAnswer = new PdfPCell(new Phrase(template.Value, fontBase))
                {
                    Padding = 10,
                    HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    BorderColor = BaseColor.WHITE,
                    BorderWidth = 0
                };
                table.AddCell(cellAnswer);
            }
            //ACTIVITY PERCENT
            PdfPCell cellActivity = new PdfPCell(new Phrase($"Посещаемость студента составила", fontBase))
            {
                Padding = 10,
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                BorderColor = BaseColor.WHITE,
                BorderWidth = 0
            };
            table.AddCell(cellActivity);
            PdfPCell cellActivityBase = new PdfPCell(new Phrase($"{(int)percent}%", fontBase))
            {
                Padding = 10,
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                BorderColor = BaseColor.WHITE,
                BorderWidth = 0
            };
            table.AddCell(cellActivityBase);
            //AVG GRADE TEST
            PdfPCell cellGrade = new PdfPCell(new Phrase("Средняя оценка по тестированию составяет", fontBase))
            {
                Padding = 10,
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                BorderColor = BaseColor.WHITE,
                BorderWidth = 0
            };
            table.AddCell(cellGrade);
            int coef = 5;
            PdfPCell cellGradeBase = new PdfPCell(new Phrase($"{grade * coef}", fontBase))
            {
                Padding = 10,
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                BorderColor = BaseColor.WHITE,
                BorderWidth = 0
            };
            table.AddCell(cellGradeBase);
            //AVG GRADE TASK
            PdfPCell cellGradeTask = new PdfPCell(new Phrase("Средняя оценка по заданиям составяет", fontBase))
            {
                Padding = 10,
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                BorderColor = BaseColor.WHITE,
                BorderWidth = 0
            };
            table.AddCell(cellGradeTask);
            PdfPCell cellGradeTaskBase = new PdfPCell(new Phrase($"{gradeTask * coef}", fontBase))
            {
                Padding = 10,
                HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                BorderColor = BaseColor.WHITE,
                BorderWidth = 0
            };
            table.AddCell(cellGradeTaskBase);
            document.Add(new Paragraph(70, "\u00a0"));
            document.Add(table);
            document.Close();
            return filename;
        }

        private string CreatePDF(Group group, List<ReportStudents> students)
        {
            var document = new Document(PageSize.A4, 15, 15, 20, 15);
            string filename = System.Guid.NewGuid().ToString() + ".pdf";
            string path = System.Web.HttpContext.Current.Server.MapPath("~/Content/Uploads/") + filename;
            BaseFont font = BaseFont.CreateFont(System.Web.HttpContext.Current.Server.MapPath("~/fonts/times-new-roman.ttf"),
                BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            var fontBase = new iTextSharp.text.Font(font, 14, iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black));
            var fontHeader = new iTextSharp.text.Font(font, 14, iTextSharp.text.Font.BOLD, new BaseColor(Color.Black));


            PdfWriter.GetInstance(document, new FileStream(path, FileMode.Create));
            document.Open();

            document.Add(new Paragraph(new Phrase($"Отчет по практике группы {group.Description}", fontBase))
            {
                Alignment = Element.ALIGN_CENTER
            });
            document.Add(new Paragraph(new Phrase($"проходящей с {group.Start.ToShortDateString()} по {group.End.ToShortDateString()}", fontBase))
            {
                Alignment = Element.ALIGN_CENTER
            });
            document.Add(new Paragraph(5, "\u00a0"));
            PdfPTable table = new PdfPTable(6)
            {
                WidthPercentage = 90,
            };
            table.DefaultCell.BorderWidth = 0;
            table.DefaultCell.HasBorder(iTextSharp.text.Rectangle.NO_BORDER);
            PdfPCell cellHeader1 = new PdfPCell(new Phrase("Фамилия", fontHeader))
            {
                Padding = 2,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                BorderColor = BaseColor.WHITE,
                BorderWidth = 0
            };
            table.AddCell(cellHeader1);
            PdfPCell cellHeader2 = new PdfPCell(new Phrase("Имя", fontHeader))
            {
                Padding = 2,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                BorderColor = BaseColor.WHITE,
                BorderWidth = 0
            };
            table.AddCell(cellHeader2);
            PdfPCell cellHeader3 = new PdfPCell(new Phrase("Отчество", fontHeader))
            {
                Padding = 2,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                BorderColor = BaseColor.WHITE,
                BorderWidth = 0
            };
            table.AddCell(cellHeader3);
            PdfPCell cellHeader4 = new PdfPCell(new Phrase("Оценка по тестам", fontHeader))
            {
                Padding = 2,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                BorderColor = BaseColor.WHITE,
                BorderWidth = 0
            };
            table.AddCell(cellHeader4);
            PdfPCell cellHeader5 = new PdfPCell(new Phrase("Оценка по заданиям", fontHeader))
            {
                Padding = 2,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                BorderColor = BaseColor.WHITE,
                BorderWidth = 0
            };
            table.AddCell(cellHeader5);
            PdfPCell cellHeader6 = new PdfPCell(new Phrase("Общая оценка", fontHeader))
            {
                Padding = 2,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                BorderColor = BaseColor.WHITE,
                BorderWidth = 0
            };
            table.AddCell(cellHeader6);
            int coef = 5;
            foreach (var student in students)
            {
                PdfPCell cellSurname = new PdfPCell(new Phrase(student.Student.Surname, fontBase))
                {
                    Padding = 10,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    BorderColor = BaseColor.WHITE,
                    BorderWidth = 0
                };
                table.AddCell(cellSurname);
                PdfPCell cellName = new PdfPCell(new Phrase(student.Student.Name, fontBase))
                {
                    Padding = 10,
                    HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    BorderColor = BaseColor.WHITE,
                    BorderWidth = 0
                };
                table.AddCell(cellName);
                PdfPCell cellMidname = new PdfPCell(new Phrase(student.Student.Midname, fontBase))
                {
                    Padding = 10,
                    HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    BorderColor = BaseColor.WHITE,
                    BorderWidth = 0
                };
                table.AddCell(cellMidname);
                PdfPCell cellGradeTest = new PdfPCell(new Phrase(string.Format("{0:0.##}", student.TestAVG * coef), fontBase))
                {
                    Padding = 10,
                    HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    BorderColor = BaseColor.WHITE,
                    BorderWidth = 0
                };
                table.AddCell(cellGradeTest);
                PdfPCell cellGradeTask = new PdfPCell(new Phrase(string.Format("{0:0.##}", student.TaskAVG * coef), fontBase))
                {
                    Padding = 10,
                    HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    BorderColor = BaseColor.WHITE,
                    BorderWidth = 0
                };
                table.AddCell(cellGradeTask);
                PdfPCell cellGradeSum = new PdfPCell(new Phrase(string.Format("{0:0.##}", student.SUM * coef), fontBase))
                {
                    Padding = 10,
                    HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    BorderColor = BaseColor.WHITE,
                    BorderWidth = 0
                };
                table.AddCell(cellGradeSum);
            }
            document.Add(new Paragraph(70, "\u00a0"));
            document.Add(table);
            document.Close();
            return filename;
        }
    }
}