/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.SystemInitSets
 *文件名：  ModuleDataParameter
 *创建人：  王颖辉
 *创建时间：2016-11-15
 *描述：模块数据
/************************************************************************************/
using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.SystemInitSets
{
    #region 模块操作类

    #region 获取模块数据
    /// <summary>
    /// 模块数据
    /// </summary>
    public class ModuleDataParameter : BaseRequest
    {
        public int ParID { get; set; }
    }
    #endregion

    #region 添加模块数据
    /// <summary>
    /// 添加模块数据
    /// </summary>
    public class AddModuleDataParameter : BaseRequest
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; }
        /// <summary>
        /// 父ID
        /// </summary>
        public int ParID { get; set; }
        /// <summary>
        /// 祖ID
        /// </summary>
        public int OID { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsUsed { get; set; }
        /// <summary>
        /// 是否是系统默认
        /// </summary>
        public int IsDefault { get; set; }
        /// <summary>
        /// 模块Code
        /// </summary>
        public string Code { get; set; }

        public int RelationTableName { get; set; }

        public string RelationCode { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Describe { get; set; }
    }
    #endregion

    #region 编辑模块
    /// <summary>
    /// 编辑模块
    /// </summary>
    public class EditModuleDataParameter : BaseRequest
    {
        /// <summary>
        /// 数据库ID
        /// </summary>
        public int ModuleID { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; }
        /// <summary>
        /// 父ID
        /// </summary>
        public int ParID { get; set; }
        /// <summary>
        /// 祖ID
        /// </summary>
        public int OID { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsUsed { get; set; }
        /// <summary>
        /// 是否是系统默认
        /// </summary>
        public int IsDefault { get; set; }
        /// <summary>
        /// 模块Code
        /// </summary>
        public string Code { get; set; }

        public int RelationTableName { get; set; }

        public string RelationCode { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Describe { get; set; }
    }
    #endregion

    #region 删除模块
    /// <summary>
    /// 删除模块
    /// </summary>
    public class DeleteModuleDataParameter : BaseRequest
    {
        public int ModuleID { get; set; }
    }
    #endregion

    #region 获取二级节点模块
    /// <summary>
    /// 获取二级节点模块
    /// </summary>
    public class SecondLevelModuleByParentIdParameter : BaseRequest
    {
        public int ParID { get; set; }
    }
    #endregion

    #endregion
}
