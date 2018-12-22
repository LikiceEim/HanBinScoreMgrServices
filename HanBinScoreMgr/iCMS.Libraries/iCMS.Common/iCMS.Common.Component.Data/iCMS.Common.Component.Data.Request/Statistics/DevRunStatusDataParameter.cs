/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.Statistics
 *文件名：  DevRunStatusDataParameter
 *创建人：  王颖辉
 *创建时间：2016-10-19
 *描述：设备运行状态查询
/************************************************************************************/
using iCMS.Common.Component.Data.Base;
namespace iCMS.Common.Component.Data.Request.Statistics
{
    #region 设备运行状态查询
    /// <summary>
    /// 设备运行状态查询
    /// </summary>
    public class DevRunStatusDataParameter : BaseRequest
    {
        public int? MonitorTreeID { set; get; }
        public int? DevId { set; get; }
        public string DevNo { set; get; }
        public int DevRunningStat { set; get; }
        public int DevAlarmStat { set; get; }
        public string Sort { set; get; }
        public string Order { set; get; }
        public int Page { set; get; }
        public int PageSize { set; get; }
        public int DevType { set; get; }
        public int UseType { set; get; }

        public int? UserID { get; set; }
    }
    #endregion

}
