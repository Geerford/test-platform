using Contracts;
using Core;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.Data.Repository
{
    public class GroupSectionRepository : IMMRepository<GroupSection>
    {
        private Context Database;

        public GroupSectionRepository(Context context)
        {
            Database = context;
        }
        public void Create(GroupSection item)
        {
            Database.GroupSection.Add(item);
        }

        public void Delete(int groupID, int sectionID)
        {
            GroupSection item = Database.GroupSection.Where(x => x.GroupID == groupID && x.SectionID == sectionID)
                .FirstOrDefault();
            if (item != null)
            {
                Database.GroupSection.Remove(item);
            }
        }

        public GroupSection Get(int groupID, int sectionID)
        {
            return Database.GroupSection.Where(x => x.GroupID == groupID && x.SectionID == sectionID)
                                 .FirstOrDefault();
        }

        public IEnumerable<GroupSection> GetAll()
        {
            return Database.GroupSection;
        }

        public void Update(GroupSection item)
        {
            Database.Entry(item).State = EntityState.Modified;
        }
    }
}