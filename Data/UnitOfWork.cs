using Contracts;
using Core;
using Infrastructure.Data.Repository;
using Interfaces;
using System;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private Context Database;

        private bool disposed = false;

        private ActivityRepository ActivityRepository;

        private AnswerRepository AnswerRepository;

        private CourseRepository CourseRepository;

        private CuratorRepository CuratorRepository;

        private GradeRepository GradeRepository;

        private GroupRepository GroupRepository;

        private QuestionRepository QuestionRepository;

        private ReportRepository ReportRepository;

        private RoleRepository RoleRepository;

        private SectionRepository SectionRepository;

        private TemplateRepository TemplateRepository;

        private TestRepository TestRepository;

        private TypeRepository TypeRepository;

        private UserRepository UserRepository;

        public UnitOfWork(string connectionString)
        {
            Database = new Context(connectionString);
        }

        public IRepository<Activity> Activity
        {
            get
            {
                if (ActivityRepository == null)
                {
                    ActivityRepository = new ActivityRepository(Database);
                }
                return ActivityRepository;
            }
        }

        public IRepository<Answer> Answer
        {
            get
            {
                if (AnswerRepository == null)
                {
                    AnswerRepository = new AnswerRepository(Database);
                }
                return AnswerRepository;
            }
        }

        public IRepository<Course> Course
        {
            get
            {
                if (CourseRepository == null)
                {
                    CourseRepository = new CourseRepository(Database);
                }
                return CourseRepository;
            }
        }

        public IRepository<Curator> Curator
        {
            get
            {
                if (CuratorRepository == null)
                {
                    CuratorRepository = new CuratorRepository(Database);
                }
                return CuratorRepository;
            }
        }

        public IRepository<Grade> Grade
        {
            get
            {
                if (GradeRepository == null)
                {
                    GradeRepository = new GradeRepository(Database);
                }
                return GradeRepository;
            }
        }

        public IRepository<Group> Group
        {
            get
            {
                if (GroupRepository == null)
                {
                    GroupRepository = new GroupRepository(Database);
                }
                return GroupRepository;
            }
        }

        public IRepository<Question> Question
        {
            get
            {
                if (QuestionRepository == null)
                {
                    QuestionRepository = new QuestionRepository(Database);
                }
                return QuestionRepository;
            }
        }

        public IRepository<Report> Report
        {
            get
            {
                if (ReportRepository == null)
                {
                    ReportRepository = new ReportRepository(Database);
                }
                return ReportRepository;
            }
        }

        public IRepository<Role> Role
        {
            get
            {
                if (RoleRepository == null)
                {
                    RoleRepository = new RoleRepository(Database);
                }
                return RoleRepository;
            }
        }

        public IRepository<Section> Section
        {
            get
            {
                if (SectionRepository == null)
                {
                    SectionRepository = new SectionRepository(Database);
                }
                return SectionRepository;
            }
        }

        public IRepository<Template> QuTemplateestion
        {
            get
            {
                if (TemplateRepository == null)
                {
                    TemplateRepository = new TemplateRepository(Database);
                }
                return TemplateRepository;
            }
        }

        public IRepository<Test> Test
        {
            get
            {
                if (TestRepository == null)
                {
                    TestRepository = new TestRepository(Database);
                }
                return TestRepository;
            }
        }

        public IRepository<Core.Type> Type
        {
            get
            {
                if (TypeRepository == null)
                {
                    TypeRepository = new TypeRepository(Database);
                }
                return TypeRepository;
            }
        }

        public IRepository<User> User
        {
            get
            {
                if (UserRepository == null)
                {
                    UserRepository = new UserRepository(Database);
                }
                return UserRepository;
            }
        }

        public void Save()
        {
            Database.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Database.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}