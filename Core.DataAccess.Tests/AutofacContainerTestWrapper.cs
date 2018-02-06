namespace Core.DataAccess.Tests
{
    using System;
    using System.Reflection;
    using Autofac;
    using Core.DataAccess.Installers;
    using TestImplementation;

    public class AutofacContainerTestWrapper : IDisposable
    {
        private readonly IContainer _container;

        public AutofacContainerTestWrapper()
        {
            var builder = new ContainerBuilder();

            var assembly = Assembly.GetAssembly(GetType());

            builder.RegisterModule(new DatabaseInstallerModule<DbContextTestImplementation, ITestDbToken>(assembly, @"Server=.;initial catalog=DBName;Integrated Security=True;"));

            _container = builder.Build();
        }

        protected TEntity Resolve<TEntity>()
        {
            return _container.Resolve<TEntity>();
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}
