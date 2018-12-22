using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Enum
{
    /// <summary>
    /// 设备形态类型枚举
    /// </summary>
    public enum EnumDevFormType
    {
        /// <summary>
        /// 单板
        /// </summary>
        [Description("单板")]
        SingleBoard = 1,

        /// <summary>
        /// 轻量级
        /// </summary>
        [Description("轻量级")]
        iWSN = 2,

        /// <summary>
        /// 有线
        /// </summary>
        [Description("有线")]
        Wired = 3
    }
}