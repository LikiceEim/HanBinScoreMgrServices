using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.WG.Agent.Operators
{
    public class ReSetAgentOper:IOperator
    {
        public Model.TaskModelBase taskModel { get; set; }

        public bool checkCmd()
        {
            return true;
        }

        public object doOperator()
        {
            ///初始化WS/WG状态列表
            try
            {
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Log.ToString(), "重新初始化Agent环境变量开始...");
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Log.ToString(), "开始与Server通讯");
                iCMS.WG.Agent.ComFunction.InitDeviceStatusFromServer();
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Log.ToString(), "与Server通讯完成");
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Log.ToString(), "开始与网关通讯");
                iCMS.WG.Agent.ComFunction.meshAdapter.QueryAllWs();
                string netID = ((int)iCMS.WG.Agent.ComFunction.meshAdapter.GetNetworkId()).ToString();
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Log.ToString(), "与网关通讯完成，获取NetID：" + netID);

                CommunicationWithServer communication2Server = new CommunicationWithServer();
                communication2Server.UploadWGNetWorkIDStatus(iCMS.WG.Agent.ComFunction.GetAppConfig("WGID").ToString(), netID);
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Log.ToString(), "重新初始化Agent环境变量完成!");
            }
            catch
            { }
                       

            return "ReSetAgentOper";
        }
    }
}
