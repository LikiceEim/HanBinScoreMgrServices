/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 *文件名：  SignalAlmRecordForDeviceTreeParameter
 *创建人：  张辽阔
 *创建时间：2016-11-02
 *描述：添加特征值及报警值信息参数
/************************************************************************************/

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.DevicesConfig
{
    #region 添加特征值及报警值信息参数
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-11-02
    /// 创建记录：添加特征值及报警值信息参数
    /// </summary>
    public class AddSignalAlmRecordForDeviceTreeParameter : BaseRequest
    {
        /// <summary>
        /// ID
        /// </summary>
        public int SignalAlmID { set; get; }

        /// <summary>
        /// 信号ID
        /// </summary>
        public int SignalID { set; get; }

        /// <summary>
        /// 特征值类型
        /// </summary>
        public int ValueType { set; get; }

        /// <summary>
        /// 高报阈值
        /// </summary>
        public string WarnValue { set; get; }

        /// <summary>
        /// 高高报阈值
        /// </summary>
        public string AlmValue { set; get; }

        /// <summary>
        /// 上传触发值
        /// </summary>
        public float? UploadTrigger { set; get; }

        /// <summary>
        /// 趋势报警预值
        /// </summary>
        public float? ThrendAlarmPrvalue { set; get; }
    }
    #endregion
}