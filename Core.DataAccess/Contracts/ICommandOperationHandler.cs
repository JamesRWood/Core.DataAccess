namespace Core.DataAccess.Contracts
{
    public interface ICommandOperationHandler<TCommandRequest> where TCommandRequest : ICommandRequest
    {
        void ExecuteCommand(TCommandRequest request);
    }
}
