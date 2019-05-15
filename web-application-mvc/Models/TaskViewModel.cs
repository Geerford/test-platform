using Core;
using System.Collections.Generic;
using System.Web.Mvc;

namespace web_application_mvc.Models
{
    public class StudentTaskViewModel
    {
        public int ID { get; set; }

        public string Task { get; set; }

        public string Answer { get; set; }
    }

    public class CuratorTaskViewModel
    {
        public int ID { get; set; }

        public string Task { get; set; }

        public string Answer { get; set; }

        public string Grade { get; set; }

        public string Comment { get; set; }
    }

    public class StudentTaskViewModelPartial
    {
        public int ID { get; set; }

        public Task Task { get; set; }

        public string Answer { get; set; }

        public string Grade { get; set; }

        public string Comment { get; set; }
    }

    public class ChooseTaskViewModel
    {
        public int ID { get; set; }

        public List<SelectListItem> DropList { get; set; }
    }

    public class AdminTaskViewModel
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Section Section { get; set; }

        public IEnumerable<AdminInnerTaskModel> Students { get; set; }
    }

    public class AdminInnerTaskModel
    {
        public User User { get; set; }

        public string Answer { get; set; }

        public string Grade { get; set; }

        public string Comment { get; set; }
    }
}