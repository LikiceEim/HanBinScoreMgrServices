/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.WG.Agent
 *文件名：  Tools1
 *创建人：  LF
 *创建时间：2016/1/27 10:10:19
 *描述：用以响应上层应用调用Agent的命令（一次响应命令有且只有一个）,并等待Agent执行完毕后返回。会阻塞主线程。
 *
 *=====================================================================**/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Threading;

using iCMS.WG.Agent.Model;
using iCMS.WG.Agent.Operators;
using iCMS.WG.Agent.Common;


namespace iCMS.WG.Agent
{

    /*
     *     iCMS.WG.Agent.Tools1 t = new iCMS.WG.Agent.Tools1();
            Console.Write("开始调用....\r\n");
            Object d = t.ExecuteCmd(new iCMS.WG.Agent.Model.UpdateFirmwareTaskModel() { mainCMD = iCMS.WG.Agent.Common.MainCmd._UpdateFiemware, isNeedResponse = false });
            Console.Write("调用成功。返回结果为：" + d .ToString()+ "\r\n");
     * */



    /// <summary>
    /// 用以响应上层应用调用Agent的命令（一次响应命令有且只有一个）,并等待Agent执行完毕后返回。会阻塞主线程。比如获取networkID
    /// </summary>
    public class SyncTools
    {

        /// <summary>
        /// 同步方法调用
        /// </summary>
        /// <param name="taskModel">操作实体类</param>
        /// <returns>返回结果</returns>
        public Object ExecuteCmd(TaskModelBase taskModel)
        {
            object backObj=null;

            try
            {
                using (System.Threading.Tasks.Task<Object> result = System.Threading.Tasks.Task.Run<object>(() =>
                {
                    IOperator _operator = null;

                    try
                    {
                        _operator = operatorFactory.CreateOperator(taskModel.operatorName);
                        _operator.taskModel = taskModel;
                    }
                    catch
                    {

                    }
                    return _operator.doOperator();

                }))
                {
                    backObj = result.Result;
                };
            } 
            catch (Exception ex)
            {
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Error.ToString(), "iCMS.WG.Agent.SyncTools.ExecuteCmd 执行上层命令失败，异常：" + ex.Message + "\r\n详细：" + ex.StackTrace.ToString());
           
               
            }

            return backObj;
        }
        /// <summary>
        /// 用在异步方法内调用  
        /// </summary>
        /// <param name="taskModel">操作实体类</param>
        /// <returns>返回结果</returns>
        public static async Task<Object> AsyncExecuteCmd(TaskModelBase taskModel)
        {

            Object backObj = null;
           
            IOperator _operator = null;

            try
            {
                _operator = operatorFactory.CreateOperator(taskModel.operatorName);
                _operator.taskModel = taskModel;
                backObj = _operator.doOperator();
            }
            catch (Exception ex)
            {
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Error.ToString(), "iCMS.WG.Agent.SyncTools.ExecuteCmd 执行上层命令失败，异常：" + ex.Message + "\r\n详细：" + ex.StackTrace.ToString());

            }
            return backObj;
        }
    }
}