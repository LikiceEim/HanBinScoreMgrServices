/************************************************************************************
 *Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Service.Statistics
 *文件名：  AlarmRemindParameter
 *创建人：  王龙杰
 *创建时间：2017-10-12
 *描述：设备报警提醒
/************************************************************************************/

using iCMS.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Request.Utility
{
    public class MoudleContentDetailParameter : BaseRequest
    {
        /// <summary>
        /// 功能介绍Code
        /// </summary>
        public string Code { get; set; }
    }

    public class AddMoudleContentParameter : BaseRequest
    {
        /// <summary>
        /// 父ID
        /// </summary>
        public int PID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 功能介绍
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 是否显示,add by lwj
        /// </summary>
        public bool IsShow { get; set; }
    }

    public class EditMoudleContentParameter : BaseRequest
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 功能介绍
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 是否显示,add by lwj
        /// </summary>
        public bool IsShow { get; set; }
    }

    public class DeleteMoudleContentParameter : BaseRequest
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
    }
}
