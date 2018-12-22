/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 *文件名：  GetMeasureSiteDetailInfoParameter 
 *创建人：  王颖辉
 *创建时间：2017/9/29 17:15:21 
 *描述：获取测量位置详细信息
/************************************************************************************/

using iCMS.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Request.DevicesConfig
{
    #region 获取测量位置详细信息
    /// <summary>
    /// 获取测量位置详细信息
    /// </summary>
    public class GetMeasureSiteDetailInfoParameter:BaseRequest
    {

        /// <summary>
        /// 测点ID
        /// </summary>
        public int MeasureSiteID
        {
            get;
            set;
        }


        /// <summary>
        /// 1:定时，2：临时
        /// </summary>
        public int DAQStyle
        {
            get;
            set;
        }

    }
    #endregion
}
