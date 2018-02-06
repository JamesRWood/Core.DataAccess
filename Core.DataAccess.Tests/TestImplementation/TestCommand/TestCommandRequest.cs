namespace Core.DataAccess.Tests.TestImplementation.TestCommand
{
    using Contracts;

    public class TestCommandRequest : ICommandRequest
    {
        public string RequestString { get; set; }
    }
}
