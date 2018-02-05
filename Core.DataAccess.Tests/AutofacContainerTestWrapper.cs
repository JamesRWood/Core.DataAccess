namespace Core.DataAccess.Tests
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;
    using Autofac;
    using Contracts;
    using Core.DataAccess.Contracts.SessionToken;
    using Core.DataAccess.Installers;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

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

    public interface ITestDbToken : IDbToken
    {
    }

    public interface IOtherTestDbToken : IDbToken
    {
    }

    public class TestDatabaseEntity
    {
        public string TestString => "Test string value";
    }

    public class TestCommandRequest : ICommandRequest
    {
        public string RequestString { get; set; }
    }

    public class TestCommand : IDataOperation<TestCommandRequest>
    {
        private IDbContextTestImplementation _dbContext;

        public TestCommand(IDbContextTestImplementation dbContext)
        {
            _dbContext = dbContext;
        }

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

    public class EfTestQuery : IDataOperation<TestQueryRequest, TestQueryResponse>
    {
        private readonly IDbContextTestImplementation _dbContext;

        public EfTestQuery(IDbContextTestImplementation dbContext)
        {
            _dbContext = dbContext;
        }

        public TestQueryResponse Execute(TestQueryRequest request)
        {
            var response = _dbContext.TestDatabaseEntity.ToList();
            return new TestQueryResponse { ResponseString = response.FirstOrDefault()?.TestString ?? "Nope" };
        }
    }
}
