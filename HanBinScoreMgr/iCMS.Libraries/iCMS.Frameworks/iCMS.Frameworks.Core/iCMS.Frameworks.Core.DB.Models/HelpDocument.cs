using iCMS.Common.Component.Data.Base.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Frameworks.Core.DB.Models
{
    /// <summary>
    /// 帮助文档信息表
    /// </summary>
    [Table("T_SYS_HELP_DOCUMENT")]
    public class HelpDocument : EntityBase
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// PID
        /// </summary>
        public int PID { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Substance { get; set; }

        /// <summary>
        /// 是否显示 add by lwj---2018.05.03
        /// </summary>
        public bool IsShow { get; set; }
    }
}
