using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iCMS.WG.AgentServer
{
    public class UploadResult
    {
        /// <summary>
        /// 返回结果  0：已受理，1，未受理失败
        /// </summary>
        public int Result { get; set; }
        /// <summary>
        /// 失败原因，返回结果为成功时，该值为空
        /// </summary>
        public string Reason { get; set; }
    }
}