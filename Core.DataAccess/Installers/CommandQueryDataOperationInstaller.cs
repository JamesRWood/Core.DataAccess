namespace Core.DataAccess.Installers
{
    using Autofac;
    using System;
    using System.Reflection;

    public static class CommandQueryDataOperationInstaller
    {
        public static void Install(ContainerBuilder builder, Type databaseType, Assembly assembly)
        {
            RegisterCommandQueryDataOperations(builder, databaseType, assembly);
        }

        private static void RegisterCommandQueryDataOperations(ContainerBuilder builder, Type databaseType, Assembly assembly)
        {
            builder.RegisterAssemblyTypes(assembly)
                   .Where(t => t.BaseType == databaseType)
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
        }
    }
}
