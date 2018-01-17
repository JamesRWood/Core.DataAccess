namespace Core.DataAccess
{
    using Autofac;
    using Core.DataAccess.Contracts;
    using System;

    public class QueryOperationHandler<TRequest, TResponse> : IQueryOperationHandler<TRequest, TResponse> where TRequest : IQueryRequest<TResponse>
    {
        private readonly IComponentContext _context;

        public QueryOperationHandler(IComponentContext context)
        {
            _context = context;
        }

        public TResponse ExecuteQuery(IQueryRequest<TResponse> request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("ExecuteQuery:- request is null");
            }

            var queryHandlerType = typeof(IDataOperation<,>).MakeGenericType(request.GetType(), typeof(TResponse));
            dynamic queryHandler = _context.Resolve(queryHandlerType);
            return queryHandler.Execute((dynamic)request);
        }
    }
}
