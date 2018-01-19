namespace Core.DataAccess.Installers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Autofac;
    using Autofac.Core;
    using Microsoft.EntityFrameworkCore;

    public class DatabaseInstallerModule : Autofac.Module
    {
        private readonly Assembly _assembly;
        private readonly string _connectionString;
        //private readonly string _datatbaseName;
        private readonly Type _dBTokenType;
        private readonly IEnumerable<DbSet<IDbSet>> _databaseEntities;

        public DatabaseInstallerModule(
            Assembly assembly, 
            string connectionString, 
            //string databaseName, 
            Type dBToken,
            IEnumerable<DbSet<IDbSet>> databaseEntities)
        {
            _assembly = assembly;
            _connectionString = connectionString;
            //_datatbaseName = databaseName;
            _dBTokenType = dBToken;
            _databaseEntities = databaseEntities;
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new NullReferenceException("ConnectionString is null");
            }

            //if (string.IsNullOrEmpty(_datatbaseName))
            //{
            //    throw new NullReferenceException("DatabaseName is null");
            //}

            var parameters = new List<Parameter>
            {
                new NamedParameter("databaseEntities", _databaseEntities),
                new NamedParameter("connectionString", _connectionString)
            };

            builder.RegisterAssemblyTypes(_assembly)
                   .Where(t => t.GetTypeInfo().ImplementedInterfaces.Any(i => i == _dBTokenType))
                   .WithParameters(parameters)
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            //builder.Register(c => BuildSessionFactory(_connectionString, _datatbaseName, _assembly))
            //       .As<ISessionFactory>()
            //       .SingleInstance();

            //builder.Register(c => c.Resolve<ISessionFactory>().OpenSession());

            DataOperationInstaller.Install(builder, _dBTokenType, _assembly);
        }

        //private static ISessionFactory BuildSessionFactory(string connectionString, string databaseName, Assembly assembly)
        //{
        //    //var config = MsSqlConfiguration.MsSql2012.Dialect<MsSql2012Dialect>()
        //    //                               .ConnectionString(connectionString)
        //    //                               .ShowSql();

        //    //return Fluently.Configure()
        //    //               .Database(config)
        //    //               .ExposeConfiguration(
        //    //                    cfg =>
        //    //                    {
        //    //                        cfg.SetProperty(NHibernate.Cfg.Environment.SessionFactoryName, databaseName);
        //    //                        cfg.SetProperty(NHibernate.Cfg.Environment.Isolation, "ReadCommitted");
        //    //                        cfg.SetProperty(NHibernate.Cfg.Environment.Hbm2ddlKeyWords, "none");
        //    //                        cfg.SetProperty(NHibernate.Cfg.Environment.GenerateStatistics, "true");
        //    //                    })
        //    //               .Mappings(m => m.FluentMappings.AddFromAssembly(assembly))
        //    //               .BuildSessionFactory();
        //}
    }
}
