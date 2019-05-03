using Core;
using System.Data.Entity;

namespace Infrastructure.Data
{
    public class Context : DbContext
    {
        public DbSet<Activity> Activity { get; set; }

        public DbSet<Answer> Answer { get; set; }

        public DbSet<Course> Course { get; set; }

        public DbSet<Curator> Curator { get; set; }

        public DbSet<Grade> Grade { get; set; }

        public DbSet<Group> Group { get; set; }

        public DbSet<Question> Question { get; set; }

        public DbSet<Report> Report { get; set; }

        public DbSet<Role> Role { get; set; }

        public DbSet<Section> Section { get; set; }

        public DbSet<Template> Template { get; set; }

        public DbSet<Test> Test { get; set; }

        public DbSet<Type> Type { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<GroupSection> GroupSection { get; set; }

        public DbSet<ReportQA> ReportQA { get; set; }

        static Context() => Database.SetInitializer(new Initializer());

        public Context() { }

        public Context(string connectionString) : base(connectionString) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Properties<System.DateTime>()
                        .Configure(c => c.HasColumnType("datetime2"));

            #region One to One
            modelBuilder.Entity<User>()
                        .HasOptional(u => u.Curator)
                        .WithOptionalPrincipal(c => c.User);
            
            modelBuilder.Entity<User>()
                        .HasOptional(u => u.Report)
                        .WithOptionalPrincipal(r => r.User);
            #endregion

            #region One to Many
            modelBuilder.Entity<User>()
                        .HasOptional(u => u.Role)
                        .WithMany(r => r.Users)
                        .HasForeignKey(u => u.RoleID);

            modelBuilder.Entity<User>()
                        .HasOptional(u => u.CurrentCurator)
                        .WithMany(c => c.Students)
                        .HasForeignKey(u => u.CurrentCuratorID);

            modelBuilder.Entity<User>()
                        .HasOptional(u => u.Group)
                        .WithMany(g => g.Students)
                        .HasForeignKey(u => u.GroupID);

            modelBuilder.Entity<Activity>()
                        .HasRequired(a => a.User)
                        .WithMany(u => u.Activities)
                        .HasForeignKey(a => a.UserID)
                        .WillCascadeOnDelete();

            modelBuilder.Entity<Grade>()
                        .HasRequired(g => g.User)
                        .WithMany(u => u.Grades)
                        .HasForeignKey(g => g.UserID)
                        .WillCascadeOnDelete();

            modelBuilder.Entity<Grade>()
                        .HasRequired(g => g.Test)
                        .WithMany(t => t.Grades)
                        .HasForeignKey(g => g.TestID);
            
            modelBuilder.Entity<Test>()
                        .HasRequired(t => t.Section)
                        .WithMany(s => s.Tests)
                        .HasForeignKey(t => t.SectionID);
            
            modelBuilder.Entity<Course>()
                        .HasRequired(c => c.Section)
                        .WithMany(s => s.Courses)
                        .HasForeignKey(c => c.SectionID);
            
            modelBuilder.Entity<Question>()
                        .HasRequired(q => q.Test)
                        .WithMany(t => t.Questions)
                        .HasForeignKey(q => q.TestID)
                        .WillCascadeOnDelete();
            
            modelBuilder.Entity<Answer>()
                        .HasRequired(a => a.Question)
                        .WithMany(q => q.Answers)
                        .HasForeignKey(a => a.QuestionID)
                        .WillCascadeOnDelete();

            modelBuilder.Entity<Question>()
                        .HasRequired(q => q.Type)
                        .WithMany(t => t.Questions)
                        .HasForeignKey(q => q.TypeID);
            #endregion
        }
    }
}