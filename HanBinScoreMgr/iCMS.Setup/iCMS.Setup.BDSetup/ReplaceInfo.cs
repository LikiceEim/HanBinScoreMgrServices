/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Setup.BDSetup

 *文件名：  DBInfo
 *创建人：  王颖辉
 *创建时间：2017-01-16
 *描述：数据库替换类
/************************************************************************************/
namespace iCMS.Setup.BDSetup
{
    #region 替换类
    /// <summary>
    /// 替换类
    /// </summary>
    public class ReplaceInfo
    {
        /// <summary>
        /// 旧值
        /// </summary>
        public string OldValue
        {
            get;
            set;
        }

        /// <summary>
        /// 新值
        /// </summary>
        public string NewValue
        {
            get;
            set;
        }
    }
    #endregion
}
