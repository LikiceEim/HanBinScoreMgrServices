using iMesh;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.WG.Agent.Operators
{
    //   获取WS的状态改为RefreshAllWSStatesOper,该方法暂停使用
    
    public class RefreshWSStatesOper : IOperator
    {
        public Model.TaskModelBase taskModel { get; set; }

        public bool checkCmd()
        {
            throw new NotImplementedException();
        }

        public object doOperator()
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    taskModel.operatorName = "";
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Log.ToString(), "调用底层（GetMoteConfig），获取WS真实状态");
                    iCMS.WG.Agent.ComFunction.getConfigMacCurr = iCMS.WG.Agent.Common.CommonConst.RequestGetMoteConfig;
                    tMAC tMac = new tMAC(iCMS.WG.Agent.Common.CommonConst.RequestGetMoteConfig);
                    iCMS.WG.Agent.ComFunction.meshAdapter.QueryWs(tMac);
                }
                catch (Exception ex)
                {
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Error.ToString(), ex.Message + "\r\n" + ex.StackTrace);
                }
            });
            GetNetWorkIDFromManager();
            return true;
        }

        private void GetNetWorkIDFromManager()
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    string netID = ((int)iCMS.WG.Agent.ComFunction.meshAdapter.GetNetworkId()).ToString();
                    CommunicationWithServer communication2Server = new CommunicationWithServer();
                    communication2Server.UploadWGNetWorkIDStatus(iCMS.WG.Agent.ComFunction.GetAppConfig("WGID").ToString(), netID);
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Log.ToString(), "WG NetWorkID " + netID);
                }
                catch (Exception ex)
                {
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Error.ToString(), ex.Message + "\r\n" + ex.StackTrace);
                }
            });
        }
    }
    
}
