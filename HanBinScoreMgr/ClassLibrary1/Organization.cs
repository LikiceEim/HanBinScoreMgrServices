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
    [Table("SYS_ORGANIZATION")]
    public class Organization : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int OrganID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OrganCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OrganFullName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OrganShortName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int OrganTypeID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int AddUserID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int LastUpdateUserID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime LastUpdateDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}

