/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Setup.BDSetup

 *文件名：  DBSetup
 *创建人：  王颖辉
 *创建时间：2017-01-13
 *描述：初始化数据库
/************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using iCMS.Common.Component.Tool;
namespace iCMS.Setup.BDSetup
{
    #region 初始化数据库
    /// <summary>
    /// 初始化数据库
    /// </summary>
    public partial class DBSetup : Form
    {
        public DBSetup()
        {
            InitializeComponent();
            ServerNametb.Focus();

            #region 隐藏提示信息
            servertiplbl.Visible = false;
            dbtiplbl.Visible = false;
            dbaddresstiplbl.Visible = false;
            nametiplbl.Visible = false;
            passwordtiplbl.Visible = false;
            parttiplbl.Visible = false;
            keytiplbl.Visible = false;
            secrettiplbl.Visible = false;
            #endregion
        }

        #region 安装按钮
        private void Setupbtn_Click(object sender, EventArgs e)
        {
            Button btn = ((Button)sender);
            btn.Enabled = false;

            bool isReturn = false;

            var serverName = this.ServerNametb.Text.Trim();
            var dbName = this.DBNametb.Text.Trim();
            var dbPath = this.dbPathtb.Text.Trim();
            var loginName = this.LoginNametb.Text.Trim();
            var passWord = this.PassWordtb.Text.Trim();
            var partition = this.Partitiontb.Text.Trim();
            var key = this.keyTb.Text.Trim();
            var secret = this.secretTb.Text.Trim();

            if (string.IsNullOrEmpty(serverName))
            {
                isReturn = true;
                this.servertiplbl.Visible = true;
            }
            if (string.IsNullOrEmpty(dbName))
            {
                isReturn = true;
                this.dbtiplbl.Visible = true;
            }
            if (string.IsNullOrEmpty(dbPath))
            {
                isReturn = true;
                this.dbaddresstiplbl.Visible = true;
            }
            if (string.IsNullOrEmpty(loginName))
            {
                isReturn = true;
                this.nametiplbl.Visible = true;
            }

            if (string.IsNullOrEmpty(passWord))
            {
                isReturn = true;
                this.passwordtiplbl.Visible = true;
            }

            if (string.IsNullOrEmpty(partition))
            {
                isReturn = true;
                this.parttiplbl.Visible = true;
            }

            if (cbUse.Checked)
            {
                if (string.IsNullOrEmpty(key))
                {
                    isReturn = true;
                    this.keytiplbl.Visible = true;
                }

                if (string.IsNullOrEmpty(secret))
                {
                    isReturn = true;
                    this.secrettiplbl.Visible = true;
                }
            }
          
            if (isReturn)
            {
                btn.Enabled = true;
                return;
            }



            Thread thread = new Thread(new ThreadStart(CreateDB));
            thread.IsBackground = true;
            thread.Start();
        }

        #region 创建数据库
        /// <summary>
        /// 创建数据库
        /// </summary>
        private void CreateDB()
        {
            try
            {
                #region 数据验证
                string serverName = ServerNametb.Text.Trim();
                string userId = LoginNametb.Text.Trim();
                string password = PassWordtb.Text.Trim();
                string dbPath = dbPathtb.Text.Trim();
                string dbName = DBNametb.Text.Trim();
                string partPath = Partitiontb.Text.Trim();
                if (string.IsNullOrEmpty(serverName.Trim()))
                {
                    MessageBox.Show("服务器名称不能为空");
                    this.BeginInvoke(new MethodInvoker(delegate ()
                    {
                        Setupbtn.Enabled = true;
                    }));
                    return;
                }

                if (string.IsNullOrEmpty(userId.Trim()))
                {
                    MessageBox.Show("用户名不能为空");
                    this.BeginInvoke(new MethodInvoker(delegate ()
                    {
                        Setupbtn.Enabled = true;
                    }));
                    return;
                }

                if (string.IsNullOrEmpty(password.Trim()))
                {
                    MessageBox.Show("密码不能为空");
                    this.BeginInvoke(new MethodInvoker(delegate ()
                    {
                        Setupbtn.Enabled = true;
                    }));
                    return;
                }

                if (!CheckPath(dbPath))
                {
                    MessageBox.Show("数据库存放路径不正确");
                    this.BeginInvoke(new MethodInvoker(delegate ()
                    {
                        Setupbtn.Enabled = true;
                    }));
                    return;
                }

                if (!CheckPath(partPath))
                {
                    MessageBox.Show("分区存放路径不正确");
                    this.BeginInvoke(new MethodInvoker(delegate ()
                    {
                        Setupbtn.Enabled = true;
                    }));
                    return;
                }

                //验证数据库文件夹是否有文件
                if (Directory.Exists(partPath))
                {
                    //判断文件夹是否有文件
                    DirectoryInfo TheFolder = new DirectoryInfo(partPath);
                    //遍历文件夹
                    int fileCount = TheFolder.GetFiles().Length;
                    if (fileCount > 0)
                    {
                        MessageBox.Show("分区文件夹已经存在文件，请先删除");
                        this.BeginInvoke(new MethodInvoker(delegate ()
                        {
                            Setupbtn.Enabled = true;
                        }));
                        return;
                    }
                }


                if (Directory.Exists(dbPath))
                {
                    //验证分区文件夹是否有文件
                    var TheFolder = new DirectoryInfo(dbPath);
                    //遍历文件夹
                    var fileCount = TheFolder.GetFiles().Length;
                    if (fileCount > 0)
                    {
                        MessageBox.Show("数据库文件夹已经存在文件，请先删除");
                        this.BeginInvoke(new MethodInvoker(delegate ()
                        {
                            Setupbtn.Enabled = true;
                        }));
                        return;
                    }
                }


                #endregion

                #region 创建数据库

                List<ReplaceInfo> replaceList = new List<ReplaceInfo>();
                string dbFile = "DBFile/";


                //创建数据库
                string filePath = AppDomain.CurrentDomain.BaseDirectory + dbFile + "DBName.sql";
                string connStr = "data source={0};user id={1};password={2};persist security info=false;packet size=4096;";
                connStr = string.Format(connStr, serverName, userId, password);

                //数据库存放文件夹
                // Determine whether the directory exists.
                if (!Directory.Exists(dbPath))
                {
                    // Create the directory it does not exist.
                    Directory.CreateDirectory(dbPath);
                }

                if (!File.Exists(filePath))
                {
                    MessageBox.Show("创建数据库文件不存在");
                    this.BeginInvoke(new MethodInvoker(delegate ()
                    {
                        Setupbtn.Enabled = true;
                    }));
                    return;
                }

                bool isConnect = ConnectionTest(connStr);
                if (!isConnect)
                {
                    MessageBox.Show("数据库配置不正确，请检查数据库配置");
                    this.BeginInvoke(new MethodInvoker(delegate ()
                    {
                        Setupbtn.Enabled = true;
                    }));
                    return;
                }
                SetRadioButton(first);

                replaceList = new List<ReplaceInfo>();
                ReplaceInfo replace = new ReplaceInfo();
                replace.OldValue = "DBName";
                replace.NewValue = dbName;
                replaceList.Add(replace);
                replace = new ReplaceInfo();
                replace.OldValue = "DBPath";
                replace.NewValue = dbPath;
                replaceList.Add(replace);
                //创建数据库
                SqlFile(connStr, filePath, replaceList);

                //创建表结构
                filePath = AppDomain.CurrentDomain.BaseDirectory + dbFile + "DB.sql";

                if (!File.Exists(filePath))
                {
                    MessageBox.Show("创建表结构文件不存在");
                    this.BeginInvoke(new MethodInvoker(delegate ()
                    {
                        Setupbtn.Enabled = true;
                    }));
                    return;
                }
                SetRadioButton(second);

                replaceList = new List<ReplaceInfo>();
                replace = new ReplaceInfo();
                replace.OldValue = "iCMSDB";
                replace.NewValue = DBNametb.Text.Trim();
                replaceList.Add(replace);


                //创建表结构
                SqlFile(connStr, filePath, replaceList);


                //创建数据
                filePath = AppDomain.CurrentDomain.BaseDirectory + dbFile + "Data.sql";


                if (!File.Exists(filePath))
                {
                    MessageBox.Show("创建表数据文件不存在");
                    this.BeginInvoke(new MethodInvoker(delegate ()
                    {
                        Setupbtn.Enabled = true;
                    }));
                    return;
                }
                SetRadioButton(third);

                replaceList = new List<ReplaceInfo>();
                replace = new ReplaceInfo();
                replace.OldValue = "iCMSDB";
                replace.NewValue = DBNametb.Text.Trim();
                replaceList.Add(replace);

                //创建表数据
                SqlFile(connStr, filePath, replaceList);

                //创建分区文件存放文件夹
                // Determine whether the directory exists.
                if (!Directory.Exists(partPath))
                {
                    // Create the directory it does not exist.
                    Directory.CreateDirectory(partPath);
                }

                SetRadioButton(fourth);

                //创建分区
                filePath = AppDomain.CurrentDomain.BaseDirectory + dbFile + "partition.sql";
                if (!File.Exists(filePath))
                {
                    MessageBox.Show("创建分区文件不存在");
                    this.BeginInvoke(new MethodInvoker(delegate ()
                    {
                        Setupbtn.Enabled = true;
                    }));
                    return;
                }

                replaceList = new List<ReplaceInfo>();
                replace = new ReplaceInfo();
                replace.OldValue = "iCMSDB";
                replace.NewValue = DBNametb.Text.Trim();
                replaceList.Add(replace);

                //创建分区
                SqlFile(connStr, filePath, replaceList);

                //创建分区
                filePath = AppDomain.CurrentDomain.BaseDirectory + dbFile + "partition1.sql";
                if (!File.Exists(filePath))
                {
                    MessageBox.Show("创建分区文件不存在");
                    this.BeginInvoke(new MethodInvoker(delegate ()
                    {
                        Setupbtn.Enabled = true;
                    }));
                    return;
                }
                //创建分区
                SqlFile1(connStr, filePath, @"PartitionPath", partPath, "iCMSDB", DBNametb.Text.Trim());

                //创建表结构
                filePath = AppDomain.CurrentDomain.BaseDirectory + dbFile + "AddTrigger.sql";

                if (!File.Exists(filePath))
                {
                    MessageBox.Show("创建触发器文件不存在");
                    this.BeginInvoke(new MethodInvoker(delegate ()
                    {
                        Setupbtn.Enabled = true;
                    }));
                    return;
                }
                SetRadioButton(fifth);

                replaceList = new List<ReplaceInfo>();
                replace = new ReplaceInfo();
                replace.OldValue = "iCMSDB";
                replace.NewValue = DBNametb.Text.Trim();
                replaceList.Add(replace);

                //创建表结构
                SqlFile(connStr, filePath, replaceList);

                #region 启用云平台
                //启用云平台
                bool isUse = cbUse.Checked;
                if (isUse)
                {
                    //创建触发器
                    filePath = AppDomain.CurrentDomain.BaseDirectory + dbFile + "Trigger.sql";

                    if (!File.Exists(filePath))
                    {
                        MessageBox.Show("触发器文件不存在");
                        this.BeginInvoke(new MethodInvoker(delegate ()
                        {
                            Setupbtn.Enabled = true;
                        }));
                        return;
                    }
                    SetRadioButton(sixth);

                    replaceList = new List<ReplaceInfo>();
                    replace = new ReplaceInfo();
                    replace.OldValue = @"iCMSDB";
                    replace.NewValue = DBNametb.Text.Trim();
                    replaceList.Add(replace);

                    replace = new ReplaceInfo();
                    replace.OldValue = @"5bcbc178cf70e1ec7ca1586a1eaac1d3";
                    replace.NewValue = keyTb.Text.Trim();
                    replaceList.Add(replace);

                    replace = new ReplaceInfo();
                    replace.OldValue = @"252a7d7582a39c899de71efa8b6fb368";
                    replace.NewValue = secretTb.Text.Trim();
                    replaceList.Add(replace);


                    //创建触发器
                    SqlFile(connStr, filePath, replaceList);

                    //开启http请求
                    filePath = AppDomain.CurrentDomain.BaseDirectory + dbFile + "OpenHttpQuest.sql";

                    if (!File.Exists(filePath))
                    {
                        MessageBox.Show("http请求文件不存在");
                        this.BeginInvoke(new MethodInvoker(delegate ()
                        {
                            Setupbtn.Enabled = true;
                        }));
                        return;
                    }

                    replaceList = new List<ReplaceInfo>();
                    replace = new ReplaceInfo();
                    replace.OldValue = @"iCMSDB";
                    replace.NewValue = DBNametb.Text.Trim();
                    replaceList.Add(replace);


                    //创建http请求
                    SqlFile(connStr, filePath, replaceList);

                }

                #endregion


                #endregion

                this.BeginInvoke(new MethodInvoker(delegate ()
                {
                    Setupbtn.Enabled = true;
                }));
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);

                MessageBox.Show("数据安装出错，请核对数据库连接或查看日志");
            }
            finally
            {
                this.BeginInvoke(new MethodInvoker(delegate ()
                {
                    Setupbtn.Enabled = true;
                }));

                MessageBox.Show("数据库安装配置完成。");
            }
        }
        #endregion

        #endregion

        #region 命令创建数据库
        /// <summary>
        /// 命令创建数据库
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static void CmdSql(string filePath, string userId, string password)
        {
            try
            {
                System.Diagnostics.Process pr = new System.Diagnostics.Process();
                pr.StartInfo.FileName = "osql.exe";
                pr.StartInfo.Arguments = "-U " + userId + " -P " + password + " -d master -s . -i " + filePath + "";
                pr.StartInfo.UseShellExecute = false;
                pr.StartInfo.RedirectStandardOutput = true;  //重定向输出
                pr.StartInfo.UseShellExecute = false;
                pr.StartInfo.RedirectStandardInput = true;
                pr.StartInfo.RedirectStandardError = true;
                pr.StartInfo.CreateNoWindow = true;

                pr.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;//隐藏输出窗口
                pr.Start();
                Console.WriteLine("正在执行创建表结构");
                System.IO.StreamReader sr = pr.StandardOutput;
                Console.WriteLine(sr.ReadToEnd());
                Console.WriteLine("创建完成");
                pr.WaitForExit();
                pr.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }
        #endregion

        #region 执行脚本
        /// <summary>
        /// 执行脚本
        /// </summary>
        /// <param name="sqlFile">文件</param>
        /// <param name="connStr">连接字符串</param>
        /// <returns></returns>
        public static int ExecuteSqlScript(string sqlFile, string connStr)
        {
            int returnValue = -1;
            int sqlCount = 0, errorCount = 0;
            if (!File.Exists(sqlFile))
            {
                // Log.WriteLog(string.Format("sql file not exists!", sqlFile));
                return -1;
            }
            using (StreamReader sr = new StreamReader(sqlFile))
            {
                string line = string.Empty;
                char spaceChar = ' ';
                string newLIne = "\r\n", semicolon = ";";
                string sprit = "/", whiffletree = "-";
                string sql = string.Empty;
                do
                {
                    line = sr.ReadLine();
                    // 文件结束
                    if (line == null) break;
                    // 跳过注释行
                    if (line.StartsWith(sprit) || line.StartsWith(whiffletree)) continue;
                    // 去除右边空格
                    line = line.TrimEnd(spaceChar);
                    sql += line;
                    // 以分号(;)结尾，则执行SQL
                    if (sql.EndsWith(semicolon))
                    {
                        try
                        {
                            sqlCount++;
                            using (SqlConnection conn = new SqlConnection(connStr))
                            {
                                conn.Open();
                                //Don't use Transaction, because some commands cannot execute in one Transaction.
                                //SqlTransaction varTrans = conn.BeginTransaction();
                                SqlCommand command = new SqlCommand();
                                command.Connection = conn;
                                try
                                {
                                    command.CommandText = sql;
                                    command.ExecuteNonQuery();
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                                finally
                                {
                                    conn.Close();
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            errorCount++;
                            // Log.WriteLog(sql + newLIne + ex.Message);
                        }
                        sql = string.Empty;
                    }
                    else
                    {
                        // 添加换行符
                        if (sql.Length > 0) sql += newLIne;
                    }
                } while (true);
            }
            if (sqlCount > 0 && errorCount == 0)
                returnValue = 1;
            if (sqlCount == 0 && errorCount == 0)
                returnValue = 0;
            else if (sqlCount > errorCount && errorCount > 0)
                returnValue = -1;
            else if (sqlCount == errorCount)
                returnValue = -2;
            return returnValue;
        }
        #endregion

        #region 读取数据
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="reader">stream</param>
        /// <returns></returns>
        protected virtual string ReadNextStatementFromStream(StreamReader reader)
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

        #region 导入sql脚本
        /// <summary>
        /// 导入sql脚本
        /// </summary>
        /// <param name="sqlConnString">连接数据库字符串</param>
        /// <param name="varFileName">脚本路径</param>
        /// <returns></returns>
        private static bool ExecuteSqlFile(string sqlConnString, string varFileName)
        {
            if (!File.Exists(varFileName))
            {
                return false;
            }
            StreamReader rs = new StreamReader(varFileName, System.Text.Encoding.Default);
            ArrayList alSql = new ArrayList();
            string commandText = "";
            string varLine = "";
            while (rs.Peek() > -1)
            {
                varLine = rs.ReadLine();
                if (varLine == "")
                {
                    continue;
                }
                if (varLine != "GO" || varLine != "go")
                {
                    commandText += varLine;
                    commandText += "\r\n";
                }
                else
                {
                    commandText += "";
                }
            }
            alSql.Add(commandText);
            rs.Close();
            try
            {
                ExecuteCommand(sqlConnString, alSql);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace.ToString());
                return false;
                //MessageBox.Show(ex.StackTrace.ToString());
                //throw ex;
            }
        }
        #endregion

        #region 执行命令
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="sqlConnString">数据库连接字符串</param>
        /// <param name="varSqlList">sql列表</param>
        private static void ExecuteCommand(string sqlConnString, ArrayList varSqlList)
        {
            using (SqlConnection conn = new SqlConnection(sqlConnString))
            {
                conn.Open();
                //Don't use Transaction, because some commands cannot execute in one Transaction.
                //SqlTransaction varTrans = conn.BeginTransaction();
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                //command.Transaction = varTrans;
                try
                {
                    foreach (string varcommandText in varSqlList)
                    {
                        command.CommandText = varcommandText;
                        command.ExecuteNonQuery();
                    }
                    //varTrans.Commit();
                }
                catch (Exception ex)
                {
                    //varTrans.Rollback();
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
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

        #region 导入sql脚本 分区
        /// <summary>
        /// 导入sql脚本
        /// </summary>
        /// <param name="sqlConnString">连接数据库字符串</param>
        /// <param name="varFileName">脚本路径</param>
        /// <returns></returns>
        private static bool SqlFile1(string sqlConnString,
            string varFileName,
            string oldValue = null,
            string newValue = null,
            string oldValue2 = null,
            string newValue2 = null)
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
                using (SqlConnection conn = new SqlConnection(sqlConnString))
                {
                    conn.Open();
                    //Don't use Transaction, because some commands cannot execute in one Transaction.
                    //SqlTransaction varTrans = conn.BeginTransaction();
                    SqlCommand command = new SqlCommand();
                    command.Connection = conn;
                    command.CommandTimeout = 600;
                    bool bl = true;
                    for (int i = 0; i < statements.Count; i++)
                    {
                        //command.Transaction = varTrans;
                        try
                        {
                            var varcommandText = statements[i];
                            if (!string.IsNullOrEmpty(oldValue) && !string.IsNullOrEmpty(newValue))
                            {
                                varcommandText = varcommandText.Replace(oldValue, newValue);
                            }
                            if (!string.IsNullOrEmpty(oldValue2) && !string.IsNullOrEmpty(newValue2))
                            {
                                varcommandText = varcommandText.Replace(oldValue2, newValue2);
                            }
                            command.CommandText = varcommandText;
                            command.ExecuteNonQuery();
                            //varTrans.Commit();
                        }
                        catch (Exception ex)
                        {
                            bl = false;
                            LogHelper.WriteLog(ex);
                            //varTrans.Rollback();
                            // throw ex;
                        }
                        finally
                        {

                        }
                    }
                    conn.Close();
                    return bl;
                }
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

        #region 文件路径验证
        /// <summary> 
        /// 文件路径验证 
        /// </summary> 
        /// <remarks> 
        /// 创建人：王颖辉
        /// 创建日期：2016-09-07
        /// </remarks> 
        /// <param name="path">路径</param> 
        /// <returns></returns> 
        public static bool CheckPath(string path)
        {
            string pattern = @"^[a-zA-Z]:(((\\(?! )[^/:*?<>\""|\\]+)+\\?)|(\\)?)\s*$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(path);
        }
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

        #region 按下事件
        /// <summary>
        /// 数据库服务名称非空验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ServerNametb_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.ServerNametb.Text.Trim()))
            {
                this.servertiplbl.Visible = false;
            }
        }

        /// <summary>
        /// 数据库非空验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DBNametb_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.DBNametb.Text.Trim()))
            {
                this.dbtiplbl.Visible = false;
            }
        }

        /// <summary>
        /// 数据库路径非空验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dbPathtb_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.dbPathtb.Text.Trim()))
            {
                this.dbaddresstiplbl.Visible = false;
            }
        }

        /// <summary>
        /// 数据库用户名非空验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void LoginNametb_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.LoginNametb.Text.Trim()))
            {
                this.nametiplbl.Visible = false;
            }
        }

        /// <summary>
        /// 数据库密码非空验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void PassWordtb_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.PassWordtb.Text.Trim()))
            {
                this.passwordtiplbl.Visible = false;
            }
        }

        /// <summary>
        /// 分区非空验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Partitiontb_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.Partitiontb.Text.Trim()))
            {
                this.parttiplbl.Visible = false;
            }
        }

        /// <summary>
        /// 钥匙非空验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void keyTb_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.keyTb.Text.Trim()))
            {
                this.keytiplbl.Visible = false;
            }
        }

        /// <summary>
        /// 密钥非空验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void secretTb_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.secretTb.Text.Trim()))
            {
                this.secrettiplbl.Visible = false;
            }
        }
        #endregion

        #region 启用改变事件

        #endregion

        #region 启用事件
        /// <summary>
        /// 启用事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbUse_CheckedChanged(object sender, EventArgs e)
        {
            bool bl = ((CheckBox)sender).Checked;
            if (bl)
            {
                keyTb.Enabled = true;
                secretTb.Enabled = true;
            }
            else
            {
                keyTb.Enabled = false;
                secretTb.Enabled = false;
            }
            sixth.Visible = bl;
            lbTrigger.Visible = bl;
        }
        #endregion

        #region 创建触发器
        /// <summary>
        /// 创建触发器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateTrigger_Click(object sender, EventArgs e)
        {
            Button btn = ((Button)sender);
            btn.Enabled = false;

            bool isReturn = false;

            var serverName = this.ServerNametb.Text.Trim();
            var dbName = this.DBNametb.Text.Trim();
            var dbPath = this.dbPathtb.Text.Trim();
            var loginName = this.LoginNametb.Text.Trim();
            var passWord = this.PassWordtb.Text.Trim();
            var partition = this.Partitiontb.Text.Trim();
            var key = this.keyTb.Text.Trim();
            var secret = this.secretTb.Text.Trim();

            if (string.IsNullOrEmpty(serverName))
            {
                isReturn = true;
                this.servertiplbl.Visible = true;
            }
            if (string.IsNullOrEmpty(dbName))
            {
                isReturn = true;
                this.dbtiplbl.Visible = true;
            }
            if (string.IsNullOrEmpty(dbPath))
            {
                isReturn = true;
                this.dbaddresstiplbl.Visible = true;
            }
            if (string.IsNullOrEmpty(loginName))
            {
                isReturn = true;
                this.nametiplbl.Visible = true;
            }

            if (string.IsNullOrEmpty(passWord))
            {
                isReturn = true;
                this.passwordtiplbl.Visible = true;
            }

            if (string.IsNullOrEmpty(partition))
            {
                isReturn = true;
                this.parttiplbl.Visible = true;
            }

            if (string.IsNullOrEmpty(key))
            {
                isReturn = true;
                this.keytiplbl.Visible = true;
            }

            if (string.IsNullOrEmpty(secret))
            {
                isReturn = true;
                this.secrettiplbl.Visible = true;
            }
            if (isReturn)
            {
                btn.Enabled = true;
                return;
            }



            Thread thread = new Thread(new ThreadStart(CreateTrigger));
            thread.IsBackground = true;
            thread.Start();
        }
        #endregion

        #region 创建触发器
        private void CreateTrigger()
        {
            try
            {
                #region 数据验证
                string serverName = ServerNametb.Text.Trim();
                string userId = LoginNametb.Text.Trim();
                string password = PassWordtb.Text.Trim();
                string dbPath = dbPathtb.Text.Trim();
                string dbName = DBNametb.Text.Trim();
                string partPath = Partitiontb.Text.Trim();
                if (string.IsNullOrEmpty(serverName.Trim()))
                {
                    MessageBox.Show("服务器名称不能为空");
                    this.BeginInvoke(new MethodInvoker(delegate ()
                    {
                        Setupbtn.Enabled = true;
                    }));
                    return;
                }

                if (string.IsNullOrEmpty(userId.Trim()))
                {
                    MessageBox.Show("用户名不能为空");
                    this.BeginInvoke(new MethodInvoker(delegate ()
                    {
                        Setupbtn.Enabled = true;
                    }));
                    return;
                }

                if (string.IsNullOrEmpty(password.Trim()))
                {
                    MessageBox.Show("密码不能为空");
                    this.BeginInvoke(new MethodInvoker(delegate ()
                    {
                        Setupbtn.Enabled = true;
                    }));
                    return;
                }

                #endregion

                #region 创建数据库

                List<ReplaceInfo> replaceList = new List<ReplaceInfo>();
                string dbFile = "DBFile/";


                //创建数据库
                string filePath = AppDomain.CurrentDomain.BaseDirectory + dbFile + "DBName.sql";
                string connStr = "data source={0};user id={1};password={2};persist security info=false;packet size=4096;";
                connStr = string.Format(connStr, serverName, userId, password);


                bool isConnect = ConnectionTest(connStr);
                if (!isConnect)
                {
                    MessageBox.Show("数据库配置不正确，请检查数据库配置");
                    this.BeginInvoke(new MethodInvoker(delegate ()
                    {
                        Setupbtn.Enabled = true;
                    }));
                    return;
                }

                #region 启用云平台
                //启用云平台

                //创建触发器
                filePath = AppDomain.CurrentDomain.BaseDirectory + dbFile + "Trigger.sql";

                if (!File.Exists(filePath))
                {
                    MessageBox.Show("触发器文件不存在");
                    this.BeginInvoke(new MethodInvoker(delegate ()
                    {
                        Setupbtn.Enabled = true;
                    }));
                    return;
                }

                replaceList = new List<ReplaceInfo>();
                ReplaceInfo replace = new ReplaceInfo();
                replace.OldValue = @"iCMSDB";
                replace.NewValue = DBNametb.Text.Trim();
                replaceList.Add(replace);

                replace = new ReplaceInfo();
                replace.OldValue = @"5bcbc178cf70e1ec7ca1586a1eaac1d3";
                replace.NewValue = keyTb.Text.Trim();
                replaceList.Add(replace);

                replace = new ReplaceInfo();
                replace.OldValue = @"252a7d7582a39c899de71efa8b6fb368";
                replace.NewValue = secretTb.Text.Trim();
                replaceList.Add(replace);


                //创建触发器
                SqlFile(connStr, filePath, replaceList);

                //开启http请求
                filePath = AppDomain.CurrentDomain.BaseDirectory + dbFile + "OpenHttpQuest.sql";

                if (!File.Exists(filePath))
                {
                    MessageBox.Show("http请求文件不存在");
                    this.BeginInvoke(new MethodInvoker(delegate ()
                    {
                        Setupbtn.Enabled = true;
                    }));
                    return;
                }

                replaceList = new List<ReplaceInfo>();
                replace = new ReplaceInfo();
                replace.OldValue = @"iCMSDB";
                replace.NewValue = DBNametb.Text.Trim();
                replaceList.Add(replace);


                //创建http请求
                SqlFile(connStr, filePath, replaceList);



                #endregion


                #endregion

                this.BeginInvoke(new MethodInvoker(delegate ()
                {
                    Setupbtn.Enabled = true;
                }));
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);

                MessageBox.Show("数据安装出错，请核对数据库连接或查看日志");
            }
            finally
            {
                this.BeginInvoke(new MethodInvoker(delegate ()
                {
                    Setupbtn.Enabled = true;
                    btnCreateTrigger.Enabled = true;
                }));

                MessageBox.Show("数据库安装配置完成。");
            }
        }
        #endregion
    }
    #endregion
}
