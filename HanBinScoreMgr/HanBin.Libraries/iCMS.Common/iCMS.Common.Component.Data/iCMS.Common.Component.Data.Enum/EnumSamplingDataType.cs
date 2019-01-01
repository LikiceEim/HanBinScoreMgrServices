using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Enum
{
    /// <summary>
    /// 采集数据项目类型
    /// </summary>
    public enum EnumSamplingDataType
    {
        /// <summary>
        /// 有线
        /// </summary>
        [Description("有线")]
        WiredSensor = 1,

        /// <summary>
        /// 无线
        /// </summary>
        [Description("无线")]
        WireLessSensor = 2,

        /// <summary>
        /// 三轴
        /// </summary>
        [Description("三轴")]
        Triaxial = 3,
    }
}