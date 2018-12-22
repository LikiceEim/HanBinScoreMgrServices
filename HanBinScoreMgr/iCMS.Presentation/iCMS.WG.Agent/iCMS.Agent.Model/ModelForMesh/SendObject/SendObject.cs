using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.WG.Agent.Model.Send
{
   public class SendObject
    {
        /// <summary>
        /// WS MAC
        /// </summary>
        public string MAC { get; set; }

        /// <summary>
        /// 主命令
        /// </summary>
        public int MainCommand { get; set; }
        
        /// <summary>
        /// 子命令
        /// </summary>
        public int SubCommand { get; set; }
        /// <summary>
        /// 是否请求
        /// </summary>
        public int Request { get; set; }
        /// <summary>
        /// 数据长度
        /// </summary>
        public int Length { get; set; }
    }
}
