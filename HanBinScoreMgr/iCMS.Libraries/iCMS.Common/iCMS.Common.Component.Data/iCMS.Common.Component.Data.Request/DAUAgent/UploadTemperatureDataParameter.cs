/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Request.DAUAgent
 * 文件名：  UploadTemperatureDataParameter
 * 创建人：  QXM
 * 创建时间：2018/01/11
 * 描述：    DAUAgent温度数据上传请求参数
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.DAUAgent
{
    public class UploadTemperatureDataParameter : BaseRequest
    {
        /// <summary>
        /// DAUID
        /// </summary>
        public int DAUID { get; set; }

        /// <summary>
        /// 采集时间
        /// </summary>
        public DateTime SamplingDate { get; set; }

        /// <summary>
        /// 温度采集值
        /// </summary>
        public List<TemperatureDataInfo> TemperatureDataInfoList { get; set; }

        public UploadTemperatureDataParameter()
        {
            TemperatureDataInfoList = new List<TemperatureDataInfo>();
        }
    }

    public class TemperatureDataInfo
    {
        /// <summary>
        /// 寄存器地址ID
        /// </summary>
        public int TemperatureChannelID { get; set; }

        /// <summary>
        /// 温度采集值
        /// </summary>
        public float TemperatureData { get; set; }
    }

    public class DAUTemperatureParameter
    {
        public DateTime SamplingDate { get; set; }

        public float TemperatureData { get; set; }
    }
}