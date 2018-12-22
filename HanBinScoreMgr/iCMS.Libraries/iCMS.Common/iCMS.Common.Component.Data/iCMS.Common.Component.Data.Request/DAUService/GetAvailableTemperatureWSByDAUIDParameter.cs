using iCMS.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Request.DAUService
{
    /// <summary>
    /// 获取可用的温度采集通道信息 请求参数
    /// </summary>
    public class GetAvailableTemperatureWSByDAUIDParameter : BaseRequest
    {
        public int DAUID { get; set; }
    }
}
