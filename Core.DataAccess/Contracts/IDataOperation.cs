namespace Core.DataAccess.Contracts
{
    public interface IDataOperation<in TRequest, out TResponse> where TRequest : IQueryRequest<TResponse> where TResponse : IQueryResponse<TRequest>
    {
        TResponse Execute(TRequest request);
    }

    public interface IDataOperation<in TRequest> where TRequest : ICommandRequest
    {
        void Execute(TRequest request);
    }
}
