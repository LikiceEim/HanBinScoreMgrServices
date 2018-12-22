using iCMS.Setup.DBUpgrade.UpgradeFM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Setup.DBUpgrade.UpgradeFM.DB
{
    /// <summary>
    /// Linq架构里对集合排序实现
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Orderable<TEntity> : IOrderable<TEntity> where TEntity : class
    {
        private IQueryable<TEntity> _queryable;

        /// <summary>
        /// 排序后的结果集
        /// </summary>
        /// <param name="enumerable"></param>
        public Orderable(IQueryable<TEntity> enumerable)
        {
            _queryable = enumerable;
        }

        /// <summary>
        /// 排序之后的结果集
        /// </summary>
        public IQueryable<TEntity> Queryable
        {
            get { return _queryable; }
        }
        /// <summary>
        /// 递增
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public IOrderable<TEntity> Asc<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            _queryable = (_queryable as IOrderedQueryable<TEntity>)
                .OrderBy(keySelector);
            return this;
        }
        /// <summary>
        /// 然后递增
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public IOrderable<TEntity> ThenAsc<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            _queryable = (_queryable as IOrderedQueryable<TEntity>)
                .ThenBy(keySelector);
            return this;
        }
        /// <summary>
        /// 递减
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public IOrderable<TEntity> Desc<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            _queryable = _queryable
                .OrderByDescending(keySelector);
            return this;
        }
        /// <summary>
        /// 然后递减
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public IOrderable<TEntity> ThenDesc<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            _queryable = (_queryable as IOrderedQueryable<TEntity>)
                .ThenByDescending(keySelector);
            return this;
        }
    }
}
