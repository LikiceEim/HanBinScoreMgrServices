using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.DAUService
{
    public class GetDAUInfoListByDAUIDResult
    {
        public List<DAUInfo> DAUInfoList { get; set; }

        public int Total { get; set; }

        public GetDAUInfoListByDAUIDResult()
        {
            this.DAUInfoList = new List<DAUInfo>();
        }
    }

    public class DAUInfo
    {
        /// <summary>
        /// DAUID
        /// </summary>
        public int DAUID { get; set; }
        /// <summary>
        /// DAU名称
        /// </summary>
        public string DAUName { get; set; }
        /// <summary>
        /// DAU状态：0，未使用；1，使用；
        /// </summary>
        public int CurrentDAUStates { get; set; }
        /// <summary>
        /// 监测节点（机组级别）
        /// </summary>
        public string MonitorTreeNames { get; set; }
    }
}
