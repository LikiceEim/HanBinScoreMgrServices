/************************************************************************************
 * Copyright (c) 2016Microsoft All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Enum
 *文件名：  EnumBaseDataType
 *创建人：  王颖辉 
 *创建时间：2016年12月17日
 *描述：云平台基础数据类型
/************************************************************************************/
using System.ComponentModel;

namespace iCMS.Common.Component.Data.Enum
{
    #region 云平台基础数据类型
    /// <summary>
    /// 云平台基础数据类型
    /// </summary>
    public enum EnumBaseDataType
    {
        /// <summary>
        /// 配置数据
        /// </summary>
        [Description("配置数据")]
        ConfigData = 0,
        /// <summary>
        /// 振动数据
        /// </summary>
        [Description("振动数据")]
        VibSignalData = 1,
    }
    #endregion
}
