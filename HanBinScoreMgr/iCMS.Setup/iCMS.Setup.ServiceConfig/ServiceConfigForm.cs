using iCMS.Common.Component.Tool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace iCMS.Setup.ServiceConfig
{
    public partial class ServiceConfigForm : Form
    {
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

        //上一次配置端口
        private string oldPort = string.Empty;

        public ServiceConfigForm()
        {
            InitializeComponent();

            IsConnOK = false;
            IsSaveOK = false;
            ConnectionString = string.Empty;

            this.txtTestResult.ForeColor = Color.Red;
            this.lblErrorDbName.Visible = false;
            this.lblErrorHost.Visible = false;
            this.lblErrorPwd.Visible = false;
            this.lblErrorUserID.Visible = false;
            this.lblkey.Visible = false;
            this.lblsecret.Visible = false;
            //this.ControlBox = false;
            #region MyRegion
            var rootPath = System.Environment.CurrentDirectory;
            var appFile = rootPath + "/iCMS.Server.WindowsService.exe.config";
            //appFile = "F:/App.config";

            //直接配置 王颖辉 2016-12-26
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
                            case "iCMS":
                                {
                                    string srcConn = EcanSecurity.Decode(xele.GetAttribute("value"));
                                    if (!string.IsNullOrEmpty(srcConn))
                                    {
                                        var arrKeyValue = srcConn.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                                        foreach (string innerArr in arrKeyValue)
                                        {
                                            var innerArrTemp = innerArr.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                                            switch (innerArrTemp[0])
                                            {
                                                case "Server":
                                                    this.txtHost.Text = innerArrTemp[1];
                                                    break;
                                                case "Initial Catalog":
                                                    this.txtDbName.Text = innerArrTemp[1];
                                                    break;
                                                case "User Id":
                                                    this.txtUserID.Text = innerArrTemp[1];
                                                    break;
                                                case "Password":
                                                    this.txtUserPwd.Text = innerArrTemp[1];
                                                    break;

                                            }
                                        }
                                    }
                                }
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
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

            #endregion
        }

        #region 数据库连接测试
        /// <summary>
        /// 数据库连接测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTestConn_Click(object sender, EventArgs e)
        {
            //判断输入是否有效
            bool isAvailab = InputValidate();

            if (!isAvailab)
            {
                return;
            }

            try
            {
                var tempConn = string.Format("Server={0};Initial Catalog={1};User Id={2};Password={3};MultipleActiveResultSets=true;Max Pool Size=150", this.txtHost.Text.Trim(), this.txtDbName.Text.Trim(), this.txtUserID.Text.Trim(), this.txtUserPwd.Text.Trim());
                using (SqlConnection connection = new SqlConnection(tempConn))
                {
                    SetButtonState(false);
                    connection.Open();

                    if (connection.State == ConnectionState.Open)
                    {
                        IsConnOK = true;
                        ConnectionString = tempConn;
                        SetButtonState(true);
                        MessageBox.Show("连接成功！");
                    }
                    else
                    {
                        SetButtonState(false);
                        MessageBox.Show("连接失败！");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                SetButtonState(true);
            }
        }
        #endregion

        #region 保存数据库
        /// <summary>
        /// 保存数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsConnOK)
            {
                Thread thread = new Thread(new ThreadStart(SetDB));
                thread.IsBackground = true;
                thread.Start();
            }
            else
            {
                MessageBox.Show("请先测试连接是否成功！");
            }
        }
        #endregion

        #region 设置数据库
        /// <summary>
        /// 设置数据库
        /// </summary>
        private void SetDB()
        {
            try
            {

                #region 配置数据库
                var rootPath = System.Environment.CurrentDirectory;
                var appFile = rootPath + "/iCMS.Server.WindowsService.exe.config";

                //如果找到Web.config
                if (File.Exists(appFile))
                {
                    XmlDocument configDoc = new XmlDocument();
                    configDoc.Load(appFile);

                    //添加云平台地址和Code

                    XmlNode appSettings = configDoc.SelectSingleNode("configuration/appSettings");

                    string serverName = string.Empty;
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
                            case "iCMS":
                                {
                                    string sql = "Server={0};Initial Catalog={1};User Id={2};Password={3};MultipleActiveResultSets=true;Max Pool Size=150";
                                    sql = string.Format(sql, txtHost.Text.Trim(), txtDbName.Text.Trim(), txtUserID.Text.Trim(), txtUserPwd.Text.Trim());
                                    xele.SetAttribute("value", EcanSecurity.Encode(sql));
                                }
                                break;
                            case "Secret":
                                string secret = this.tbSecret.Text.Trim();
                                xele.SetAttribute("value", secret);
                                break;
                            case "Key":
                                string key = this.tbKey.Text.Trim();
                                xele.SetAttribute("value", key);
                                break;
                            case "ServiceName":
                                serverName = xele.GetAttribute("value");
                                break;
                        }
                    }
                    configDoc.Save(appFile);

                    //判断服务是否安装，如果安装则进行重新服务

                    #region 验证服务是否存在
                    var serviceName = this.txtServiceName.Text.Trim();
                    var serviceControllers = ServiceController.GetServices();
                    if (!string.IsNullOrWhiteSpace(serviceName))
                    {
                        var server = serviceControllers.FirstOrDefault(service => service.ServiceName == serviceName);
                        if (server != null)
                        {
                            //重新服务
                            if (WindowsServiceHelp.WindowsServiceHelp.IsServiceStart(serviceName))
                            {
                                WindowsServiceHelp.WindowsServiceHelp.StopService(serviceName);
                                WindowsServiceHelp.WindowsServiceHelp.StartService(serviceName);
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    MessageBox.Show(this, "配置文件未找到！");
                    return;
                }

                #endregion
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);

                MessageBox.Show(this, "配置出错");
            }
            finally
            {
                this.BeginInvoke(new MethodInvoker(delegate ()
                {
                    btnSaveDB.Enabled = true;
                }));

                MessageBox.Show(this, "保存数据库连接成功。");
            }
        }
        #endregion


        #region 设置服务名称
        /// <summary>
        /// 设置服务名称
        /// </summary>
        private void SetServiceName()
        {
            try
            {

                #region 配置数据库
                var rootPath = System.Environment.CurrentDirectory;
                var appFile = rootPath + "/iCMS.Server.WindowsService.exe.config";

                //如果找到Web.config
                if (File.Exists(appFile))
                {
                    XmlDocument configDoc = new XmlDocument();
                    configDoc.Load(appFile);

                    //添加云平台地址和Code

                    XmlNode appSettings = configDoc.SelectSingleNode("configuration/appSettings");

                    string serverName = string.Empty;
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
                            case "ServiceName":
                                serverName = xele.GetAttribute("value");
                                break;
                        }
                    }
                    configDoc.Save(appFile);
                }
                else
                {
                    MessageBox.Show(this, "配置文件未找到！");
                    return;
                }

                #endregion

                //判断文件是否存在
                if (File.Exists(appFile))
                {
                    XmlDocument configDoc = new XmlDocument();
                    configDoc.Load(appFile);

                    XmlNode appSettings = configDoc.SelectSingleNode("configuration/appSettings");

                    #region 修改节点
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
                            case "ServiceName":
                                {
                                    xele.SetAttribute("value", this.txtServiceName.Text.Trim());
                                }
                                break;
                            case "ServiceDisplayName":
                                xele.SetAttribute("value", this.txtServiceDisplay.Text.Trim());
                                break;
                            case "ServiceDescription":
                                xele.SetAttribute("value", this.txtServiceDispscription.Text.Trim());
                                break;
                        }
                    }
                    #endregion

                    #region 保存文件
                    configDoc.Save(appFile);
                    #endregion

                    #region 修改端口
                    AppConfigHelper.SetConfigPath(appFile);
                    List<AppInfo> list = AppConfigHelper.GetEndPoint();
                    foreach (var info in list)
                    {
                        string name = info.Key;
                        string address = info.Value;
                        string port = @":[0-9]{1,10}?/";
                        var regex = new Regex(port, RegexOptions.Singleline);
                        Match match = regex.Match(address);
                        port = match.Value.Replace(":", "").Replace("/", "");
                        AppConfigHelper.SetEndpointAddress(name, address.Replace(oldPort, this.txtSericePort.Text.Trim()));
                    }
                    #endregion

                    #region 修改批处理文件
                    string filePath = System.Environment.CurrentDirectory + @"\UnInstall.bat";
                    FileHelper.ReplaceFileContent(filePath, "iCMS.ServerService", this.txtServiceName.Text.Trim());

                    filePath = System.Environment.CurrentDirectory + @"\Install.bat";
                    FileHelper.ReplaceFileContent(filePath, "iCMS.ServerService", this.txtServiceName.Text.Trim());
                    #endregion

                    #region 执行cmd

                    Process proc = null;
                    try
                    {
                        string targetDir = System.Environment.CurrentDirectory;//this is where mybatch.bat lies
                        proc = new Process();
                        proc.StartInfo.WorkingDirectory = targetDir;
                        proc.StartInfo.FileName = "Install.bat";
                        proc.StartInfo.Arguments = string.Format("10");//this is argument
                        //proc.StartInfo.CreateNoWindow = false;

                        //隐藏窗口
                        proc.StartInfo.UseShellExecute = false;
                        proc.StartInfo.CreateNoWindow = true;
                        proc.Start();
                        proc.WaitForExit();
                        Application.Exit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception Occurred :{0},{1}", ex.Message, ex.StackTrace.ToString());
                    }
                    #endregion
                }
                else
                {
                    MessageBox.Show(this, "配置文件未找到！");
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);

                MessageBox.Show(this, "配置出错");
            }
            finally
            {
                this.BeginInvoke(new MethodInvoker(delegate ()
                {
                    btnSaveDB.Enabled = true;
                }));

                MessageBox.Show(this, "配置完成。");
            }
        }
        #endregion

        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ServiceConfigForm_Load(object sender, EventArgs e)
        {
            SetWindowPos(this.Handle, -1, 0, 0, 0, 0, 1 | 2); //最后参数也有用1 | 4

            #region 加载服务数据

            var rootPath = System.Environment.CurrentDirectory;
            var appFile = rootPath + "/iCMS.Server.WindowsService.exe.config";

            bool isSetConfigServer = false;
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
                        XmlElement value = (XmlElement)node;
                        var key = value.GetAttribute("key");

                        switch (key)
                        {
                            case "ServiceName":
                                this.txtServiceName.Text = value.GetAttribute("value");
                                break;
                            case "ServiceDisplayName":
                                this.txtServiceDisplay.Text = value.GetAttribute("value");
                                break;
                            case "ServiceDescription":
                                this.txtServiceDispscription.Text = value.GetAttribute("value");
                                break;
                            case "IsSetConifgServer":
                                {
                                    if (value.GetAttribute("value") == "1")
                                    {
                                        isSetConfigServer = true;
                                    }
                                }
                                break;
                        }
                    }

                    #region 修改端口
                    AppConfigHelper.SetConfigPath(appFile);
                    List<AppInfo> list = AppConfigHelper.GetEndPoint();
                    foreach (var info in list)
                    {
                        string name = info.Key;
                        string address = info.Value;

                        string port = @":[0-9]{1,10}?/";
                        var regex = new Regex(port, RegexOptions.Singleline);
                        Match match = regex.Match(address);
                        port = match.Value.Replace(":", "").Replace("/", "");
                        this.txtSericePort.Text = port;
                        oldPort = port;
                    }
                    #endregion

                    #region 修改界面
                    if (isSetConfigServer)
                    {
                        gbServer.Visible = true;
                        gbServerSetup.Visible = true;
                    }
                    else
                    {
                        var gbdbTop = gbDb.Top;
                        var gbdbHeight = gbDb.Height;
                        gbServerSetup.Top = gbdbTop + gbdbHeight + 5;
                        var size = this.Size;
                        this.Size = new Size(size.Width, 350);
                    }
                    #endregion
                }
                else
                {
                    MessageBox.Show(" 配置文件未找到！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            #endregion
        }

        private bool InputValidate()
        {
            bool isAvialiab = true;
            var host = this.txtHost.Text.Trim();
            var userID = this.txtUserID.Text.Trim();
            var userPwd = this.txtUserPwd.Text.Trim();
            var dbName = this.txtDbName.Text.Trim();
            var secret = this.tbSecret.Text.Trim();
            var key = this.tbKey.Text.Trim();
            if (string.IsNullOrEmpty(host))
            {
                this.lblErrorHost.Show();
                isAvialiab = false;
            }
            if (string.IsNullOrEmpty(userID))
            {
                this.lblErrorUserID.Show();
                isAvialiab = false;
            }
            if (string.IsNullOrEmpty(userPwd))
            {
                this.lblErrorPwd.Show();
                isAvialiab = false;
            }
            if (string.IsNullOrEmpty(dbName))
            {
                this.lblErrorDbName.Show();
                isAvialiab = false;
            }

            if (string.IsNullOrEmpty(secret))
            {
                this.lblsecret.Show();
                isAvialiab = false;
            }
            if (string.IsNullOrEmpty(key))
            {
                this.lblkey.Show();
                isAvialiab = false;
            }

            return isAvialiab;
        }

        private void txtHost_KeyUp(object sender, KeyEventArgs e)
        {
            ResetConnectState();
            if (!string.IsNullOrEmpty(this.txtHost.Text.Trim()))
            {
                this.lblErrorHost.Visible = false;
            }
        }

        private void txtUserID_KeyUp(object sender, KeyEventArgs e)
        {
            ResetConnectState();
            if (!string.IsNullOrEmpty(this.txtUserID.Text.Trim()))
            {
                this.lblErrorUserID.Visible = false;
            }
        }

        private void txtUserPwd_KeyUp(object sender, KeyEventArgs e)
        {
            ResetConnectState();
            if (!string.IsNullOrEmpty(this.txtUserPwd.Text.Trim()))
            {
                this.lblErrorPwd.Visible = false;
            }
        }

        private void txtDbName_KeyUp(object sender, KeyEventArgs e)
        {
            ResetConnectState();
            if (!string.IsNullOrEmpty(this.txtDbName.Text.Trim()))
            {
                this.lblErrorDbName.Visible = false;
            }
        }


        private void tbKey_KeyUp(object sender, KeyEventArgs e)
        {
            ResetConnectState();
            if (!string.IsNullOrEmpty(this.tbKey.Text.Trim()))
            {
                this.lblkey.Visible = false;
            }
        }

        private void tbSecret_KeyUp(object sender, KeyEventArgs e)
        {
            ResetConnectState();
            if (!string.IsNullOrEmpty(this.tbSecret.Text.Trim()))
            {
                this.lblsecret.Visible = false;
            }
        }


        private void ResetConnectState()
        {
            IsConnOK = false;
            this.txtTestResult.Text = string.Empty;
        }

        private void SetButtonState(bool state)
        {
            this.btnTestConn.Enabled = state;
            this.btnSaveDB.Enabled = state;
        }

        private void ServiceConfigForm_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void lblsecret_Click(object sender, EventArgs e)
        {

        }

        private void lblkey_Click(object sender, EventArgs e)
        {

        }

        private void tbSecret_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbKey_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void lblErrorUserID_Click(object sender, EventArgs e)
        {

        }

        private void lblErrorPwd_Click(object sender, EventArgs e)
        {

        }

        private void lblErrorDbName_Click(object sender, EventArgs e)
        {

        }

        private void lblErrorHost_Click(object sender, EventArgs e)
        {

        }

        private void txtUserID_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDbName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUserPwd_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtHost_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void txtTestResult_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        #region 保存服务
        /// <summary>
        /// 保存服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveService_Click(object sender, EventArgs e)
        {
            if (IsConnOK)
            {
                Button btn = ((Button)sender);
                btn.Enabled = false;

                bool isReturn = false;

                var serviceName = this.txtServiceName.Text.Trim();
                var serviceDisplay = this.txtServiceDisplay.Text.Trim();
                var serviceDispscription = this.txtServiceDispscription.Text.Trim();
                var sericePort = this.txtSericePort.Text.Trim();
                if (string.IsNullOrEmpty(serviceName))
                {
                    isReturn = true;
                    this.lblServiceNameTip.Visible = true;
                }
                if (string.IsNullOrEmpty(serviceDisplay))
                {
                    isReturn = true;
                    this.lblServiceDisplayNameTip.Visible = true;
                }
                if (string.IsNullOrEmpty(serviceDispscription))
                {
                    isReturn = true;
                    this.lblServiceDiscriptionTip.Visible = true;
                }
                if (string.IsNullOrEmpty(sericePort))
                {
                    isReturn = true;
                    this.lblServicePortTip.Visible = true;
                }

                #region 验证服务是否存在
                var serviceControllers = ServiceController.GetServices();
                var server = serviceControllers.FirstOrDefault(service => service.ServiceName == serviceName);
                if (server != null)
                {
                    MessageBox.Show("服务已经存在");
                    isReturn = true;
                }
                #endregion

                #region 验证端口是否占用
                bool isUse = SocketHelper.IsUsePort(Convert.ToInt32(sericePort));
                if (isUse)
                {
                    MessageBox.Show("端口已经占用");
                    isReturn = true;
                }
                #endregion

                if (isReturn)
                {
                    btn.Enabled = true;
                    return;
                }

                Thread thread = new Thread(new ThreadStart(SetServiceName));
                thread.IsBackground = true;
                thread.Start();
            }
            else
            {
                MessageBox.Show("请先测试连接是否成功！");
            }
        }
        #endregion
    }
}