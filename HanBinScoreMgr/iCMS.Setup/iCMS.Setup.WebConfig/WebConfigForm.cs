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

namespace iCMS.Setup.WebConfig
{
    public partial class WebConfigForm : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int Width, int Height, int flags);
        /// <summary> 
        /// 得到当前活动的窗口 
        /// </summary> 
        /// <returns></returns> 
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern System.IntPtr GetForegroundWindow();

        private bool IsSaveOK { get; set; }

        private string OriServiceAdd { get; set; }
        private string OriKey { get; set; }
        private string OriSecret { get; set; }

        public WebConfigForm()
        {
            InitializeComponent();

            this.lblError.ForeColor = Color.Red;
            this.IsSaveOK = false;
            this.keylbl.Visible = false;
            this.secretlbl.Visible = false;
            this.serverlbl.Visible = false;
            #region

            var rootPath = System.Environment.CurrentDirectory;
            var appFile = rootPath + "/Web.config";

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
                        var keyToken = xele.GetAttribute("key");

                        switch (keyToken)
                        {
                            case "ServiceIpAddress":
                                this.txtIPAddress.Text = this.OriServiceAdd = xele.GetAttribute("value");
                                break;
                            case "Key":
                                this.txtKey.Text = this.OriKey = xele.GetAttribute("value");
                                break;
                            case "Secret":
                                this.txtSecret.Text = this.OriSecret = xele.GetAttribute("value");
                                break;
                        }                     
                    }
                }
                else
                {
                    MessageBox.Show("Web.config 配置文件未找到！");
                }

                this.txtKey.Text = this.txtKey.Text.Trim();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            #endregion
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.txtIPAddress.Text = OriServiceAdd;
            this.lblError.Text = string.Empty;

            //Key,Secret
            this.txtKey.Text = this.OriKey;
            this.txtSecret.Text = this.OriSecret;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            bool isReturn = false;

            var key = this.txtKey.Text.Trim();
            var secret = this.txtSecret.Text.Trim();
            var server = this.txtIPAddress.Text.Trim();
      
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
            if (string.IsNullOrEmpty(server))
            {
                isReturn = true;
                this.serverlbl.Visible = true;
            }
            if (isReturn)
            {
                return;
            }
            //获取当前程序根目录
            var rootPath = System.Environment.CurrentDirectory;
            var appFile = rootPath + "/Web.config";

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
                        var keyToken = xele.GetAttribute("key");

                        switch (keyToken)
                        {
                            case "ServiceIpAddress":
                                xele.SetAttribute("value", this.txtIPAddress.Text.Trim());
                                break;
                            case "Key":
                                xele.SetAttribute("value", this.txtKey.Text.Trim());
                                break;
                            case "Secret":
                                xele.SetAttribute("value", this.txtSecret.Text.Trim());
                                break;
                        }                     
                    }
                    configDoc.Save(appFile);
                    MessageBox.Show("配置文件保存成功！");
                   // Process.Start("iisreset");
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

        private void txtIPAddress_MouseUp(object sender, MouseEventArgs e)
        {
            this.lblError.Text = string.Empty;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetWindowPos(this.Handle, -1, 0, 0, 0, 0, 1 | 2); //最后参数也有用1 | 4
        }

        private void WebConfigForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsSaveOK) { return; }
            if (DialogResult.OK == MessageBox.Show("关闭应用程序，只能手动配置应用程序，你确定要关闭程序吗？", "关闭提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                this.FormClosing -= new FormClosingEventHandler(this.WebConfigForm_FormClosing);//为保证Application.Exit();时不再弹出提示，所以将FormClosing事件取消
                Application.Exit();//退出整个应用程序
            }
            else
            {
                e.Cancel = true;  //取消关闭事件
            }
        }

        #region 按下事件
        private void txtKey_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtKey.Text.Trim()))
            {
                this.keylbl.Visible = false;
            }
        }

        private void txtSecret_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtSecret.Text.Trim()))
            {
                this.secretlbl.Visible = false;
            }
        }

        private void txtIPAddress_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtIPAddress.Text.Trim()))
            {
                this.serverlbl.Visible = false;
            }
        }
        #endregion

    }
}
