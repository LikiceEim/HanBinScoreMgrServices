using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.DiagnosticControl
{
    /// <summary>
    /// 检测转速是否有最新数据结果类
    /// </summary>
    public class HasRTTrendDataForSpeedResult
    {
        public bool HasNewData { get; set; }

        public string ChartID { get; set; }
    }
}
