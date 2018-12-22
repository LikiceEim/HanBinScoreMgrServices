/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.DiagnosticControl
 *文件名：  DeviceInfoResult
 *创建人：  王颖辉
 *创建时间：2016-10-28
 *描述：设备返回实体相关类
/************************************************************************************/

using System;
using System.Collections.Generic;

namespace iCMS.Common.Component.Data.Response.DiagnosticControl
{
    #region 查看设备信息实体类
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-07-30
    /// 创建记录：查看设备信息实体类
    /// </summary>
    public class DeviceInfoResult
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        public int DevID { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DevName { get; set; }

        /// <summary>
        /// 转速
        /// </summary>
        public float Rotate { get; set; }

        /// <summary>
        /// 功率
        /// </summary>
        public float Power { get; set; }

        /// <summary>
        /// 设备类型名称
        /// </summary>
        public string DevTypeName { get; set; }

        /// <summary>
        /// 泵送介质
        /// </summary>
        public string Media { get; set; }

        /// <summary>
        /// 设备生产厂家
        /// </summary>
        public string DevManufacture { get; set; }

        /// <summary>
        /// 设备联轴器形式
        /// </summary>
        public string CouplingType { get; set; }

        /// <summary>
        /// 设备型号
        /// </summary>
        public string DevModel { get; set; }

        /// <summary>
        /// 测量位置轴承形式
        /// </summary>
        public string MSBraringType { get; set; }

        /// <summary>
        /// 轴承库厂商
        /// </summary>
        public string MSBraringManufacture { get; set; }

        /// <summary>
        /// 测量位置轴承型号
        /// </summary>
        public string MSBraringModel { get; set; }

        /// <summary>
        /// 测量位置润滑形式
        /// </summary>
        public string MSLubricatingForm { get; set; }

        /// <summary>
        /// 转频
        /// </summary>
        public string RotationFrequency { get; set; }


        /// <summary>
        /// 当前请求的控件编号
        /// </summary>
        public string ChartID { get; set; }

        /// <summary>
        /// 轴承库信息
        /// </summary>
        public List<BearingInfoForDeviceResult> BearingInfoList
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 添加轴承信息
    /// </summary>
    public class BearingInfoForDeviceResult
    {

        /// <summary>
        /// 轴承ID
        /// </summary>
        public int BearingID
        {
            get;
            set;
        }

        /// <summary>
        /// 轴承厂商ID
        /// </summary>
        public string FactoryID
        {
            get;
            set;
        }

        /// <summary>
        /// 轴承形式
        /// </summary>
        public string BearingType
        {
            get;
            set;
        }


        /// <summary>
        /// 润滑形式
        /// </summary>
        public string LubricatingForm
        {
            get;
            set;
        }

        /// <summary>
        /// 轴承型号
        /// </summary>
        public string BearingNum
        {
            get;
            set;
        }


        /// <summary>
        /// 轴承厂商
        /// </summary>
        public string FactoryName
        {
            get;
            set;
        }

    }


    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-08-01
    /// 创建记录：波形图实体类
    /// </summary>
    public class WaveDataResult
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public int DevID { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DevName { get; set; }

        /// <summary>
        /// 测量位置ID
        /// </summary>
        public int MsiteID { get; set; }

        /// <summary>
        /// 测量位置名称
        /// </summary>
        public string MsiteName { get; set; }

        /// <summary>
        /// 振动信号类型
        /// </summary>
        public int SignalType { get; set; }

        /// <summary>
        /// 振动信号名称
        /// </summary>
        public string SignalName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 波形采集时间
        /// </summary>
        public List<string> SamplingDate { get; set; }

        /// <summary>
        /// 采集点数
        /// </summary>
        public int SamplingPointData { get; set; }

        /// <summary>
        /// X轴数据
        /// </summary>
        public List<double> XData { get; set; }

        /// <summary>
        /// Y轴数据
        /// </summary>
        public List<double> YData { get; set; }

        /// <summary>
        /// 采集方式：1.定时采集，2.临时采集
        /// </summary>
        public string DAQStyle { get; set; }

        /// <summary>
        /// 峰值集合 对应于1.4版本的E1
        /// </summary>
        public List<float?> PKList { get; set; }

        /// <summary>
        /// 返回状态
        /// </summary>
        public List<int> EStatus { get; set; }

        /// <summary>
        /// 峰峰值集合 对应于1.4版本的E2
        /// </summary>
        public List<float?> DPKList { get; set; }

        /// <summary>
        /// 有效值集合 对应于1.4版本的E3
        /// </summary>
        public List<float?> EFFList { get; set; }

        /// <summary>
        /// 地毯值集合 对应于1.4版本的E4
        /// </summary>
        public List<float?> CaptList { get; set; }

        /// <summary>
        /// 轴承状态采集值集合 对应于1.4版本的E5
        /// </summary>
        public List<float?> LQList { get; set; }

        /// <summary>
        /// 可扩展值集合  对应于1.4版本的E6
        /// </summary>
        public List<float?> E1 { get; set; }






        /// <summary>
        /// 可扩展值集合
        /// </summary>
        public string Unit { get; set; }



        /// <summary>
        /// 标题：设备名称+测量位置名称+振动信号名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 当前请求的控件编号
        /// </summary>
        public string ChartID { get; set; }

        /// <summary>
        /// 对应包络时间
        /// </summary>
        public string TimeForEnv { get; set; }

        /// <summary>
        /// 对应速度时间
        /// </summary>
        public string TimeForSpeed { get; set; }

        /// <summary>
        /// 对应加速度时间
        /// </summary>
        public string TimeForACC { get; set; }

        /// <summary>
        /// 对应位移时间
        /// </summary>
        public string TimeForDisp { get; set; }

    }

    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-08-02
    /// 创建记录：频谱图实体类
    /// </summary>
    public class SpectrumDataResult
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public int DevID { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DevName { get; set; }

        /// <summary>
        /// 测量位置ID
        /// </summary>
        public int MSiteID { get; set; }

        /// <summary>
        /// 测量位置名称
        /// </summary>
        public string MSiteName { get; set; }

        /// <summary>
        /// 振动信号类型
        /// </summary>
        public int SignalType { get; set; }

        /// <summary>
        /// 振动信号名称
        /// </summary>
        public string SignalName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 波形采集时间集合
        /// </summary>
        public List<string> SamplingDate { get; set; }

        /// <summary>
        /// 采集点数
        /// </summary>
        public int SamplingPointData { get; set; }

        /// <summary>
        /// X轴数据
        /// </summary>
        public List<double> XData { get; set; }

        /// <summary>
        /// Y轴数据
        /// </summary>
        public List<double> YData { get; set; }

        /// <summary>
        /// 峰值集合 对应于1.4版本的E1
        /// </summary>
        public List<float?> PKList { get; set; }

        /// <summary>
        /// 峰峰值集合 对应于1.4版本的E2
        /// </summary>
        public List<float?> DPKList { get; set; }

        /// <summary>
        /// 有效值集合 对应于1.4版本的E3
        /// </summary>
        public List<float?> EFFList { get; set; }

        /// <summary>
        /// 地毯值集合 对应于1.4版本的E4
        /// </summary>
        public List<float?> CaptList { get; set; }

        /// <summary>
        /// 轴承状态采集值集合 对应于1.4版本的E5
        /// </summary>
        public List<float?> LQList { get; set; }

        /// <summary>
        /// 可扩展值集合 对应于1.4版本的E1
        /// </summary>
        public List<float?> E1 { get; set; }

        /// <summary>
        /// 返回状态
        /// </summary>
        public List<int> EStatus { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 转频
        /// </summary>
        public float RF { get; set; }

        /// <summary>
        /// 多轴承详细信息
        /// </summary>
        public List<BearingInfoData> BearingInfoList { get; set; }

        /// <summary>
        /// 当前请求的控件编号
        /// </summary>
        public string ChartID { get; set; }

        /// <summary>
        /// 标题：设备名称+测量位置名称+振动信号名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 设备转速
        /// </summary>
        public float Rotate { get; set; }
    }

    public class BearingInfoData
    {
        /// <summary>
        /// 当前请求的控件编号
        /// </summary>
        public string ChartID { get; set; }

        /// <summary>
        /// 主键ID
        /// </summary>
        public int BearingID { get; set; }

        /// <summary>
        /// 厂商编号
        /// </summary>
        public string FactoryID { get; set; }

        /// <summary>
        /// 厂商名称
        /// </summary>
        public string FactoryName { get; set; }

        /// <summary>
        /// 轴承型号
        /// </summary>
        public string BearingNum { get; set; }

        /// <summary>
        /// 轴承描述
        /// </summary>
        public string BearingDescribe { get; set; }

        /// <summary>
        /// 轴承特征频率表示ID
        /// </summary>
        public int EigenvalueID { get; set; }

        /// <summary>
        /// 滚球/柱数
        /// </summary>
        public int? BallsNumber { get; set; }

        /// <summary>
        /// 滚球/柱直径
        /// </summary>
        public float? BallDiameter { get; set; }

        /// <summary>
        /// 节圆直径
        /// </summary>
        public float? PitchDiameter { get; set; }

        /// <summary>
        /// 接触角
        /// </summary>
        public float? ContactAngle { get; set; }

        /// <summary>
        /// 轴承外圈特征频率
        /// </summary>
        public float BPFO { get; set; }

        /// <summary>
        /// 轴承内圈特征频率
        /// </summary>
        public float BPFI { get; set; }

        /// <summary>
        /// 轴承滚动体特征平频率
        /// </summary>
        public float FTF { get; set; }

        /// <summary>
        /// 轴承保持架特征频率
        /// </summary>
        public float BSF { get; set; }
    }

    #endregion
}