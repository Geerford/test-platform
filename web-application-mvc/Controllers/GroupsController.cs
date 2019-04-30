using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Application.Interfaces;
using Core;
using Infrastructure.Data;

namespace web_application_mvc.Controllers
{
    public class GroupsController : Controller
    {
        IGroupService groupService;
        IUserService userService;
        ISectionService sectionService;
        IGroupSectionService groupSectionService;

        public GroupsController(IGroupService groupService, IUserService userService,
            ISectionService sectionService, IGroupSectionService groupSectionService)
        {
            this.groupService = groupService;
            this.userService = userService;
            this.sectionService = sectionService;
            this.groupSectionService = groupSectionService;
        }

        // GET: Groups
        public ActionResult Index()
        {
            return View(groupService.GetAll());
        }

        // GET: Groups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = groupService.Get(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            ViewBag.Students = group.Students;
            List<Section> sections = new List<Section>();
            foreach(var item in groupSectionService.GetAll().Where(x => x.GroupID == group.ID))
            {
                sections.Add(sectionService.Get(item.SectionID));
            }
            ViewBag.Sections = sections;
            return View(group);
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            ViewBag.Students = userService.GetAll().Where(x => x.GroupID == null).ToList();
            ViewBag.Sections = sectionService.GetAll().ToList();
            return View();
        }

        // POST: Groups/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection formval, [Bind(Include = "ID,Description,University,Department,Start,End")] Group group)
        {
            if (group.Start > group.End)
            {
                ModelState.AddModelError("End", "Начальная дата должна быть меньше конечной");
            }
            if (ModelState.IsValid)
            {
                List<string> students = new List<string>(), sections = new List<string>();
                if (formval["students"] != null)
                {
                    students = formval["students"].Split(',').ToList();
                }
                if (formval["sections"] != null)
                {
                    sections = formval["sections"].Split(',').ToList();
                }
                group.Students = new List<User>();
                if (students.Count > 0 || sections.Count > 0)
                {
                    if(students.Count > 0)
                    {
                        foreach (var studentID in students)
                        {
                            User user = userService.Get(int.Parse(studentID));
                            user.Group = group;
                            userService.Edit(user);
                        }
                    }
                    if(sections.Count > 0)
                    {
                        foreach (var sectionID in sections)
                        {
                            groupSectionService.Create(new GroupSection
                            {
                                GroupID = group.ID,
                                SectionID = int.Parse(sectionID)
                            });
                        }
                    }
                }
                else
                {
                    groupService.Create(group);
                }
                return RedirectToAction("Index");
            }
            ViewBag.Students = userService.GetAll().Where(x => x.GroupID == null).ToList();
            ViewBag.Sections = sectionService.GetAll().ToList();
            return View(group);
        }

        // GET: Groups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = groupService.Get(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            ViewBag.Users = userService.GetAll().Where(x => x.GroupID == null).ToList();
            ViewBag.Students = group.Students;

            List<Section> sectionsSelected = new List<Section>(), sectionsNotSelected = sectionService.GetAll().ToList();
            foreach (var item in groupSectionService.GetAll().Where(x => x.GroupID == group.ID))
            {
                sectionsSelected.Add(sectionService.Get(item.SectionID));
                sectionsNotSelected.Remove(sectionsNotSelected.Where(x => x.ID == item.SectionID).FirstOrDefault());
            }
            /*
            foreach (var item in sectionService.GetAll())
            {
                for (int i = 0; i < notSelected.Count; i++)
                {
                    if (item.ID == i)
                    {
                        sectionsNotSelected.Add(item);
                        notSelected.Remove(notSelected[i]);
                    }
                }
            }*/
            ViewBag.Sections = sectionsSelected;
            ViewBag.SectionsNew = sectionsNotSelected;
            return View(group);
        }

        // POST: Groups/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection formval, [Bind(Include = "ID,Description,University,Department,Start,End")] Group group)
        {
            if (ModelState.IsValid)
            {
                List<string> students = new List<string>(), users = new List<string>(), 
                    sections = new List<string>(), sectionsnew = new List<string>();
                if (formval["students"] != null)
                {
                    students = formval["students"].Split(',').ToList();
                }
                if(formval["users"] != null)
                {
                    users = formval["users"].Split(',').ToList();
                }
                if (formval["sections"] != null)
                {
                    sections = formval["sections"].Split(',').ToList();
                }
                if (formval["sectionsnew"] != null)
                {
                    sections = formval["sectionsnew"].Split(',').ToList();
                }
                //Clear
                foreach (var item in userService.GetAll().Where(x => x.GroupID == group.ID).ToList())
                {
                    item.GroupID = null;
                    userService.Edit(item);
                }
                List<GroupSection> trytodelete = new List<GroupSection>();
                foreach (var item in groupSectionService.GetAll())
                {
                    if (item.GroupID == group.ID)
                    {
                        trytodelete.Add(item);
                    }
                }
                foreach(var item in trytodelete)
                {
                    groupSectionService.Delete(item);
                }

                if (students.Count > 0 || users.Count > 0 || sections.Count > 0 || sectionsnew.Count > 0)
                {
                    if (students.Count > 0)
                    {
                        foreach (var studentID in students)
                        {
                            User user = userService.Get(int.Parse(studentID));
                            user.GroupID = group.ID;
                            userService.Edit(user);
                        }
                    }
                    if (users.Count > 0)
                    {
                        foreach (var studentID in users)
                        {
                            User user = userService.Get(int.Parse(studentID));
                            user.GroupID = group.ID;
                            userService.Edit(user);
                        }
                    }
                    if (sections.Count > 0)
                    {
                        foreach (var sectionID in sections)
                        {
                            groupSectionService.Create(new GroupSection
                            {
                                GroupID = group.ID,
                                SectionID = int.Parse(sectionID)
                            });
                        }
                    }
                }
                else
                {
                    groupService.Edit(group);
                }
                return RedirectToAction("Index");
            }
            ViewBag.Users = userService.GetAll().Where(x => x.GroupID == null).ToList();
            ViewBag.Students = group.Students;

            List<Section> sectionsSelected = new List<Section>(), sectionsNotSelected = sectionService.GetAll().ToList();
            foreach (var item in groupSectionService.GetAll().Where(x => x.GroupID == group.ID))
            {
                sectionsSelected.Add(sectionService.Get(item.SectionID));
                sectionsNotSelected.Remove(sectionsNotSelected.Where(x => x.ID == item.SectionID).FirstOrDefault());
            }
            /*
            foreach (var item in sectionService.GetAll())
            {
                for (int i = 0; i < notSelected.Count; i++)
                {
                    if (item.ID == i)
                    {
                        sectionsNotSelected.Add(item);
                        notSelected.Remove(notSelected[i]);
                    }
                }
            }*/
            ViewBag.Sections = sectionsSelected;
            ViewBag.SectionsNew = sectionsNotSelected;
            return View(group);
        }

        // GET: Groups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = groupService.Get(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Group group = groupService.Get(id);
            groupService.Delete(group);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                groupService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}