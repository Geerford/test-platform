using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core
{
    public class GroupSection
    {
        [Key, Column(Order = 0)]
        public int GroupID { get; set; }

        [Key, Column(Order = 1)]
        public int SectionID { get; set; }
    }
}