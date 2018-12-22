/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.WG.Agent.Operators
 *文件名：  operatorFactory
 *创建人：  LF
 *创建时间：2016/2/15 10:10:19
 *描述：iCMS.WG.Agent 操作工厂类
 *
 *=====================================================================**/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;

namespace iCMS.WG.Agent.Operators
{
    public static class operatorFactory
    {

        public static IOperator CreateOperator(string type)
        {
            return System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace+"." + type, false) as IOperator;
        }
    }
}
