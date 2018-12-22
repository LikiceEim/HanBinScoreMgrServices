/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 *文件名：  AddMonitorTreeParameter
 *创建人：  LF
 *创建时间：2016-10-19
 *描述：添加监测树，参数类
/************************************************************************************/

using iCMS.Common.Component.Data.Base;
using System;

namespace iCMS.Common.Component.Data.Request.DevicesConfig
{
    #region 监测树信息
    /// <summary>
    /// 监测树信息
    /// </summary>
    public class AddMonitorTreeParameter : BaseRequest
    {
        /// <summary>
        ///  监测树ID	
        /// </summary>
        public int MonitorTreeID { get; set; }

        /// <summary>
        ///	监测树父ID	 
        /// </summary>
        public int PID { get; set; }

        /// <summary>
        /// 源ID	
        /// </summary>
        public int OID { get; set; }

        /// <summary>
        /// 是否系统默认
        /// </summary>
        public int IsDefault { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///  描述
        /// </summary>
        public string Des { get; set; }
        /// <summary>
        /// 类型	
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 添加日期
        /// </summary>
        public DateTime AddDate { get; set; }
        /// <summary>
        /// 对应图片ID	
        /// </summary>
        public int ImageID { get; set; }
        /// <summary>
        /// 状态	0:未采集;1正常;2:高报;3:高高报
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 地址	
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// URL
        /// </summary>
        public string URL { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string TelphoneNO { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        public string FaxNO { get; set; }
        /// <summary>
        /// 维度
        /// </summary>
        public float? Latitude { get; set; }
        /// <summary>
        /// 精度
        /// </summary>
        public float? Longtitude { get; set; }
        /// <summary>
        /// 长	
        /// </summary>
        public float? Length { get; set; }
        /// <summary>
        /// 宽
        /// </summary>
        public float? Width { get; set; }
        /// <summary>
        /// 面积
        /// </summary>
        public float? Area { get; set; }
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
        /// 孩子数量
        /// </summary>
        public int ChildCount { get; set; }

    }
    #endregion
}
