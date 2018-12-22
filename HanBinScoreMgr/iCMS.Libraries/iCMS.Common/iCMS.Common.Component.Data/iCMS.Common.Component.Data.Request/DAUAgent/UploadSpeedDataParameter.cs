/*************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Request.DAUAgent
 * 文件名：  UploadSpeedDataParameter
 * 创建人：  QXM
 * 创建时间：2018/01/11
 * 描述：    DAUAgent转速数据上传请求参数
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.DAUAgent
{
    public class UploadSpeedDataParameter : BaseRequest
    {
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-18
        /// 创建记录：测量位置ID
        /// </summary>
        public int MSiteID { get; set; }

        /// <summary>
        /// DAUID
        /// </summary>
        public int DAUID { get; set; }

        /// <summary>
        /// 采集时间
        /// </summary>
        public DateTime SamplingDate { get; set; }

        /// <summary>
        /// 采集通道
        /// </summary>
        public int SampleDataChannelID { get; set; }

        /// <summary>
        /// 转速数据
        /// </summary>
        public int SpeedData { get; set; }

        /// <summary>
        /// 线数
        /// </summary>
        public int LineCnt { get; set; }

        /// <summary>
        /// 转速波形数据
        /// </summary>    
        public byte[] SpeedWaveData { get; set; }
    }
}