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
        private readonly IDapperContext _dapperContext;
        public DataBase(IDapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public int Execute(string sql, object param = null, IDbTransaction transaction = null)
        {
            return _dapperContext.Connection.Execute(sql, param, transaction);
        }

        public Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null)
        {
            return _dapperContext.Connection.ExecuteAsync(sql, param, transaction);
        }

        public T ExecuteScalar<T>(string sql, object param = null, IDbTransaction transaction = null)
        {
            return _dapperContext.Connection.ExecuteScalar<T>(sql, param, transaction);
        }

        public Task<T> ExecuteScalarAsync<T>(string sql, object param = null, IDbTransaction transaction = null)
        {
            return _dapperContext.Connection.ExecuteScalarAsync<T>(sql, param, transaction);
        }

        public IEnumerable<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null)
        {
            return _dapperContext.Connection.Query<T>(sql, param, transaction);
        }

        public Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null)
        {
            return _dapperContext.Connection.QueryAsync<T>(sql, param, transaction);
        }

        public virtual IPage<T> GetPage<T>(PageRequest pageRequest) where T:class
        {
            int recordCount = _dapperContext.Connection.ExecuteScalar<int>(PageUtil.CreateCountingSql(pageRequest.SqlText), pageRequest.SqlParam);
            var list = _dapperContext.Connection.Query<T>(PageUtil.CreatePagingSql(recordCount, pageRequest.PageSize, pageRequest.PageIndex, pageRequest.SqlText, pageRequest.OrderFiled), pageRequest.SqlParam);
            IPage<T> result = new Page<T>();
            result.Count = recordCount;
            result.Data = list;
            return result;
        }

    }
}
