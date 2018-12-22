/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.DiagnosticAnalysis
 *文件名：  GetConfigListForDeviceMonitorCenteResult 
 *创建人：  王颖辉
 *创建时间：2017/10/13 16:35:05 
 *描述：系统配置信息通用查询接口
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.DiagnosticAnalysis
{
    /// <summary>
    /// 系统配置信息通用查询接口
    /// </summary>
    public class GetConfigListForDeviceMonitorCenterResult
    {
        /// <summary>
        /// 配置集合
        /// </summary>
        public List<ConfigInfo> ConfigInfoList
        {
            get;
            set;
        }

        /// <summary>
        /// 配置集合
        /// </summary>
        public List<ConfigInfo> MSConfigByDeviceID
        {
            get;
            set;
        }


        /// <summary>
        /// 配置集合
        /// </summary>
        public List<ConfigInfo> TopographicMapSetsConfig
        {
            get;
            set;
        }

        /// <summary>
        /// 配置集合
        /// </summary>
        public List<ConfigInfo> TopographicMapPictureConfig
        {
            get;
            set;
        }
    }
}
