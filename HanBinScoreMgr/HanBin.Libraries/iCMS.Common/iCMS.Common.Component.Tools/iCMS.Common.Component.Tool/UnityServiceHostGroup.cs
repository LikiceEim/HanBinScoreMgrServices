/************************************************************************************
 * Copyright (c) 2017iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Tool

 *文件名：  UnityServiceHostGroup
 *创建人：  王颖辉
 *创建时间：2017/2/23 16:43:28
 *描述：说明
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HanBin.Common.Component.Tool.IoC;
using System.Configuration;
using System.ServiceModel.Configuration;
using System.Reflection;

namespace HanBin.Common.Component.Tool
{
    public class UnityServiceHostGroup
    {
        static List<UnityServiceHost> _hosts = new List<UnityServiceHost>();

        private static void OpenHost(Type type)
        {
            UnityServiceHost svt = new UnityServiceHost(type, "defaultContainer");
            svt.Open();
            _hosts.Add(svt);
        }

        public static void StartAllConfigureService()
        {
            Configuration conf = ConfigurationManager.OpenExeConfiguration(Assembly.GetEntryAssembly().Location);
            ServiceModelSectionGroup svcmod = (ServiceModelSectionGroup)conf.GetSectionGroup("system.serviceModel");


            foreach (ServiceElement el in svcmod.Services.Services)
            {
                //#region 获取项目名称，解决不同项目
                //string[] array = el.Name.Split('.');
                //string projectName = string.Empty;
                //string namespaces = string.Empty;
                //if (array.Length > 0)
                //{
                //    projectName = array[0];
                //    namespaces = projectName + ".Presentation.Server";
                //}
                //#endregion

                Type type = Type.GetType(el.Name + "," + "iCMS.Presentation.Server");
                if (type == null)
                {
                    //continue;
                    throw new Exception();
                }

                OpenHost(type);
            }
        }

        public static void CloseAllService()
        {
            foreach (UnityServiceHost host in _hosts)
            {
                host.Close();
            }
        }
    }
}
