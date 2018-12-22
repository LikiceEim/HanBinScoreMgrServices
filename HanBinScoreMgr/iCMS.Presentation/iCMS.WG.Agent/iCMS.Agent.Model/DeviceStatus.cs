
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.WG.Agent.Model
{
    public class DeviceStatus
    {
        /// <summary>
        /// WS编号
        /// </summary>
        public string WSID { set; get; }
        /// <summary>
        /// WS的MAC地址
        /// </summary>
        public string WSMAC { set; get; }
        /// <summary>
        /// WS连接状态
        /// </summary>
        public string WSLinkstatu { set; get; }
        /// <summary>
        /// WG编号
        /// </summary>
        public string WGID { set; get; }
        /// <summary>
        /// WG连接状态
        /// </summary>
        public string WGLinkstatu { set; get; }

    }
}
