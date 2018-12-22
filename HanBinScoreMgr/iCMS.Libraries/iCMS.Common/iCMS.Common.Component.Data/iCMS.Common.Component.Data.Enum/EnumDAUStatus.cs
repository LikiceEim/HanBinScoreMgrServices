using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Enum
{
    public enum EnumDAUStatus
    {
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-01-10
        /// 创建记录：初始化状态
        /// </summary>
        InitStatus = 0,

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-01-10
        /// 创建记录：空闲状态
        /// </summary>
        IdleStatus = 1,

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-01-10
        /// 创建记录：采集状态
        /// </summary>
        CollectingStatus = 2,

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-01-10
        /// 创建记录：配置状态
        /// </summary>
        ConfigingStatus = 3,

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-01-10
        /// 创建记录：测试状态
        /// </summary>
        TestStatus = 4,

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-01-10
        /// 创建记录：故障状态
        /// </summary>
        FaultStatus = 5,

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-01-10
        /// 创建记录：错误状态
        /// </summary>
        ErrorStatus = 0xff,
    }
}
