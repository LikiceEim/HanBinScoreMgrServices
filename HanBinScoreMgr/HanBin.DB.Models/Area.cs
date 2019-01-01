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
    [Table("SYS_AREA")]
    public class Area : EntityBase
    {
        [Key]
        public int AreaID { get; set; }

        public string AreaName { get; set; }

        public string Remark { get; set; }

        public bool IsDeleted { get; set; }
    }
}
