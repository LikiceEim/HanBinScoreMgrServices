/* ==============================================================================
* 功能描述：Aagent下发测量定义
* 创 建 者：LF
* 创建日期：2016年7月20日13:50:02
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
    public class ConfigMeasureDefineOper : IOperator
    {

        public Model.ConfigMeasureDefineTaskModel configModel { get; set; }
        public Model.TaskModelBase taskModel
        {
            get
            {
                return configModel;
            }
            set
            {
                configModel = (Model.ConfigMeasureDefineTaskModel)value;
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
                ConfigMeasureDefineTaskModel model = (configModel as ConfigMeasureDefineTaskModel);
                foreach (SendMeasureDefine measureDefine in model.measureDefineList)
                {
                    //System.Threading.Tasks.Task result = System.Threading.Tasks.Task.Run(() =>
                    //{
                    iMesh.tSetMeasDefParam setMeasDefParam = new iMesh.tSetMeasDefParam();
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "开始下发WS的测量定义 ：" + measureDefine.MAC);
                    setMeasDefParam.mac = new iMesh.tMAC(measureDefine.MAC);
                    switch (measureDefine.DAQStyle)
                    {
                        case 1:
                            setMeasDefParam.DaqMode = iMesh.enWsDaqMode.eTiming;
                            //设备总数
                            setMeasDefParam.u8DevTotal =  Convert.ToByte(measureDefine.DevTotal);
                            //设备编号
                            setMeasDefParam.u8DevNum = Convert.ToByte(measureDefine.DevNum);

                            iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "定时采集 " + measureDefine.MAC);
                            break;
                        case 2:
                            setMeasDefParam.DaqMode = iMesh.enWsDaqMode.eImmediate;
                            iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "临时采集 " + measureDefine.MAC);
                            break;
                        default:
                            break;
                    }


                  



                    setMeasDefParam.TmpDaqPeriod.u8Hour = measureDefine.DAQPeriodTemperature[0];
                    setMeasDefParam.TmpDaqPeriod.u8Min = measureDefine.DAQPeriodTemperature[1];
                    setMeasDefParam.u16EigenDaqMult = measureDefine.DAQPeriodCharacterValue;
                    setMeasDefParam.u16WaveDaqMult = measureDefine.DAQPeriodWave;

                    if (measureDefine.ACCSubscribe == 1)
                    {
                        setMeasDefParam.AccMdf.MeasDefType = iMesh.enMeasDefType.eAccWaveform;
                        setMeasDefParam.AccMdf.bSubscribed = true;
                        setMeasDefParam.AccMdf.u8EigenValueType = measureDefine.ACCValue;
                        setMeasDefParam.AccMdf.u16WaveLen = measureDefine.ACCWaveLength;
                        setMeasDefParam.AccMdf.u16UpperFreq = measureDefine.ACCUpLimitFreq;
                        setMeasDefParam.AccMdf.u16LowFreq = measureDefine.ACCLowLimitFreq;
                    }
                    if (measureDefine.ACCEnvlSubscribe == 1)
                    {
                        setMeasDefParam.AccEnvMdf.MeasDefType = iMesh.enMeasDefType.eAccEnvelope;
                        setMeasDefParam.AccEnvMdf.bSubscribed = true;
                        setMeasDefParam.AccEnvMdf.u8EigenValueType = measureDefine.ACCEnvlValue;
                        setMeasDefParam.AccEnvMdf.u16WaveLen = measureDefine.ACCEnvlWaveLength;
                        setMeasDefParam.AccEnvMdf.u16UpperFreq = measureDefine.ACCEnvlpBandWidth;
                        setMeasDefParam.AccEnvMdf.u16LowFreq = measureDefine.ACCEnvlpFilter;
                    }
                    if (measureDefine.DispSubscribe == 1)
                    {
                        setMeasDefParam.DspMdf.MeasDefType = iMesh.enMeasDefType.eDspWaveform;
                        setMeasDefParam.DspMdf.bSubscribed = true;
                        setMeasDefParam.DspMdf.u8EigenValueType = measureDefine.DispValue;
                        setMeasDefParam.DspMdf.u16WaveLen = measureDefine.DispWaveLength;
                        setMeasDefParam.DspMdf.u16UpperFreq = measureDefine.DispUpLimitFreq;
                        setMeasDefParam.DspMdf.u16LowFreq = measureDefine.DispLowLimitFreq;
                    }
                    if (measureDefine.VelSubscribe == 1)
                    {
                        setMeasDefParam.VelMdf.MeasDefType = iMesh.enMeasDefType.eVelWaveform;
                        setMeasDefParam.VelMdf.bSubscribed = true;
                        setMeasDefParam.VelMdf.u8EigenValueType = measureDefine.VelValue;
                        setMeasDefParam.VelMdf.u16WaveLen = measureDefine.VelWaveLength;
                        setMeasDefParam.VelMdf.u16UpperFreq = measureDefine.VelUpLimitFreq;
                        setMeasDefParam.VelMdf.u16LowFreq = measureDefine.VelLowLimitFreq;
                    }
                    //启停机
                    if (measureDefine.CriticalSubscribe == 1)
                    {
                        if (setMeasDefParam.RevStop != null)
                        {
                            setMeasDefParam.RevStop.MeasDefType = iMesh.enMeasDefType.eRevStopform;
                            setMeasDefParam.RevStop.bSubscribed = true;
                            setMeasDefParam.RevStop.u8EigenValueType = measureDefine.CriticalValue;
                            setMeasDefParam.RevStop.u16WaveLen = measureDefine.CriticalWaveLength;
                            setMeasDefParam.RevStop.u16UpperFreq = measureDefine.CriticalBandWidth;
                            setMeasDefParam.RevStop.u16LowFreq = measureDefine.CriticalFilter;
                        }

                    }
                    //LQ
                    if (measureDefine.LQSubscribe == 1)
                    {
                        setMeasDefParam.LQMdf.MeasDefType = iMesh.enMeasDefType.eLQform;
                        setMeasDefParam.LQMdf.bSubscribed = true;

                        setMeasDefParam.LQMdf.u8EigenValueType = measureDefine.LQValue;
                        setMeasDefParam.LQMdf.u16WaveLen = measureDefine.LQWaveLength;
                        setMeasDefParam.LQMdf.u16UpperFreq = measureDefine.LQUpLimitFreq;
                        setMeasDefParam.LQMdf.u16LowFreq = measureDefine.LQLowLimitFreq;
                    }

                    //向底层发送信息
                    if (!iCMS.WG.Agent.ComFunction.Send2WS(setMeasDefParam, Common.Enum.EnumRequestWSType.SetMeasDef))
                    {
                        CommunicationWithServer communication2Server = new CommunicationWithServer();
                        communication2Server.UploadConfigResponse(measureDefine.MAC, Enum_ConfigType.ConfigType_MeasDefine, Convert.ToInt32(EnmuReceiveStatus.Unaccept), "网络正忙下发测量定义失败，退出对该WS的下发测量定义");
                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), measureDefine.MAC + "：网络正忙下发测量定义失败，退出对该WS的下发测量定义");
                    }
                    else
                    {

                        lock (iCMS.WG.Agent.ComFunction.sendMeasureDefineList)
                        {
                            System.Threading.Timer timer = new System.Threading.Timer(new System.Threading.TimerCallback(CheckSetMeasDefTimeOut), measureDefine.MAC, 40000, -1);
                            measureDefine.time = timer;
                            iCMS.WG.Agent.ComFunction.sendMeasureDefineList.Add(measureDefine);
                        }
                    }

                    System.Threading.Thread.Sleep(100);

                }
            }
            catch (Exception ex)
            {
                iCMS.WG.Agent.Common.LogHelper.WriteLog(ex);
            }
            return "ConfigMeasureDefineOper";

        }

        private void CheckSetMeasDefTimeOut(object state)
        {
            try
            {
                int index = iCMS.WG.Agent.ComFunction.GetIndexOfSendMeasDefineList((string)state);
                if (index < 0)
                    return;
                string mac = iCMS.WG.Agent.ComFunction.sendMeasureDefineList[index].MAC.ToUpper();
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), mac + "下发测量定义超时WS返回结果： 4");
                CommunicationWithServer communication2Server = new CommunicationWithServer();
                communication2Server.UploadConfigResponse(mac, Enum_ConfigType.ConfigType_MeasDefine, Convert.ToInt32(EnmuReceiveStatus.Faild), "下发测量定义失败！");
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), mac + "上传测量定义下发失败");


                lock (iCMS.WG.Agent.ComFunction.sendMeasureDefineList)
                {
                    index = iCMS.WG.Agent.ComFunction.GetIndexOfSendMeasDefineList((string)state);
                    if (iCMS.WG.Agent.ComFunction.sendMeasureDefineList[index].time != null)
                    iCMS.WG.Agent.ComFunction.sendMeasureDefineList[index].time.Dispose();
                    iCMS.WG.Agent.ComFunction.sendMeasureDefineList.RemoveAt(index);

                }
            }
            catch
            {
            }
        }

       

    }
}
