/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.DevicesConfig
 *文件名：  MonitorTreeTypeDataResult
 *创建人：  LF
 *创建时间：2016-10-26
 *描述：监测树类型返回数据实体
/************************************************************************************/
using System.Collections.Generic;

namespace iCMS.Common.Component.Data.Response.DevicesConfig
{
    #region 监测树类型

    /// <summary>
    /// 监测树类型结果集合
    /// </summary>
    public class MonitorTreeTypeDataResult
    {
      public  List<MonitorTreeTypeInfo> TypeInfos { set; get; }
    }




    /// <summary>
    /// 监测树类型
    /// </summary>
    public class MonitorTreeTypeInfo
    {
        /// <summary>
        /// 监测树类型ID
        /// </summary>
        public int ID { set; get; }
        /// <summary>
        /// 监测树名称
        /// </summary>
        public string Name { set; get; }
    }

    #endregion
}
