/* ==============================================================================
* 功能描述：获取当前网络中所有存在的WS的状态
* 创 建 者：仵书婷
* 创建日期：2016年7月30日10:59:20
* ==============================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iMesh;

namespace iCMS.WG.Agent.Operators
{
    public class RefreshAllWSStatesOper : IOperator
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
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Log.ToString(), "调用底层（QueryAllWs），获取WS真实状态");
                    iCMS.WG.Agent.ComFunction.meshAdapter.QueryAllWs();
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
