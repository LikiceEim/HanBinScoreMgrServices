/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Enum
 *文件名：  EnumOperationResultType
 *创建人：  王颖辉
 *创建时间：2016-11-15
 *描述：表示业务操作结果的枚举
/************************************************************************************/
using System.ComponentModel;

namespace HanBin.Common.Commonent.Data.Enum
{
    #region 表示业务操作结果的枚举
    /// <summary>
    /// 表示业务操作结果的枚举
    /// </summary>
    [Description("业务操作结果的枚举")]
    public enum EnumOperationResultType
    {
        /// <summary>
        ///     操作成功
        /// </summary>
        [Description("操作成功。")]
        Success,

        /// <summary>
        ///     操作取消或操作没引发任何变化
        /// </summary>
        [Description("操作没有引发任何变化，提交取消。")]
        NoChanged,

        /// <summary>
        ///     参数错误
        /// </summary>
        [Description("参数错误。")]
        ParamError,

        /// <summary>
        ///     指定参数的数据不存在
        /// </summary>
        [Description("指定参数的数据不存在。")]
        QueryNull,

        /// <summary>
        ///     权限不足
        /// </summary>
        [Description("当前用户权限不足，不能继续操作。")]
        PurviewLack,

        /// <summary>
        ///     非法操作
        /// </summary>
        [Description("非法操作。")]
        IllegalOperation,

        /// <summary>
        ///     警告
        /// </summary>
        [Description("警告")]
        Warning,

        /// <summary>
        ///     操作引发错误
        /// </summary>
        [Description("操作引发错误。")]
        Error,
    }
    #endregion
}
