/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.DevicesConfig
 *文件名：  GetWSDataByWGIDResult 
 *创建人：  王颖辉
 *创建时间：2017/9/28 14:33:25 
 *描述：获取网关下的传感器返回值
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.DevicesConfig
{

    #region 获取网关下的传感器返回值
    /// <summary>
    /// 获取网关下的传感器返回值
    /// </summary>
    public class GetWSDataByWGIDResult
    {

        /// <summary>
        /// 
        /// </summary>
        public List<WSInfo> WSInfoList
        {
            get;
            set;
        }


    }
    #endregion

    public class WSInfo
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
        /// 无线传感器编号
        /// </summary>
        public int WSNO
        {
            get;
            set;
        }


        /// <summary>
        /// 传感器名称
        /// </summary>
        public string WSName
        {
            get;
            set;
        }

        /// <summary>
        /// 传感器类型名称
        /// </summary>
        public string SensorTypeName
        {
            get;
            set;
        }
    }
}
