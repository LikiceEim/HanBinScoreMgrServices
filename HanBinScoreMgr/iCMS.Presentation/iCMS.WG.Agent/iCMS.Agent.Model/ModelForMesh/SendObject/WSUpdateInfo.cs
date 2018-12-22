using iMesh;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace iCMS.WG.Agent.Model.Send
{
    public class WSUpdateInfo
    {
        public WSUpdateInfo()
        {
            UpdatingAllWSInfo = new Dictionary<string, Timer>();
        }

        /// <summary>
        /// 本次选择升级的所有WS是否超时的总定时器
        /// </summary>
        public Timer UpdateAllWSTimer { get; set; }

        /// <summary>
        /// 保存本次所有要升级的WS
        /// </summary>
        public Dictionary<string, Timer> UpdatingAllWSInfo { get; private set; }
    }
}
