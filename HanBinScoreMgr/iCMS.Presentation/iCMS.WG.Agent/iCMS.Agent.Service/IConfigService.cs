using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace iCMS.WG.AgentServer
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IConfigService”。
    [ServiceContract]
    public interface IConfigService
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "ConfigMeasureDefine",
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json,
           Method = "POST")]
        string ConfigMeasureDefine(Stream stream);

        [OperationContract]
        [WebInvoke(UriTemplate = "UpdateFirmware",
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json,
           Method = "POST")]
        string UpdateFirmware(Stream stream);

        
        [OperationContract]
        [WebInvoke(UriTemplate = "GetWSInfo",
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json,
           Method = "POST")]
        void GetWSInfo();
        

        [OperationContract]
        [WebInvoke(UriTemplate = "GetAllWSInfo",
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json,
           Method = "POST")]
        void GetAllWSInfo();
        
        [OperationContract]
        [WebInvoke(UriTemplate = "SetStatusOfCritical",
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json,
           Method = "POST")]
        string SetStatusOfCritical(Stream stream);

        [OperationContract]
        [WebInvoke(UriTemplate = "ConfigTriggerDefine",
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json,
           Method = "POST")]
        string ConfigTriggerDefine(Stream stream);

        #region 验证是否可以访问
        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2016-09-07
        /// 验证是否可以访问,成功1
        /// </summary>
        /// <returns>是否可以访问</returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "IsAccess",
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json,
        Method = "POST")]
        string IsAccess();
        #endregion

        [OperationContract(IsOneWay=true)]
        [WebInvoke(UriTemplate = "ReSetAgent",
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json,
        Method = "POST")]
        void ReSetAgent();

    }
}
