using HanBin.Common.Component.Data.Base.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Core.DB.Models
{
    [Table("SYS_BACKUPLOG")]
    public class BackupLog : EntityBase
    {
        [Key]
        public int ID { get; set; }

        public DateTime BackupDate { get; set; }

        public string BackupPath { get; set; }

        public long BackupSize { get; set; }

        public bool IsDeleted { get; set; }
    }
}
