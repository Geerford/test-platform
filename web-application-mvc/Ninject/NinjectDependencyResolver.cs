using Application.Interfaces;
using Ninject;
using Services.Business.Service;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace web_application_mvc.Ninject
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<IActivityService>().To<ActivityService>();
            kernel.Bind<IAnswerService>().To<AnswerService>();
            kernel.Bind<ICourseService>().To<CourseService>();
            kernel.Bind<ICuratorService>().To<CuratorService>();
            kernel.Bind<IGradeService>().To<GradeService>();
            kernel.Bind<IGroupService>().To<GroupService>();
            kernel.Bind<IQuestionService>().To<QuestionService>();
            kernel.Bind<IReportService>().To<ReportService>();
            kernel.Bind<IRoleService>().To<RoleService>();
            kernel.Bind<ISectionService>().To<SectionService>();
            kernel.Bind<ITemplateService>().To<TemplateService>();
            kernel.Bind<ITestService>().To<TestService>();
            kernel.Bind<ITypeService>().To<TypeService>();
            kernel.Bind<IUserService>().To<UserService>();
        }
    }
}