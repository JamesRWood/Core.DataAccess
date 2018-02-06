namespace Core.DataAccess.Tests.TestImplementation.TestCommand
{
    using System;
    using Contracts;

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
}
