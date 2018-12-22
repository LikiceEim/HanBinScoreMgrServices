/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.WG.Agent.Operators
 *文件名：  operatorFactory
 *创建人：  LF
 *创建时间：2016/2/15 10:10:19
 *描述：iCMS.WG.Agent 操作基类
 *
 *=====================================================================**/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iCMS.WG.Agent.Model;
namespace iCMS.WG.Agent.Operators
{
    /// <summary>
    /// Agent操作接口
    /// </summary>
    public interface IOperator
    {

        /// <summary>
        /// 命令实体
        /// </summary>
        iCMS.WG.Agent.Model.TaskModelBase taskModel { get; set; }

      
        /// <summary>
        /// check命令信息
        /// </summary>
        /// <returns></returns>
        bool checkCmd();

        /// <summary>
        /// 开始操作Agent
        /// </summary>
        /// <returns></returns>
        Object doOperator();


        

    }
}
