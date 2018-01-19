namespace Core.DataAccess.Tests
{
    using System;
    using System.Reflection;
    using Autofac;
    using Contracts;
    using Core.DataAccess.Contracts.SessionToken;
    using Core.DataAccess.Installers;

    public class AutofacContainerTestWrapper : IDisposable
    {
        private readonly IContainer _container;

        public AutofacContainerTestWrapper()
        {
            var builder = new ContainerBuilder();

            var assembly = Assembly.GetAssembly(GetType());
            builder.RegisterModule(new DatabaseInstallerModule(assembly, @"Server=.;initial catalog=DBName;Integrated Security=True;", "DBName", typeof(ITestDbToken)));

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

    public interface ITestDbToken : IDbToken
    {
    }

    public interface IOtherTestDbToken : IDbToken
    {
    }

    public class TestCommandRequest : ICommandRequest
    {
        public string RequestString { get; set; }
    }

    public class TestCommand : IDataOperation<TestCommandRequest>, ITestDbToken
    {
        public void Execute(TestCommandRequest request)
        {
            throw new Exception(request.RequestString);
        }
    }

    public class TestQueryResponse : IQueryResponse<TestQueryRequest>
    {
        public string ResponseString { get; set; }
    }

    public class TestQueryRequest : IQueryRequest<TestQueryResponse>
    {
        public string RequestString { get; set; }
    }

    public class TestQuery : IDataOperation<TestQueryRequest, TestQueryResponse>, ITestDbToken
    {
        public TestQueryResponse Execute(TestQueryRequest request)
        {
            return new TestQueryResponse { ResponseString = request.RequestString };
        }
    }
}
