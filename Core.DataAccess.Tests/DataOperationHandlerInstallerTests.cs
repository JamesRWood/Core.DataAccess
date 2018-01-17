namespace Core.DataAccess.Tests
{
    using Core.DataAccess.Contracts;
    using Core.DataAccess.Contracts.SessionToken;
    using Core.DataAccess.Installers;
    using System;
    using Xunit;

    public class DataOperationHandlerInstallerTests : AutofacContainerTestWrapper<DataOperationHandlerInstaller>
    {
        [Fact]
        public void CommandRegistration()
        {
            var dataOperationHandler = Resolve<ICommandOperationHandler<TestCommandRequest>>();

            var exception = Assert.Throws<Exception>(() => dataOperationHandler.ExecuteCommand(new TestCommandRequest()));
            Assert.Equal("TestCommand Execute method resolved", exception.Message);

            ShutdownIoC();
        }

        [Fact]
        public void QueryRegistration()
        {
            var dataOperationHandler = Resolve<IQueryOperationHandler<TestQueryRequest, TestQueryResponse>>();

            var exception = Assert.Throws<Exception>(() => dataOperationHandler.ExecuteQuery(new TestQueryRequest()));
            Assert.Equal("TestQuery Execute method resolved", exception.Message);

            ShutdownIoC();
        }
    }

    public interface ITestDBToken : IDBToken
    {
    }

    public class TestCommandRequest : ICommandRequest
    {
    }

    public class TestCommand : IDataOperation<TestCommandRequest>, ITestDBToken
    {
        public void Execute(TestCommandRequest request)
        {
            throw new Exception("TestCommand Execute method resolved");
        }
    }

    public class TestQueryResponse : IResponse<TestQueryRequest>
    {
    }

    public class TestQueryRequest : IQueryRequest<TestQueryResponse>
    {
    }

    public class TestQuery : IDataOperation<TestQueryRequest, TestQueryResponse>, ITestDBToken
    {
        public TestQueryResponse Execute(TestQueryRequest request)
        {
            throw new Exception("TestQuery Execute method resolved");
        }
    }
}
