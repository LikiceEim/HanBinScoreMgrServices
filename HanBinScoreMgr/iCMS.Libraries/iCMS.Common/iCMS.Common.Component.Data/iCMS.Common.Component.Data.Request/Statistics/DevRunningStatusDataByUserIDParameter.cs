/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.Statistics
 *文件名：  DevRunningStatusDataByUserIDParameter
 *创建人：  王颖辉
 *创建时间：2016-10-19
 *描述：GetDevRunningStatusDataByUserIDParameter:调用方法
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iCMS.Common.Component.Data.Base;
namespace iCMS.Common.Component.Data.Request.Statistics
{
    #region 通过用户ID获取设备运行状态的统计信息
    /// <summary>
    /// 通过用户ID获取设备运行状态的统计信息
    /// </summary>
    public class DevRunningStatusDataByUserIDParameter : BaseRequest
    {
        public int UserID { get; set; }
    }
    #endregion

}
