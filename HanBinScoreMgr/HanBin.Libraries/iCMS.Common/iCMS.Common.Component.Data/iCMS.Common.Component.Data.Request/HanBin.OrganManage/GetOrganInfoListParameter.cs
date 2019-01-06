using HanBin.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Data.Request.HanBin.OrganManage
{
    /// <summary>
    /// 获取单位列表请求参数
    /// </summary>
    public class GetOrganInfoListParameter : BaseRequest
    {
        /// <summary>
        /// 单位分类
        /// </summary>
        public int? OrganTypeID { get; set; }
        /// <summary>
        /// 检索关键字
        /// </summary>
        public string Keyword { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Sort { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Order { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int PageSize { get; set; }

        public int CurrentUserID { get; set; }
    }
}
