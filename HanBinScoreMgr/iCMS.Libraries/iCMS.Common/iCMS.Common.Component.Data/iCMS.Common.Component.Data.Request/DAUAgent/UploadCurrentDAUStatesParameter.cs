/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Request.DAUAgent
 * 文件名：  UploadCurrentDAUStatesParameter
 * 创建人：  QXM
 * 创建时间：2018/01/11
 * 描述：    DAUAgent属性数据上传请求参数
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.DAUAgent
{
    public class UploadCurrentDAUStatesParameter : BaseRequest
    {
        /// <summary>
        /// DAUID
        /// </summary>
        public int DAUID { get; set; }

        /// <summary>
        /// 采集单元当前状态 
        /// </summary>
        public int? CurrentDAUStates { get; set; }
    }
}