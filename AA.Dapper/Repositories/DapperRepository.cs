using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AA.Dapper.FluentMap;
using AA.Dapper.FluentMap.Dommel.Mapping;
using AA.Dapper.FluentMap.Dommel.Resolvers;
using AA.Dapper.FluentMap.Mapping;
using AA.FrameWork.Domain;
using AA.Dapper.Dommel;
using AA.Dapper.Advanced;

namespace AA.Dapper.Repositories
{
    public class DapperRepository<TEntity> : IDapperRepository<TEntity>
         where TEntity : class
    {
        private IDapperContext dapperContext;
        public DapperRepository(IDapperContext dapperContext) 
        {
            this.dapperContext = dapperContext;
        }
       // public IDbConnection Connection => RoutingDataSource.GetConnection();
        //private IDbTransaction transaction => DapperContext.Current.dbTransaction;
        #region Insert
        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual object Insert(TEntity entity)
        {
            return dapperContext.Connection.Insert(entity, dapperContext.dbTransaction);
        }

        public virtual Task<object> InsertAsync(TEntity entity)
        {
            return dapperContext.Connection.InsertAsync(entity, dapperContext.dbTransaction);
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual bool Delete(TEntity entity)
        {
            return dapperContext.Connection.Delete(entity, dapperContext.dbTransaction);
        }

        public virtual Task<bool> DeleteAsync(TEntity entity)
        {
            return dapperContext.Connection.DeleteAsync(entity, dapperContext.dbTransaction);
        }


        public virtual int DeleteMultiple(Expression<Func<TEntity, bool>> predicate)
        {
            return dapperContext.Connection.DeleteMultiple(predicate,  dapperContext.dbTransaction);
        }

        public virtual Task<int> DeleteMultipleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return dapperContext.Connection.DeleteMultipleAsync(predicate, dapperContext.dbTransaction);
        }
        #endregion

        #region Update

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        public virtual bool Update(TEntity entity)
        {
            return dapperContext.Connection.Update(entity,  dapperContext.dbTransaction);
        }

        public virtual Task<bool> UpdateAsync(TEntity entity)
        {
            return dapperContext.Connection.UpdateAsync(entity,  dapperContext.dbTransaction);
        }
        #endregion

        #region Get

        //T Query();
        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        public virtual TEntity Get(object id)
        {
            return dapperContext.Connection.Get<TEntity>(id, dapperContext.dbTransaction);
        }

        public virtual Task<TEntity> GetAsync(object id)
        {
            return dapperContext.Connection.GetAsync<TEntity>(id, dapperContext.dbTransaction);
        }
        /// <summary>
        /// GetAll
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            return dapperContext.Connection.GetAll<TEntity>( dapperContext.dbTransaction);
        }

        public virtual Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return dapperContext.Connection.GetAllAsync<TEntity>( dapperContext.dbTransaction);
        }

        #endregion

        #region Select
        /// <summary>
        /// Selects all the entities matching the specified predicate.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Select(Expression<Func<TEntity, bool>> predicate)
        {
            return dapperContext.Connection.Select(predicate, dapperContext.dbTransaction);
        }

        public virtual Task<IEnumerable<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return dapperContext.Connection.SelectAsync(predicate, dapperContext.dbTransaction);
        }
        /// <summary>
        /// Selects the first entity matching the specified predicate, or a default value if no entity matched.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return dapperContext.Connection.FirstOrDefault<TEntity>(predicate, dapperContext.dbTransaction);
        }

        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return dapperContext.Connection.FirstOrDefaultAsync<TEntity>(predicate, dapperContext.dbTransaction);
        }
        #endregion

        #region Count
        public virtual long Count() 
        {
            return dapperContext.Connection.Count<TEntity>(dapperContext.dbTransaction);
        }

        public virtual Task<long> CountAsync() 
        {
            return dapperContext.Connection.CountAsync<TEntity>(dapperContext.dbTransaction);
        }

        public virtual long Count(Expression<Func<TEntity, bool>> predicate) 
        {

            return dapperContext.Connection.Count(predicate, dapperContext.dbTransaction);
        }

        public virtual Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate) 
        {
            return dapperContext.Connection.CountAsync(predicate, dapperContext.dbTransaction);
        }

        #endregion

        #region from
        public virtual IEnumerable<TEntity> From(Action<SqlExpression<TEntity>> sqlBuilder)
        {
            return dapperContext.Connection.From<TEntity>(sqlBuilder, dapperContext.dbTransaction);
        }
        #endregion

    }
}
