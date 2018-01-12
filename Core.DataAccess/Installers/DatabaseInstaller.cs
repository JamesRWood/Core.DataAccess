namespace Core.DataAccess.Installers
{
    using Autofac;
    using Core.DataAccess.Contracts.SessionToken;
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using NHibernate;
    using NHibernate.Dialect;
    using System;
    using System.Reflection;

    public class DatabaseInstaller : Autofac.Module
    {
        public IDBToken DBToken { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            if (DBToken == null)
            {
                throw new NullReferenceException("DBToken is null");
            }

            var assembly = Assembly.GetCallingAssembly();

            builder.Register(c => BuildSessionFactory(DBToken.DatabaseName, assembly))
                   .As<ISessionFactory>()
                   .SingleInstance();

            builder.Register(c => c.Resolve<ISessionFactory>().OpenSession());

            CommandQueryDataOperationInstaller.Install(builder, DBToken.GetType(), assembly);
        }

        private static ISessionFactory BuildSessionFactory(string databaseName, Assembly assembly)
        {
            var config = MsSqlConfiguration.MsSql2012.Dialect<MsSql2012Dialect>()
                                           .ConnectionString(@"Server=.;initial catalog=" + databaseName + @";Integrated Security=True;")
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
