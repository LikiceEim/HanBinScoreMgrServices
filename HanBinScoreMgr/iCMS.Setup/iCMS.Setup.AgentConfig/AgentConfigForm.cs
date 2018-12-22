using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;

namespace iCMS.Setup.AgentConfig
{
    public partial class AgentConfigForm : Form
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

        /// <summary>
        /// 修改人：张辽阔
        /// 修改时间：2017-06-15
        /// 修改记录：增加是否启动看门狗和最大温度采集时间间隔倍数配置项
        /// </summary>
        public AgentConfigForm()
        {
            InitializeComponent();

            this.lblErrorCLIPortName.Visible = false;
            this.lblErrorPortName.Visible = false;
            this.lblErrorWGID.Visible = false;
            this.lblErrorServiceAdd.Visible = false;
            this.lblErrorKey.Visible = false;
            this.lblErrorSecrect.Visible = false;
            this.lblErrorMaxTemperatureTimeMulti.Visible = false;
            //this.ControlBox = false;
            IsSaveOK = false;

            #region Read src
            var rootPath = System.Environment.CurrentDirectory;
            var appFile = rootPath + "/iCMS.WG.AgentWindowsService.exe.config";
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
                        case "ApiPortName":
                            this.txtPortName.Text = xele.GetAttribute("value");
                            break;

                        case "CliPortName":
                            this.txtCLIPortName.Text = xele.GetAttribute("value");
                            break;

                        case "iCMSServer":
                            this.txtServiceAdd.Text = xele.GetAttribute("value");
                            break;

                        case "HostIP":
                            this.txtIP.Text = xele.GetAttribute("value");
                            break;

                        case "Secret":
                            this.txtSecret.Text = xele.GetAttribute("value");
                            break;

                        case "Key":
                            this.txtKey.Text = xele.GetAttribute("value");
                            break;

                        //是否启动看门狗
                        case "IsStartWatchdog":
                            if (xele.GetAttribute("value") == "1")
                                this.rbtn_Start.Checked = true;
                            else
                                this.rbtn_Stop.Checked = true;
                            break;

                        //最大温度采集时间间隔倍数
                        case "MaxTemperatureTimeMulti":
                            this.txtMaxTemperatureTimeMulti.Text = xele.GetAttribute("value");
                            break;
                    }
                }
            }
            #endregion
        }

        /// <summary>
        /// 修改人：张辽阔
        /// 修改时间：2017-06-15
        /// 修改记录：增加是否启动看门狗和最大温度采集时间间隔倍数配置项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            bool isReturn = false;

            var portName = this.txtPortName.Text.Trim();
            var cliPortName = this.txtCLIPortName.Text.Trim();
            var serviceAdd = this.txtServiceAdd.Text.Trim();
            var ip = this.txtIP.Text.Trim();
            var key = this.txtKey.Text.Trim();
            var secret = this.txtSecret.Text.Trim();
            var maxTemperatureTimeMulti = this.txtMaxTemperatureTimeMulti.Text.Trim();
            if (string.IsNullOrEmpty(portName))
            {
                isReturn = true;
                this.lblErrorPortName.Visible = true;
            }
            if (string.IsNullOrEmpty(cliPortName))
            {
                isReturn = true;
                this.lblErrorCLIPortName.Visible = true;
            }
            if (string.IsNullOrEmpty(serviceAdd))
            {
                isReturn = true;
                this.lblErrorServiceAdd.Visible = true;
            }
            if (string.IsNullOrEmpty(ip))
            {
                isReturn = true;
                this.lblErrorWGID.Visible = true;
            }

            if (string.IsNullOrEmpty(key))
            {
                isReturn = true;
                this.lblErrorKey.Visible = true;
            }

            if (string.IsNullOrEmpty(secret))
            {
                isReturn = true;
                this.lblErrorSecrect.Visible = true;
            }

            if (string.IsNullOrWhiteSpace(maxTemperatureTimeMulti))
            {
                isReturn = true;
                this.lblErrorMaxTemperatureTimeMulti.Text = "请输入最大温度采集时间间隔倍数";
                this.lblErrorMaxTemperatureTimeMulti.Visible = true;
            }
            else
            {
                float maxTemperatureTimeMultiFloat;
                if (!float.TryParse(maxTemperatureTimeMulti, out maxTemperatureTimeMultiFloat))
                {
                    isReturn = true;
                    this.lblErrorMaxTemperatureTimeMulti.Text = "最大温度采集时间间隔倍数格式错误";
                    this.lblErrorMaxTemperatureTimeMulti.Visible = true;
                }
            }

            if (isReturn)
            {
                return;
            }

            //Save
            try
            {
                var rootPath = System.Environment.CurrentDirectory;
                var appFile = rootPath + "/iCMS.WG.AgentWindowsService.exe.config";
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
                            case "ApiPortName":
                                xele.SetAttribute("value", portName);
                                break;

                            case "CliPortName":
                                xele.SetAttribute("value", cliPortName);
                                break;

                            case "iCMSServer":
                                xele.SetAttribute("value", serviceAdd);
                                break;

                            case "HostIP":
                                xele.SetAttribute("value", ip);
                                break;

                            case "Secret":
                                xele.SetAttribute("value", secret);
                                break;

                            case "Key":
                                xele.SetAttribute("value", key);
                                break;

                            //是否启动看门狗
                            case "IsStartWatchdog":
                                xele.SetAttribute("value", this.rbtn_Start.Checked ? "1" : "0");
                                break;

                            //最大温度采集时间间隔倍数
                            case "MaxTemperatureTimeMulti":
                                xele.SetAttribute("value", maxTemperatureTimeMulti);
                                break;
                        }
                    }
                    configDoc.Save(appFile);
                    MessageBox.Show("配置文件保存成功！");
                    if (WindowsServiceHelp.WindowsServiceHelp.IsServiceStart("iCMS.WG.AgentGuard"))
                    {
                        WindowsServiceHelp.WindowsServiceHelp.StopService("iCMS.WG.AgentGuard");
                        WindowsServiceHelp.WindowsServiceHelp.StopService("iCMS.WG.AgentService");
                        WindowsServiceHelp.WindowsServiceHelp.StartService("iCMS.WG.AgentService");
                        WindowsServiceHelp.WindowsServiceHelp.StartService("iCMS.WG.AgentGuard");
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

        private void AgentConfigForm_Load(object sender, EventArgs e)
        {
            SetWindowPos(this.Handle, -1, 0, 0, 0, 0, 1 | 2); //最后参数也有用1 | 4
        }

        private void AgentConfigForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsSaveOK) { return; }
            if (DialogResult.OK == MessageBox.Show("关闭应用程序，只能手动配置应用程序，你确定要关闭程序吗？",
                "关闭提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                //为保证Application.Exit();时不再弹出提示，所以将FormClosing事件取消
                this.FormClosing -= new FormClosingEventHandler(this.AgentConfigForm_FormClosing);
                Application.Exit();//退出整个应用程序
            }
            else
            {
                e.Cancel = true;  //取消关闭事件
            }
        }

        #region 按下事件

        private void txtPortName_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtPortName.Text.Trim()))
            {
                this.lblErrorPortName.Visible = false;
            }
        }

        private void txtCLIPortName_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtCLIPortName.Text.Trim()))
            {
                this.lblErrorCLIPortName.Visible = false;
            }
        }

        private void txtServiceAdd_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtServiceAdd.Text.Trim()))
            {
                this.lblErrorServiceAdd.Visible = false;


            }
        }

        private void txtIP_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtIP.Text.Trim()))
            {
                this.lblErrorWGID.Visible = false;
            }
        }

        private void tbKey_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtKey.Text.Trim()))
            {
                this.lblErrorKey.Visible = false;
            }
        }

        private void tbSecret_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtSecret.Text.Trim()))
            {
                this.lblErrorSecrect.Visible = false;
            }
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2017-06-15
        /// 创建记录：
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMaxTemperatureTimeMulti_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtMaxTemperatureTimeMulti.Text.Trim()))
            {
                this.lblErrorMaxTemperatureTimeMulti.Visible = false;
            }
        }

        #endregion
    }
}