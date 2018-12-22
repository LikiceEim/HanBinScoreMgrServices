/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.DevicesConfig
 *文件名：  GetFullMonitorTreeDataByTypeResult 
 *创建人：  王颖辉
 *创建时间：2017/9/28 10:58:32 
 *描述： 根据传入监测树类型，获取系统默认最高级别监测树节点，且返回节点均存在子节点
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.DevicesConfig
{
    #region 根据传入监测树类型，获取系统默认最高级别监测树节点，且返回节点均存在子节点
    /// <summary>
    ///  根据传入监测树类型，获取系统默认最高级别监测树节点，且返回节点均存在子节点
    /// </summary>
    public class GetFullMonitorTreeDataByTypeResult
    {
        /// <summary>
        /// 监测树节点集合
        /// </summary>
        public List<MTInfoWithType> MTInfoWithType
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 监测树节点集合
    /// </summary>
    public class MTInfoWithType
    {
        /// <summary>
        /// 监测树类型ID
        /// </summary>
        public int TypeID
        {
            get;
            set;
        }

        /// <summary>
        /// 监测树类型名称
        /// </summary>
        public string TypeName
        {
            get;
            set;
        }


        /// <summary>
        /// 选中节点ID
        /// </summary>
        public int SelectedID
        {
            get;
            set;
        }

        /// <summary>
        /// 顺序
        /// </summary>
        public int Order
        {
            get;
            set;
        }

        /// <summary>
        /// 监测树集合
        /// </summary>
        public List<MTInfo> MTInfo
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 监测树类型
    /// </summary>
    public class MTInfo
    {
        /// <summary>
        /// 监测树ID
        /// </summary>
        public int MonitorTreeID
        {
            get;
            set;
        }

        /// <summary>
        /// 监测树父ID
        /// </summary>
        public int ParentID
        {
            get;
            set;
        }

        /// <summary>
        /// 是否系统默认
        /// </summary>
        public int IsDefault
        {
            get;
            set;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// 类型
        /// </summary>
        public int TypeID
        {
            get;
            set;
        }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName
        {
            get;
            set;
        }
    }
    #endregion
}
