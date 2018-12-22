/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Response.DiagnosticAnalysis
 * 文件名：  GetDeviceInfoByWSIDResult 
 * 创建人：  王颖辉
 * 创建时间：2017/10/14 19:08:48 
 * 描述：通过WSID获取挂靠设备信息
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.DiagnosticAnalysis
{
    /// <summary>
    ///通过WSID获取挂靠设备信息
    /// </summary>
    public class GetDeviceInfoByWSIDResult
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public int DeviceID
        {
            get;
            set;
        }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName
        {
            get;
            set;
        }

        /// <summary>
        /// 设备类型
        /// </summary>
        public int DeviceType
        {
            get;
            set;
        }

        /// <summary>
        /// 设备编号
        /// </summary>
        public int DeviceNO
        {
            get;
            set;
        }

        /// <summary>
        /// 监测树节点
        /// </summary>
        public string MonitorTreeRoute
        {
            get;
            set;
        }

        /// <summary>
        /// 设备负责人
        /// </summary>
        public string DeviceManage
        {
            get;
            set;
        }

        /// <summary>
        /// 设备报警状态
        /// </summary>
        public int AlmStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 使用类型
        /// </summary>
        public int UseType
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
        /// 投运时间
        /// </summary>
        public DateTime? OperationDate
        {
            get;
            set;
        }

        /// <summary>
        /// 网关名称
        /// </summary>
        public string WG
        {
            get;
            set;
        }
    }
}