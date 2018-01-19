namespace Core.DataAccess.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Autofac;
    using Contracts;
    using Core.DataAccess.Contracts.SessionToken;
    using Core.DataAccess.Installers;
    using Microsoft.EntityFrameworkCore;

    public class AutofacContainerTestWrapper : IDisposable
    {
        private readonly IContainer _container;

        public AutofacContainerTestWrapper()
        {
            var builder = new ContainerBuilder();

            var assembly = Assembly.GetAssembly(GetType());
            var entityList = new List<DbSet<IDbSet>>
            {
                new DatabaseEntity()
            };

            builder.RegisterModule(new DatabaseInstallerModule(assembly, @"Server=.;initial catalog=DBName;Integrated Security=True;", typeof(ITestDbContext), entityList));

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

    //public class TestQuery : IDataOperation<TestQueryRequest, TestQueryResponse>, ITestDbToken
    //{
    //    public TestQueryResponse Execute(TestQueryRequest request)
    //    {
    //        return new TestQueryResponse { ResponseString = request.RequestString };
    //    }
    //}

    public class EfTestQuery : IDataOperation<TestQueryRequest, TestQueryResponse>
    {
        private readonly ITestDbContext _dbContext;

        public EfTestQuery(ITestDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TestQueryResponse Execute(TestQueryRequest request)
        {
            var response = _dbContext.DatabaseEntities.FirstOrDefault(x => x.GetType() == typeof(DatabaseEntity));
            return new TestQueryResponse { ResponseString = request.RequestString };
        }
    }

    public class DatabaseEntity : DbSet<IDbSet>
    {
        public string Property => "ThisProperty";
    }

    public interface ITestDbContext : IDbContext
    {
    }
}
