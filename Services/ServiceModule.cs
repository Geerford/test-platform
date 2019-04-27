using Infrastructure.Data;
using Interfaces;
using Ninject.Modules;

namespace Services.Business
{
    public class ServiceModule : NinjectModule
    {
        private readonly string connectionString;

        public ServiceModule(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument(connectionString);
        }
    }
}