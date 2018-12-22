/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.Setup.CloudProxyConfig
 *文件名：  CloudProxyConfigForm
 *创建人：  张辽阔
 *创建时间：2016-12-23
 *描述：配置云代理工具
 ************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using WindowsServiceHelp;

namespace iCMS.Setup.CloudProxyConfig
{
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-12-23
    /// 创建记录：配置云代理工具
    /// </summary>
    public partial class CloudProxyConfigForm : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int Width, int Height, int flags);

        /// <summary> 
        /// 得到当前活动的窗口 
        /// </summary> 
        /// <returns></returns> 
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern System.IntPtr GetForegroundWindow();

        /// <summary>
        /// 正在保存
        /// </summary>
        private bool IsSaveOK { get; set; }

        /// <summary>
        /// 保存老的云通讯地址
        /// </summary>
        private string OldCommunicationAddress { get; set; }

        /// <summary>
        /// 钥匙
        /// </summary>
        private string OldKey { get; set; }

        /// <summary>
        /// 密匙
        /// </summary>
        private string OldSecret { get; set; }


        public CloudProxyConfigForm()
        {
            InitializeComponent();

            this.IsSaveOK = false;

            var appFile = Environment.CurrentDirectory + "/iCMS.CloudProxy.WindowsService.exe.config";

            try
            {
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
                            case "CloudCommunicationURL":
                                this.OldCommunicationAddress = xele.GetAttribute("value");
                                this.txtCloudCommunicationAddress.Text = this.OldCommunicationAddress;
                             
                                break;
                            case "Key":
                                this.OldKey= xele.GetAttribute("value");
                                this.tbKey.Text = OldKey;
                                break;
                            case "Secret":
                                this.OldSecret= xele.GetAttribute("value");
                                this.tbSecret.Text = OldSecret;
                                break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("iCMS.CloudProxy.WindowsService.exe.config 配置文件未找到！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-12-23
        /// 创建记录：界面加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloudProxyConfigForm_Load(object sender, EventArgs e)
        {
            this.lbl_ErrorMessage.Text = "";
            SetWindowPos(this.Handle, -1, 0, 0, 0, 0, 1 | 2); //最后参数也有用1 | 4
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-12-23
        /// 创建记录：界面关闭中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloudProxyConfigForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsSaveOK)
                return;

            if (DialogResult.OK == MessageBox.Show("关闭应用程序，只能手动配置应用程序，你确定要关闭程序吗？", "关闭提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                this.FormClosing -= new FormClosingEventHandler(this.CloudProxyConfigForm_FormClosing);//为保证Application.Exit();时不再弹出提示，所以将FormClosing事件取消
                Application.Exit();//退出整个应用程序
            }
            else
                e.Cancel = true;  //取消关闭事件
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-12-23
        /// 创建记录：
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCloudCommunicationAddress_MouseUp(object sender, MouseEventArgs e)
        {
            this.lbl_ErrorMessage.Text = "";
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-12-23
        /// 创建记录：按钮重置事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Reset_Click(object sender, EventArgs e)
        {
            this.lbl_ErrorMessage.Text = "";
            this.txtCloudCommunicationAddress.Text = this.OldCommunicationAddress;
            this.tbKey.Text = this.OldKey;
            this.tbSecret.Text = this.OldSecret;
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-12-23
        /// 创建记录：按钮保存事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtCloudCommunicationAddress.Text.Trim()))
            {
                this.lbl_ErrorMessage.Text = "请输入云通讯服务地址！";
                return;
            }

            if (string.IsNullOrEmpty(this.tbKey.Text.Trim()))
            {
                this.lbl_ErrorMessage.Text = "请输入钥匙！";
                return;
            }

            if (string.IsNullOrEmpty(this.tbSecret.Text.Trim()))
            {
                this.lbl_ErrorMessage.Text = "请输入密钥！";
                return;
            }

            var appFile = Environment.CurrentDirectory + "/iCMS.CloudProxy.WindowsService.exe.config";

            try
            {
                //如果找到Web.config
                if (File.Exists(appFile))
                {
                    IsSaveOK = true;

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
                        if (keyValue.Equals("CloudCommunicationURL"))
                        {
                            xele.SetAttribute("value", this.txtCloudCommunicationAddress.Text.Trim());
                            continue;
                        }

                        //王颖辉 2016-12-26 Key
                        keyValue = xele.GetAttribute("key");
                        if (keyValue.Equals("Key"))
                        {
                            xele.SetAttribute("value", this.tbKey.Text.Trim());
                            continue;
                        }

                        //王颖辉 2016-12-23 Secret
                        keyValue = xele.GetAttribute("key");
                        if (keyValue.Equals("Secret"))
                        {
                            xele.SetAttribute("value", this.tbSecret.Text.Trim());
                            continue;
                        }
                    }
                    configDoc.Save(appFile);
                    MessageBox.Show("配置文件保存成功！");
                    if (WindowsServiceHelp.WindowsServiceHelp.IsServiceStart("iCMS.CloudProxyService"))
                    {
                        WindowsServiceHelp.WindowsServiceHelp.StopService("iCMS.CloudProxyService");
                        WindowsServiceHelp.WindowsServiceHelp.StartService("iCMS.CloudProxyService");
                    }
                    Application.Exit();
                }
                else
                {
                    MessageBox.Show("iCMS.CloudProxy.WindowsService.exe.config 配置文件未找到！");
                }
            }
            catch (Exception ex)
            {
                IsSaveOK = false;
                MessageBox.Show(ex.Message);
            }
        }
    }
}