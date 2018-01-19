namespace Core.DataAccess.Installers
{
    using System;
    using System.Reflection;
    using Autofac;
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using NHibernate;
    using NHibernate.Dialect;

    public class DatabaseInstallerModule : Autofac.Module
    {
        private readonly Assembly _assembly;
        private readonly string _connectionString;
        private readonly string _datatbaseName;
        private readonly Type _dBTokenType;

        public DatabaseInstallerModule(Assembly assembly, string connectionString, string databaseName, Type dBToken)
        {
            _assembly = assembly;
            _connectionString = connectionString;
            _datatbaseName = databaseName;
            _dBTokenType = dBToken;
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new NullReferenceException("ConnectionString is null");
            }

            if (string.IsNullOrEmpty(_datatbaseName))
            {
                throw new NullReferenceException("DatabaseName is null");
            }

            builder.Register(c => BuildSessionFactory(_connectionString, _datatbaseName, _assembly))
                   .As<ISessionFactory>()
                   .SingleInstance();

            builder.Register(c => c.Resolve<ISessionFactory>().OpenSession());

            DataOperationInstaller.Install(builder, _dBTokenType, _assembly);
        }

        private static ISessionFactory BuildSessionFactory(string connectionString, string databaseName, Assembly assembly)
        {
            var config = MsSqlConfiguration.MsSql2012.Dialect<MsSql2012Dialect>()
                                           .ConnectionString(connectionString)
                                           .ShowSql();

            return Fluently.Configure()
                           .Database(config)
                           .ExposeConfiguration(
                                cfg =>
                                {
                                    cfg.SetProperty(NHibernate.Cfg.Environment.SessionFactoryName, databaseName);
                                    cfg.SetProperty(NHibernate.Cfg.Environment.Isolation, "ReadCommitted");
                                    cfg.SetProperty(NHibernate.Cfg.Environment.Hbm2ddlKeyWords, "none");
                                    cfg.SetProperty(NHibernate.Cfg.Environment.GenerateStatistics, "true");
                                })
                           .Mappings(m => m.FluentMappings.AddFromAssembly(assembly))
                           .BuildSessionFactory();
        }
    }
}
