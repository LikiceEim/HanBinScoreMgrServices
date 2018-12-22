/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.WG.Agent.Operators
 *文件名：  GetSNCodeOper
 *创建人：  LF
 *创建时间：2016/2/15 10:10:19
 *描述：iCMS.WG.Agent 获取SNCode操作类
 *
 *=====================================================================**/
using iCMS.WG.Agent.Common.Enum;
using iMesh;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.WG.Agent.Operators
{
    public class GetSNCodeOper : IOperator
    {
        public Model.GetSNCodeTaskModel getSNCodeModel { get; set; }
        public Model.TaskModelBase taskModel
        {
            get
            {
                return getSNCodeModel;
            }
            set
            {
                getSNCodeModel = (Model.GetSNCodeTaskModel)value;
            }
        }

        public bool checkCmd()
        {
            throw new NotImplementedException();
        }

        public object doOperator()
        {
            tGetWsSnParam getWsSNPara = new tGetWsSnParam()
            {
                mac = new tMAC(getSNCodeModel.Mac)
            };

            if (!iCMS.WG.Agent.ComFunction.Send2WS(getWsSNPara, Common.Enum.EnumRequestWSType.GetWsSn))
            {
                CommunicationWithServer communication2Server = new CommunicationWithServer();
                communication2Server.UploadConfigResponse(getSNCodeModel.Mac.ToString().ToUpper(), Enum_ConfigType.ConfigType_WS_Get_SNCode, 1, "网络正忙获取SN失败。");
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), getSNCodeModel.Mac.ToString() + "：网络正忙获取SN失败。");

            }

            return "GetSNCodeOper";
        }
    }
}
