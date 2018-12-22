using iCMS.Common.Component.Tool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.WG.Agent.Common.Enum
{
    public enum EnumRequestType
    {
        /// <summary>
        /// 获取网关与传感器关系
        /// </summary>
        GetSensorsInfo,
        /// <summary>
        /// 上传数据
        /// </summary>
        UpLoadData
    }
}
