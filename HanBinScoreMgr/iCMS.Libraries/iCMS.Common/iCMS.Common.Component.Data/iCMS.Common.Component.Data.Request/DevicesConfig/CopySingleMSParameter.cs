/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 *文件名：  CopySingleMSParameter
 *创建人：  王颖辉
 *创建时间：2016-11-15
 *描述：复制单一位置信息
/************************************************************************************/
using iCMS.Common.Component.Data.Base;
namespace iCMS.Common.Component.Data.Request.DevicesConfig
{
    #region 复制单一位置信息
    /// <summary>
    /// 复制单一位置信息
    /// </summary>
    public class CopySingleMSParameter : BaseRequest
    {
        /// <summary>
        /// 测量位置ID
        /// </summary>
        public int SourceMSId { get; set; }
        /// <summary>
        /// 目标设备ID
        /// </summary>
        public int TargetDevId { get; set; }
        /// <summary>
        /// 目的的测量位置名称ID
        /// </summary>
        public int TargetMsName { get; set; }
        /// <summary>
        /// Int? = -1	
        /// </summary>
        public int? WSID { get; set; }

        public CopySingleMSParameter()
        {
            WSID = 0;
        }
    }
    #endregion
}
