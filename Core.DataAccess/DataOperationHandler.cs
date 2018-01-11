namespace Core.DataAccess
{
    using System;
    using global::Autofac;
    using Core.DataAccess.Contracts;

    public class DataOperationHandler<TRequest> : IDataOperationHandler<TRequest> where TRequest : IRequest
    {
        private readonly IContainer container;

        public DataOperationHandler(IContainer container)
        {
            this.container = container;
        }

        public void ExecuteCommand<TCommandRequest>(TCommandRequest request) where TCommandRequest : ICommandRequest
        {
            if (request == null)
            {
                throw new ArgumentNullException("ExecuteCommand:- request is null");
            }

            var commandHandlerType = typeof(IDataOperation<>).MakeGenericType(request.GetType());
            dynamic commandHandler = container.Resolve(commandHandlerType);
            commandHandler.Execute((dynamic)request);
        }

        public TResponse ExecuteQuery<TResponse>(IQueryRequest<TResponse> request) where TResponse : IQueryResponse<TRequest>
        {
            if (request == null)
            {
                throw new ArgumentNullException("ExecuteQuery:- request is null");
            }

            var queryHandlerType = typeof(IDataOperation<,>).MakeGenericType(request.GetType(), typeof(TResponse));
            dynamic queryHandler = container.Resolve(queryHandlerType);
            return queryHandler.Execute((dynamic)request);
        }
    }
}
