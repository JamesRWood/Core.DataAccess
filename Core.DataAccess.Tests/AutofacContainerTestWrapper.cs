using Autofac;
using Autofac.Core;
using Core.DataAccess.Contracts.SessionToken;
using Core.DataAccess.Installers;
using System.Reflection;

namespace Core.DataAccess.Tests
{
    public class AutofacContainerTestWrapper
    {
        private readonly IContainer _container;

        public AutofacContainerTestWrapper()
        {
            var builder = new ContainerBuilder();

            var assembly = Assembly.GetAssembly(GetType());
            builder.RegisterModule(new DatabaseInstaller(assembly, @"Server=.;initial catalog=DBName;Integrated Security=True;", "DBName", typeof(ITestDBToken)));

            _container = builder.Build();
        }

        protected TEntity Resolve<TEntity>()
        {
            return _container.Resolve<TEntity>();
        }

        protected void ShutdownIoC()
        {
            _container.Dispose();
        }
    }
}
