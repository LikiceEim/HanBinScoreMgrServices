using iCMS.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Request.DAUService
{
    /// <summary>
    /// 根据DAUID 获取传感器信息请求参数
    /// </summary>
    public class GetAllWSInfoByDAUIDParameter : BaseRequest
    {
        /// <summary>
        /// DAU ID
        /// </summary>
        public List<int> DAUID { get; set; }
    }
}
