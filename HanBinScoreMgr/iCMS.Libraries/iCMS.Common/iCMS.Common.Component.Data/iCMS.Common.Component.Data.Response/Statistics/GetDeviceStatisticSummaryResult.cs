using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.Statistics
{
    public class GetDeviceStatisticSummaryResult
    {
        /// <summary>
        /// 运行总数
        /// </summary>
        public int RunningDeviceCount { get; set; }
        /// <summary>
        /// 停机总数
        /// </summary>
        public int StoppedDeviceCount { get; set; }
        /// <summary>
        /// 正常总数
        /// </summary>
        public int NormalDeviceCount { get; set; }
        /// <summary>
        /// 中级报警总数
        /// </summary>
        public int AlarmDeviceCount { get; set; }
        /// <summary>
        /// 高级报警总数
        /// </summary>
        public int WarnDeviceCount { get; set; }
        /// <summary>
        /// 设备总数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 无线设备
        /// </summary>
        public int WirelessDevice { get; set; }
        /// <summary>
        /// 有线设备
        /// </summary>
        public int WireDevice { get; set; }
    }
}