using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Base.DB;

namespace iCMS.Frameworks.Core.DB
{
    public interface IBaseCoreService<TEntity> where TEntity : EntityBase
    {
        //ILogService LogService { get; set; }

        /// <summary>
        /// 释放资源
        /// </summary>
        void Dispose();

        /// <summary>
        /// 事物操作
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        OperationResult TranMethod(Dictionary<EntityBase, EntityState> models);
        ///// <summary>
        /////   为指定的类型返回 System.Data.Entity.DbSet，这将允许对上下文中的给定实体执行 CRUD 操作。
        ///// </summary>
        ///// <typeparam name="TEntity"> 应为其返回一个集的实体类型。 </typeparam>
        ///// <returns> 给定实体类型的 System.Data.Entity.DbSet 实例。 </returns>
        //IQueryable<TEntity> GetDatas<TEntity>(bool isTracking) where TEntity : EntityBase;
        //add by iLine 20160427
        /// <summary>
        /// 为指定的类型返回 System.Data.Entity.DbSet，这将允许对上下文中的给定实体执行 CRUD 操作。
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate"> 查询条件</param>
        /// <param name="isTracking"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetDatas<TEntity>(Expression<Func<TEntity, bool>> predicate, bool isTracking) where TEntity : EntityBase;
        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="TEntity"> 类型 </typeparam>
        /// <param name="entity"> 对象 </param>
        OperationResult AddNew<TEntity>(TEntity entity) where TEntity : EntityBase;
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entities"> 要注册的对象集合 </param>
        OperationResult AddNew<TEntity>(IEnumerable<TEntity> entities) where TEntity : EntityBase;
        /// <summary>
        ///  更新
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        OperationResult Update<TEntity>(TEntity entity) where TEntity : EntityBase;
        /// <summary>
        /// 批量更新  add by masu 2016年3月17日09:13:47
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entities"> 要注册的对象集合 </param>
        OperationResult Update<TEntity>(IEnumerable<TEntity> entities) where TEntity : EntityBase;

        /// <summary>
        ///  删除
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        OperationResult Delete<TEntity>(TEntity entity) where TEntity : EntityBase;
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entities"> 要注册的对象集合 </param>
        OperationResult Delete<TEntity>(IEnumerable<TEntity> entities) where TEntity : EntityBase;
        /// <summary>
        ///     删除指定编号的记录
        /// </summary>
        /// <param name="id"> 实体记录编号 </param>
        /// <returns> 操作影响的行数 </returns>
        OperationResult Delete<TEntity>(int id) where TEntity : EntityBase;
        /// <summary>
        ///     删除所有符合特定表达式的数据
        /// </summary>
        /// <param name="predicate"> 查询条件谓语表达式 </param>
        /// <returns> 操作影响的行数 </returns>
        OperationResult Delete(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 查找指定主键的实体记录
        /// </summary>
        /// <param name="key"> 指定主键 </param>
        /// <returns> 符合编号的记录，不存在返回null </returns>
        TEntity GetByKey(object key);
        /// <summary>
        /// 直接执行SQL
        /// </summary>
        /// <param name="sql"></param>
        OperationResult ExecuteSqlCommand(string sql);
        /// <summary>
        /// 直接执行SQL
        /// </summary>
        /// <param name="sql"></param>
        OperationResult ExecuteSqlCommand(string sql, SqlParameter[] para);
        /// <summary>
        /// 执行SQl，返回对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        OperationResult SqlQuery<T>(string sql);
        /// <summary>
        /// 执行SQl，返回对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        OperationResult SqlQuery<T>(string sql, SqlParameter[] para);
    }
}