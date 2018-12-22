/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.Statistics
 *文件名：  DevHistoryDataParameter
 *创建人：  王颖辉
 *创建时间：2016-10-19
 *描述：GetDevHistoryDataParameter:调用方法
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Enum;
namespace iCMS.Common.Component.Data.Request.Statistics
{
    #region GetDevHistoryDataParameter 参数
    /// <summary>
    /// GetDevHistoryDataParameter:调用方法
    /// </summary>
    public class DevHistoryDataParameter : BaseRequest
    {
       public int? MonitorTreeId { get; set; }
       public int? DevId { get; set; }
        public int? MSId { get; set; }
        public DateTime BDate { get; set; }
        public DateTime EDate { get; set; }
        public int DataStat { get; set; }
        public int DataType { get; set; }
        public EnumDataType DateType { get; set; }
        public string Sort { get; set; }
        public string Order { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public int UserID { get; set; }
    }
    #endregion

}
