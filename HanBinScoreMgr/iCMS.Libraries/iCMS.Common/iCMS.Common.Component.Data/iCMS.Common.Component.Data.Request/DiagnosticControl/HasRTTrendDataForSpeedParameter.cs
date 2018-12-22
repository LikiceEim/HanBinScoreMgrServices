using iCMS.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Request.DiagnosticControl
{
    public class HasRTTrendDataForSpeedParameter : BaseRequest
    {
        /// <summary>
        /// 客户最后一条数据对应的时间
        /// </summary>
        public DateTime CheckDate { get; set; }
        /// <summary>
        /// 测量位置ID
        /// </summary>
        public int MSiteID { get; set; }
        /// <summary>
        /// 当前请求的控件编号
        /// </summary>
        public string ChartID { get; set; }
        /// <summary>
        /// 转速开始值
        /// </summary>
        public int RotationStart { get; set; }
        /// <summary>
        /// 转速结束值
        /// </summary>
        public int RotationEnd { get; set; }
    }
}
