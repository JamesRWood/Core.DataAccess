namespace Core.DataAccess.Installers
{
    using Autofac;
    using Core.DataAccess.Contracts;
    using System;
    using System.Linq;
    using System.Reflection;

    public static class CommandQueryDataOperationInstaller
    {
        public static void Install(ContainerBuilder builder, Type databaseType, Assembly assembly)
        {
            RegisterDataOperations(builder, assembly, databaseType, typeof(IDataOperation<>));
            RegisterDataOperations(builder, assembly, databaseType, typeof(IDataOperation<,>));
        }

        private static void RegisterDataOperations(ContainerBuilder builder, Assembly assembly, Type databaseTokenType, Type dataOperationType)
        {
            builder.RegisterAssemblyTypes(assembly)
                   .Where(t => t.GetTypeInfo().ImplementedInterfaces.Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == dataOperationType) &&
                               t.GetTypeInfo().ImplementedInterfaces.Any(i => i == databaseTokenType))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
        }
    }
}
