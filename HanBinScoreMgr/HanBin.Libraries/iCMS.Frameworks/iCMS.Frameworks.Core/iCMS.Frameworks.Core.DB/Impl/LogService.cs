
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Frameworks.Core.DB.Impl
{
    public class LogService : ILogService
    {
        ILog TxtLog = LogManager.GetLogger("TxtLog");


        #region ILogger 成员

        static LogService()
        {
            log4net.Config.XmlConfigurator.Configure();
        }
        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="logMessage"></param>
        public void ErrorLog(string logMessage)
        {
            TxtLog.Error(logMessage);
        }


        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="ex"></param>
        public void ErrorLog(string logMessage, Exception ex)
        {
            TxtLog.Error(logMessage, ex);
        }



        /// <summary>
        /// 重要程序流程进入
        /// </summary>
        /// <param name="logMessage"></param>
        public void InfoLog(string logMessage)
        {
            TxtLog.Info(logMessage);
        }
        public void InfoLog(string logMessage, Exception ex)
        {
            TxtLog.Info(logMessage, ex);
        }


        /// <summary>
        /// 调试消息
        /// </summary>
        /// <param name="logMessage"></param>
        public void DebugLog(string logMessage)
        {

            TxtLog.Debug(logMessage);
        }
        public void DebugLog(string logMessage, Exception ex)
        {

            TxtLog.Debug(logMessage, ex);
        }

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="logMessage"></param>
        public void WarmLog(string logMessage)
        {
            TxtLog.Warn(logMessage);

        }
        public void WarmLog(string logMessage, Exception ex)
        {
            TxtLog.Warn(logMessage, ex);

        }
        /// <summary>
        /// 严重错误
        /// </summary>
        /// <param name="logMessage"></param>
        public void FatalLog(string logMessage)
        {
            TxtLog.Fatal(logMessage);
        }
        public void FatalLog(string logMessage, Exception ex)
        {
            TxtLog.Fatal(logMessage, ex);
        }
        #endregion
    }
}
