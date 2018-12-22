using iCMS.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Request.DAUService
{
    public class GetDAUInfoListByDAUIDParameter : BaseRequest
    {
        /// <summary>
        /// -1，用户关联的所有DAU
        /// </summary>
        public int? DAUID { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 排序名,如WGNO,WGName ,NetWorkID, WGTypeNamee
        /// </summary>
        public string Sort { get; set; }
        /// <summary>
        /// 排序方式，desc/asc
        /// </summary>
        public string Order { get; set; }
        /// <summary>
        /// 页数，从1开始,若为-1返回所有
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// 页面行数，从1开始
        /// </summary>
        public int PageSize { get; set; }
    }
}
