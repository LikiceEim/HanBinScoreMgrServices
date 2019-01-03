using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Data.Request.HanBin.SystemManage
{
    public class QueryLogParameter
    {
        public int? RoleID { get; set; }

        public DateTime? BeginTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string Keyword { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}
