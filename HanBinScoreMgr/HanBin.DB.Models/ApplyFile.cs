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
    [Table("DATA_APPLY_UPLOAD_FILE")]
    public class ApplyUploadFile : EntityBase
    {
        [Key]
        public int ID { get; set; }

        public int ApplyID { get; set; }

        public string FilePath { get; set; }

        public bool IsDeleted { get; set; }
    }
}
