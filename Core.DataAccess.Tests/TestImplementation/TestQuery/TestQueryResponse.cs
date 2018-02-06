namespace Core.DataAccess.Tests.TestImplementation.TestQuery
{
    using Contracts;

    public class TestQueryResponse : IQueryResponse<TestQueryRequest>
    {
        public string ResponseString { get; set; }
    }
}
