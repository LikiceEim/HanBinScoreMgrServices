/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.WirelessSensors
 *文件名：  CheckWSVersionResult
 *创建人：  LF
 *创建时间：2016-09-05
 *描述：检测无线传感器版本结果类
 *
/************************************************************************************/

namespace iCMS.Common.Component.Data.Response.WirelessSensors
{
    #region 检测无线传感器版本结果类
    /// <summary>
    /// 检测无线传感器版本结果类
    /// </summary>
    public class CheckWSVersionResult
    {

        /// <summary>
        /// 一致位置列表用逗号隔开
        /// </summary>
        public string WSIDs
        {
            get;
            set;
        }
    }
    #endregion

}
