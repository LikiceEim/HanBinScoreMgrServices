/* ==============================================================================
* 功能描述：Aagent下发触发式上传测量定义
* 创 建 者：wst
* 创建日期：2016年7月28日16:01:02
* 修改记录：
* ==============================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.WG.Agent.Model;
using iCMS.WG.Agent.Common;
using iCMS.WG.Agent.Model.Send;
using iCMS.WG.Agent.Common.Enum;

namespace iCMS.WG.Agent.Operators
{
    public class ConfigTriggerDefineOper : IOperator
    {

        public Model.ConfigTriggerDefineTaskModel configModel { get; set; }
        public Model.TaskModelBase taskModel
        {
            get
            {
                return configModel;
            }
            set
            {
                configModel = (Model.ConfigTriggerDefineTaskModel)value;
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

                ConfigTriggerDefineTaskModel model = (configModel as ConfigTriggerDefineTaskModel);
                foreach (SendTriggerDefine TriggerDefine in model.triggerDefineList)
                {
                    iMesh.tSetTrigParam setTriggerDefParam = new iMesh.tSetTrigParam();
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "开始下发WS的触发式配置 ：" + TriggerDefine.MAC);
                    setTriggerDefParam.mac = new iMesh.tMAC(TriggerDefine.MAC);
                    switch (TriggerDefine.bEnable)
                    {
                        case 0:
                            setTriggerDefParam.Enable = iMesh.enEnableFun.eCloseFun;
                            iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "关闭触发功能 " + TriggerDefine.MAC);
                            break;
                        case 1:
                            setTriggerDefParam.Enable = iMesh.enEnableFun.eOpenFun;
                            iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "开启触发功能 " + TriggerDefine.MAC);
                            break;
                        default:
                            break;
                    }
                    setTriggerDefParam.Enable = (iMesh.enEnableFun)TriggerDefine.bEnable;
                    if (TriggerDefine.ACCEnable == 1)
                    {
                        setTriggerDefParam.AccMdf.MeasDefType    = iMesh.enMeasDefType.eAccWaveform;
                        setTriggerDefParam.AccMdf.bEnable        = true;
                        setTriggerDefParam.AccMdf.u8Flag         = TriggerDefine.ACCValue;
                        setTriggerDefParam.AccMdf.fCharRmsvalue  = TriggerDefine.ACCRmsThreshold;
                        setTriggerDefParam.AccMdf.fCharPKvalue   = TriggerDefine.ACCPKThreshold;
                        setTriggerDefParam.AccMdf.fCharPKPKvalue = TriggerDefine.ACCPKPKThreshold;

                    }
                    if (TriggerDefine.ACCEnvlEnable == 1)
                    {
                        setTriggerDefParam.AccEnvMdf.MeasDefType = iMesh.enMeasDefType.eAccEnvelope;
                        setTriggerDefParam.AccEnvMdf.bEnable = true;
                        setTriggerDefParam.AccEnvMdf.u8Flag = TriggerDefine.ACCEnvlValue;
                        setTriggerDefParam.AccEnvMdf.fCharPKvalue = TriggerDefine.ACCEnvlPKThreshold;
                        setTriggerDefParam.AccEnvMdf.fCharPKCvalue = TriggerDefine.ACCEnvlPKCThreshold;
                    }
                    if (TriggerDefine.VelEnable == 1)
                    {
                        setTriggerDefParam.VelMdf.MeasDefType = iMesh.enMeasDefType.eVelWaveform;
                        setTriggerDefParam.VelMdf.bEnable = true;
                        setTriggerDefParam.VelMdf.u8Flag = TriggerDefine.VelValue;
                        setTriggerDefParam.VelMdf.fCharRmsvalue = TriggerDefine.VelRmsThreshold;
                        setTriggerDefParam.VelMdf.fCharPKvalue = TriggerDefine.VelPKThreshold;
                        setTriggerDefParam.VelMdf.fCharPKPKvalue = TriggerDefine.VelPKPKThreshold;
                    }
                    if (TriggerDefine.DispEnable == 1)
                    {
                        setTriggerDefParam.DspMdf.MeasDefType = iMesh.enMeasDefType.eDspWaveform;
                        setTriggerDefParam.DspMdf.bEnable = true;
                        setTriggerDefParam.DspMdf.u8Flag = TriggerDefine.DispValue;
                        setTriggerDefParam.DspMdf.fCharRmsvalue = TriggerDefine.DispRmsThreshold;
                        setTriggerDefParam.DspMdf.fCharPKvalue = TriggerDefine.DispPKThreshold;
                        setTriggerDefParam.DspMdf.fCharPKPKvalue = TriggerDefine.DispPKPKThreshold;
                    }                   

                    //向底层发送信息
                    if (!iCMS.WG.Agent.ComFunction.Send2WS(setTriggerDefParam, Common.Enum.EnumRequestWSType.SetTrigger))
                    {
                        CommunicationWithServer communication2Server = new CommunicationWithServer();
                        communication2Server.UploadConfigResponse(TriggerDefine.MAC, Enum_ConfigType.ConfigType_MeasDefine, Convert.ToInt32(EnmuReceiveStatus.Unaccept), "网络正忙下发触发式配置失败，退出");
                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), TriggerDefine.MAC + "：网络正忙下发触发式配置失败，退出");
                    }
                    else
                    {

                        lock (iCMS.WG.Agent.ComFunction.sendTriggerDefineList)
                        {
                            System.Threading.Timer timer = new System.Threading.Timer(new System.Threading.TimerCallback(CheckSetTriggerDefTimeOut), TriggerDefine.MAC, 40000, -1);
                            TriggerDefine.time = timer;
                            iCMS.WG.Agent.ComFunction.sendTriggerDefineList.Add(TriggerDefine);
                        }
                    }

                    System.Threading.Thread.Sleep(100);

                }
            }
            catch (Exception ex)
            {
                iCMS.WG.Agent.Common.LogHelper.WriteLog(ex);
            }
            return "ConfigTriggerDefineOper";

        }
        //触发式上传配置的超时处理
        private void CheckSetTriggerDefTimeOut(object state)
        {
            try
            {
                int index = iCMS.WG.Agent.ComFunction.GetIndexOfSendTriggerDefineList((string)state);
                if (index < 0)
                    return;
                string mac = iCMS.WG.Agent.ComFunction.sendTriggerDefineList[index].MAC.ToUpper();
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), mac + "下发触发式配置超时WS返回结果： 4");
                CommunicationWithServer communication2Server = new CommunicationWithServer();
                communication2Server.UploadConfigResponse(mac, Enum_ConfigType.ConfigType_TriggerDefine, Convert.ToInt32(EnmuReceiveStatus.Faild), "下发触发式配置失败！");
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), mac + "上传触发式配置下发失败");


                lock (iCMS.WG.Agent.ComFunction.sendTriggerDefineList)
                {
                    index = iCMS.WG.Agent.ComFunction.GetIndexOfSendTriggerDefineList((string)state);
                    if (iCMS.WG.Agent.ComFunction.sendTriggerDefineList[index].time != null)
                        iCMS.WG.Agent.ComFunction.sendTriggerDefineList[index].time.Dispose();
                    iCMS.WG.Agent.ComFunction.sendTriggerDefineList.RemoveAt(index);

                }
            }
            catch
            {
            }
        }

       

    }
}
