/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Setup.DBUpgrade.DBUpgradeVersion

 *文件名：  UpgradeVersion
 *创建人：  王颖辉
 *创建时间：2017-01-19
 *描述：数据库版本升级
/************************************************************************************/

using iCMS.Common.Component.Tool;
using iCMS.Frameworks.Core.DB.Models;
using iCMS.Frameworks.Core.Repository;
using iCMS.Setup.DBUpgrade.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Linq;

namespace iCMS.Setup.DBUpgrade.DBUpgradeVersion
{
    #region 数据库版本升级
    /// <summary>
    /// 数据库版本升级
    /// </summary>
    public partial class UpgradeVersion : Form
    {
        #region 初始化
        public UpgradeVersion()
        {
            InitializeComponent();

            #region 隐藏验证
            lblDBServerName.Visible = false;
            lblDBName.Visible = false;
            lblUserName.Visible = false;
            lblPassWord.Visible = false;
            #endregion
        }
        #endregion

        #region 确定事件
        /// <summary>
        /// 确定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            #region 验证数据
            Button btn = ((Button)sender);
            btn.Enabled = false;

            bool isReturn = false;

         
            var dbServerName = this.txtDBServerName.Text.Trim();
            var dbName = this.txtDBName.Text.Trim();
            var userName = this.txtUserName.Text.Trim();
            var passWord = this.txtPassWord.Text.Trim();
            if (string.IsNullOrEmpty(dbServerName))
            {
                isReturn = true;
                this.lblDBServerName.Visible = true;
            }
            if (string.IsNullOrEmpty(dbName))
            {
                isReturn = true;
                this.lblDBName.Visible = true;
            }
            if (string.IsNullOrEmpty(userName))
            {
                isReturn = true;
                this.lblUserName.Visible = true;
            }
            if (string.IsNullOrEmpty(passWord))
            {
                isReturn = true;
                this.lblPassWord.Visible = true;
            }
         

            if (isReturn)
            {
                btn.Enabled = true;
                return;
            }
            #endregion

            #region 多线程执行

            #endregion

            Thread thread = new Thread(new ThreadStart(UpgradeDB));
            thread.IsBackground = true;
            thread.Start();
        }
        #endregion

        #region 验证事件
        private void txtDBServerName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

            ValidaTxt(sender, e, lblDBServerName);
        }

        private void txtDBName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ValidaTxt(sender, e, lblDBName);
        }

        private void txtUserName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ValidaTxt(sender, e, lblUserName);
        }

        private void txtPassWord_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ValidaTxt(sender, e, lblPassWord);
        }

        #region 统一验证
        /// <summary>
        /// 统一验证
        /// </summary>
        /// <param name="sender">验证控件</param>
        /// <param name="e">事件</param>
        /// <param name="lbl">提示文本</param>
        private void ValidaTxt(object sender, System.ComponentModel.CancelEventArgs e, Label lbl)
        {
            var txt = sender as TextBox;
            if (txt == null) return;
           // e.Cancel = (txt.Text == string.Empty);
            if (string.IsNullOrEmpty(txt.Text))
            {
                lbl.Visible = true;
            }
            else
            {
                lbl.Visible = false;
            }
        }
        #endregion

        #endregion

        #region 升级数据库
        private void UpgradeDB()
        {
            #region 升级
            #region 变量
            List<ReplaceInfo> replaceList = new List<ReplaceInfo>();
            string filePath = string.Empty;
            string dbConnection = string.Empty;
            string dbFile = "DBFile/";
            bool isExist = false;//文件是否存在
            #endregion

            try
            {
                #region 执行过程

                #region 验证数据库连接是否成功
                //验证数据库连接是否成功
                bool isConnection = CheckDBConnection(out dbConnection);
                if (!isConnection)
                {
                    MessageBox.Show("数据库无法连接，请检查数据库配置");
                    return;
                }
                #endregion

                #region 第一步：添加表
                SetRadioButton(first);
                //脚本地址
                filePath = AppDomain.CurrentDomain.BaseDirectory + dbFile + "AddTable.sql";
                isExist = CheckFileIsExist(filePath, "表脚本不存在");
                if (!isExist)
                {
                    return;
                }
                #region 替换信息
                replaceList = new List<ReplaceInfo>();
                ReplaceInfo replace = new ReplaceInfo();
                replace.OldValue = "iCMSDB";
                replace.NewValue = txtDBName.Text.Trim();
                replaceList.Add(replace);
                #endregion
                //执行脚本
                SqlFile(dbConnection, filePath, replaceList);
                #endregion

                #region 第二步：添加字段
                SetRadioButton(second);
                //脚本地址
                filePath = AppDomain.CurrentDomain.BaseDirectory + dbFile + "AddField.sql";
                isExist = CheckFileIsExist(filePath, "字段脚本不存在");
                if (!isExist)
                {
                    return;
                }
                //执行脚本
                SqlFile(dbConnection, filePath, replaceList);
                #endregion

                #region 第三步：添加函数
                SetRadioButton(third);
                //脚本地址
                filePath = AppDomain.CurrentDomain.BaseDirectory + dbFile + "AddFunction.sql";
                isExist = CheckFileIsExist(filePath, "函数脚本不存在");
                if (!isExist)
                {
                    return;
                }
                //执行脚本
                SqlFile(dbConnection, filePath, replaceList);
                #endregion

                #region 第四步：升级数据
                SetRadioButton(fourth);
                //脚本地址
                filePath = AppDomain.CurrentDomain.BaseDirectory + dbFile + "Data.sql";
                isExist = CheckFileIsExist(filePath, "数据脚本不存在");
                if (!isExist)
                {
                    return;
                }
                //执行脚本
                SqlFile(dbConnection, filePath, replaceList);
                #endregion

                #region 第五步：添加模块
                SetRadioButton(fifth);
                //脚本地址
                filePath = AppDomain.CurrentDomain.BaseDirectory + dbFile + "AddModule.sql";
                isExist = CheckFileIsExist(filePath, "模块脚本不存在");
                if (!isExist)
                {
                    return;
                }
                //执行脚本
                SqlFile(dbConnection, filePath, replaceList);
                #endregion

                #region 第六步：添加角色
                SetRadioButton(sixth);
                //脚本地址
                filePath = AppDomain.CurrentDomain.BaseDirectory + dbFile + "AddRole.sql";
                isExist = CheckFileIsExist(filePath, "角色脚本不存在");
                if (!isExist)
                {
                    return;
                }
                //执行脚本
                SqlFile(dbConnection, filePath, replaceList);
                #endregion

                #region 第七步：添加视图
                SetRadioButton(seventh);
                //脚本地址
                filePath = AppDomain.CurrentDomain.BaseDirectory + dbFile + "AddView.sql";
                isExist = CheckFileIsExist(filePath, "视图脚本不存在");
                if (!isExist)
                {
                    return;
                }
                //执行脚本
                SqlFile(dbConnection, filePath, replaceList);
                #endregion

                #region 第八步：添加存储过程
                SetRadioButton(eighth);
                //脚本地址
                filePath = AppDomain.CurrentDomain.BaseDirectory + dbFile + "AddProcedure.sql";
                isExist = CheckFileIsExist(filePath, "存储过程脚本不存在");
                if (!isExist)
                {
                    return;
                }
                //执行脚本
                SqlFile(dbConnection, filePath, replaceList);
                #endregion

                #region 第九步：添加触发器
                SetRadioButton(ninth);
                //脚本地址
                filePath = AppDomain.CurrentDomain.BaseDirectory + dbFile + "AddTrigger.sql";
                isExist = CheckFileIsExist(filePath, "触发器脚本不存在");
                if (!isExist)
                {
                    return;
                }
                //执行脚本
                SqlFile(dbConnection, filePath, replaceList);
                #endregion

                #region 第十步：添加角色和模板关联
                SetRadioButton(ten);
                //脚本地址
                filePath = AppDomain.CurrentDomain.BaseDirectory + dbFile + "AddUserRole.sql";
                isExist = CheckFileIsExist(filePath, "添加角色和模板关联脚本不存在");
                if (!isExist)
                {
                    return;
                }
                //执行脚本
                SqlFile(dbConnection, filePath, replaceList);
                #endregion

                #region 第十一步：添加用户和WS关联
                SetRadioButton(eleven);
                //脚本地址
                filePath = AppDomain.CurrentDomain.BaseDirectory + dbFile + "AddUserDeviceWS.sql";
                isExist = CheckFileIsExist(filePath, "添加用户和WS关联脚本不存在");
                if (!isExist)
                {
                    return;
                }
                //执行脚本
                SqlFile(dbConnection, filePath, replaceList);
                #endregion

                #region 第十二步：添加形貌图报警阈值数据
                SetRadioButton(twelve);
                //脚本地址
                filePath = AppDomain.CurrentDomain.BaseDirectory + dbFile + "AddAlarmThreshoud.sql";
                isExist = CheckFileIsExist(filePath, "添加形貌图报警阈值数据脚本不存在");
                if (!isExist)
                {
                    return;
                }
                //执行脚本
                SqlFile(dbConnection, filePath, replaceList);
                #endregion

                #region 第十三步：添加集团和机组
                SetRadioButton(thirteen);
                //脚本地址
                filePath = AppDomain.CurrentDomain.BaseDirectory + dbFile + "AddMonitorTree.sql";
                isExist = CheckFileIsExist(filePath, "添加集团和机组脚本不存在");
                if (!isExist)
                {
                    return;
                }
                //执行脚本
                SqlFile(dbConnection, filePath, replaceList);
                #endregion

                #region 第十四步：添加系统参数数据
                SetRadioButton(fourteen);
                //脚本地址
                filePath = AppDomain.CurrentDomain.BaseDirectory + dbFile + "AddConfig.sql";
                isExist = CheckFileIsExist(filePath, "添加系统参数数据脚本不存在");
                if (!isExist)
                {
                    return;
                }
                //执行脚本
                SqlFile(dbConnection, filePath, replaceList);
                #endregion

                #endregion

                this.BeginInvoke(new MethodInvoker(delegate ()
                {
                    btnSubmit.Enabled = true;
                }));
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
            }
            finally
            {
                this.BeginInvoke(new MethodInvoker(delegate ()
                {
                    btnSubmit.Enabled = true;
                }));
                MessageBox.Show("数据库升级执行完成");
            }
            #endregion
        }
        #endregion

        #region 私有方法

        #region 检测数据库是否可以连接
        /// <summary>
        /// 检测数据库是否可以连接
        /// </summary>
        /// <returns></returns>
        private bool CheckDBConnection(out string dbConnection)
        {
            bool isConnection = false;
            string dbServerName = txtDBServerName.Text.Trim();
            string userName = txtUserName.Text.Trim();
            string passWord = txtPassWord.Text.Trim();
            dbConnection = "data source={0};user id={1};password={2};persist security info=false;packet size=4096;";
            dbConnection = string.Format(dbConnection, dbServerName, userName, passWord);
            isConnection = ConnectionTest(dbConnection);
            return isConnection;
        }
        #endregion

        #region 测试连接数据库是否成功
        /// <summary>
        /// 测试连接数据库是否成功
        /// </summary>
        /// <returns></returns>
        public static bool ConnectionTest(string connectionString)
        {
            connectionString = connectionString + " Connection Timeout=3";
            bool IsCanConnectioned = false;
            //创建连接对象
            SqlConnection mySqlConnection = new SqlConnection(connectionString);
            //ConnectionTimeout 在.net 1.x 可以设置 在.net 2.0后是只读属性，则需要在连接字符串设置
            //如：server=.;uid=sa;pwd=;database=PMIS;Integrated Security=SSPI; Connection Timeout=30
            //mySqlConnection.
            try
            {
                //Open DataBase
                //打开数据库
                mySqlConnection.Open();
                IsCanConnectioned = true;
            }
            catch
            {
                //Can not Open DataBase
                //打开不成功 则连接不成功
                IsCanConnectioned = false;
            }
            finally
            {
                //Close DataBase
                //关闭数据库连接
                mySqlConnection.Close();
            }
            //mySqlConnection   is   a   SqlConnection   object 
            if (mySqlConnection.State == ConnectionState.Closed || mySqlConnection.State == ConnectionState.Broken)
            {
                //Connection   is   not   available  
                return IsCanConnectioned;
            }
            else
            {
                //Connection   is   available  
                return IsCanConnectioned;
            }
        }
        #endregion

        #region 数据库操作
        #region 导入sql脚本
        /// <summary>
        /// 导入sql脚本
        /// </summary>
        /// <param name="sqlConnString">连接数据库字符串</param>
        /// <param name="varFileName">脚本路径</param>
        /// <returns></returns>
        private static bool SqlFile(string sqlConnString, string varFileName, List<ReplaceInfo> replaceList)
        {


            var statements = new List<string>();
            using (var stream = File.OpenRead(varFileName))
            using (var reader = new StreamReader(stream))
            {
                string statement;
                while ((statement = FromStream(reader)) != null)
                {
                    statements.Add(statement);
                }
            }
            try
            {
                Command(sqlConnString, statements.ToArray(), replaceList);
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return false;
                // throw ex;
            }
        }
        #endregion

        #region 命令
        /// <summary>
        /// 命令
        /// </summary>
        /// <param name="sqlConnString"></param>
        /// <param name="varSqlList"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        private static bool Command(string sqlConnString, string[] varSqlList, List<ReplaceInfo> replaceList)
        {
            using (SqlConnection conn = new SqlConnection(sqlConnString))
            {
                conn.Open();
                //Don't use Transaction, because some commands cannot execute in one Transaction.
                //SqlTransaction varTrans = conn.BeginTransaction();
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                bool bl = true;
                var varcommandText = string.Empty;
                for (int i = 0; i < varSqlList.Length; i++)
                {
                    //command.Transaction = varTrans;
                    try
                    {
                        varcommandText = varSqlList[i];

                        foreach (var info in replaceList)
                        {
                            varcommandText = varcommandText.Replace(info.OldValue, info.NewValue);
                        }

                        command.CommandText = varcommandText;
                        command.ExecuteNonQuery();
                        //varTrans.Commit();
                    }
                    catch (Exception ex)
                    {
                        bl = false;
                        LogHelper.WriteLog(ex);
                        LogHelper.WriteLog("-----出错执行语句-------");
                        LogHelper.WriteLog(varcommandText);
                        LogHelper.WriteLog("-----出错执行语句-------");
                    }
                    finally
                    {

                    }
                }
                conn.Close();
                return bl;
            }
        }
        #endregion

        #region 导入流文件
        /// <summary>
        /// 导入流文件
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected static string FromStream(StreamReader reader)
        {
            var sb = new StringBuilder();

            while (true)
            {
                var lineOfText = reader.ReadLine();
                if (lineOfText == null)
                {
                    if (sb.Length > 0)
                        return sb.ToString();

                    return null;
                }

                if (lineOfText.TrimEnd().ToUpper() == "GO")
                    break;

                sb.Append(lineOfText + Environment.NewLine);
            }

            return sb.ToString();
        }
        #endregion
        #endregion

        #region 按钮代理
        private delegate void SetRadioButtonDelegate(RadioButton rb);
        private void SetRadioButton(RadioButton rb)
        {
            if (rb.InvokeRequired)
            {
                Invoke(new SetRadioButtonDelegate(SetRadioButton), new object[] { rb });

            }
            else
            {
                rb.Select();
            }
        }


        #endregion

        #region 验证文件是否存在
        /// <summary>
        /// 验证文件是否存在
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="message">提示信息</param>
        /// <returns></returns>
        private bool CheckFileIsExist(string filePath,string message)
        {
            bool isExist = true;
            if (!File.Exists(filePath))
            {
                MessageBox.Show(message);
                this.BeginInvoke(new MethodInvoker(delegate ()
                {
                    btnSubmit.Enabled = true;
                }));
                isExist = false;
            }
            return isExist;
        }
        #endregion


        #endregion

    }
    #endregion
}
