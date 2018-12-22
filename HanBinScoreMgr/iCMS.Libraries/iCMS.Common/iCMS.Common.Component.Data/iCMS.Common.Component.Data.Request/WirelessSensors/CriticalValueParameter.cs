/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.WirelessSensorst
 *文件名：  CriticalValueParameter
 *创建人：  LF
 *创建时间：2016/10/19 10:10:19
 *描述：无线传感器上传启停机信息实体类
 *=====================================================================**/

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.WirelessSensors
{
    #region 无线传感器上传启停机信息实体类
    /// <summary>
    /// 无线传感器上传启停机信息实体类
    /// </summary>
    public class CriticalValueParameter : BaseRequest
    {
        /// <summary>
        /// 采集方式 1：定时采集；2：临时采集
        /// </summary>
        public string DAQStyle { get; set; }

        /// <summary>
        /// 采集时间
        /// </summary>
        public System.DateTime SamplingTime { get; set; }
        /// <summary>
        /// WS MAC地址
        /// </summary>
        public string WSMAC { get; set; }

        /// <summary>
        /// 启停机信号类型，8：启停机
        /// </summary>
        public int SignalType { get; set; }

        /// <summary>
        /// 启停机特征值
        /// 1：RMS（有效值）；2：PK（峰值）；4：PKPK（峰峰值）；8：PKC（地毯值）；10：轴承状态（轴承状态）
        /// </summary>
        public int EigenType { get; set; }

        /// <summary>
        /// 特征值Value
        /// </summary>
        public float EigenValue { get; set; }
    }
    #endregion
}