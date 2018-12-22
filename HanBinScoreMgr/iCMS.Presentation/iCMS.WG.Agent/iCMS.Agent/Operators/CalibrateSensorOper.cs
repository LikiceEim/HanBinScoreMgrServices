/* ==============================================================================
* 功能描述：配置参数传感器校准
* 创 建 者：LF
* 创建日期：2016年2月19日16:15:20
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
    public class CalibrateSensorOper : IOperator
    {
        public Model.CalibrateSensorTaskModel calibrateSensorModel { get; set; }
        public Model.TaskModelBase taskModel
        {
            get
            {
                return calibrateSensorModel;
            }
            set
            {
                calibrateSensorModel = (Model.CalibrateSensorTaskModel)value;
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
                tCaliSensorParam caliSensorParam = new tCaliSensorParam();
                CalibrateSensorTaskModel model = (calibrateSensorModel as CalibrateSensorTaskModel);
                foreach (CheckMonitor checkMonitor in model.checkMonitorList)
                {
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "开始传感器校准 ：" + checkMonitor.MAC);
                    caliSensorParam.mac = new tMAC(checkMonitor.MAC);
                    //向底层发送信息
                    if (!iCMS.WG.Agent.ComFunction.Send2WS(caliSensorParam, Common.Enum.EnumRequestWSType.CalibrateWsSensor))
                    {
                        CommunicationWithServer communication2Server = new CommunicationWithServer();
                        communication2Server.UploadConfigResponse(checkMonitor.MAC.ToUpper(), Enum_ConfigType.ConfigType_UpdateFirmware, 1, "网络正忙传感器校准失败，退出对该WS的传感器校准");
                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), checkMonitor.MAC + "：网络正忙传感器校准失败，退出对该WS的传感器校准");
                    }
                    else
                    {
                        lock (iCMS.WG.Agent.ComFunction.checkMonitorList)
                        {
                            iCMS.WG.Agent.ComFunction.checkMonitorList.Add(checkMonitor);
                        }
                    }
                    System.Threading.Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                iCMS.WG.Agent.Common.LogHelper.WriteLog(ex);
            }
            return "CalibrateSensorOper";
        }
    }
}
