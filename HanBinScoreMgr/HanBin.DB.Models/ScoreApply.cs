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

    /// <summary>
    /// 积分申请表
    /// </summary>
    [Table("DATA_SCORE_APPLY")]
    public class ScoreApply : EntityBase
    {
        [Key]
        public int ApplyID { get; set; }
        /// <summary>
        /// 干部ID
        /// </summary>
        public int OfficerID { get; set; }
        /// <summary>
        /// 积分条目ID
        /// </summary>
        public int ItemID { get; set; }
        /// <summary>
        /// 分值（冗余）
        /// </summary>
        public int ItemScore { get; set; }
        /// <summary>
        /// 申请状态 审批状态：0：审批中；1：审批通过；2：审批驳回。4：撤销
        /// </summary>
        public int ApplyStatus { get; set; }
        /// <summary>
        /// 处里者
        /// </summary>
        public int? ProcessUserID { get; set; }
        /// <summary>
        /// 申请提出者
        /// </summary>
        public int ProposeID { get; set; }
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
        /// <summary>
        /// 申请摘要
        /// </summary>
        public string ApplySummary { get; set; }

        /// <summary>
        /// 驳回理由
        /// </summary>
        public string RejectReason { get; set; }

    }
}
