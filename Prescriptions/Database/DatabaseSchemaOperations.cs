using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using log4net;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Prescriptions.ModelMappings;
using System;

namespace Prescriptions.Database
{
    public class DatabaseSchemaOperations : IDisposable
    {
        private readonly ILog Logger = LogManager.GetLogger(typeof(DatabaseSchemaOperations));
        private ISessionFactory sessionFactory;

        private FluentConfiguration BuildConfiguration(Action<MySQLConnectionStringBuilder> configBuilder)
        {
            return Fluently.Configure()
                        .Database(MySQLConfiguration.Standard.ConnectionString(configBuilder))
                        .Mappings(x => x.FluentMappings.AddFromAssemblyOf<DrugMap>());
        }

        private ISession GetSession(Action<MySQLConnectionStringBuilder> configBuilder)
        {
            this.sessionFactory = BuildConfiguration(configBuilder).BuildSessionFactory();

            return this.sessionFactory.OpenSession();
            
        }

        private Func<MySQLConnectionStringBuilder, MySQLConnectionStringBuilder> GetConfigBuilderFunction(string host, string username)
        {
            Func<MySQLConnectionStringBuilder, MySQLConnectionStringBuilder> builder = connection =>
               connection
               .Server(host)
               .Username(username)
               .Password(Environment.GetEnvironmentVariable("Optiplex_MySql_Password"));

            return builder;
        }

        public void UpdateSchema(string host)
        {
            new SchemaUpdate(BuildConfiguration(c => GetConfigBuilderFunction(host, "root").Invoke(c).Database("Prescriptor")).BuildConfiguration()).Execute(true, true);
        }

        public void DeleteSchema(string host)
        {
            this.Logger.Info("Deleting database [Prescriptor]");

            this.sessionFactory = BuildConfiguration(c => GetConfigBuilderFunction(host, "root").Invoke(c)).BuildSessionFactory();

            using (var session = this.sessionFactory.OpenSession())
            {
                session.CreateSQLQuery($"DROP DATABASE Prescriptor").List();
            }
        }

        public void CreateSchema(string host)
        {
            this.Logger.Info("Deleting database [Prescriptor]");

            this.sessionFactory = BuildConfiguration(c => GetConfigBuilderFunction(host, "root").Invoke(c)).BuildSessionFactory();

            using (var session = this.sessionFactory.OpenSession())
            {
                session.CreateSQLQuery($"CREATE DATABASE Prescriptor").List();
            }
        }

        public void Dispose()
        {
            this.sessionFactory?.Dispose();
        }
    }
}
