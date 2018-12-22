/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.WG.Agent.Model
 *文件名：  SetWSnStatesModel
 *创建人：  LF
 *创建时间：2016/5/30 10:10:19
 *描述：启停机操作执行model
 *=====================================================================**/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.WG.Agent.Model
{
    public class SetWSnStatesModel : TaskModelBase
    {
        /// <summary>
        /// 待处理WS的MAC地址结合
        /// </summary>
        public List<string> macList { set; get; }

        /// <summary>
        /// 操作状态，1=启动；2=停机
        /// </summary>
        public int OperatorType { set; get; }

        public override string operatorName
        {
            get
            {
                return "SetWSnStatesOper";
            }

        }
    }
}
