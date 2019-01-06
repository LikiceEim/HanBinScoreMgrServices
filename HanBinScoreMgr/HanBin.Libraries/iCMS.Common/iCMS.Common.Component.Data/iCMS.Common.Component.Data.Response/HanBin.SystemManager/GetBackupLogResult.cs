using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Data.Response.HanBin.SystemManager
{
    public class GetBackupLogResult
    {
        public List<BackupInfo> BackupList { get; set; }

        public int Total { get; set; }

        public GetBackupLogResult()
        {
            BackupList = new List<BackupInfo>();
        }
    }


    public class BackupInfo
    {
        public int BackupID { get; set; }

        public DateTime BackupDate { get; set; }

        public string BackupPath { get; set; }

        public long BackupSize { get; set; }
    }
}
