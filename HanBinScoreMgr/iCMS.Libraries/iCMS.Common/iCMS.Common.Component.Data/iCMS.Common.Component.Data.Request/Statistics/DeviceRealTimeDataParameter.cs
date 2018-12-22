using iCMS.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Request.Statistics
{
    /// <summary>
    /// 获取设备实时数据请求参数
    /// </summary>
    public class DeviceRealTimeDataParameter : BaseRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int? UserID { get; set; }
        /// <summary>
        /// 监测树ID
        /// </summary>
        public int? MonitorID { get; set; }
        /// <summary>
        /// 设备ID
        /// </summary>
        public int? DeviceID { get; set; }

        public string Sort { get; set; }

        public string Order { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public List<EigenMSiteMapper> EigenMSiteMapperList { get; set; }

        public DeviceRealTimeDataParameter()
        {
            EigenMSiteMapperList = new List<EigenMSiteMapper>();
        }
    }

    public class EigenMSiteMapper
    {
        /// <summary>
        /// 特征值ID
        /// </summary>
        public int EigenValueID { get; set; }

        /// <summary>
        /// 特征值下配置的测点ID
        /// </summary>
        public List<int> MeasureSiteTypeIDList { get; set; }

        public EigenMSiteMapper()
        {
            MeasureSiteTypeIDList = new List<int>();
        }
    }
}
