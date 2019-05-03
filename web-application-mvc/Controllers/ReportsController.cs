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

        public ReportsController(IReportService reportService, IUserService userService, ITemplateService templateService,
            IGradeService gradeService, IActivityService activityService, IGroupService groupService, IReportQAService reportQAService)
        {
            this.reportService = reportService;
            this.userService = userService;
            this.templateService = templateService;
            this.gradeService = gradeService;
            this.activityService = activityService;
            this.groupService = groupService;
            this.reportQAService = reportQAService;
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
                List<Grade> grades = gradeService.GetAll().Where(x => x.UserID == report.ID).ToList();
                double gradeAVG = grades.Select(y => y.Value).Sum() / grades.Count;
                var student = userService.Get(report.ID);
                double period = (student.Group.End - student.Group.Start).TotalDays;
                int count = activityService.GetAll().Where(x => x.User.ID == report.ID).Count();
                double percent = 0;
                if (period > 0)
                {
                    percent = (count / period) * 100;
                }
                Report result = new Report
                {
                    Link = CreatePDF(student, gradeAVG, percent, report.Templates)
                };
                student.Report = result;
                userService.Edit(student);
                if(report.ReportID != 0)
                {
                    reportService.Delete(reportService.Get(report.ReportID));
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
                List<Grade> grades = gradeService.GetAll().Where(x => x.UserID == report.ID).ToList();
                double gradeAVG = grades.Select(y => y.Value).Sum() / grades.Count;
                var student = userService.Get(report.ID);
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
                    Link = CreatePDF(student, gradeAVG, percent, report.Templates)
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

        private string CreatePDF(User student, double grade, double percent, Dictionary<string, string> templates)
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
            document.Add(new Paragraph(new Phrase($"группы {student.Group.Description} {student.Name} {student.Midname}",
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
            //AVG GRADE
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
            document.Add(new Paragraph(70, "\u00a0"));
            document.Add(table);
            document.Close();
            return filename;
        }
    }
}