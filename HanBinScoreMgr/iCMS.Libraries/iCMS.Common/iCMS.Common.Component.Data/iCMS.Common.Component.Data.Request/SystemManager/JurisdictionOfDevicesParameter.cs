/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request
 *文件名：  JurisdictionOfDevicesParameter
 *创建人：  LF
 *创建时间：2016/2/15 10:10:19
 *描述：用户管理设备范围参数
 *=====================================================================**/
using System.Collections.Generic;
using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.SystemManager
{
    #region 设备权限
    /// <summary>
    /// 设备权限
    /// </summary>
    public class JurisdictionOfDevicesParameter : BaseRequest
    {

        /// <summary>
        /// 管理设备ID集合
        /// </summary>
        public List<int> DevicesID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
    }
    #endregion
}
