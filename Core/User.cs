using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Midname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        public bool Status { get; set; } // IsActive

        public virtual Curator Curator { get; set; }
        
        public int RoleID { get; set; }

        public Role Role { get; set; }

        public virtual Report Report { get; set; }

        public virtual int? CurrentCuratorID { get; set; }

        public virtual Curator CurrentCurator { get; set; }

        public virtual int? GroupID { get; set; }

        public virtual Group Group { get; set; }

        public ICollection<Activity> Activities { get; set; }

        public ICollection<Grade> Grades { get; set; }
    }
}