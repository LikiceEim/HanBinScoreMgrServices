/* ==============================================================================
* 功能描述：Server侧判断启停机状态发生变化后，通知WS改变采集状态
* 创 建 者：李峰
* 创建日期：2016年5月30日13:50:02
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
    public class SetWSnStatesOper : IOperator
    {

        public Model.SetWSnStatesModel setWSnStatesModel { get; set; }
        public TaskModelBase taskModel
        {
            get
            {
                return setWSnStatesModel;
            }
            set
            {
                setWSnStatesModel = (Model.SetWSnStatesModel)value;
            }
        }

        public bool checkCmd()
        {
            throw new NotImplementedException();
        }

        public object doOperator()
        {
            iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "设备启停机状态发生变化，通知WS作出调整。");
            System.Threading.Tasks.Task.Run(() =>
             {
                 try
                 {
                     tMAC tmac=null;
                     tSetWsStateParam tsetWsState = null;
                     string strSateInfo = string.Empty;
                     for (int i = 0; i < setWSnStatesModel.macList.Count; i++)
                     {
                         try
                         {
                             tmac = new tMAC(setWSnStatesModel.macList[i]);
                             tsetWsState = new tSetWsStateParam();
                             tsetWsState.mac = tmac;
                             if (setWSnStatesModel.OperatorType == 1)
                             {
                                 strSateInfo = "开机";
                                 tsetWsState.WsState = (byte)0;
                             }
                             else
                             {
                                 strSateInfo = "停机";
                                 tsetWsState.WsState = (byte)1;
                             }
                             if (!iCMS.WG.Agent.ComFunction.meshAdapter.SetWsStartOrStop(tsetWsState))
                             {
                                 iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "Mesh拒绝受理:通知WS [ " + setWSnStatesModel.macList[i].ToString() + " ] 设备进入 " + strSateInfo + "状态。");

                             }
                             else
                             {
                                 iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "通知WS: [ " + setWSnStatesModel.macList[i].ToString() + " ] 设备进入 " + strSateInfo + "状态。");
                             }
                         }
                         catch
                         {
                             continue;
                         }

                     }
                 }
                 catch (Exception ex)
                 {
                     iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Error.ToString(), ex.Message + "\r\n" + ex.StackTrace);

                 }
             });
            return true;
        }
    }
}
