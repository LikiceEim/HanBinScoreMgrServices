﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;





using HanBin.Presentation.Service.ScoreService;
using System.IO;
using HanBin.Common.Component.Tool;
using HanBin.Common.Component.Data.Request.HanBin.ScoreManager;
using HanBin.Common.Component.Data.Request.HanBin.OfficerManager;
using System.Data;
using System.Data.SqlClient;
using HanBin.Common.Component.Data.Request.HanBin.SystemManage;


namespace TESTConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string cloudServer = @"http://111.231.200.224:8842/HanBinScoreService.svc";
            string localServer = @"http://localhost:2892/HanBin/ScoreService";
            RestClient client = new RestClient(localServer);
            DownLoadFileParameter param = new DownLoadFileParameter();
            param.FilePath = @"D:\Backup\Test.txt";
            //UpFile upfile = new UpFile();
            //upfile.FileName = "testFile";
            //upfile.FileSize = 100;

            //upfile.FileStream = File.Create(filePath);
            string json = param.ToClientString();
            var res = client.Post(json, "DownLoadFile");

            Console.WriteLine(res);

            Console.ReadKey();
        }

        static void RestoreDB(string dataBaseName, string path)
        {
            path = @"C:\Program Files (x86)\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA";

            //连接字符串
            var connectionStr = EcanSecurity.Decode(Utilitys.GetAppConfig("iCMS"));
            DataTable DBNameTable = new DataTable();
            SqlDataAdapter Adapter = new SqlDataAdapter("select name from master..sysdatabases", connectionStr);
            lock (Adapter)
            {
                Adapter.Fill(DBNameTable);
            }
            foreach (DataRow row in DBNameTable.Rows)
            {
                if (row["name"].ToString() == dataBaseName)
                {
                    throw new Exception("已存在对应的数据，请勿重复还原数据库！");
                }
            }
            //检测真正当前bak文件真正的log mdf的名字
            var strsql = " restore  filelistonly from disk = '" + path + "'";
            SqlDataAdapter Adapter2 = new SqlDataAdapter(strsql, connectionStr);
            var dt = new DataTable();
            lock (Adapter2)
            {
                Adapter2.Fill(dt);
            }
            var mdf = dt.Rows[0][0].ToString();
            var log = dt.Rows[1][0].ToString();
            string restore = string.Format(@"restore database {0} from disk = '{1}'
                                                with REPLACE
                                                , move '{2}' to 'D:\{3}.mdf'
                                                ,move '{4}' to 'D:\{5}.ldf'", dataBaseName, path, mdf, dataBaseName, log, dataBaseName);
            SqlConnection conn = new SqlConnection(connectionStr);
            SqlCommand cmd1 = new SqlCommand(restore, conn);
            conn.Open();//k
            cmd1.ExecuteNonQuery();
            conn.Close();//g
        }

    }
}