using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.DAUService
{
    /// <summary>
    /// 获取可用的温度通道返回结果
    /// </summary>
    public class GetAvailableTemperatureWSByDAUIDResult
    {
        public List<TemperatureWS> TemperatureWSInfo { get; set; }

        public GetAvailableTemperatureWSByDAUIDResult()
        {
            this.TemperatureWSInfo = new List<TemperatureWS>();
        }
    }

    public class TemperatureWS
    {
        /// <summary>
        /// 通道号
        /// </summary>
        public int WSID { get; set; }

        /// <summary>
        /// 通道名称
        /// </summary>
        public string WSName { get; set; }
    }
}
