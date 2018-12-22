using iCMS.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Request.Utility
{
    /// <summary>
    /// 获取用户权限请求参数
    /// </summary>
    public class GetAuthorizedParam : BaseRequest
    {
        public string RoleCode { get; set; }

        public List<string> Code { get; set; }
    }
}
