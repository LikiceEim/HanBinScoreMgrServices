/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DiagnosticControl
 *文件名：  DeviceInfoParameter
 *创建人：  王颖辉
 *创建时间：2016-10-20
 *描述：获取设备信息
/************************************************************************************/

using iCMS.Common.Component.Data.Base;
namespace iCMS.Common.Component.Data.Request.DiagnosticControl
{
    #region 获取振动信号类型
    /// <summary>
    /// 获取振动信号类型
    /// </summary>
    public class SignalTypeParameter: BaseRequest
    {
        public string ChartID
        {
            get;
            set;
        }

        /// <summary>
        /// 无用 兼容iPVP
        /// 1振动趋势图
        /// 2 波形频谱图
        /// </summary>
        public int Type
        {
            get;
            set;
        }
    }
    #endregion
}
