/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Setup.CloudCommunicationConfig

 *文件名：  ServiceConfigForm
 *创建人：  王颖辉
 *创建时间：2016-12-02
 *描述：请求基类
/************************************************************************************/
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;

namespace iCMS.Setup.CloudCommunicationConfig
{
    #region 云通讯配置
    /// <summary>
    /// 云通讯配置
    /// </summary>
    public partial class ServiceConfigForm : Form
    {
        #region 变更
        private bool IsConnOK { get; set; }
        private string ConnectionString { get; set; }
        private bool IsSaveOK { get; set; }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int Width, int Height, int flags);
        /// <summary> 
        /// 得到当前活动的窗口 
        /// </summary> 
        /// <returns></returns> 
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern System.IntPtr GetForegroundWindow();
        #endregion

        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        public ServiceConfigForm()
        {
            InitializeComponent();

            IsConnOK = false;
            IsSaveOK = false;
            ConnectionString = string.Empty;

            this.txtTestResult.ForeColor = Color.Red;
            this.codelbl.Visible = false;
            this.keylbl.Visible = false;
            this.secretlbl.Visible = false;
            this.serveraddresslbl.Visible = false;
            this.ploxylbl.Visible = false;
            this.cloudlbl.Visible = false;

            #region 加载


            var rootPath = System.Environment.CurrentDirectory;
            var appFile = rootPath + "/iCMS.CloudCommunication.WindowsService.exe.config";
            //appFile = rootPath + "/CloudCommunicationHost.exe.config";
            //appFile = "F:/iCMS.WG.AgentWindowsService.exe.config";
            //如果找到Web.config
            if (File.Exists(appFile))
            {
                XmlDocument configDoc = new XmlDocument();

                configDoc.Load(appFile);

                XmlNode appSettingsNode = configDoc.SelectSingleNode("configuration/appSettings");

                //遍历所有子节点，找到ServiceIPAddress节点并返回
                foreach (XmlNode node in appSettingsNode.ChildNodes)
                {
                    if (node.NodeType != XmlNodeType.Element)
                    {
                        continue;
                    }
                    XmlElement xele = (XmlElement)node;
                    var keyValue = xele.GetAttribute("key");
                    switch (keyValue)
                    {
                        case "CloudCode":
                            this.codeTxt.Text = xele.GetAttribute("value");
                            break;
                        case "ServiceIpAddress":
                            this.tbServerUrl.Text = xele.GetAttribute("value");
                            break;
                        case "CloudProxyURL":
                            this.txt_CloudProxyAddress.Text = xele.GetAttribute("value");
                            break;
                        case "CloudAddress":
                            this.tbCloudAddress.Text = xele.GetAttribute("value");
                            break;
                        case "Secret":
                            this.tbSecret.Text = xele.GetAttribute("value");
                            break;
                        case "Key":
                            this.tbKey.Text = xele.GetAttribute("value");
                            break;
                    }
                }
            }
            #endregion
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            bool isReturn = false;

            var code = this.codeTxt.Text.Trim();
            var serverUrl = this.tbServerUrl.Text.Trim();
            var proxyAddress = this.txt_CloudProxyAddress.Text.Trim();
            var cloudAddress = this.tbCloudAddress.Text.Trim();
            var key = this.tbKey.Text.Trim();
            var secret = this.tbSecret.Text.Trim();
            if (string.IsNullOrEmpty(code))
            {
                isReturn = true;
                this.codelbl.Visible = true;
            }
            if (string.IsNullOrEmpty(serverUrl))
            {
                isReturn = true;
                this.serveraddresslbl.Visible = true;
            }
            if (string.IsNullOrEmpty(proxyAddress))
            {
                isReturn = true;
                this.ploxylbl.Visible = true;
            }
            if (string.IsNullOrEmpty(cloudAddress))
            {
                isReturn = true;
                this.cloudlbl.Visible = true;
            }

            if (string.IsNullOrEmpty(key))
            {
                isReturn = true;
                this.keylbl.Visible = true;
            }

            if (string.IsNullOrEmpty(secret))
            {
                isReturn = true;
                this.secretlbl.Visible = true;
            }
            if (isReturn)
            {
                return;
            }

            var rootPath = System.Environment.CurrentDirectory;
            var appFile = rootPath + "/iCMS.CloudCommunication.WindowsService.exe.config";
            //appFile = rootPath + "/CloudCommunicationHost.exe.config";
            //appFile = "F:/App.config";
            try
            {
                //如果找到Web.config
                if (File.Exists(appFile))
                {
                    XmlDocument configDoc = new XmlDocument();
                    configDoc.Load(appFile);

                    //添加云平台地址和Code

                    XmlNode appSettings = configDoc.SelectSingleNode("configuration/appSettings");

                    //遍历所有子节点，找到ServiceIPAddress节点并返回
                    foreach (XmlNode node in appSettings.ChildNodes)
                    {
                        if (node.NodeType != XmlNodeType.Element)
                        {
                            continue;
                        }

                        XmlElement xele = (XmlElement)node;
                        var keyValue = xele.GetAttribute("key");


                        switch (keyValue)
                        {
                            case "CloudCode":
                                xele.SetAttribute("value", this.codeTxt.Text.Trim());
                                break;
                            case "ServiceIpAddress":
                                xele.SetAttribute("value", this.tbServerUrl.Text.Trim());
                                break;
                            case "CloudProxyURL":
                                xele.SetAttribute("value", this.txt_CloudProxyAddress.Text.Trim());
                                break;
                            case "CloudAddress":
                                xele.SetAttribute("value", this.tbCloudAddress.Text.Trim());
                                break;
                            case "Secret":
                                xele.SetAttribute("value", this.tbSecret.Text.Trim());
                                break;
                            case "Key":
                                xele.SetAttribute("value", this.tbKey.Text.Trim());
                                break;
                        }
                    }

                    configDoc.Save(appFile);
                    MessageBox.Show("配置文件保存成功！");
                    if (WindowsServiceHelp.WindowsServiceHelp.IsServiceStart("iCMS.CloudCommunicationService"))
                    {
                        WindowsServiceHelp.WindowsServiceHelp.StopService("iCMS.CloudCommunicationService");
                        WindowsServiceHelp.WindowsServiceHelp.StartService("iCMS.CloudCommunicationService");
                    }

                    IsSaveOK = true;
                    Application.Exit();
                }
                else
                {
                    MessageBox.Show("Web.config 配置文件未找到！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 加载
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ServiceConfigForm_Load(object sender, EventArgs e)
        {
            SetWindowPos(this.Handle, -1, 0, 0, 0, 0, 1 | 2); //最后参数也有用1 | 4
        }
        #endregion

        #region 输入验证
        /// <summary>
        /// 输入验证
        /// </summary>
        /// <returns></returns>
        private bool InputValidate()
        {
            bool isAvialiab = true;

            return isAvialiab;
        }
        #endregion

        #region 主机
        /// <summary>
        /// 主机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtHost_KeyUp(object sender, KeyEventArgs e)
        {
            ResetConnectState();

        }
        #endregion

        #region 用户Id
        /// <summary>
        /// 用户Id
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUserID_KeyUp(object sender, KeyEventArgs e)
        {
            ResetConnectState();

        }
        #endregion

        #region 密码
        /// <summary>
        /// 密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUserPwd_KeyUp(object sender, KeyEventArgs e)
        {
            ResetConnectState();

        }
        #endregion

        #region 数据库名称
        /// <summary>
        /// 数据库名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDbName_KeyUp(object sender, KeyEventArgs e)
        {
            ResetConnectState();

        }
        #endregion

        #region 重置
        /// <summary>
        /// 重置
        /// </summary>
        private void ResetConnectState()
        {
            IsConnOK = false;
            this.txtTestResult.Text = string.Empty;
        }
        #endregion

        #region 设置状态
        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="state"></param>
        private void SetButtonState(bool state)
        {
            this.btnSave.Enabled = state;
        }
        #endregion

        #region 关闭
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ServiceConfigForm_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            if (IsSaveOK) { return; }
            if (DialogResult.OK == MessageBox.Show("关闭应用程序，只能手动配置连接字符串，你确定要关闭应用程序吗？", "关闭提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                this.FormClosing -= new FormClosingEventHandler(this.ServiceConfigForm_FormClosing_1);//为保证Application.Exit();时不再弹出提示，所以将FormClosing事件取消
                Application.Exit();//退出整个应用程序
            }
            else
            {
                e.Cancel = true;  //取消关闭事件
            }
        }
        #endregion

        #region 按下事件
        private void codeTxt_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.codeTxt.Text.Trim()))
            {
                this.codelbl.Visible = false;
            }
        }

        private void tbKey_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.tbKey.Text.Trim()))
            {
                this.keylbl.Visible = false;
            }
        }

        private void tbSecret_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.tbSecret.Text.Trim()))
            {
                this.secretlbl.Visible = false;
            }
        }

        private void tbServerUrl_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.tbServerUrl.Text.Trim()))
            {
                this.serveraddresslbl.Visible = false;
            }
        }

        private void txt_CloudProxyAddress_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txt_CloudProxyAddress.Text.Trim()))
            {
                this.ploxylbl.Visible = false;
            }
        }

        private void tbCloudAddress_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.tbCloudAddress.Text.Trim()))
            {
                this.cloudlbl.Visible = false;
            }
        }
        #endregion

    }
    #endregion
}