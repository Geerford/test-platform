using Application.Interfaces;
using Core;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace web_application_mvc.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        ICourseService courseService;
        IGroupSectionService groupSectionService;
        IUserService userService;
        ISectionService sectionService;

        public CourseController(IGroupSectionService groupSectionService, IUserService userService,
            ISectionService sectionService, ICourseService courseService)
        {
            this.groupSectionService = groupSectionService;
            this.userService = userService;
            this.sectionService = sectionService;
            this.courseService = courseService;
        }

        // GET: Course
        public ActionResult Index()
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
            List<Course> courses = new List<Course>();
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
            foreach(var item in sections)
            {
                courses.AddRange(item.Courses);
            }
            //foreach (var item in courseService.GetAll())
            //{
            //    for (int i = 0; i < sections.Count; i++)
            //    {
            //        if (item.SectionID == sections[i].ID)
            //        {
            //            courses.Add(item);
            //            sections.Remove(sections[i]);
            //        }
            //    }
            //}
            return View(courses);
        }
    }
}