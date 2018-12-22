/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.Common

 *文件名：  ResponseResult
 *创建人：  王颖辉
 *创建时间：2016-12-09
 *描述：返回特殊结果如整形，字符串，布尔形
/************************************************************************************/
namespace iCMS.Common.Component.Data.Response.Common
{
    #region 返回单独值
    /// <summary>
    /// 返回单独值
    /// </summary>
    public class ResponseResult
    {
        /// <summary>
        /// 返回整形
        /// </summary>
        public int INT
        {
            get;
            set;
        }

        /// <summary>
        /// 返回布尔形
        /// </summary>
        public bool BOOL
        {
            get;
            set;
        }


        /// <summary>
        /// 返回字符串
        /// </summary>
        public string STRING
        {
            get;
            set;
        }
    }
    #endregion
}
