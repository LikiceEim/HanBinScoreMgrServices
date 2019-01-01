using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Enum
{
    public enum EnumCloudMonitorTreeType
    {
        None = 0,
        //企业
        Enterprise = 1,

        //车间
        Workshop = 2
    }

    public enum EnumCloudCollectDataType
    {
        None = 0,

        /// <summary>
        /// 波形数据
        /// </summary>
        WaveData = 1,

        /// <summary>
        /// 特征值数据
        /// </summary>
        EigenValue = 2
    }
}
