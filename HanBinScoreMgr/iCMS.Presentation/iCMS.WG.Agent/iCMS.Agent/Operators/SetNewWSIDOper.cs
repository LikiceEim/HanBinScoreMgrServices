/* ==============================================================================
* 功能描述：配置参数WSID
* 创 建 者：LF
* 创建日期：2016年2月19日14:20:54
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
    public class SetNewWSIDOper : IOperator
    {
        public Model.SetWSIDTaskModel newWSIDModel { get; set; }
        public Model.TaskModelBase taskModel
        {
            get
            {
                return newWSIDModel;
            }
            set
            {
                newWSIDModel = (Model.SetWSIDTaskModel)value;
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
                tSetWsIdParam WSIDParam = new tSetWsIdParam();
                SetWSIDTaskModel model = (newWSIDModel as SetWSIDTaskModel);
                foreach (SetNewWSID newWSID in model.newWSIDList)
                {
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "开始更新WS的WSID ：" + newWSID.MAC);
                    WSIDParam.u8ID = newWSID.NewWSID;
                    WSIDParam.u32Reserved1 = newWSID.Reserved1;
                    WSIDParam.u32Reserved2 = newWSID.Reserved2;
                    WSIDParam.mac = new tMAC(newWSID.MAC);
                    //向底层发送信息
                    if (!iCMS.WG.Agent.ComFunction.Send2WS(WSIDParam, Common.Enum.EnumRequestWSType.SetWsID))
                    {
                        CommunicationWithServer communication2Server = new CommunicationWithServer();
                        communication2Server.UploadConfigResponse(newWSID.MAC, Enum_ConfigType.ConfigType_WS_NO, 1, "网络正忙下发WSID失败，退出对该WS的WSID更新");
                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), newWSID.MAC + "：网络正忙下发WSID失败，退出对该WS的WSID更新");
                    }
                    else
                    {
                        lock (iCMS.WG.Agent.ComFunction.setNewWSIDList)
                        {
                            iCMS.WG.Agent.ComFunction.setNewWSIDList.Add(newWSID);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                iCMS.WG.Agent.Common.LogHelper.WriteLog(ex);   
            }
            return "NewWSIDOperOper";
        }
    }
}
