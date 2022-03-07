using AA.Dapper.Advanced;
using AA.Dapper.Util;
using AA.FrameWork.Extensions;
using AA.FrameWork.Util;
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
        private IDbConnection _connection = null;
        private IDbTransaction _dbTransaction = null;

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
                    var prefixDataSource = DbContextHolder.GetDbSourceMode();
                    _connection = ConnectionManager.GetConnection(prefixDataSource);
                }
                return _connection;
            }
        }

        public IDbTransaction dbTransaction { get { return _dbTransaction; } }
        public void Dispose()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
                _connection.Close();
        }
        

        public IDbTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }
            _dbTransaction = Connection.BeginTransaction(isolationLevel);
            return dbTransaction;
        }

        public void Commit()
        {
            _dbTransaction.Commit();
            _dbTransaction = null;
        }

        public void RollBack()
        {
            _dbTransaction.Rollback();
        }

    }
}
