namespace Core.DataAccess.Tests
{
    using Core.DataAccess.Contracts;
    using Core.DataAccess.Contracts.SessionToken;
    using System;
    using Xunit;

    public class DataOperationHandlerInstallerTests : AutofacContainerTestWrapper
    {
        private const string CommandRequestString = "TestCommand Execute method resolved";
        private const string QueryRequestString = "TestQuery Execute method resolved";

        [Fact]
        public void CommandRegistration()
        {
            var dataOperation = Resolve<IDataOperation<TestCommandRequest>>();

            var exception = Assert.Throws<Exception>(() => dataOperation.Execute(new TestCommandRequest { RequestString = CommandRequestString }));
            Assert.Equal(CommandRequestString, exception.Message);

            ShutdownIoC();
        }

        [Fact]
        public void EnsureOnlyCorrectVersionOfQueryIsResolved()
        {
            // Only expect the TestQuery which implements the ITestDBToken interface to be resolved
            // as the DatabaseInstaller has not been called with the IOtherTestDBToken interface type
            // IRL situation there would not be 2 queries using the same request and response objects

            var dataOperation = Resolve<IDataOperation<TestQueryRequest, TestQueryResponse>>();

            Assert.IsAssignableFrom<TestQuery>(dataOperation);
        }

        [Fact]
        public void QueryRegistration()
        {
            var dataOperation = Resolve<IDataOperation<TestQueryRequest, TestQueryResponse>>();

            var response = dataOperation.Execute(new TestQueryRequest { RequestString = QueryRequestString });
            Assert.Equal(QueryRequestString, response.ReponseString);

            ShutdownIoC();
        }
    }

    public interface ITestDBToken : IDBToken
    {
    }

    public interface IOtherTestDBToken : IDBToken
    {
    }

    public class TestCommandRequest : ICommandRequest
    {
        public string RequestString { get; set; }
    }

    public class TestCommand : IDataOperation<TestCommandRequest>, ITestDBToken
    {
        public void Execute(TestCommandRequest request)
        {
            throw new Exception(request.RequestString);
        }
    }

    public class TestQueryResponse : IQueryResponse
    {
        public string ReponseString { get; set; }
    }

    public class TestQueryRequest : IQueryRequest<IQueryResponse>
    {
        public string RequestString { get; set; }
    }

    public class TestQuery : IDataOperation<TestQueryRequest, TestQueryResponse>, ITestDBToken
    {
        public TestQueryResponse Execute(TestQueryRequest request)
        {
            return new TestQueryResponse { ReponseString = request.RequestString };
        }
    }

    public class OtherTestQuery : IDataOperation<TestQueryRequest, TestQueryResponse>, IOtherTestDBToken
    {
        public TestQueryResponse Execute(TestQueryRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
