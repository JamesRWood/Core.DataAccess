namespace Core.DataAccess.Contracts
{
    public interface IDataOperationHandler<TRequest> where TRequest : IRequest
    {
        TResponse ExecuteQuery<TResponse>(IQueryRequest<TResponse> request) where TResponse : IQueryResponse<TRequest>;

        void ExecuteCommand<TCommandRequest>(TCommandRequest request) where TCommandRequest : ICommandRequest;
    }
}
