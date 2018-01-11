namespace Core.DataAccess.Installers
{
    using global::Autofac;
    using Core.Autofac;
    using Core.DataAccess.Contracts;

    public class DataOperationHandlerInstaller : IAutofacBuilderExtension
    {
        public ContainerBuilder AddRegistrations(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterGeneric(typeof(DataOperationHandler<>)).As(typeof(IDataOperationHandler<>)).InstancePerLifetimeScope();
            return containerBuilder;
        }
    }
}
