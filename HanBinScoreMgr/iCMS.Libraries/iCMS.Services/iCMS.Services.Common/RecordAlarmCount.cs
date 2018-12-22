/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Service.Common
 *文件名：  RecordAlarmCount
 *创建人：  王颖辉
 *创建时间：2016-11-15
 *描述：记录告警次数
/************************************************************************************/

namespace iCMS.Service.Common
{
    #region 记录告警次数
    /// <summary>
    /// 记录告警次数
    /// </summary>
    public class RecordAlarmCount
    {
        public int? SignalID { get; set; }

        public int? EigenValueTypeID { get; set; }

        public int WarnCount { get; set; }

        public int AlarmCount { get; set; }
    }
    #endregion

}
