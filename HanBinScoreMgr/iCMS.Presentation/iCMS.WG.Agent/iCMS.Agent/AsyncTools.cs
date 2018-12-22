/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.WG.Agent
 *文件名：  Tools
 *创建人：  LF
 *创建时间：2016/1/27 10:10:19
 *描述：暴露给上层应用的接口。用以响应上层应用调用Agent的命令。不给与返回。不会阻塞主线程。
 *
 *=====================================================================**/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.WG.Agent.Model;
using System.Collections;

using iCMS.WG.Agent.Operators;
using iCMS.WG.Agent.Common;
using System.Threading;

namespace iCMS.WG.Agent
{

    /*
     *   iCMS.WG.Agent.Tools tool = new iCMS.WG.Agent.Tools();
         tool.AddCmd(new iCMS.WG.Agent.Model.UpdateFirmwareTaskModel());
         tool.AddCmd(new iCMS.WG.Agent.Model.UpdateFirmwareTaskModel());

     * */


    /// <summary>
    /// 用以响应上层应用调用Agent的命令（响应命令至少存在1+个，按照加入顺序依次执行）。不给与返回。不会阻塞主线程。比如下发测量定义
    /// </summary>
    public class AsyncTools:IDisposable
    {
        CancellationTokenSource cts = null;
        private ManualResetEvent _hasNew;
        private System.Threading.Tasks.Task task= null;
        bool autoStart;
 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_autoStart">是否自动启动线程，默认自动启动，否则请设为false并手动启动</param>
        public AsyncTools(bool _autoStart=true)
        {
            autoStart = _autoStart;
            TaskList = new Queue<TaskModelBase>();
            _hasNew = new ManualResetEvent(true);
            cts = new CancellationTokenSource();

           task = new System.Threading.Tasks.Task(new Action(this.ThreadMain));
           if(autoStart)
            this.Start();           
        }

        public void Start()
        {
            task.Start();
        }

        public void Stop()
        {
            cts.Cancel();
        }


        private Queue<TaskModelBase> TaskList
        {
            get;
            set;
        }

        /// <summary>
        /// Agent响应上层应用命令
        /// </summary>
        /// <param name="taskModel">命令实体类</param>
        public void AddCmd(TaskModelBase taskModel)
        {

            TaskList.Enqueue(taskModel);
            _hasNew.Set();
        }

        private void ExecuteCmd(TaskModelBase taskmMdel)
        {
            IOperator _operator = null;

            try
            {
                _operator = operatorFactory.CreateOperator(taskmMdel.operatorName);
                if (_operator != null)
                {
                    _operator.taskModel = taskmMdel;
                    _operator.doOperator();
                }
           
            }
            catch (Exception ex)
            {
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Error.ToString(), "iCMS.WG.Agent.AsyncTools.ExecuteCmd 执行上层命令失败，异常：" + ex.Message + "\r\n详细：" + ex.StackTrace.ToString());
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Error.ToString(), "详细执行对象Json：：" + iCMS.WG.Agent.ComFunction.GetJson(taskmMdel));

               
            }
            finally
            {

                _hasNew.Set();
            }
        }

        private  void ThreadMain()
        {

            while (true)
            {
                //等待执行信号
                _hasNew.WaitOne();

                if (cts.IsCancellationRequested)
                    break;
                if (TaskList.Count != 0)
                {

                    _hasNew.Reset();
                    // 执行任务
                    System.Threading.Tasks.Task.Run(() =>
                   {
                       ExecuteCmd(TaskList.Dequeue());
                   });
                }
                else
                {
                    _hasNew.Reset();
                    task.Wait(500);
                }
            }
            
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            TaskList = null;
            _hasNew = null;
            cts = null;
            task.Dispose();
        }
    }
}
