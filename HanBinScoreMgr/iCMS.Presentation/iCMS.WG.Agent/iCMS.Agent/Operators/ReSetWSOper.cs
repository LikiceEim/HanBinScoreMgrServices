/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.WG.Agent.Operators
 *文件名：  ReSetWSOper
 *创建人：  LF
 *创建时间：2016/2/15 10:10:19
 *描述：iCMS.WG.Agent 重启WS操作类
 *
 *=====================================================================**/
using iCMS.WG.Agent.Common;
using iCMS.WG.Agent.Common.Enum;
using iMesh;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.WG.Agent.Operators
{
    public class ReSetWSOper : IOperator
    {
        public Model.ReSetWSTaskModel reSetWSModel { set; get; }
        public Model.TaskModelBase taskModel
        {
            get
            {
                return reSetWSModel;
            }
            set
            {
                reSetWSModel = (Model.ReSetWSTaskModel)value;
            }
        }

        public bool checkCmd()
        {
            throw new NotImplementedException();
        }

        public object doOperator()
        {
            reSetWSModel.macList.ForEach(obj =>
            {
                tResetWSParam reSetWSParam = new tResetWSParam()
                {
                    mac = new tMAC(obj.ToString()),
                      u8Mote = 0x01,
                       u8MCU=0x01
                };
                if (!iCMS.WG.Agent.ComFunction.Send2WS(reSetWSParam, Common.Enum.EnumRequestWSType.ResetWS))
                {
                    CommunicationWithServer communication2Server = new CommunicationWithServer();
                    communication2Server.UploadConfigResponse(obj.ToString(), Enum_ConfigType.ConfigType_ReSetWS, 1, "网络正忙重启WS失败。");
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), obj.ToString() + "：网络正忙重启WS失败");

                }
            });

            return "ResetWS";
        }
    }
}
