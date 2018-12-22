using iCMS.Common.Component.Data.Base.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Core.DB.Models
{
    [Table("DICT_ORGANIZATION_TYPE")]
    public class OrganType : EntityBase
    {
        [Key]
        public int OrganTypeID { get; set; }

        public string OrganTypeName { get; set; }

        public int CategoryID { get; set; }

        public int AddUserID { get; set; }

        public int LastUpdateUserID { get; set; }

        public DateTime LastUpdateDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
