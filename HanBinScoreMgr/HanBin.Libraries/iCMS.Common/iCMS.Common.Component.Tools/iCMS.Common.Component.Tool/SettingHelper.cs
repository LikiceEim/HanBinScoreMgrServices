/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Tool
 *文件名：  SettingHelper 
 *创建人：  王颖辉
 *创建时间：2017/11/1 11:15:47 
 *描述：初始化服务配置帮助类
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HanBin.Common.Component.Tool
{
    /// <summary>
    /// 初始化服务配置帮助类 
    /// </summary>
    public class SettingHelper : IDisposable
    {
        #region 私有成员
        private string _ServiceName;
        private string _DisplayName;
        private string _Description;
        #endregion

        #region 构造函数
        /// <summary> 
        /// 初始化服务配置帮助类 
        /// </summary> 
        public SettingHelper()
        {
            InitSettings();
        }
        #endregion

        #region 属性
        /// <summary> 
        /// 系统用于标志此服务的名称 
        /// </summary> 
        public string ServiceName
        {
            get { return _ServiceName; }
        }
        /// <summary> 
        /// 向用户标志服务的友好名称 
        /// </summary> 
        public string DisplayName
        {
            get { return _DisplayName; }
        }
        /// <summary> 
        /// 服务的说明 
        /// </summary> 
        public string Description
        {
            get { return _Description; }
        }
        #endregion

        #region 私有方法
        #region 初始化服务配置信息
        /// <summary> 
        /// 初始化服务配置信息 
        /// </summary> 
        private void InitSettings()
        {
            var rootPath = System.Environment.CurrentDirectory;
            var appFile = rootPath + "/iCMS.Server.WindowsService.exe.config";
            if (File.Exists(appFile))
            {
                //系统用于标志此服务名称(唯一性)
                _ServiceName = Get_ConfigValue(appFile, "ServiceName");
                ////向用户标志服务的显示名称(可以重复)
                _DisplayName = Get_ConfigValue(appFile, "ServiceDisplayName");
                ////服务的说明(描述)
                _Description = Get_ConfigValue(appFile, "ServiceDescription");
            }
            else
            {
               // throw new FileNotFoundException("未能找到服务名称配置文件 ChinaHN.XHService.exe.config！路径:" + xmlfile);
            }

        }
        /// <summary>
        /// 读取 XML中指定节点值
        /// </summary>
        /// <param name="configpath">配置文件路径</param>
        /// <param name="strKeyName">键值</param>        
        /// <returns></returns>
        protected static string Get_ConfigValue(string configpath, string strKeyName)
        {
            using (XmlTextReader tr = new XmlTextReader(configpath))
            {
                while (tr.Read())
                {
                    if (tr.NodeType == XmlNodeType.Element)
                    {
                        if (tr.Name == "add" && tr.GetAttribute("key") == strKeyName)
                        {
                            return tr.GetAttribute("value");
                        }
                    }
                }
            }
            return "";
        }
        #endregion
        #endregion

        #region IDisposable 成员
        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    //managed dispose 
                    _ServiceName = null;
                    _DisplayName = null;
                    _Description = null;
                }
                //unmanaged dispose 
            }
            disposed = true;
        }
        ~SettingHelper()
        {
            Dispose(false);
        }
        #endregion 
    }
}
