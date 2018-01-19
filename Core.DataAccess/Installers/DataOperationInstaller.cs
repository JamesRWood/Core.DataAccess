namespace Core.DataAccess.Installers
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Autofac;
    using Autofac.Core;
    using Core.DataAccess.Contracts;

    public static class DataOperationInstaller
    {
        public static void Install(ContainerBuilder builder, Type databaseType, Assembly assembly)
        {
            RegisterDataOperations(builder, assembly, databaseType, typeof(IDataOperation<>));
            RegisterDataOperations(builder, assembly, databaseType, typeof(IDataOperation<,>));
        }

        private static void RegisterDataOperations(ContainerBuilder builder, Assembly assembly, Type databaseTokenType, Type dataOperationType)
        {
            builder.RegisterAssemblyTypes(assembly)
                   .Where(t => t.GetTypeInfo().ImplementedInterfaces.Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == dataOperationType))
                   .WithParameter(new ResolvedParameter((p, c) => p.ParameterType == databaseTokenType,
                                                        (p, c) => c.Resolve<IDbContext>(TypedParameter.From(databaseTokenType))))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
        }
    }
}
