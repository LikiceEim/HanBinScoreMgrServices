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
    [Table("SYS_USER")]
    public class HBUser : EntityBase
    {
        [Key]
        public int UserID { get; set; }

        public string UserToken { get; set; }

        public string PWD { get; set; }

        public int OrganizationID { get; set; }

        public int Gender { get; set; }

        public int RoleID { get; set; }

        public DateTime LastUpdateDate { get; set; }

        public int AddUserID { get; set; }

        public int LastUpdateUserID { get; set; }

        /// <summary>
        /// 使用状态 启用/禁用
        /// </summary>
        public bool UseStatus { get; set; }

        public bool IsDeleted { get; set; }
    }
}
