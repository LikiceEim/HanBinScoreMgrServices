/* ==============================================================================
* 功能描述：配置参数NetworkID
* 创 建 者：LF
* 创建日期：2016年2月19日15:12:13
* ==============================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.WG.Agent.Model;
using iCMS.WG.Agent.Common;
using iCMS.WG.Agent.Model.Send;
using iMesh;
using iCMS.WG.Agent.Common.Enum;

namespace iCMS.WG.Agent.Operators
{
    public class SetNetworkIDOper : IOperator
    {
        public Model.SetNetworkIDTaskModel newNetworkIDModel { get; set; }
        public Model.TaskModelBase taskModel
        {
            get
            {
                return newNetworkIDModel;
            }
            set
            {
                newNetworkIDModel = (Model.SetNetworkIDTaskModel)value;
            }
        }


        public bool checkCmd()
        {
            throw new NotImplementedException();
        }

        public object doOperator()
        {
            try
            {
                tSetNetworkIdParam networkIDParam = new tSetNetworkIdParam();
                SetNetworkIDTaskModel model = (newNetworkIDModel as SetNetworkIDTaskModel);
                foreach (SetNetworkID networkID in model.networkIDList)
                {
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "开始更新WS的NetworkID ：" + networkID.MAC);
                    networkIDParam.u16ID = networkID.NewNetWorkID;
                    networkIDParam.mac = new tMAC(networkID.MAC);

                    //向底层发送信息
                    if (!iCMS.WG.Agent.ComFunction.Send2WS(networkIDParam, Common.Enum.EnumRequestWSType.SetNetworkID))
                    {
                        CommunicationWithServer communication2Server = new CommunicationWithServer();
                        communication2Server.UploadConfigResponse(networkID.MAC, Enum_ConfigType.ConfigType_WS_NetID, 1, "网络正忙下发NetworkID失败，退出对该WS的NetworkID更新");
                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), networkID.MAC + "：网络正忙下发NetworkID失败，退出对该WS的NetworkID更新");
                    }
                    else
                    {
                        lock (iCMS.WG.Agent.ComFunction.setNetworkIDList)
                        {
                            iCMS.WG.Agent.ComFunction.setNetworkIDList.Add(networkID);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                iCMS.WG.Agent.Common.LogHelper.WriteLog(ex);   
            }
            return "SetNetworkIDOper";
        }
    }
}
