
/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.Statistics
 *文件名：  WgnAlarmDataParameter
 *创建人：  赵鹏伟
 *创建时间：2016-11-15
 *描述：WG报警数据
/************************************************************************************/

using System;
using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.Statistics
{
    
        #region WS报警数据
        /// <summary>
        /// WS报警数据
        /// </summary>
        public class WgnAlarmDataParameter : BaseRequest
        {
            /// <summary>
            /// 开始时间
            /// 修改人：张辽阔
            /// 修改时间：2016-11-10
            /// 修改记录：把 DateTime 改成 DateTime?
            /// </summary>
            public DateTime? BDate { get; set; }
            /// <summary>
            /// 结束时间
            /// 
            /// 修改人：张辽阔
            /// 修改时间：2016-11-10
            /// 修改记录：把 DateTime 改成 DateTime?
            /// </summary>
            public DateTime? EDate { get; set; }

            /// <summary>
            /// 监测设备类别
            /// 1 网关
            /// 2 传感器
            /// </summary>
            public int? Type { get; set; }

            /// <summary>
            /// 用户ID
            /// </summary>
            public int UserID { get; set; }

            /// <summary>
            /// 报警类型
            /// </summary>
            public int? MSAlmID { get; set; }
            /// <summary>
            /// 位置结点-mtid
            /// </summary>
            public int? MonitorTreeID { get; set; }
            /// <summary>
            /// 报警状态-正常，高报，高高报
            /// </summary>
            public int? AlmStatus { get; set; }
            /// <summary>
            /// 设备ID
            /// </summary>
            public int? DevID { get; set; }
            /// <summary>
            /// 测量位置ID
            /// </summary>
            public int? MSiteID { get; set; }
            /// <summary>
            /// 网关ID
            /// </summary>
            public int? WGID { get; set; }
            /// <summary>
            /// 传感器ID
            /// </summary>
            public int? WSID { get; set; }
            /// <summary>
            /// 是否已确认
            /// </summary>
            public int ViewStatus { get; set; }
            /// <summary>
            /// 排序字段，如BDate, EDate, MSAlmID,MonitorTreeID, AlmStatus, DevID, MSiteID, WGID, WSID
            /// </summary>
            public string Sort { get; set; }

            /// <summary>
            /// 排序方式，desc/asc
            /// </summary>
            public string Order { get; set; }
            /// <summary>
            /// 页数，从1开始, 若为-1返回所有的报警记录
            /// </summary>
            public int Page { get; set; }
            /// <summary>
            /// 页面行数，从1开始
            /// </summary>
            public int PageSize { get; set; }

            public string DateType { get; set; }
        }
        #endregion
    
}
