/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.Cloud

 *文件名：  PlatformInfoResult
 *创建人：  王颖辉
 *创建时间：2016-12-09
 *描述：根据云平台ID获取平台信息，返回结果值
/************************************************************************************/
using iCMS.Common.Component.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.Cloud
{
    #region 根据云平台ID获取平台信息，返回结果值
    /// <summary>
    /// 根据云平台ID获取平台信息，返回结果值
    /// </summary>
    public class PlatformInfoResult
    {
        /// <summary>
        /// 平台集合
        /// </summary>
        public List<Platforminfo> Platforminfo
        {
            get;
            set;
        }
    }

    #region 平台实体类
    /// <summary>
    /// 平台实体类
    /// </summary>
    public class Platforminfo
    {
        /// <summary>
        /// 平台ID
        /// </summary>
        public int ID
        {
            get;
            set;
        }

        /// <summary>
        /// 平台名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 父Id
        /// </summary>
        public int ParentId
        {
            get;
            set;
        }

        /// <summary>
        /// Url
        /// </summary>

        public string Value
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
        /// 是否启用
        /// </summary>
        public EnumCloudConfigIsUse IsUse
        {
            get;
            set;
        }

    }
    #endregion

    #endregion
}
