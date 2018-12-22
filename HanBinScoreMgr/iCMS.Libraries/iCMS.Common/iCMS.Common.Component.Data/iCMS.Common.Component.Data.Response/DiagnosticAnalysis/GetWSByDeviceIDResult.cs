/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Response.DiagnosticAnalysis
 * 文件名：  GetWSByDeviceIDResult 
 * 创建人：  王颖辉
 * 创建时间：2017/10/14 19:31:02 
 * 描述：通过设备ID获取传感器
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.DiagnosticAnalysis
{
    /// <summary>
    /// 通过设备ID获取传感器
    /// </summary>
    public class GetWSByDeviceIDResult
    {
        /// <summary>
        /// ws信息列表
        /// </summary>
        public List<WSInfoList> WSInfoList
        {
            get;
            set;
        }
    }

    /// <summary>
    /// ws信息列表
    /// </summary>
    public class WSInfoList
    {
        /// <summary>
        /// 无线传感器ID
        /// </summary>
        public int WSID
        {
            get;
            set;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string WSName
        {
            get;
            set;
        }

        /// <summary>
        /// 无线传感器编号
        /// </summary>
        public int WSNO
        {
            get;
            set;
        }

        /// <summary>
        /// SN
        /// </summary>
        public string SN
        {
            get;
            set;
        }

        /// <summary>
        /// 传感器类型
        /// </summary>
        public string SensorTypeName
        {
            get;
            set;
        }

        /// <summary>
        /// 报警状态
        /// </summary>
        public int AlmStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 使用状态
        /// </summary>
        public int UseStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 连接状态
        /// </summary>
        public int LinkStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 运行状态
        /// </summary>
        public int RunStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 传感器类型
        /// </summary>
        public int SensorType
        {
            get;
            set;
        }

        /// <summary>
        /// MAC地址
        /// </summary>
        public string MACADDR
        {
            get;
            set;
        }

        /// <summary>
        /// 设备形态类型：1、单板；2、轻量级网关；3、有线
        /// </summary>
        public int DevFormType
        {
            get;
            set;
        }

        /// <summary>
        /// 1：振动传感器，2：转速传感器，3：油液传感器，4：过程量传感器
        /// </summary>
        public int? SensorCollectType
        {
            get;
            set;
        }
    }
}