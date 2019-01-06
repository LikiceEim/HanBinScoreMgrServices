using HanBin.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Data.Request.HanBin.SystemManage
{
    public class GetBackupLogParameter : BaseRequest
    {
        public int Page { get; set; }

        public int PageSize { get; set; }
    }

    public class DeleteBackupParameter : BaseRequest
    {
        public int BackupID { get; set; }
    }
}
