namespace Core.DataAccess.Installers
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Autofac;
    using Autofac.Core;
    using Contracts.SessionToken;
    using Core.DataAccess.Contracts;

    public static class DataOperationInstaller<TDbContext, TDbToken> where TDbContext : IDbContext<TDbToken> where TDbToken : IDbToken
    {
        public static void Install(ContainerBuilder builder, Assembly assembly)
        {
            RegisterDataOperations(builder, assembly, typeof(IDataOperation<>));
            RegisterDataOperations(builder, assembly, typeof(IDataOperation<,>));
        }

        private static void RegisterDataOperations(ContainerBuilder builder, Assembly assembly, Type dataOperationType)
        {
            builder.RegisterAssemblyTypes(assembly)
                   .Where(t => t.GetTypeInfo().ImplementedInterfaces.Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == dataOperationType))
                   .WithParameter(new ResolvedParameter((p, c) => p.ParameterType == typeof(TDbContext),
                                                        (p, c) => c.Resolve<IDbContext<TDbToken>>(TypedParameter.From(typeof(TDbContext)))))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
        }
    }
}
