
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;

namespace HanBin.Server.WindowsService
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new  WCFStartupService() 
            };

            UnityContainer container = new UnityContainer();//创建容器
            UnityConfigurationSection configuration = (UnityConfigurationSection)ConfigurationManager.GetSection(UnityConfigurationSection.SectionName);
            configuration.Configure(container, "defaultContainer");

            //  IRepository<Module> moduleRepository
            //using (IInitializeServer initializeServer = new InitializeServer())
            //{
            //    Task InitTask = new Task(initializeServer.InitializeEnvironmentVariables);
            //    InitTask.Start();
            //}
            ServiceBase.Run(ServicesToRun);
        }
    }
}
