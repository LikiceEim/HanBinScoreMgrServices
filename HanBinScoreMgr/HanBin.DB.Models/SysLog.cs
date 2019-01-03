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
    [Table("SYS_OPERATION_LOG")]
    public class SysLog : EntityBase
    {
        [Key]
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
