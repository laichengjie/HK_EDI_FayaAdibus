using System.Collections.Generic;
using System.Data.Entity;
using HK_EDI.Core;
using System.Data;
using System.Data.Entity.Infrastructure;

namespace HK_EDI.Data
{
    public interface IDbContext
    {
        /// <summary>
        /// Get DbSet
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>DbSet</returns>
        IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;

        /// <summary>
        /// Save changes
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        /// Execute stores procedure and load a list of entities at the end
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="commandText">Command text</param>
        /// <param name="parameters">Parameters</param>
        /// <returns>Entities</returns>
        IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters)
            where TEntity : BaseEntity, new();

           /// <summary>
        /// 执行sql语句，返回对象实体列表
        /// </summary>
        /// <typeparam name="TEntity">实体</typeparam>
        /// <param name="commandText">存储过程名称</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
         IList<TEntity> ExecuteStoreCommandList<TEntity>(string commandText, params object[] parameters) where TEntity : BaseEntity, new();

         IList<TEntity> ExecuteStoredProcedureListN<TEntity>(string commandText, params object[] parameters) where TEntity : BaseEntity, new();
        /// <summary>
        /// Creates a raw SQL query that will return elements of the given generic type.  The type can be any type that has properties that match the names of the columns returned from the query, or can be a simple primitive type. The type does not have to be an entity type. The results of this query are never tracked by the context even if the type of object returned is an entity type.
        /// </summary>
        /// <typeparam name="TElement">The type of object returned by the query.</typeparam>
        /// <param name="sql">The SQL query string.</param>
        /// <param name="parameters">The parameters to apply to the SQL query string.</param>
        /// <returns>Result</returns>
        IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters);

        /// <summary>
        /// Executes the given DDL/DML command against the database.
        /// </summary>
        /// <param name="sql">The command string</param>
        /// <param name="doNotEnsureTransaction">false - the transaction creation is not ensured; true - the transaction creation is ensured.</param>
        /// <param name="timeout">Timeout value, in seconds. A null value indicates that the default value of the underlying provider will be used</param>
        /// <param name="parameters">The parameters to apply to the command string.</param>
        /// <returns>The result returned by the database after executing the command.</returns>
        int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters);

      
        /// <summary>
        /// 执行sql返回第一行第一列数据 
        /// </summary>
        /// <param name="commandText">要执行的sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        object ExecuteScalar(string commandText, params object[] parameters);

        void ExecuteStoredProcedure(string commandText, params object[] parameters);

        object ExecuteStoredProcedureObject(string commandText, params object[] parameters);

    }
}
