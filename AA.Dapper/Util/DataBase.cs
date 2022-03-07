using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using AA.FrameWork.Domain;
using Dapper;
namespace AA.Dapper.Util
{
    public class DataBase
    {
        private IDapperContext dapperContext;
        public DataBase(DapperContext dapperContext) 
        {
           this.dapperContext = dapperContext;
        }
        public int Execute(string sql, object param = null)
        {
            return dapperContext.Connection.Execute(sql, param, dapperContext.dbTransaction);
        }

        public Task<int> ExecuteAsync(string sql, object param = null)
        {
            return dapperContext.Connection.ExecuteAsync(sql, param, dapperContext.dbTransaction);
        }

        public T ExecuteScalar<T>(string sql, object param = null)
        {
            return dapperContext.Connection.ExecuteScalar<T>(sql, param, dapperContext.dbTransaction);
        }

        public Task<T> ExecuteScalarAsync<T>(string sql, object param = null)
        {
            return dapperContext.Connection.ExecuteScalarAsync<T>(sql, param, dapperContext.dbTransaction);
        }

        public IEnumerable<T> Query<T>(string sql, object param = null)
        {
            return dapperContext.Connection.Query<T>(sql, param, dapperContext.dbTransaction);
        }

        public Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null)
        {
            return dapperContext.Connection.QueryAsync<T>(sql, param, dapperContext.dbTransaction);
        }    

    }
}
