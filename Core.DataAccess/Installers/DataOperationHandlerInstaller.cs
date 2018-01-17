namespace Core.DataAccess.Installers
{
    using global::Autofac;
    using Core.DataAccess.Contracts;

    public class DataOperationHandlerInstaller : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(CommandOperationHandler<>)).As(typeof(ICommandOperationHandler<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(QueryOperationHandler<,>)).As(typeof(IQueryOperationHandler<,>)).InstancePerLifetimeScope();
        }
    }
}
