/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.WirelessDevicesConfig
 *文件名：  GetWSDataResult
 *创建人：  张辽阔
 *创建时间：2016-10-28
 *描述：无线传感器返回结果类
/************************************************************************************/

using iCMS.Common.Component.Tool;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace iCMS.Common.Component.Data.Response.WirelessDevicesConfig
{
    #region 无线传感器返回结果类
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-10-28
    /// 创建记录：无线传感器返回结果类
    /// </summary>
    public class GetWSDataResult
    {
        /// <summary>
        /// 无线传感器返回实体类集合
        /// </summary>
        public List<GetWSDataInfo> WSInfo { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }
    }
    #endregion

    #region 无线传感器返回实体类
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-10-28
    /// 创建记录：无线传感器返回实体类
    /// </summary>
    public class GetWSDataInfo
    {
        /// <summary>
        /// 无线传感器ID
        /// </summary>
        public int WSID { get; set; }

        /// <summary>
        /// 挂靠网关
        /// </summary>
        public int WGID { get; set; }

        /// <summary>
        /// 挂靠网关名称
        /// </summary>
        public string WGName { get; set; }

        /// <summary>
        /// 无线传感器编号
        /// </summary>
        public int WSNO { get; set; }

        /// <summary>
        /// 无线传感器名称
        /// </summary>
        public string WSName { get; set; }

        /// <summary>
        /// 电池电压
        /// </summary>
        public float BatteryVolatage { get; set; }

        /// <summary>
        /// 使用状态（0未使用，1使用）
        /// </summary>
        public int UseStatus { get; set; }

        /// <summary>
        /// 报警状态
        /// </summary>
        public int AlmStatus { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string MACADDR { get; set; }

        /// <summary>
        /// 传感器类型ID
        /// </summary>
        public int SensorTypeId { get; set; }

        /// <summary>
        /// 连接状态
        /// </summary>
        public int LinkStatus { get; set; }

        /// <summary>
        /// 升级状态ID
        /// </summary>
        public int OperationStatus { get; set; }

        /// <summary>
        /// 升级状态名称
        /// </summary>
        public string OperationStatusName { get; set; }

        /// <summary>
        /// 传感器类型
        /// </summary>
        public string SensorTypeName { get; set; }

        /// <summary>
        /// 添加日期
        /// </summary>
        public string AddData { get; set; }

        /// <summary>
        /// 厂商
        /// </summary>
        public string Vendor { get; set; }

        /// <summary>
        /// 传感器型号
        /// </summary>
        public string WSModel { get; set; }

        /// <summary>
        /// 安装时间
        /// </summary>
        public string SetupTime { get; set; }

        /// <summary>
        /// 安装负责人
        /// </summary>
        public string SetupPersonInCharge { get; set; }

        /// <summary>
        /// SN码
        /// </summary>
        public string SNCode { get; set; }

        /// <summary>
        /// 固件版本
        /// </summary>
        public string FirmwareVersion { get; set; }

        /// <summary>
        /// 防爆认证编号
        /// </summary>
        public string AntiExplosionSerialNo { get; set; }

        /// <summary>
        /// 运行状态
        /// </summary>
        public int RunStatus { get; set; }

        /// <summary>
        /// 图片ID
        /// </summary>
        public int ImageID { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public string PersonInCharge { get; set; }

        /// <summary>
        /// 负责人电话
        /// </summary>
        public string PersonInChargeTel { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// WS设备形态
        /// </summary>
        public int WSFormType { get; set; }
        /// <summary>
        /// 轴向
        /// </summary>
        public int? Axial { get; set; }
        /// <summary>
        /// 轴向名称
        /// </summary>
        public string AxialName { get; set; }

        public int? ChannelID { get; set; }
        /// <summary>
        /// WS采集类型
        /// </summary>
        public int? SensorCollectType { get; set; }
    }
    #endregion

    #region 无线传感器返回实体类
    /// <summary>
    /// 无线传感器返回实体类
    /// </summary>
    public class WSInfo
    {
        /// <summary>
        /// 无线传感器ID
        /// </summary>
        public int WSID { get; set; }

        /// <summary>
        /// 挂靠网关
        /// </summary>
        public int WGID { get; set; }

        /// <summary>
        /// 挂靠网关名称
        /// </summary>
        public string WGName { get; set; }

        /// <summary>
        /// 无线传感器编号
        /// </summary>
        public int WSNO { get; set; }

        /// <summary>
        /// 无线传感器名称
        /// </summary>
        public string WSName { get; set; }

        /// <summary>
        /// 电池电压
        /// </summary>
        public float BatteryVolatage { get; set; }

        /// <summary>
        /// 使用状态（0未使用，1使用）
        /// </summary>
        public int UseStatus { get; set; }

        /// <summary>
        /// 报警状态
        /// </summary>
        public int AlmStatus { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string MACADDR { get; set; }

        /// <summary>
        /// 传感器类型ID
        /// </summary>
        public int SensorTypeId { get; set; }

        /// <summary>
        /// 连接状态
        /// </summary>
        public int LinkStatus { get; set; }

        /// <summary>
        /// 升级状态ID
        /// </summary>
        public int OperationStatus { get; set; }

        /// <summary>
        /// 升级状态名称
        /// </summary>
        public string OperationStatusName { get; set; }

        /// <summary>
        /// 传感器类型
        /// </summary>
        public string SensorTypeName { get; set; }

        /// <summary>
        /// 添加日期
        /// </summary>
        public string AddData { get; set; }

        /// <summary>
        /// 厂商
        /// </summary>
        public string Vendor { get; set; }

        /// <summary>
        /// 传感器型号
        /// </summary>
        public string WSModel { get; set; }

        /// <summary>
        /// 安装时间
        /// </summary>
        public string SetupTime { get; set; }

        /// <summary>
        /// 安装负责人
        /// </summary>
        public string SetupPersonInCharge { get; set; }

        /// <summary>
        /// SN码
        /// </summary>
        public string SNCode { get; set; }

        /// <summary>
        /// 固件版本
        /// </summary>
        public string FirmwareVersion { get; set; }

        /// <summary>
        /// 防爆认证编号
        /// </summary>
        public string AntiExplosionSerialNo { get; set; }

        /// <summary>
        /// 运行状态
        /// </summary>
        public int RunStatus { get; set; }

        /// <summary>
        /// 图片ID
        /// </summary>
        public int ImageID { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public string PersonInCharge { get; set; }

        /// <summary>
        /// 负责人电话
        /// </summary>
        public string PersonInChargeTel { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
    #endregion

    #region 无线传感器返回结果类
    /// <summary>
    /// 创建人：王颖辉
    /// 创建时间：2016-11-29
    /// 创建记录：无线传感器集合返回结果类
    /// </summary>
    public class AddWSListDataResult
    {
        public List<WSListData> WSListData
        {
            get;
            set;
        }
    }

    #region 返回集合
    /// <summary>
    /// 返回集合
    /// </summary>
    public class WSListData
    {

        public WSListData()
        {
            this.Code = string.Empty;
        }

        private string ErrorCode = string.Empty;

        /// <summary>返回结果操作代码，当isSuccessfull为True此字段为空串，不需要此字段请赋值 null
        /// </summary>
        public string Code
        {
            get
            {
                return ErrorCode;
            }
            set
            {
                ErrorCode = value;
                if (ConstObject.ErrorCode.Count <= 0)
                {
                    InitErrorCode();
                }

                if (!string.IsNullOrEmpty(value))
                {
                    object message = string.Empty;
                    //string json = JsonConvert.SerializeObject(ConstObject.ErrorCode);
                    //LogHelper.WriteLog("输出日志json" + json);
                    //LogHelper.WriteLog("输出日志"+value);
                    if (ConstObject.ErrorCode.TryGetValue(value, out message))
                    {
                        this.Reason = message.ToString();
                    }
                    else
                    {
                        this.Reason = "发生未知错误，请与管理员联系";
                    }
                }
                else
                {
                    this.Reason = null;
                }
            }
        }

        /// <summary>
        /// 不需要手动赋值
        /// 返回结果描述信息，当isSuccessfull为True此字段为空串，不需要此字段请赋值  null
        /// </summary>
        public string Reason { get; private set; }


        /// <summary>
        /// 初始化iCMS错误代码
        /// </summary>
        private void InitErrorCode()
        {
            try
            {
                StreamReader streamReader = new StreamReader(System.AppDomain.CurrentDomain.BaseDirectory + @"\Resource\ErrorCode.json", System.Text.Encoding.Default);

                ConstObject.ErrorCode = JsonConvert.DeserializeObject<Dictionary<string, object>>(streamReader.ReadToEnd());
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
            }
        }

        /// <summary>
        /// 索引
        /// </summary>
        public int Index
        {
            get;
            set;
        }
    }
    #endregion

    #endregion

    #region 可用传感器列表

    public class AllUsableWSResult
    {
        public List<UsableWS> AllUsableWS { get; set; }
    }

    public class UsableWS
    {
        public int WSID { get; set; }

        public int WGID { get; set; }

        public string WSName { get; set; }
    }

    #endregion

    #region ws升级状态
    /// <summary>
    /// ws升级状态
    /// </summary>
    public class WSUpdateStatus
    {
        /// <summary>
        /// 升级状态
        /// </summary>
        public string OperationStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 升级状态名称
        /// </summary>
        public string OperationStatusName
        {
            get;
            set;
        }

        /// <summary>
        /// 结束操作时间
        /// </summary>
        public DateTime? EDate { get; set; }
    }
    #endregion

}