using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace iCMS.Setup.DBUpgrade.UpgradeFM.Common
{
    /// <summary>
    /// 最对winform的配置文件修改(现改现用)
    /// </summary>
    public class AppSettingHelper
    {
        #region Appconfig操作

        /// <summary>
        /// 修改App.config的Appsetting节点的配置信息
        /// </summary>
        /// <param name="AppKey">key</param>
        /// <param name="AppValue">Value</param>
        public static void SetValue(string AppKey, string AppValue)
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                //获取App.config文件绝对路径
                var rootPath = System.Environment.CurrentDirectory;
                var appFile = rootPath + "/iCMS.Setup.DBUpgrade.UpgradeFM.exe.config";


                xDoc.Load(appFile);

                XmlNode xNode;
                XmlElement xElem1;
                //修改完文件内容，还需要修改缓存里面的配置内容，使得刚修改完即可用
                //如果不修改缓存，需要等到关闭程序，在启动，才可使用修改后的配置信息
                Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                xNode = xDoc.SelectSingleNode("//connectionStrings");
                xElem1 = (XmlElement)xNode.SelectSingleNode("//add[@name='" + AppKey + "']");
                if (xElem1 != null)
                {
                    xElem1.SetAttribute("connectionString", AppValue);
                    //cfa.AppSettings.Settings["AppKey"].Value = AppValue;
                   // cfa.ConnectionStrings.
                }
                //改变缓存中的配置文件信息（读取出来才会是最新的配置）
                cfa.Save();
                ConfigurationManager.RefreshSection("connectionStrings");

                xDoc.Save(appFile);
               
                //Properties.Settings.Default.Reload();
            }
            catch (Exception e)
            {
                string error = e.Message;

            }
        }

        /// <summary>
        /// 获取节点信息【推荐】
        /// </summary>
        /// <param name="appKey">Key</param>
        /// <returns></returns>
        public static String GetValue(String appKey)
        {
            return ConfigurationManager.AppSettings[appKey];
        }

        /// <summary>
        /// 读配置文件里面 Appsetting节点的内容【不推荐】
        /// </summary>
        /// <param name="strExecutablePath"></param>
        /// <param name="appKey"></param>
        /// <returns></returns>
        public static String GetValueByXml(String appKey)
        {
            XmlDocument xDoc = new XmlDocument();
            try
            {
                //获取App.config文件绝对路径
                String basePath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                basePath = basePath.Substring(0, basePath.Length - 10);
                String path = basePath + "App.config";
                xDoc.Load(path);

                XmlNode xNode;
                XmlElement xElem;
                xNode = xDoc.SelectSingleNode("//appSettings");
                xElem = (XmlElement)xNode.SelectSingleNode("//add[@key='" + appKey + "']");
                if (xElem != null)
                    return xElem.GetAttribute("value");
                else
                    return "";
            }
            catch (Exception)
            {
                return "";
            }
        }
        #endregion
    }
}
