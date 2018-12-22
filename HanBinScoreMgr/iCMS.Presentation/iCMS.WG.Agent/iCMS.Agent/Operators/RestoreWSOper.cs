/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.WG.Agent.Operators
 *文件名：  RestoreWSOper
 *创建人：  LF
 *创建时间：2016/2/15 10:10:19
 *描述：iCMS.WG.Agent 恢复WS出厂设置操作类
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
    public class RestoreWSOper : IOperator
    {
        public Model.RestoreWSTaskModel RestoreWSModel { set; get; }

        public Model.TaskModelBase taskModel
        {
            get
            {
                return RestoreWSModel;
            }
            set
            {
                RestoreWSModel = (Model.RestoreWSTaskModel)value;
            }
        }



        public bool checkCmd()
        {
            throw new NotImplementedException();
        }

        public object doOperator()
        {
            RestoreWSModel.macList.ForEach(obj =>
            {
                tRestoreWSParam reSetWGParam = new tRestoreWSParam()
                {
                    mac = new tMAC(obj.ToString()),
                    u8MCU = 0x00,
                    u8Mote = 0X00
                };
                if (!iCMS.WG.Agent.ComFunction.Send2WS(reSetWGParam, Common.Enum.EnumRequestWSType.RestoreWS))
                {
                    CommunicationWithServer communication2Server = new CommunicationWithServer();
                    communication2Server.UploadConfigResponse(obj.ToString(), Enum_ConfigType.ConfigType_ReSetWS, 1, "网络正忙恢复WS出厂设置失败。");
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), obj.ToString() + "：网络正忙恢复WS出厂设置失败");

                }
            });

            return "RestoreWS";
        }
    }
}
