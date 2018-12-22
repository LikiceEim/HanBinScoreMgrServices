/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Tool

 *文件名：  FileHelper
 *创建人：  王颖辉
 *创建时间：2017-01-06
 *描述：文件处理
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Tool
{
    #region 文件处理
    /// <summary>
    /// 文件处理
    /// </summary>
    public class FileHelper
    {
        public static bool connectState(string path)
        {
            return connectState(path, "", "");
        }

        public static bool connectState(string path, string userName, string passWord)
        {
            bool Flag = false;
            Process proc = new Process();
            try
            {
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
                //string dosLine = @"net use " + path + " /User:" + userName + " " + passWord + " /PERSISTENT:YES";
                string dosLine = @"net use " + path;
                proc.StandardInput.WriteLine(dosLine);
                proc.StandardInput.WriteLine("exit");
                while (!proc.HasExited)
                {
                    proc.WaitForExit(1000);
                }
                string errormsg = proc.StandardError.ReadToEnd();
                proc.StandardError.Close();
                if (string.IsNullOrEmpty(errormsg))
                {
                    Flag = true;
                }
                else
                {
                    throw new Exception(errormsg);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                proc.Close();
                proc.Dispose();
            }
            return Flag;
        }


        //read file
        public static void ReadFiles(string path)
        {
            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(path))
                {
                    String line;
                    // Read and display lines from the file until the end of 
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);

                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
            }
        }

        //write file
        public static void WriteFiles(string path)
        {
            try
            {
                // Create an instance of StreamWriter to write text to a file.
                // The using statement also closes the StreamWriter.
                using (StreamWriter sw = new StreamWriter(path))
                {
                    // Add some text to the file.
                    sw.Write("This is the ");
                    sw.WriteLine("header for the file.");
                    sw.WriteLine("-------------------");
                    // Arbitrary objects can also be written to the file.
                    sw.Write("The date is: ");
                    sw.WriteLine(DateTime.Now);
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// 替换文件内容
        /// </summary>
        /// <param name="filePath">文件地址</param>
        /// <param name="resorce">被替换</param>
        /// <param name="target">替换</param>
        public static void ReplaceFileContent(string filePath, string resorce, string target)
        {
            try
            {
                string con = "";
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                byte[] info = new byte[fs.Length];
                fs.Read(info, 0, info.Length);
                con = Encoding.Default.GetString(info);
                con = con.Replace(resorce, target);
                sr.Close();
                fs.Close();
                FileStream fs2 = new FileStream(filePath, FileMode.Open, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs2, Encoding.Default);
                sw.WriteLine(con);
                sw.Close();
                fs2.Close();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }
    }
    #endregion
}
