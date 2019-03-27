using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AA.Dapper.Repositories
{
    public interface IDapperRepository<TEntity> where TEntity : class
    {
        #region  Insert
        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        object Insert(TEntity entity, IDbTransaction transaction = null);

        Task<object> InsertAsync(TEntity entity, IDbTransaction transaction = null);
        #endregion

        #region Delete
        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        bool Delete(TEntity entity, IDbTransaction transaction = null);


        Task<bool> DeleteAsync(TEntity entity, IDbTransaction transaction = null);



        /// <summary>
        ///  Deletes all entities of type <typeparamref name="TEntity"/> matching the specified predicate from the database.
        /// Returns a value indicating whether the operation succeeded.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        bool DeleteMultiple(Expression<Func<TEntity, bool>> predicate, IDbTransaction transaction = null);

        Task<bool> DeleteMultipleAsync(Expression<Func<TEntity, bool>> predicate, IDbTransaction transaction = null);

        #endregion
        #region Update

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        bool Update(TEntity entity, IDbTransaction transaction = null);

        Task<bool> UpdateAsync(TEntity entity, IDbTransaction transaction = null);
        #endregion
        #region Get
        //T Query();
        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        TEntity Get(object id);

        Task<TEntity> GetAsync(object id);

        /// <summary>
        /// GetAll
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();
        #endregion

        #region Select

        /// <summary>
        /// Selects all the entities matching the specified predicate.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Select(Expression<Func<TEntity, bool>> predicate, IDbTransaction transaction = null);
        Task<IEnumerable<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> predicate, IDbTransaction transaction = null);

        /// <summary>
        /// Selects the first entity matching the specified predicate, or a default value if no entity matched.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        #endregion
    }
}
