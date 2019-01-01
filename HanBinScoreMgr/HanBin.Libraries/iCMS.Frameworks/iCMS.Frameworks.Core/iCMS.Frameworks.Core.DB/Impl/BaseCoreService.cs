using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Enum;
using iCMS.Common.Component.Data.Base.DB;

namespace iCMS.Frameworks.Core.DB.Impl
{
    public abstract class BaseCoreService<TEntity> : IBaseCoreService<TEntity> where TEntity : EntityBase
    {
        public BaseCoreService()
        {

        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public virtual void Dispose()
        {
            //Context.Dispose();
        }


        /// <summary>
        ///   为指定的类型返回 System.Data.Entity.DbSet，这将允许对上下文中的给定实体执行 CRUD 操作。
        /// </summary>
        /// <typeparam name="TEntity"> 应为其返回一个集的实体类型。 </typeparam>
        /// <returns> 给定实体类型的 System.Data.Entity.DbSet 实例。 </returns>
        private DbSet<TEntity> Set<TEntity>(iCMSDbContext ctx) where TEntity : EntityBase
        {
            return ctx.Set<TEntity>();
        }

        private IQueryable<TEntity> Set<TEntity>(iCMSDbContext ctx, Expression<Func<TEntity, bool>> predicate) where TEntity : EntityBase
        {
            return ctx.Set<TEntity>().Where(predicate).AsQueryable();
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <typeparam name="TEntity"></typeparam>
        ///// <param name="isTracking">是否追踪</param>
        ///// <returns></returns>
        //public virtual IQueryable<TEntity> GetDatas<TEntity>(bool isTracking) where TEntity : EntityBase
        //{
        //    using (iCMSDbContext ctx = new iCMSDbContext())
        //    {
        //        return Set<TEntity>(ctx).ToList().AsQueryable();
        //    }
        //}

        //add by iLine 20160427 增加条件查询
        /// <summary>
        /// 根据条件获取数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="isTracking"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetDatas<TEntity>(Expression<Func<TEntity, bool>> predicate, bool isTracking) where TEntity : EntityBase
        {
            using (iCMSDbContext ctx = new iCMSDbContext())
            {
                if (predicate != null)
                {
                    return ctx.Set<TEntity>().Where(predicate).ToList().AsQueryable();
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="TEntity"> 类型 </typeparam>
        /// <param name="entity"> 对象 </param>
        public virtual OperationResult AddNew<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            string exMsg = string.Empty;
            try
            {
                using (iCMSDbContext ctx = new iCMSDbContext())
                {
                    EntityState state = ctx.Entry(entity).State;
                    if (state == EntityState.Detached)
                    {
                        ctx.Entry(entity).State = EntityState.Added;
                    }
                    ctx.SaveChanges();


                    //try
                    //{
                    //    #region 云推送添加
                    //    BaseCloudService cloud = BaseCloudService.GetInstance();
                    //    cloud.SyncInfo(entity, EnumCloudDataOperation.Add);
                    //    #endregion
                    //}
                    //catch (Exception ex)
                    //{
                    //    LogHelper.WriteLog(ex);
                    //}
                    //finally
                    //{

                    //}


                    #region 云平台添加 Multi Thread

                    //Thread thread = new Thread(YunyiThreadHelper.YunyiAdd);
                    //thread.Start(entity);

                    #endregion

                    return new OperationResult(EnumOperationResultType.Success, "操作成功！");

                }
            }
            catch (Exception ex)
            {
                exMsg = ex.Message;
            }
            return new OperationResult(EnumOperationResultType.Error, exMsg);

        }
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entities"> 要注册的对象集合 </param>
        public virtual OperationResult AddNew<TEntity>(IEnumerable<TEntity> entities) where TEntity : EntityBase
        {
            string exMsg = string.Empty;
            try
            {
                using (iCMSDbContext ctx = new iCMSDbContext())
                {
                    EntityState state;
                    //不跟踪变化，提高效率
                    ctx.Configuration.AutoDetectChangesEnabled = false;
                    foreach (TEntity entity in entities)
                    {
                        state = ctx.Entry(entity).State;
                        if (state == EntityState.Detached)
                        {
                            ctx.Entry(entity).State = EntityState.Added;
                        }
                    }
                    ctx.SaveChanges();
                    return new OperationResult(EnumOperationResultType.Success, "操作成功！");
                }
            }
            catch (Exception ex)
            {
                exMsg = ex.Message;
            }

            return new OperationResult(EnumOperationResultType.Error, exMsg);

        }

        /// <summary>
        ///  更新
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        public virtual OperationResult Update<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            string exMsg = string.Empty;
            try
            {
                using (iCMSDbContext ctx = new iCMSDbContext())
                {
                    if (ctx.Entry(entity).State == EntityState.Detached)
                    {
                        ctx.Set<TEntity>().Attach(entity);
                    }

                    ctx.Entry(entity).State = EntityState.Modified;
                    ctx.SaveChanges();


                    //try
                    //{
                    //    #region 云推送添加
                    //    BaseCloudService cloud = BaseCloudService.GetInstance();
                    //    cloud.SyncInfo(entity, EnumCloudDataOperation.Update);
                    //    #endregion
                    //}
                    //catch (Exception ex)
                    //{
                    //    LogHelper.WriteLog(ex);
                    //}
                    //finally
                    //{

                    //}


                    #region 云平台修改 Multi Thread

                    //Thread thread = new Thread(YunyiThreadHelper.YunyiUpdate);
                    //thread.Start(entity);

                    #endregion

                    return new OperationResult(EnumOperationResultType.Success, "操作成功！");
                }
            }
            catch (Exception ex)
            {
                exMsg = ex.Message;
            }
            return new OperationResult(EnumOperationResultType.Error, exMsg);
        }

        /// <summary>
        ///  add by masu 2016年3月17日09:10:11 批量更新
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        public virtual OperationResult Update<TEntity>(IEnumerable<TEntity> entities) where TEntity : EntityBase
        {
            string exMsg = string.Empty;
            try
            {
                using (iCMSDbContext ctx = new iCMSDbContext())
                {
                   // EntityState state;
                    //不跟踪变化，提高效率
                    ctx.Configuration.AutoDetectChangesEnabled = false;
                    foreach (TEntity entity in entities)
                    {
                        if (ctx.Entry(entity).State == EntityState.Detached)
                        {
                            ctx.Entry(entity).State = EntityState.Modified;
                        }

                    }

                    ctx.SaveChanges();



                    return new OperationResult(EnumOperationResultType.Success, "操作成功！");
                }
            }
            catch (Exception ex)
            {
                exMsg = ex.Message;
            }
            return new OperationResult(EnumOperationResultType.Error, exMsg);
        }

        /// <summary>
        ///  删除
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        public virtual OperationResult Delete<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            string exMsg = string.Empty;
            try
            {
                using (iCMSDbContext ctx = new iCMSDbContext())
                {
                    ctx.Entry(entity).State = EntityState.Deleted;

                    ctx.SaveChanges();



                    return new OperationResult(EnumOperationResultType.Success, "操作成功！");
                }
            }
            catch (Exception ex)
            {
                exMsg = ex.Message;
            }
            return new OperationResult(EnumOperationResultType.Error, exMsg);
        }


        /// <summary>
        /// 批量删除
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entities"> 要注册的对象集合 </param>
        public virtual OperationResult Delete<TEntity>(IEnumerable<TEntity> entities) where TEntity : EntityBase
        {
            string exMsg = string.Empty;
            try
            {
                using (iCMSDbContext ctx = new iCMSDbContext())
                {
                    foreach (TEntity entity in entities)
                    {
                        ctx.Entry(entity).State = EntityState.Deleted;

                    }
                    ctx.SaveChanges();




                    return new OperationResult(EnumOperationResultType.Success, "操作成功！");
                }
            }
            catch (Exception ex)
            {
                exMsg = ex.Message;
            }
            finally
            {
                // Context.Configuration.AutoDetectChangesEnabled = true;

            }
            return new OperationResult(EnumOperationResultType.Error, exMsg);
        }

        /// <summary>
        ///     删除指定编号的记录
        /// </summary>
        /// <param name="id"> 实体记录编号 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual OperationResult Delete<TEntity>(int id) where TEntity : EntityBase
        {
            using (iCMSDbContext ctx = new iCMSDbContext())
            {
                TEntity entity = Set<TEntity>(ctx).Find(id);
                return Delete<TEntity>(entity);
            }
        }
        /// <summary>
        ///     删除所有符合特定表达式的数据
        /// </summary>
        /// <param name="predicate"> 查询条件谓语表达式 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual OperationResult Delete(Expression<Func<TEntity, bool>> predicate)
        {
            using (iCMSDbContext ctx = new iCMSDbContext())
            {
                List<TEntity> entities = Set<TEntity>(ctx).Where(predicate).ToList();

                return Delete<TEntity>(entities);
            }
        }

        /// <summary>
        /// 查找指定主键的实体记录
        /// </summary>
        /// <param name="key"> 指定主键 </param>
        /// <returns> 符合编号的记录，不存在返回null </returns>
        public virtual TEntity GetByKey(object key)
        {
            using (iCMSDbContext ctx = new iCMSDbContext())
            {
                return Set<TEntity>(ctx).Find(key);
            }
        }
        /// <summary>
        /// 直接执行SQL
        /// </summary>
        /// <param name="sql"></param>
        public OperationResult ExecuteSqlCommand(string sql)
        {
            string exMsg = string.Empty;
            try
            {
                using (iCMSDbContext ctx = new iCMSDbContext())
                {
                    ctx.Database.ExecuteSqlCommand(sql);

                    return new OperationResult(EnumOperationResultType.Success, "操作成功！");
                }
                //Context.Database.ExecuteSqlCommand(sql);

                //return new OperationResult(EnumOperationResultType.Success, "操作成功！");
            }
            catch (Exception ex)
            {
                exMsg = ex.Message;
            }
            return new OperationResult(EnumOperationResultType.Error, exMsg);
        }


        /// <summary>
        /// 直接执行SQL
        /// </summary>
        /// <param name="sql"></param>
        public OperationResult ExecuteSqlCommand(string sql, SqlParameter[] para)
        {
            string exMsg = string.Empty;
            try
            {
                using (iCMSDbContext ctx = new iCMSDbContext())
                {
                    ctx.Database.ExecuteSqlCommand(sql, para);



                    return new OperationResult(EnumOperationResultType.Success, "操作成功！");

                }
                //Context.Database.ExecuteSqlCommand(sql);

                //return new OperationResult(EnumOperationResultType.Success, "操作成功！");
            }
            catch (Exception ex)
            {
                exMsg = ex.Message;
            }
            return new OperationResult(EnumOperationResultType.Error, exMsg);
        }

        /// <summary>
        /// 执行SQl，返回对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public OperationResult SqlQuery<T>(string sql)
        {
            string exMsg = string.Empty;
            try
            {
                using (iCMSDbContext ctx = new iCMSDbContext())
                {
                    var sqlQuery = ctx.Database.SqlQuery<T>(sql).ToList();
                    return new OperationResult(EnumOperationResultType.Success, "操作成功！", sqlQuery);
                }

            }
            catch (Exception ex)
            {
                exMsg = ex.Message;
            }
            return new OperationResult(EnumOperationResultType.Error, exMsg);
        }


        public OperationResult SqlQuery<T>(string sql, SqlParameter[] para)
        {
            string exMsg = string.Empty;
            try
            {
                using (iCMSDbContext ctx = new iCMSDbContext())
                {
                    var sqlQuery = ctx.Database.SqlQuery<T>(sql, para).ToList();
                    return new OperationResult(EnumOperationResultType.Success, "操作成功！", sqlQuery);
                }

            }
            catch (Exception ex)
            {
                exMsg = ex.Message;
            }
            return new OperationResult(EnumOperationResultType.Error, exMsg);
        }
        /// <summary>
        /// 事务操作
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public OperationResult TranMethod(Dictionary<EntityBase, EntityState> models)
        {
            string exMsg = string.Empty;
            try
            {
                using (iCMSDbContext ctx = new iCMSDbContext())
                {
                    foreach (var model in models)
                    {
                        ctx.Entry(model.Key).State = model.Value;
                    }

                    ctx.SaveChanges();

                    return new OperationResult(EnumOperationResultType.Success, "操作成功！");
                }

            }
            catch (Exception ex)
            {
                exMsg = ex.Message;
            }
            finally
            {
                // Context.Configuration.AutoDetectChangesEnabled = true;

            }
            return new OperationResult(EnumOperationResultType.Error, exMsg);
        }
    }
}