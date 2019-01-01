/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Tool
 *文件名：  ConfigHelper
 *创建人：  王颖辉
 *创建时间：2017-11-11
 *描述：配置操作类
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Tool
{
    /// <summary>
    /// 配置操作类
    /// </summary>
    public class ConfigHelper
    {
        #region 从配置文件获取Value
        /// <summary>
        /// 从配置文件获取Value
        /// </summary>
        /// <param name="key">配置文件中key字符串</param>
        /// <returns></returns>
        public static string GetValueFromConfig(string key)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                //获取AppSettings的节点 
                AppSettingsSection appsection = (AppSettingsSection)config.GetSection("appSettings");
                return appsection.Settings[key].Value;
            }
            catch
            {
                return "";
            }
        }
        #endregion

        #region 设置配置文件
        /// <summary>
        /// 设置配置文件
        /// </summary>
        /// <param name="key">配置文件中key字符串</param>
        /// <param name="value">配置文件中value字符串</param>
        /// <returns></returns>
        public static bool SetValueFromConfig(string key, string value)
        {
            try
            {
                //打开配置文件 
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                //获取AppSettings的节点 
                AppSettingsSection appsection = (AppSettingsSection)config.GetSection("appSettings");
                appsection.Settings[key].Value = value;
                config.Save();

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
