using iCMS.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Request.Statistics
{
    public class GetDevInfoByGroupIDParameter : BaseRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 机组ID
        /// </summary>
        public int ParentID { get; set; }

    }
}
