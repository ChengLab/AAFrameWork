using AA.Dapper.Util;
using AA.FrameWork.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace AA.Dapper
{
    public class DapperContext : IDapperContext
    {

        public const string PropertyDataSourcePrefix = "aa.dataSource";
        public const string PropertyDataSourceProvider = "provider";
        public const string PropertyDataSourceConnectionString = "connectionString";


        private PropertiesParser cfg;
        private readonly string _connectionString;
        private IDbConnection _connection = null;
        public IDbTransaction dbTransaction = null;

        public DapperContext(NameValueCollection props)
        {
            IDbConnectionManager dbMgr = null;
            cfg = new PropertiesParser(props);

            var dsNames = cfg.GetPropertyGroups(PropertyDataSourcePrefix);
            foreach (string dataSourceName in dsNames)
            {
                string datasourceKey = "{0}.{1}".FormatInvariant(PropertyDataSourcePrefix, dataSourceName);
                NameValueCollection propertyGroup = cfg.GetPropertyGroup(datasourceKey, true);
                PropertiesParser pp = new PropertiesParser(propertyGroup);
                string dsProvider = pp.GetStringProperty(PropertyDataSourceProvider, null);
                string dsConnectionString = pp.GetStringProperty(PropertyDataSourceConnectionString, null);

                if (dsProvider == null)
                {
                    //initException = new SchedulerException("Provider not specified for DataSource: {0}".FormatInvariant(dataSourceName));
                    //throw initException;
                }
                if (dsConnectionString == null)
                {
                    //initException = new SchedulerException("Connection string not specified for DataSource: {0}".FormatInvariant(dataSourceName));
                    //throw initException;
                }
                try
                {
                    DbProvider dbp = new DbProvider(dsProvider, dsConnectionString);
                    dbp.Initialize();

                    dbMgr = DBConnectionManager.Instance;
                    dbMgr.AddConnectionProvider(dataSourceName, dbp);
                    this.DataSource = dataSourceName;
                }
                catch (Exception exception)
                {
                    //initException = new SchedulerException("Could not Initialize DataSource: {0}".FormatInvariant(dataSourceName), exception);
                    //throw initException;
                }
            }


            //_connectionString = nameOrConnectionString;
        }

        /// <summary>
        /// Get or set the datasource name.
        /// </summary>
        public string DataSource { get; set; }
        public DataBase DataBase
        {
            get
            {
                return new DataBase(this);
            }
        }
        /// <summary>
        /// Get or set the database connection manager.
        /// </summary>
        public IDbConnectionManager ConnectionManager { get; set; } = DBConnectionManager.Instance;
        public IDbConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    //_connection = new SqlConnection(_connectionString);
                    _connection = ConnectionManager.GetConnection(DataSource);
                }
                return _connection;
            }
        }
        public void Dispose()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
                _connection.Close();
        }

        public IDbConnection GetCurrentConnection()
        {
            return _connection;
        }

        public IDbTransaction GetCurrentTransaction()
        {
            return dbTransaction;
        }

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }
            dbTransaction = Connection.BeginTransaction(isolationLevel);

        }

        public void Commit()
        {
            dbTransaction.Commit();
            dbTransaction = null;
        }

        public void RollBack()
        {
            dbTransaction.Rollback();
        }

    }
}
