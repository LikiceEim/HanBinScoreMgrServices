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
    [Table("DICT_ORGANIZATION_CATEGORY")]
    public class OrganCategory : EntityBase
    {
        /// <summary>
        /// 单位类别
        /// </summary>
        [Key]
        public int CategoryID { get; set; }
        /// <summary>
        /// 单位类别名称
        /// </summary>
        public string CategoryName { get; set; }

        public int AddUserID { get; set; }

        public int LastUpdateUserID { get; set; }

        public DateTime LastUpdateDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
