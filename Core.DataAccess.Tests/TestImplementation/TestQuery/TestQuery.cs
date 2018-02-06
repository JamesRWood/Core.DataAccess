namespace Core.DataAccess.Tests.TestImplementation.TestQuery
{
    using Contracts;

    public class TestQuery : IDataOperation<TestQueryRequest, TestQueryResponse>
    {
        private readonly IDbContextTestImplementation _dbContext;

        public TestQuery(IDbContextTestImplementation dbContext)
        {
            _dbContext = dbContext;
        }

        public TestQueryResponse Execute(TestQueryRequest request)
        {
            return new TestQueryResponse { ResponseString = "TestQuery Execute method resolved" };
        }
    }
}
