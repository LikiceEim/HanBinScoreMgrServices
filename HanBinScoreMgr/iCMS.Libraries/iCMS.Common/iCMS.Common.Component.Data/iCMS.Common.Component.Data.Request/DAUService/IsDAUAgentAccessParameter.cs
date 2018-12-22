using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.DAUService
{
    /// <summary>
    /// 验证DAUAagentURL是否可访问请求参数
    /// </summary>
    public class IsDAUAgentAccessParameter : BaseRequest
    {
        public string DAUAgentUrlAddress { get; set; }
    }
}