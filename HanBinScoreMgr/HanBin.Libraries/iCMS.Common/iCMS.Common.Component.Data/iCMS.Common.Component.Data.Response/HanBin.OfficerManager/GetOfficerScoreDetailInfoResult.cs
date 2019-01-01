using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Data.Response.HanBin.OfficerManager
{
    /// <summary>
    /// 获取干部积分信息返回结果类
    /// </summary>
    public class GetOfficerScoreDetailInfoResult
    {
        public int OfficerID { get; set; }

        public int InitialScore { get; set; }

        public int CurrentScore { get; set; }

        /// <summary>
        /// 已审批过的积分项
        /// </summary>
        public List<ApplyItemInfo> ApprovedApplyItemList { get; set; }
        /// <summary>
        /// 未审批的积分项
        /// </summary>
        public List<ApplyItemInfo> ApprovingApplyItemList { get; set; }

        public GetOfficerScoreDetailInfoResult()
        {
            this.ApprovedApplyItemList = new List<ApplyItemInfo>();
            this.ApprovingApplyItemList = new List<ApplyItemInfo>();
        }

    }

    public class ApplyItemInfo
    {
        /// <summary>
        /// 积分申请ID
        /// </summary>
        public int ApplyID { get; set; }
        /// <summary>
        /// 积分条目ID
        /// </summary>
        public int ItemID { get; set; }
        /// <summary>
        /// 条目分值
        /// </summary>
        public int ItemScore { get; set; }
        /// <summary>
        /// 条目描述
        /// </summary>
        public string ItemDescription { get; set; }
        /// <summary>
        /// 审批状态：0：审批中；1：审批通过；2：审批驳回；4：撤销
        /// </summary>
        public int ApplyStatus { get; set; }
    }
}
