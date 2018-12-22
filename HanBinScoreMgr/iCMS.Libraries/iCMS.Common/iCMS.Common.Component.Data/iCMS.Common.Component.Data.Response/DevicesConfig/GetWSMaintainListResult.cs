using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.DevicesConfig
{
    public class GetWSMaintainListResult
    {
        public List<WSInfoItem> WSInfoList { get; set; }

        public GetWSMaintainListResult()
        {
            this.WSInfoList = new List<WSInfoItem>();
        }
    }

    public class WSInfoItem
    {
        public int WSID { get; set; }

        public string WSName { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-10
        /// 创建记录：传感器形态类型，1、无线 2、有线 3、三轴
        /// </summary>
        public int WSFormType { get; set; }
    }
}