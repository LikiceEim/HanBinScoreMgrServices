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
    [Table("SYS_OFFICER")]
    public class Officer : EntityBase
    {
        [Key]
        public int OfficerID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 性别： 1：男；2：女
        /// </summary>
        public int Gender { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string IdentifyCardNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public int OrganizationID { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        public int PositionID { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        public int LevelID { get; set; }
        /// <summary>
        /// 分管工作
        /// </summary>
        public string Duty { get; set; }
        /// <summary>
        /// 入职日期
        /// </summary>
        public DateTime OnOfficeDate { get; set; }
        /// <summary>
        /// 初始分
        /// </summary>
        public int InitialScore { get; set; }
        /// <summary>
        /// 当前积分
        /// </summary>
        public int CurrentScore { get; set; }
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
