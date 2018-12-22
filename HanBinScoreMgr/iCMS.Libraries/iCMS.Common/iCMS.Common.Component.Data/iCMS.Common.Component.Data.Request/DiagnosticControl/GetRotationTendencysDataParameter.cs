using iCMS.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Request.DiagnosticControl
{
    /// <summary>
    /// 获取转速趋势图数据的参数
    /// </summary>
    public class GetRotationTendencysDataParameter : BaseRequest
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public int DevID { get; set; }

        /// <summary>
        /// 设备位置ID
        /// </summary>
        public int MSiteID { get; set; }

        /// <summary>
        /// 趋势数据起始时间
        /// </summary>
        public string BeginDate { get; set; }

        /// <summary>
        /// 趋势数据截止时间
        /// </summary>
        public string EndDate { get; set; }

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
