/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.SystemInitSets
 *文件名：  ViewVibrationTypeResult
 *创建人：  QXM
 *创建时间：2016-11-3
 *描述：振动信号类型
/************************************************************************************/

using System.Collections.Generic;

namespace iCMS.Common.Component.Data.Response.SystemInitSets
{
    #region 振动信号类型
    /// <summary>
    /// 振动信号类型
    /// </summary>
    public class VibSignalTypeResult
    {
        public List<VibrationTypeData> VibrationTypeList { get; set; }

        public VibSignalTypeResult()
        {
            VibrationTypeList = new List<VibrationTypeData>();
        }
    }
    #endregion
}
