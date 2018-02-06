namespace Core.DataAccess.Tests.TestImplementation.TestQuery
{
    using Contracts;

    public class TestQueryRequest : IQueryRequest<TestQueryResponse>
    {
        public string RequestString { get; set; }
    }
}
