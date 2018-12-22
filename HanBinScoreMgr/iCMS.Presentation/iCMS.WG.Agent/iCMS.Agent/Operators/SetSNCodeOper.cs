/* ==============================================================================
* 功能描述：配置参数SN串码
* 创 建 者：LF
* 创建日期：2016年2月19日15:49:10
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
    public class SetSNCodeOper : IOperator
    {
        public Model.SetSNCodeTaskModel snCodeModel { get; set; }
        public Model.TaskModelBase taskModel
        {
            get
            {
                return snCodeModel;
            }
            set
            {
                snCodeModel = (Model.SetSNCodeTaskModel)value;
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
                tSetWsSnParam wsSNParam = new tSetWsSnParam();
                SetSNCodeTaskModel model = (snCodeModel as SetSNCodeTaskModel);
                foreach (SetSNCode snCode in model.snCodeList)
                {
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "开始下发WS的SN串码 ：" + snCode.MAC);
                    wsSNParam.sn = snCode.sn;
                    wsSNParam.mac = new tMAC(snCode.MAC);
                    //向底层发送信息
                    if (!iCMS.WG.Agent.ComFunction.Send2WS(wsSNParam, Common.Enum.EnumRequestWSType.SetWsSn))
                    {
                        CommunicationWithServer communication2Server = new CommunicationWithServer();
                        communication2Server.UploadConfigResponse(snCode.MAC, Enum_ConfigType.ConfigType_WS_SNCode, 1, "网络正忙下发SN串码失败，退出对该WS的SN串码设置");
                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), snCode.MAC + "：网络正忙下发SN串码失败，退出对该WS的SN串码设置");
                    }
                    else
                    {
                        lock (iCMS.WG.Agent.ComFunction.setSNCodeList)
                        {
                            iCMS.WG.Agent.ComFunction.setSNCodeList.Add(snCode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                iCMS.WG.Agent.Common.LogHelper.WriteLog(ex);
            }
            return "SNCodeOper";
        }
    }
}
