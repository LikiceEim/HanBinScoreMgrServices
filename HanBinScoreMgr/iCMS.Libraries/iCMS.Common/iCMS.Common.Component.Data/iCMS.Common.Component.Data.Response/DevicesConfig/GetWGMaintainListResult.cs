using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.DiagnosticControl
{
    public class GetWGMaintainListResult
    {
        public List<WGInfoItem> WGInfoList { get; set; }

        public GetWGMaintainListResult()
        {
            this.WGInfoList = new List<WGInfoItem>();
        }
    }

    public class WGInfoItem
    {
        public int WGID { get; set; }

        public string WGName { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-10
        /// 创建记录：网关形态类型，1：普通网关 2：轻量级网关 3：采集单元
        /// </summary>
        public int WGFormType { get; set; }
    }
}