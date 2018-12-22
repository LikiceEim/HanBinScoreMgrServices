/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.Utility
 *文件名：  IsNewValueExistedParameter
 *创建人：  王颖辉
 *创建时间：2016-11-15
 *描述：判断输入新值唯一性，请求参数
/************************************************************************************/
using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.Utility
{
    #region 判断输入新值唯一性，请求参数
    /// <summary>
    /// 判断输入新值唯一性，请求参数
    /// </summary>
    public class IsNewValueExistedParameter : BaseRequest
    {
        /// <summary>
        /// 厂家ID(编辑轴承时使用)
        /// </summary>
        public string FactoryID { get; set; }

        /// <summary>
        /// 新值
        /// </summary>
        public string NewValue { get; set; }
        /// <summary>
        /// 原ID
        /// </summary>
        public int? OID { get; set; }
        /// <summary>
        /// 待验证的表
        /// T_SYS_MONITOR_TREE：1
        /// T_SYS_WG：2
        /// T_ SYS_WS：3
        /// T_SYS_DEVICE：4
        /// T_SYS_USER：5
        /// T_SYS_ROLE：6
        /// T_SYS_MODULE：7
        /// </summary>
        public int Table { get; set; }
        /// <summary>
        /// 待验证的字段
        /// 字段名
        /// 监测树配置：名称
        /// 无限网关：网关编号，网关名称
        /// 无线传感器：SN,MAC地址
        /// 设备树管理：设备名称，设备编号
        /// 用户管理：用户名
        /// 权限功能：模组名称,code
        /// 角色管理：角色名称
        /// </summary>
        public string Name { get; set; }
    }
    #endregion
}
