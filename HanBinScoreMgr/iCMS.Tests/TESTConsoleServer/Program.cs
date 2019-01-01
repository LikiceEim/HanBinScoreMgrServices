using System;
using System.Threading.Tasks;
using System.Configuration;

using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

using HanBin.Common.Component.Tool;

using HanBin.Frameworks.Core.Repository;
using HanBin.Common.Component.Tool.IoC;
using System.ServiceModel;

namespace TESTConsoleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //#region Start WCF services

            //  AppDomain.CurrentDomain.BaseDirectory + ""

            //UnityServiceHostGroup.StartAllConfigureService();

            //UnityContainer container = new UnityContainer();//创建容器
            //UnityConfigurationSection configuration = (UnityConfigurationSection)ConfigurationManager.GetSection(UnityConfigurationSection.SectionName);
            //configuration.Configure(container, "defaultContainer");

            //using (IInitializeServer initializeServer = container.Resolve<IInitializeServer>())
            //{
            //    Task InitTask = new Task(initializeServer.InitializeEnvironmentVariables);
            //    InitTask.Start();
            //}
            //#endregion


            using (ServiceHostCollection hosts = new ServiceHostCollection())
            {
                foreach (ServiceHost host in hosts)
                {
                    host.Opened += (sender, arg) => Console.WriteLine("服务{0}开始监听",
                       (sender as ServiceHost).Description.ServiceType);
                }
                hosts.Open();


                //Initial
                //UnityContainer container = new UnityContainer();//创建容器
                //UnityConfigurationSection configuration = (UnityConfigurationSection)ConfigurationManager.GetSection(UnityConfigurationSection.SectionName);
                //configuration.Configure(container, "defaultContainer");



                Console.WriteLine("All services have been started!");
                Console.ReadKey();
            }

            Console.WriteLine("Server 服务已启动......");
            Console.ReadKey();
        }
    }
}