using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Frameworks.Core.DB
{
     public interface ILogService
    {
        /// <summary>
        /// 系统调试信息
        /// </summary>
        /// <param name="logMessage"></param>
        void DebugLog(string logMessage);
        void DebugLog(string logMessage,Exception ex);

        /// <summary>
        /// 系统重要运行信息
        /// </summary>
        /// <param name="logMessage"></param>
        void InfoLog(string logMessage);
        void InfoLog(string logMessage,Exception ex);


        /// <summary>
        /// 错误发生
        /// </summary>
        /// <param name="logMessage"></param>
        void ErrorLog(string logMessage);
        void ErrorLog(string logMessage, Exception ex);
        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="logMessage"></param>
        void WarmLog(string logMessage);
        void WarmLog(string logMessage,Exception ex);
        /// <summary>
        /// 严重错误
        /// </summary>
        /// <param name="logMessage"></param>
        void FatalLog(string logMessage);
        void FatalLog(string logMessage,Exception ex);
    }
   
}
