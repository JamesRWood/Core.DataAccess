namespace Core.DataAccess.Contracts
{
    public interface IDataOperation<TRequest, TResponse> where TRequest : IQueryRequest<TResponse>
    {
        TResponse Execute(TRequest request);
    }

    public interface IDataOperation<TRequest> where TRequest : ICommandRequest
    {
        void Execute(TRequest request);
    }
}
