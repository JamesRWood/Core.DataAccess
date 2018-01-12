namespace Core.DataAccess.Installers
{
    using global::Autofac;
    using Core.DataAccess.Contracts;

    public class DataOperationHandlerInstaller : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(DataOperationHandler<>)).As(typeof(IDataOperationHandler<>)).InstancePerLifetimeScope();
        }
    }
}
