using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.DAUAgent
{
    /// <summary>
    /// 有线网关启动采集请求参数
    /// </summary>
    public class StartCollectionParameter : BaseRequest
    {
        /// <summary>
        /// DAU ID集合
        /// </summary>
        public List<int> DAUIDList { get; set; }

        public StartCollectionParameter()
        {
            this.DAUIDList = new List<int>();
        }
    }
}