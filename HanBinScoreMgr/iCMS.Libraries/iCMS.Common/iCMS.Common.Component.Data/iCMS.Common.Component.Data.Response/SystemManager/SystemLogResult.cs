/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.SystemManager
 *文件名：  SystemLogResult
 *创建人：  LF
 *创建时间：2016-10-26
 *描述：系统日志数据返回结果
/************************************************************************************/

using System.Collections.Generic;
using iCMS.Common.Component.Data.Base.DB;

namespace iCMS.Common.Component.Data.Response.SystemManager
{
    #region 系统日志返回类
    /// <summary>
    /// 系统日志返回类
    /// </summary>
    public class SystemLogResult
    {
        public List<SysLogInfo> SysLogInfo { get; set; }

        public int Total { get; set; }

        public string Reason { get; set; }
    }
    #endregion

    #region 系统日志实体类
    /// <summary>
    /// 系统日志实体类
    /// 修改人：张辽阔
    /// 修改时间：2016-09-06
    /// 修改记录：增加IsDeleted字段
    /// </summary>
    public class SysLogInfo : EntityBase
    {
        public int SysLogID { get; set; }

        public int UserID { get; set; }

        public string UserName { get; set; }

        public string Record { get; set; }

        /// <summary>
        /// 是否删除false否，true是
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-08
        /// 创建记录：IP地址
        /// </summary>
        public string IPAddress { get; set; }
    }

    #endregion
}