/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.SystemInitSets
 *文件名：  ModuleResult
 *创建人：  QXM
 *创建时间：2016-11-3
 *描述：返回设备类型数据信息的参数
/************************************************************************************/

using System.Collections.Generic;

namespace iCMS.Common.Component.Data.Response.SystemInitSets
{
    #region 返回模块
    /// <summary>
    /// 返回模块
    /// </summary>
    public class ModuleResult
    {
        public List<ModuleInfo> ModuleInfo { get; set; }
    }
    #endregion

    #region 模块信息
    /// <summary>
    /// 模块信息
    /// </summary>
    public class ModuleInfo
    {
        public int ModuleID { get; set; }

        public string ModuleName { get; set; }

        public int ParID { get; set; }

        public int OID { get; set; }

        public string Code { get; set; }

        public int IsUsed { get; set; }

        public int IsDefault { get; set; }

        public string AddDate { get; set; }

        /// <summary>
        /// 是否具有子节点，Added by QXM
        /// </summary>
        public bool IsExistChild { get; set; }

        public int? RelationTableName { get; set; }

        public string RelationCode { get; set; }

        public string Describe { get; set; }
    }
}
    #endregion
