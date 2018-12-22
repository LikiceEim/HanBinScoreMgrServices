using iCMS.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Request.DevicesConfig
{
    public class GetWSMaintainListParameter : BaseRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 网关ID -1：全部
        /// </summary>
        public int WGID { get; set; }
        /// <summary>
        /// 是否被使用-1：全部；1：使用；2：未使用
        /// </summary>
        public int IsUsed { get; set; }
    }
}
