/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Base.DB
 *文件名：  EntityBase
 *创建人：  王颖辉
 *创建时间：2016-11-15
 *描述：实体基类
/************************************************************************************/
using System;
using System.ComponentModel.DataAnnotations;

namespace iCMS.Common.Component.Data.Base.DB
{
    #region 实体基类
    /// <summary>
    /// 实体基类
    /// </summary>
    public abstract class EntityBase
    {
        #region 构造函数

        /// <summary>
        /// 数据实体基类
        /// </summary>
        protected EntityBase()
        {
            // IsDeleted = false;
            AddDate = DateTime.Now;
        }

        #endregion

        #region 实体属性

        /// <summary>
        /// 获取或设置 添加时间
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime AddDate { get; set; }

        #endregion
    }
    #endregion
}
