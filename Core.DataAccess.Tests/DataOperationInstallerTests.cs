namespace Core.DataAccess.Tests
{
    using System;
    using Core.DataAccess.Contracts;
    using TestImplementation.TestCommand;
    using TestImplementation.TestQuery;
    using Xunit;

    public class DataOperationInstallerTests : AutofacContainerTestWrapper
    {
        private const string CommandRequestString = "TestCommand Execute method resolved";
        private const string QueryRequestString = "TestQuery Execute method resolved";

        [Fact]
        public void EnsureCommandResolvedAsExpected()
        {
            var dataOperation = Resolve<IDataOperation<TestCommandRequest>>();

            Assert.IsAssignableFrom<TestCommand>(dataOperation);
        }

        [Fact]
        public void CommandRegistration()
        {
            var dataOperation = Resolve<IDataOperation<TestCommandRequest>>();

            var exception = Assert.Throws<Exception>(() => dataOperation.Execute(new TestCommandRequest { RequestString = CommandRequestString }));
            Assert.Equal(CommandRequestString, exception.Message);
        }

        [Fact]
        public void EnsureQueryResolvedAsExpected()
        {
            var dataOperation = Resolve<IDataOperation<TestQueryRequest, TestQueryResponse>>();

            Assert.IsAssignableFrom<TestQuery>(dataOperation);
        }

        [Fact]
        public void QueryRegistration()
        {
            var dataOperation = Resolve<IDataOperation<TestQueryRequest, TestQueryResponse>>();

            var response = dataOperation.Execute(new TestQueryRequest { RequestString = QueryRequestString });
            Assert.Equal(QueryRequestString, response.ResponseString);
        }
    }
}
