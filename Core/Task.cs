namespace Core
{
    public class Task
    {
        public int ID { get; set; }

        public string Description { get; set; }

        public int SectionID { get; set; }

        public Section Section { get; set; }
    }
}