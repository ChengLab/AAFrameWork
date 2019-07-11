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
using Dommel;
namespace AA.Dapper.Repositories
{
    public class DapperRepository<TEntity> : IDapperRepository<TEntity>
         where TEntity : class
    {
        public IDbConnection Connection => DapperContext.Current.Connection;
        #region Insert
        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual object Insert(TEntity entity, IDbTransaction transaction = null)
        {
            return Connection.Insert(entity, transaction);
        }

        public virtual Task<object> InsertAsync(TEntity entity, IDbTransaction transaction = null)
        {
            return Connection.InsertAsync(entity, transaction);
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual bool Delete(TEntity entity, IDbTransaction transaction = null)
        {
            return Connection.Delete(entity, transaction);
        }

        public virtual Task<bool> DeleteAsync(TEntity entity, IDbTransaction transaction = null)
        {
            return Connection.DeleteAsync(entity, transaction);
        }


        public virtual bool DeleteMultiple(Expression<Func<TEntity, bool>> predicate, IDbTransaction transaction = null)
        {
            return Connection.DeleteMultiple(predicate, transaction);
        }

        public virtual Task<bool> DeleteMultipleAsync(Expression<Func<TEntity, bool>> predicate, IDbTransaction transaction = null)
        {
            return Connection.DeleteMultipleAsync(predicate, transaction);
        }
        #endregion

        #region Update

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        public virtual bool Update(TEntity entity, IDbTransaction transaction = null)
        {
            return Connection.Update(entity, transaction);
        }

        public virtual Task<bool> UpdateAsync(TEntity entity, IDbTransaction transaction = null)
        {
            return Connection.UpdateAsync(entity, transaction);
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
            return Connection.Get<TEntity>(id);
        }

        public virtual Task<TEntity> GetAsync(object id)
        {
            return Connection.GetAsync<TEntity>(id);
        }
        /// <summary>
        /// GetAll
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            return Connection.GetAll<TEntity>();
        }

        public virtual Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return Connection.GetAllAsync<TEntity>();
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
        public virtual IEnumerable<TEntity> Select(Expression<Func<TEntity, bool>> predicate, IDbTransaction transaction = null)
        {
            return Connection.Select(predicate);
        }

        public virtual Task<IEnumerable<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> predicate, IDbTransaction transaction = null)
        {
            return Connection.SelectAsync(predicate);
        }
        /// <summary>
        /// Selects the first entity matching the specified predicate, or a default value if no entity matched.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Connection.FirstOrDefault<TEntity>(predicate);
        }

        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Connection.FirstOrDefaultAsync<TEntity>(predicate);
        }
        #endregion


    }
}
