using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Setup.DBUpgrade.UpgradeFM.Common
{
    /// <summary>
    /// 基础的数据操作规范
    /// 与ＯＲＭ架构无关
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity>
           where TEntity : class
    {
        /// <summary>
        /// 设定数据上下文,它一般由构架方法注入
        /// </summary>
        /// <param name="unitOfWork"></param>
        void SetDbContext(IUnitOfWork unitOfWork);

        /// <summary>
        /// 添加实体并提交到数据服务器
        /// </summary>
        /// <param name="item">Item to add to repository</param>
        void Insert(TEntity item);

        /// <summary>
        /// 移除实体并提交到数据服务器
        /// 如果表存在约束，需要先删除子表信息
        /// </summary>
        /// <param name="item">Item to delete</param>
        void Delete(TEntity item);

        /// <summary>
        /// 修改实体并提交到数据服务器
        /// </summary>
        /// <param name="item"></param>
        void Update(TEntity item);

        /// <summary>
        /// 得到指定的实体集合（延时结果集）
        /// Get all elements of type {T} in repository
        /// </summary>
        /// <returns>List of selected elements</returns>
        IQueryable<TEntity> GetModel();

        /// <summary>
        /// 根据主键得到实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Find(params object[] id);
    }
}
