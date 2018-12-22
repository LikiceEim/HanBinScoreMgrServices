/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.SystemManager
 *文件名：  UserLogResult
 *创建人：  LF
 *创建时间：2016-10-26
 *描述：用户日志数据返回结果
/************************************************************************************/
using System.Collections.Generic;
using iCMS.Common.Component.Data.Base.DB;

namespace iCMS.Common.Component.Data.Response.SystemManager
{
    #region 工作记录查看返回结果包装类
    /// <summary>
    /// 工作记录查看返回结果包装类
    /// </summary>
    public class UserLogResult
    {
        public List<UserLogInfo> UserLogInfo { get; set; }

        public int Total { get; set; }



        public UserLogResult()
        {
            UserLogInfo = new List<UserLogInfo>();
        }
    }
    #endregion

    #region 用户日志
    /// <summary>
    /// 修改人：张辽阔
    /// 修改时间：2016-09-06
    /// 修改记录：增加IsDeleted字段
    /// </summary>
    public class UserLogInfo : EntityBase
    {
        public int UserLogID { get; set; }

        public int UserID { get; set; }

        public string UserName { get; set; }

        public string Record { get; set; }

        //public string AddDate { get; set; }

        /// <summary>
        /// 是否删除false否，true是
        /// </summary>
        public bool IsDeleted { get; set; }
    }
    #endregion
}

