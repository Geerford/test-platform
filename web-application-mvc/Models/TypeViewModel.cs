using Core;
using System.Collections.Generic;

namespace web_application_mvc.Models
{
    public class TypeViewModel
    {
        public Type Type { get; set; }

        public IEnumerable<Question> Questions { get; set; }
    }
}