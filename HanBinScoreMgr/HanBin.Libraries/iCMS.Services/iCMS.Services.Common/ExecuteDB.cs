/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：iCMS.Service.Common
 *文件名：  ExecuteDB
 *创建人：  张辽阔
 *创建时间：2016-10-26
 *描述：数据库批量执行
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Data.SqlClient;

using HanBin.Common.Component.Data.Base;
using HanBin.Common.Component.Data.Base.DB;
using HanBin.Frameworks.Core.Repository;
using HanBin.Common.Commonent.Data.Enum;

namespace HanBin.Service.Common
{
    #region 数据库批量执行
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-10-26
    /// 创建记录：数据库批量执行
    /// </summary>
    public class ExecuteDB
    {
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-26
        /// 创建记录：执行有增、删、改操作的方法并且有返回值，带事务控制
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public static T ExecuteTrans<T>(Func<iCMSDbContext, T> func)
        {
            if (func != null)
            {
                using (iCMSDbContext BaseContext = new iCMSDbContext())
                {
                    DbContextTransaction BaseTrans = BaseContext.Database.BeginTransaction();
                    //设置EF执行存储过程超时时间
                    BaseContext.Database.CommandTimeout = 5 * 60 * 1000;
                    try
                    {
                        T result = func(BaseContext);
                        BaseTrans.Commit();
                        return result;
                    }
                    catch (Exception e)
                    {
                        BaseTrans.Rollback();
                        throw e;
                    }
                }
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-26
        /// 创建记录：执行没有增、删、改操作的方法并且有返回值，无事务控制
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public static T ExecuteNotTrans<T>(Func<iCMSDbContext, T> func)
        {
            if (func != null)
            {
                using (iCMSDbContext BaseContext = new iCMSDbContext())
                {
                    try
                    {
                        return func(BaseContext);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-26
        /// 创建记录：执行有增、删、改操作的方法并且没有返回值，带事务控制
        /// </summary>
        /// <param name="func"></param>
        public static void ExecuteTrans(Action<iCMSDbContext> func)
        {
            if (func != null)
            {
                using (iCMSDbContext BaseContext = new iCMSDbContext())
                {
                    DbContextTransaction BaseTrans = BaseContext.Database.BeginTransaction();
                    try
                    {
                        func(BaseContext);
                        BaseTrans.Commit();
                    }
                    catch (Exception e)
                    {
                        BaseTrans.Rollback();
                        throw e;
                    }
                }
            }
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-26
        /// 创建记录：执行没有增、删、改操作的方法并且没有返回值，无事务控制
        /// </summary>
        /// <param name="func"></param>
        public static void ExecuteNotTrans(Action<iCMSDbContext> func)
        {
            if (func != null)
            {
                using (iCMSDbContext BaseContext = new iCMSDbContext())
                {
                    try
                    {
                        func(BaseContext);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
        }
    }
    #endregion

    #region iCMSDbContext扩展
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-10-27
    /// 创建记录：iCMSDbContext扩展
    /// </summary>
    public static class DbSetExtensions
    {
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-27
        /// 创建记录：根据条件查询数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate">要查询数据的条件</param>
        /// <returns></returns>
        public static IQueryable<TEntity> GetDatas<TEntity>(this DbSet<TEntity> source, iCMSDbContext context, Expression<Func<TEntity, bool>> predicate) where TEntity : EntityBase
        {
            return context.Set<TEntity>().Where(predicate);
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-27
        /// 创建记录：添加一条数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="entity">要添加的对象</param>
        /// <returns></returns>
        public static OperationResult AddNew<TEntity>(this DbSet<TEntity> source, iCMSDbContext context, TEntity entity) where TEntity : EntityBase
        {
            context.Entry(entity).State = EntityState.Added;
            context.SaveChanges();
            return new OperationResult(EnumOperationResultType.Success, "操作成功！");
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-27
        /// 创建记录：添加多条数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="entities">要添加的对象集合</param>
        /// <returns></returns>
        public static OperationResult AddNew<TEntity>(this DbSet<TEntity> source, iCMSDbContext context, IEnumerable<TEntity> entities) where TEntity : EntityBase
        {
            //不跟踪变化，提高效率
            context.Configuration.AutoDetectChangesEnabled = false;
            foreach (TEntity entity in entities)
            {
                context.Entry(entity).State = EntityState.Added;
            }
            context.SaveChanges();
            return new OperationResult(EnumOperationResultType.Success, "操作成功！");
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-27
        /// 创建记录：修改一条数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="entity">要修改的对象</param>
        /// <returns></returns>
        public static OperationResult Update<TEntity>(this DbSet<TEntity> source, iCMSDbContext context, TEntity entity) where TEntity : EntityBase
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
            return new OperationResult(EnumOperationResultType.Success, "操作成功！");
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-27
        /// 创建记录：修改多条数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="entities">要修改的对象集合</param>
        /// <returns></returns>
        public static OperationResult Update<TEntity>(this DbSet<TEntity> source, iCMSDbContext context, IEnumerable<TEntity> entities) where TEntity : EntityBase
        {
            //不跟踪变化，提高效率
            context.Configuration.AutoDetectChangesEnabled = false;
            foreach (TEntity entity in entities)
            {
                context.Entry(entity).State = EntityState.Modified;
            }
            context.SaveChanges();
            return new OperationResult(EnumOperationResultType.Success, "操作成功！");
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-27
        /// 创建记录：删除多条数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="entities">要删除的对象集合</param>
        /// <returns></returns>
        public static OperationResult Delete<TEntity>(this DbSet<TEntity> source, iCMSDbContext context, IEnumerable<TEntity> entities) where TEntity : EntityBase
        {
            //不跟踪变化，提高效率
            context.Configuration.AutoDetectChangesEnabled = false;
            foreach (TEntity entity in entities)
            {
                context.Entry(entity).State = EntityState.Deleted;
            }
            context.SaveChanges();
            return new OperationResult(EnumOperationResultType.Success, "操作成功！");
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-27
        /// 创建记录：根据条件删除数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate">要删除数据的条件</param>
        /// <returns></returns>
        public static OperationResult Delete<TEntity>(this DbSet<TEntity> source, iCMSDbContext context, Expression<Func<TEntity, bool>> predicate) where TEntity : EntityBase
        {
            return Delete<TEntity>(source, context, GetDatas<TEntity>(source, context, predicate));
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-27
        /// 创建记录：删除一条数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="entity">要删除的对象</param>
        /// <returns></returns>
        public static OperationResult Delete<TEntity>(this DbSet<TEntity> source, iCMSDbContext context, TEntity entity) where TEntity : EntityBase
        {
            context.Entry(entity).State = EntityState.Deleted;
            context.SaveChanges();
            return new OperationResult(EnumOperationResultType.Success, "操作成功！");
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-14
        /// 创建记录：根据主键删除数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="ID">要删除的对象的主键ID</param>
        /// <returns></returns>
        public static OperationResult Delete<TEntity>(this DbSet<TEntity> source, iCMSDbContext context, int ID) where TEntity : EntityBase
        {
            return Delete(source, context, GetByKey(source, context, ID));
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-27
        /// 创建记录：根据主键返回一条数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="key">主键</param>
        /// <returns></returns>
        public static TEntity GetByKey<TEntity>(this DbSet<TEntity> source, iCMSDbContext context, object key) where TEntity : EntityBase
        {
            return context.Set<TEntity>().Find(key);
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：直接执行SQL
        /// </summary>
        /// <param name="source"></param>
        /// <param name="context"></param>
        /// <param name="sql">要直接执行的SQL</param>
        /// <returns></returns>
        public static OperationResult ExecuteSqlCommand<TEntity>(this DbSet<TEntity> source, iCMSDbContext context, string sql) where TEntity : EntityBase
        {
            context.Database.ExecuteSqlCommand(sql);
            return new OperationResult(EnumOperationResultType.Success, "操作成功！");
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：直接执行SQL
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="context"></param>
        /// <param name="sql">要直接执行的SQL</param>
        /// <param name="para">SQL的参数</param>
        /// <returns></returns>
        public static OperationResult ExecuteSqlCommand<TEntity>(this DbSet<TEntity> source, iCMSDbContext context, string sql, SqlParameter[] para) where TEntity : EntityBase
        {
            context.Database.ExecuteSqlCommand(sql, para);
            return new OperationResult(EnumOperationResultType.Success, "操作成功！");
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：执行SQl，返回对象
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="context"></param>
        /// <param name="sql">要直接执行的SQL</param>
        /// <returns></returns>
        public static OperationResult SqlQuery<TEntity>(this DbSet<TEntity> source, iCMSDbContext context, string sql) where TEntity : EntityBase
        {
            var sqlQuery = context.Database.SqlQuery<TEntity>(sql).ToList();
            return new OperationResult(EnumOperationResultType.Success, "操作成功！", sqlQuery);
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：执行SQl，返回对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="context"></param>
        /// <param name="sql">要直接执行的SQL</param>
        /// <param name="para">SQL的参数</param>
        /// <returns></returns>
        public static OperationResult SqlQuery<TEntity>(this DbSet<TEntity> source, iCMSDbContext context, string sql, SqlParameter[] para) where TEntity : EntityBase
        {
            var sqlQuery = context.Database.SqlQuery<TEntity>(sql, para).ToList();
            return new OperationResult(EnumOperationResultType.Success, "操作成功！", sqlQuery);
        }
    }
    #endregion
}