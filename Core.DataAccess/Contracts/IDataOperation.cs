namespace Core.DataAccess.Contracts
{
    public interface IDataOperation<in TRequest, out TResponse> where TRequest : IQueryRequest<IQueryResponse>
    {
        TResponse Execute(TRequest request);
    }

    public interface IDataOperation<in TRequest> where TRequest : ICommandRequest
    {
        void Execute(TRequest request);
    }
}
