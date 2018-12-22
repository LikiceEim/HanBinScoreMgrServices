using iCMS.Common.Component.Tool;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Configuration;

namespace iCMS.Setup.CloudPushManually
{
    public partial class DBConfigFrm : Form
    {
        private bool IsConnOK = false;
        private bool IsSaveOK = false;
        private string ConnectionString = string.Empty;

        public DBConfigFrm()
        {
            InitializeComponent();
        }

        private void btnTestConn_Click(object sender, EventArgs e)
        {
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
                        ConnectionString = EcanSecurity.Encode(tempConn);

                        this.txtTestResult.Text = "连接成功！";
                        SetButtonState(true);
                    }
                    else
                    {
                        this.txtTestResult.Text = "连接失败！";
                        SetButtonState(true);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                SetButtonState(true);
            }
        }

        private bool InputValidate()
        {
            bool isAvialiab = true;
            var host = this.txtHost.Text.Trim();
            var userID = this.txtUserID.Text.Trim();
            var userPwd = this.txtUserPwd.Text.Trim();
            var dbName = this.txtDbName.Text.Trim();
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

            return isAvialiab;
        }

        private void SetButtonState(bool state)
        {
            this.btnTestConn.Enabled = state;
            this.btnSave.Enabled = state;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsConnOK && !string.IsNullOrEmpty(ConnectionString))
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["iCMS"].Value = ConnectionString;
                config.Save();

                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("数据库已完成配置，是否立即进行数据推送?", "退出系统", messButton);
                if (dr == DialogResult.OK)//如果点击“确定”按钮
                {
                    this.DialogResult = DialogResult.OK;
                    this.Dispose();
                }
                else//如果点击“取消”按钮
                {
                    System.Environment.Exit(0);
                }
                IsSaveOK = true;
  

                return;

                //var rootPath = System.Environment.CurrentDirectory;
                //var appFile = rootPath + "/iCMS.Setup.CloudPushManually.exe.config";
                //try
                //{
                //    //如果找到App.config
                //    if (File.Exists(appFile))
                //    {
                //        XmlDocument configDoc = new XmlDocument();
                //        configDoc.Load(appFile);

                //        XmlNode appSettings = configDoc.SelectSingleNode("configuration/appSettings");

                //        //遍历所有子节点，找到ServiceIPAddress节点并返回
                //        foreach (XmlNode node in appSettings.ChildNodes)
                //        {
                //            //如果不是有效的配置节点（比如是注释）
                //            if (node.NodeType != XmlNodeType.Element)
                //            {
                //                continue;
                //            }

                //            XmlElement xele = (XmlElement)node;
                //            var keyValue = xele.GetAttribute("key");

                //            if (keyValue.Equals("iCMS"))
                //            {
                //                xele.SetAttribute("value", ConnectionString);
                //                break;
                //            }
                //        }

                //        configDoc.Save(appFile);

                //        MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                //        DialogResult dr = MessageBox.Show("数据库已完成配置，是否立即进行数据推送?", "退出系统", messButton);
                //        if (dr == DialogResult.OK)//如果点击“确定”按钮
                //        {
                //            this.DialogResult = DialogResult.OK;
                //            this.Dispose();
                //        }
                //        else//如果点击“取消”按钮
                //        {
                //            System.Environment.Exit(0);
                //        }
                //        IsSaveOK = true;
                //    }
                //    else
                //    {
                //        MessageBox.Show("Web.config 配置文件未找到！");
                //        this.DialogResult = DialogResult.Cancel;
                //    }
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message);
                //    this.DialogResult = DialogResult.Cancel;
                //}
            }
            else
            {
                MessageBox.Show("请先进行数据库配置并测试连接是否成功！");
            }
        }
    }
}
