/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.WG.Agent
 *文件名：  WGAgent
 *创建人：  LF
 *创建时间：2016/2/14 10:10:19
 *描述：iCMS.WG.Agent主线程
 *
 *=====================================================================**/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.WG.Agent
{
    /// <summary>
    /// iCMS.WG.Agent 
    /// </summary>
    public class WGAgent
    {
        iCMS.WG.Agent.CommunicationWithMesh communication2Mesh = null;
        /// <summary>
        /// 启动iCMS.WG.Agent
        /// </summary>
        public void Strart()
        {
            try
            {

                communication2Mesh = new iCMS.WG.Agent.CommunicationWithMesh();
                communication2Mesh.Run();
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Log.ToString(), "iCMS.WG.Agent 启动成功。");

            }
            catch (Exception ex)
            {
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Error.ToString(), "iCMS.WG.Agent 启动失败，异常：" + ex.Message + ":\r\n详细" + ex.StackTrace.ToString());
                iCMS.WG.Agent.Common.Interop.ShowMessageBox("构建网络通讯失败,原因可能为：串口被占用或不存在，详细请查看日志处理", "iCMS.WG.Agent警告");
                throw new Exception("iCMS.WG.Agent 启动失败，异常：" + ex.Message);
            }
        }

        public void Stop()
        {
            try
            {
                communication2Mesh.Stop();
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Log.ToString(), "iCMS.WG.Agent 停止。");
            }
            catch
            { 
            
            }
        }

    }
}
