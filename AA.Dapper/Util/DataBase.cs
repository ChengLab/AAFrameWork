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
        private IDbConnection Connection => DapperContext.Current.Connection;
        private IDbTransaction transaction => DapperContext.Current.dbTransaction;
        public int Execute(string sql, object param = null)
        {
            return Connection.Execute(sql, param, transaction);
        }

        public Task<int> ExecuteAsync(string sql, object param = null)
        {
            return Connection.ExecuteAsync(sql, param, transaction);
        }

        public T ExecuteScalar<T>(string sql, object param = null)
        {
            return Connection.ExecuteScalar<T>(sql, param, transaction);
        }

        public Task<T> ExecuteScalarAsync<T>(string sql, object param = null)
        {
            return Connection.ExecuteScalarAsync<T>(sql, param, transaction);
        }

        public IEnumerable<T> Query<T>(string sql, object param = null)
        {
            return Connection.Query<T>(sql, param, transaction);
        }

        public Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null)
        {
            return Connection.QueryAsync<T>(sql, param, transaction);
        }

        public virtual IPage<T> GetPage<T>(PageRequest pageRequest) where T : class
        {
            int recordCount = Connection.ExecuteScalar<int>(PageUtil.CreateCountingSql(pageRequest.SqlText), pageRequest.SqlParam);
            var list = Connection.Query<T>(PageUtil.CreatePagingSql(recordCount, pageRequest.PageSize, pageRequest.PageIndex, pageRequest.SqlText, pageRequest.OrderFiled), pageRequest.SqlParam);
            IPage<T> result = new Page<T>();
            result.Count = recordCount;
            result.Data = list;
            return result;
        }

    }
}
