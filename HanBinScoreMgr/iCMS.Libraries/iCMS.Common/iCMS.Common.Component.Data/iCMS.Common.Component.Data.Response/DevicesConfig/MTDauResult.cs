using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.DevicesConfig
{
    public class MTDauResult
    {
        /// <summary>
        /// 采集单元ID
        /// </summary>
        public int DAUID { get; set; }

        /// <summary>
        /// 监测树ID（18-01-10新加）
        /// </summary>
        public int MonitorTreeID { get; set; }

        /// <summary>
        /// 采集单元名称
        /// </summary>
        public string DAUName { get; set; }

        /// <summary>
        /// 采集单元当前状态
        /// </summary>
        public int? CurrentDAUStates { get; set; }

        /// <summary>
        /// IP
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }
    }
}