using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Data.Response.HanBin.SystemManager
{
    public class QueryLogResult
    {
        public List<LogInfo> LogList { get; set; }

        public int Total { get; set; }

        public QueryLogResult()
        {
            this.LogList = new List<LogInfo>();
        }
    }

    public class LogInfo
    {
        public int ID { get; set; }

        public int OperationUserID { get; set; }

        public string UserToken { get; set; }

        public int RoleID { get; set; }

        public string RoleName { get; set; }

        public string Content { get; set; }

        public int OrganID { get; set; }

        public string OrganName { get; set; }

        public string IP { get; set; }

        public string HTTPType { get; set; }

        public DateTime OperationDate { get; set; }

    }
}
