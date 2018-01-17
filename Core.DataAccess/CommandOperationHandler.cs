namespace Core.DataAccess
{
    using System;
    using global::Autofac;
    using Core.DataAccess.Contracts;

    public class CommandOperationHandler<TRequest> : ICommandOperationHandler<TRequest> where TRequest : ICommandRequest
    {
        private readonly IComponentContext _context;

        public CommandOperationHandler(IComponentContext context)
        {
            _context = context;
        }

        public void ExecuteCommand(TRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("ExecuteCommand:- request is null");
            }

            var commandHandlerType = typeof(IDataOperation<>).MakeGenericType(request.GetType());
            dynamic commandHandler = _context.Resolve(commandHandlerType);
            commandHandler.Execute((dynamic)request);
        }
    }
}
