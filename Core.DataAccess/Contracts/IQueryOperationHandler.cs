namespace Core.DataAccess.Contracts
{
    public interface IQueryOperationHandler<TRequest, TResponse> where TRequest : IQueryRequest<TResponse>
    {
        TResponse ExecuteQuery(IQueryRequest<TResponse> request);
    }
}
