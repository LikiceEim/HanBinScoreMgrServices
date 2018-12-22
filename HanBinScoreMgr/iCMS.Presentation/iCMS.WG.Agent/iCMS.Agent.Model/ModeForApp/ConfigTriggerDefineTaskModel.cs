using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.WG.Agent.Model.Send;

namespace iCMS.WG.Agent.Model
{
    public class ConfigTriggerDefineTaskModel : TaskModelBase
    {
        /// <summary>
        /// 发送测量定义列表
        /// </summary>
        public List<SendTriggerDefine> triggerDefineList { get; set; }
        //public Dictionary<string, byte[]> measureDefine { set; get; }
        /// <summary>
        /// WS版本（4byte）
        /// </summary>
        public string Version;
        /// <summary>
        /// 端口号1
        /// </summary>
        public string Port1;
        /// <summary>
        /// 端口号2
        /// </summary>
        public string Port2;

        public override string operatorName
        {
            get
            {
                return "ConfigTriggerDefineOper";
            }

        }
    }
}
