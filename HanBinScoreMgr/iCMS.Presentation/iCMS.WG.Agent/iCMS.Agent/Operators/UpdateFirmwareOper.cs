/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.WG.Agent.Operators
 *文件名：  UpdateFirmwareOper
 *创建人：  LF
 *创建时间：2016/2/15 10:10:19
 *描述：iCMS.WG.Agent 升级WS操作类
 *
 *=====================================================================**/
using iCMS.WG.Agent.Common;
using iCMS.WG.Agent.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iCMS.WG.Agent.Model.Send;
using iMesh;
using iCMS.WG.Agent.Common.Enum;
using System.IO;
namespace iCMS.WG.Agent.Operators
{
    public class UpdateFirmwareOper :  IOperator
    {

      
        public Model.UpdateFirmwareTaskModel updateModel { get; set; }


        public Model.TaskModelBase taskModel
        {
            get
            {
                return updateModel;
            }
            set
            {
                updateModel = (Model.UpdateFirmwareTaskModel)value;
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
                             
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "准备升级");
                CommunicationWithServer communication2Server = new CommunicationWithServer();
                if (ComFunction.WSUpdatingInfo.UpdatingAllWSInfo.Count == 0)
                {   
                    UpdateFirmwareTaskModel model = (updateModel as UpdateFirmwareTaskModel);
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "开始验证升级数据包...");
                    if (!ComFunction.JudgeLegitimacyOfUpgradeFile(model.updateFile))
                    {
                        foreach (string mac in model.macList)
                        {                        
                            communication2Server.UploadConfigResponse(mac, Enum_ConfigType.ConfigType_UpdateFirmware, Convert.ToInt32(EnmuReceiveStatus.Faild), "升级信息非法，退出对该WS的升级");
                            iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), mac + " 升级信息非法，退出对该WS的升级");
                        }
                        return false;
                    }
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "升级数据包符合要求");
                    tFirmware firmware;
                    try
                    {
                        firmware = new tFirmware(model.updateFile);
                    }
                    catch
                    {
                        return null;
                    }

                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "开始：PreludeUpdate ");
                    if (ComFunction.meshAdapter.PreludeUpdate())
                    {
                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "PreludeUpdate 完成 ");
                        lock (ComFunction.WSUpdatingInfo)
                        {
                            foreach (string mac in model.macList)
                            {
                                if (!string.IsNullOrWhiteSpace(mac))
                                    ComFunction.WSUpdatingInfo.UpdatingAllWSInfo.Add(mac, null);
                            }
                        }

                        List<tMAC> tempMACList = model.macList
                            .Where(macInfo => !string.IsNullOrWhiteSpace(macInfo))
                            .Select(macInfo => new tMAC(macInfo))
                            .ToList();                       
                        if (ComFunction.meshAdapter.Update(tempMACList, firmware))
                        {
                            if (model.CommandSuccessed != null)
                                model.CommandSuccessed();

                            iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "Update成功，开始升级已选择的WS ");                           
                        }
                        else
                        {
                            iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(),"Update失败，退出升级");

                            if (model.CommandFailed != null)
                                model.CommandFailed();

                            lock (ComFunction.WSUpdatingInfo)
                            {
                                foreach (string mac in model.macList)
                                {
                                   iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(),
                                        " Update失败，无法升级，退出对该WS的升级 ：" + mac);
                                   communication2Server.UploadConfigResponse(mac, Enum_ConfigType.ConfigType_UpdateFirmware, Convert.ToInt32(EnmuReceiveStatus.Faild), "Update失败，退出对该WS的升级");                           
                                }
                                try
                                {
                                    ComFunction.WSUpdatingInfo.UpdatingAllWSInfo.Clear();
                                    if (ComFunction.WSUpdatingInfo.UpdateAllWSTimer != null)
                                    {
                                        ComFunction.WSUpdatingInfo.UpdateAllWSTimer.Stop();
                                        ComFunction.WSUpdatingInfo.UpdateAllWSTimer.Close();
                                        ComFunction.WSUpdatingInfo.UpdateAllWSTimer.Dispose();
                                        ComFunction.WSUpdatingInfo.UpdateAllWSTimer = null;
                                    }
                                }
                                catch
                                {
                                }
                            }

                            iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "开始 PostludeUpdate");
                            ComFunction.meshAdapter.PostludeUpdate();
                            iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "开始 PostludeUpdate 完成");
                            return false;
                        }
                    }
                    else
                    {
                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "PreludeUpdate 失败");

                        if (model.CommandFailed != null)
                            model.CommandFailed();

                        lock (ComFunction.WSUpdatingInfo)
                        {
                            foreach (string mac in model.macList)
                            {
                                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(),
                                    " PreludeUpdate 失败，网络异常无法升级，退出对该WS的升级 ：" + mac);
                                communication2Server.UploadConfigResponse(mac, Enum_ConfigType.ConfigType_UpdateFirmware, Convert.ToInt32(EnmuReceiveStatus.Faild), "PreludeUpdate 失败，退出对该WS的升级");
                            }
                            try
                            {
                                ComFunction.WSUpdatingInfo.UpdatingAllWSInfo.Clear();
                                if (ComFunction.WSUpdatingInfo.UpdateAllWSTimer != null)
                                {
                                    ComFunction.WSUpdatingInfo.UpdateAllWSTimer.Stop();
                                    ComFunction.WSUpdatingInfo.UpdateAllWSTimer.Close();
                                    ComFunction.WSUpdatingInfo.UpdateAllWSTimer.Dispose();
                                    ComFunction.WSUpdatingInfo.UpdateAllWSTimer = null;
                                }
                            }
                            catch
                            {
                            }
                        }

                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "开始 PostludeUpdate！");
                        ComFunction.meshAdapter.PostludeUpdate();
                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "PostludeUpdate 完成！");
                        return false;
                    }
                }
                else
                {
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "当前有WS正在进行升级");
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
            }
            return null;
        }
            
    }
}
