using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using log4net;
using NHibernate;
using Prescriptions.API.Services;
using Prescriptions.ModelMappings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prescriptions.Database
{
    public class DatabaseAccess : IDisposable, IDatabaseAccess
    {
        private readonly ILog Logger = LogManager.GetLogger(typeof(DatabaseAccess));

        private FluentConfiguration configuration;
        private ISessionFactory sessionFactory;

        private string database = "Prescriptor";
        private string username = "root";
        private string server = Environment.GetEnvironmentVariable("Optiplex_Address");

        private ISession session;
        private readonly object sessionLock = new object();

        private bool isInitiated  = false;

        public DatabaseAccess()
        {
        }

        public IDatabaseAccess InitDbConnection()
        {
            if (!isInitiated)
            {
                this.configuration = SetupNHibernate();
                this.sessionFactory = this.configuration.BuildSessionFactory();
                this.session = this.sessionFactory.OpenSession();
                this.isInitiated = false;
            }

            return this;
        }

        public IDatabaseAccess InitDbConnection(string host)
        {
            if (!isInitiated)
            {
                this.server = host;
            }
            return InitDbConnection();

        }

        private FluentConfiguration SetupNHibernate()
        {
            this.Logger.Info($"Conneting to server:[{this.server}] database:[{this.database}] as user:[{this.username}]");

            return Fluently.Configure()
                            .Database(MySQLConfiguration.Standard.ConnectionString(
                                connection =>
                                    connection
                                    .Server($"{this.server}")
                                    .Database(this.database)
                                    .Username(this.username)
                                    .Password(Environment.GetEnvironmentVariable("Optiplex_MySql_Password"))
                                ))
                            .Mappings(x => x.FluentMappings.AddFromAssemblyOf<DrugMap>());
        }

        private ISession GetSession()
        {
            lock(this.sessionLock)
            {
                if(!this.session.IsOpen)
                {
                    this.session = this.sessionFactory.OpenSession();
                }
                return this.session;
            }
        }

        public IList<T> GetAllEntitiesOfType<T>() where T:class
        {
            return GetSession()
                .CreateCriteria<T>()
                .List<T>();
        }

        public IList<T> GetEntitiesOfType<T>(Func<ICriteria,ICriteria> criteriaStrategy) where T : class
        {
            var criteria = GetSession().CreateCriteria<T>();
            criteria = criteriaStrategy.Invoke(criteria);
            return criteria.List<T>();
        }

        public void Save<T>(T entity)
        {
            var session = GetSession();
            session.Save(entity);
            session.Flush();
        }

        public void SaveBatch<T>(IEnumerable<T> items)
        {
            using (var session = this.sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    items.ToList().ForEach(x=>session.Save(x));
                    session.Flush();

                    tx.Commit();
                }
            }
        }

        public async void Flush()
        {
            await this.sessionFactory.GetCurrentSession()
                .FlushAsync();
        }

        public void Dispose()
        {
            lock(this.sessionLock)
            {
                this.session.Close();
                this.sessionFactory.Close();
            }
        }
    }
}
