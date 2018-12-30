/************************************************************************************
 *Copyright (c) 2017iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Tool 
 *文件名：  AppConfigHelper 
 *创建人：  王颖辉
 *创建时间：2017/11/21 11:02:21
 *描述：配置处理类
 *
 *修改人：
 *修改时间：
 *描述：
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Tool
{
    /// <summary>
    /// 配置处理类
    /// </summary>
    public class AppConfigHelper
    {
        static Configuration config = null;
        public static void SetConfigPath(string filePath)
        {
            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = filePath;
            config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
        }
        private T GetConfigValue<T>(string hashKey, T defaultValue)
        {
            try
            {
                return (T)Convert.ChangeType(ConfigurationManager.AppSettings[hashKey], typeof(T));

            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 修改AppSettings中配置项的内容
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetConfigValue(string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.AppSettings.Settings[key] != null)
                config.AppSettings.Settings[key].Value = value;
            else
                config.AppSettings.Settings.Add(key, value);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        /// <summary>
        /// 修改AppSettings中配置项的内容
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static string GetConfigValue(string key)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var value = string.Empty;
            if (config.AppSettings.Settings[key] != null)
                value = config.AppSettings.Settings[key].Value;
            return value;
        }


        /// <summary>
        /// 读取EndpointAddress
        /// </summary>
        /// <param name="endpointName"></param>
        /// <returns></returns>
        public static string GetEndpointAddress(string endpointName)
        {
            ServicesSection servicesSection = config.GetSection("system.serviceModel/services") as ServicesSection;
            foreach (ServiceElement service in servicesSection.Services)
            {
                foreach (ServiceEndpointElement item in service.Endpoints)
                {
                    if (item.Name == endpointName)
                        return item.Address.ToString();
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取终节点集合
        /// </summary>
        /// <returns></returns>
        public static List<AppInfo> GetEndPoint()
        {
            ServicesSection servicesSection = config.GetSection("system.serviceModel/services") as ServicesSection;
            List<AppInfo> list = new List<AppInfo>();
            foreach (ServiceElement service in servicesSection.Services)
            {
                string endpointName = service.Name;
                foreach (BaseAddressElement item in service.Host.BaseAddresses)
                {
                    AppInfo appInfo = new AppInfo();
                    appInfo.Key = endpointName;
                    appInfo.Value = item.BaseAddress;
                    list.Add(appInfo);
                }
            }
            return list;
        }

        /// <summary>
        /// 设置EndpointAddress
        /// </summary>
        /// <param name="endpointName"></param>
        /// <param name="address"></param>
        public static void SetEndpointAddress(string endpointName, string address)
        {
            ServicesSection clientSection = config.GetSection("system.serviceModel/services") as ServicesSection;
            foreach (ServiceElement service in clientSection.Services)
            {

                if (service.Name == endpointName)
                {
                    foreach (BaseAddressElement item in service.Host.BaseAddresses)
                    {
                        item.BaseAddress = address;
                    }
                    break;
                }
            }
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("system.serviceModel");
        }
    }

    #region AppInfo
    /// <summary>
    /// AppInfo
    /// </summary>
    public class AppInfo
    {
        public string Key
        {
            get;
            set;
        }

        public string Value
        {
            get;
            set;
        }
    }
    #endregion

}
