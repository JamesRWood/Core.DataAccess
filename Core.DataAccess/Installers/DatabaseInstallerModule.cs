namespace Core.DataAccess.Installers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Autofac;
    using Autofac.Core;
    using Contracts;
    using Contracts.SessionToken;

    public class DatabaseInstallerModule<TDbContext, TDbToken> : Autofac.Module where TDbContext : IDbContext<TDbToken> where TDbToken : IDbToken
    {
        private readonly Assembly _assembly;
        private readonly string _connectionString;

        public DatabaseInstallerModule(
            Assembly assembly, 
            string connectionString)
        {
            _assembly = assembly;
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new NullReferenceException("ConnectionString is null");
            }

            var parameters = new List<Parameter>
            {
                new NamedParameter("connectionString", _connectionString)
            };

            builder.RegisterAssemblyTypes(_assembly)
                   .Where(t => t.GetTypeInfo().ImplementedInterfaces.Any(i => i == typeof(IDbContext<TDbToken>)))
                   .WithParameters(parameters)
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            DataOperationInstaller<TDbContext, TDbToken>.Install(builder, _assembly);
        }
    }
}
