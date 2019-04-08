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
    [Table("DICT_MAIN_ORGAN_TYPE")]
    public class MainOrganType : EntityBase
    {
        [Key]
        public int OrganTypeID { get; set; }

        public string OrganTypeName { get; set; }

        public int AddUserID { get; set; }

        public DateTime AddDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
