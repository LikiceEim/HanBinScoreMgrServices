using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Tool
{
    public static class ServiceLocator
    {
        private static IUnityContainer container;

        static ServiceLocator()
        {
            //注册初始服务
            container = new UnityContainer();


            //注入全局服务
            //日志服务
            //ServiceLocator.RegisterSingleton<ILogService>(new LogService());
        }


        /// <summary>
        /// 获取服务
        /// </summary>
        /// <typeparam name="IService"></typeparam>
        /// <returns></returns>
        public static IService GetService<IService>()
        {
            return container.Resolve<IService>();
        }

        /// <summary>
        /// 根据名称获取实例 
        /// </summary>
        /// <typeparam name="IService"></typeparam>
        /// <param name="name">实例名称</param>
        /// <returns></returns>
        public static IService GetService<IService>(string name)
        {
            return container.Resolve<IService>(name);
        }


        /// <summary>
        /// 注册服务(默认使用TransientLifetimeManager,每次调用的时候都会返回新的对象)
        /// </summary>
        /// <typeparam name="ISvc"></typeparam>
        /// <typeparam name="TSvc"></typeparam>
        public static void RegisterService<ISvc, TSvc>() where TSvc : ISvc
        {
            if (!container.IsRegistered<TSvc>())
            {
                container.RegisterType<ISvc, TSvc>();
            }
        }
        public static void RegisterService<ISvc, TSvc>(string name) where TSvc : ISvc
        {
            if (!container.IsRegistered<TSvc>(name))
            {
                container.RegisterType<ISvc, TSvc>(name);
            }
        }

        /// <summary>
        /// 注册服务,指定生命周期管理
        /// </summary>
        /// <typeparam name="ISvc"></typeparam>
        /// <typeparam name="TSvc"></typeparam>
        public static void RegisterService<ISvc, TSvc>(LifetimeManager lm) where TSvc : ISvc
        {

        }


        /// <summary>
        /// 注册单实例服务，线程安全的(使用RegisterInstance来将已存在的 实例注册到UnityContainer中)
        /// 默认使用ContainerControlledLifetimeManager生命周期管理器,每次调用的时候都会返回同一对象
        /// </summary>
        /// <typeparam name="ISvc"></typeparam>
        /// <param name="instance"></param>
        public static void RegisterSingleton<ISvc>(ISvc instance)
        {
            container.RegisterInstance<ISvc>(instance);
        }
        public static void RegisterSingleton<ISvc>(string name, ISvc instance)
        {
            container.RegisterInstance<ISvc>(name, instance);
        }

        #region Registers types from app.config,added by QXM 2016/11/24
        public static void RegisterTypesFromConfig()
        {
            UnityConfigurationSection configuration = 
                (UnityConfigurationSection)ConfigurationManager.GetSection(UnityConfigurationSection.SectionName);
            configuration.Configure(container, "DBTypes");
        }
        #endregion
    }
}
