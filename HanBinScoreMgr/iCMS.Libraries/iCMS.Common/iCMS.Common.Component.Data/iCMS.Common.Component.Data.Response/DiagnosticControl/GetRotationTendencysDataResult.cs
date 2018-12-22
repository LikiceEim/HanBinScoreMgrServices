using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.DiagnosticControl
{
    public class GetRotationTendencysDataResult
    {
        /// <summary>
        /// 数据集合
        /// </summary>
        public List<RotationTendencysData> RotationTendencysDatas { get; set; }
    }

    public class RotationTendencysData
    {

        /// <summary>
        /// 设备ID
        /// </summary>
        public int DevID { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DevName { get; set; }

        /// <summary>
        /// 测量位置ID
        /// </summary>
        public int MsiteID { get; set; }

        /// <summary>
        /// 测量位置名称
        /// </summary>
        public string MsiteName { get; set; }

        /// <summary>
        /// 开始时间(秒) eg：2015-8-10 13:23:50
        /// </summary>
        public string BeginDate { get; set; }

        /// <summary>
        /// 结束时间(秒) eg：2015-8-10 13:23:50
        /// </summary>
        public string EndDate { get; set; }

        /// <summary>
        /// 测量位置数据
        /// </summary>
        public List<float> YData { get; set; }

        /// <summary>
        /// 时间轴数据
        /// </summary>
        public List<DateTime> XData { get; set; }

        /// <summary>
        /// 当前请求的控件编号
        /// </summary>
        public string ChartID { get; set; }

        /// <summary>
        /// 标题：设备名称+挂靠的测点名称（如果存在的话则有该名称，否则没有）+测量位置名称 （转速测量位置名称）
        /// </summary>
        public string Title { get; set; }
    }
}
