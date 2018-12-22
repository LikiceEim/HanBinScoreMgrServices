using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Enum
{
    public enum EnumWSFormType
    {
        /// <summary>
        /// 无线
        /// </summary>
        [Description("无线")]
        WireLessSensor = 1,

        /// <summary>
        /// 有线
        /// </summary>
        [Description("有线")]
        WiredSensor = 2,

        /// <summary>
        /// 三轴
        /// </summary>
        [Description("三轴")]
        Triaxial = 3
    }
}