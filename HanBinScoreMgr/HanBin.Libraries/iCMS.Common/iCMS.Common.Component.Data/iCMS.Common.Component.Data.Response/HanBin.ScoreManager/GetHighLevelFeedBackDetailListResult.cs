using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Data.Response.HanBin.ScoreManager
{
    public class GetHighLevelFeedBackDetailListResult
    {
        public List<ApprovedApplyDetail> ApplovedApplyDetailList { get; set; }

        public int Total { get; set; }

        public GetHighLevelFeedBackDetailListResult()
        {
            this.ApplovedApplyDetailList = new List<ApprovedApplyDetail>();
        }
    }

    public class ApprovedApplyDetail
    {
        public int ApplyID { get; set; }

        public string OfficerName { get; set; }

        public string IdentifyCardNumber { get; set; }

        public int OrganID { get; set; }

        public string OrganFullName { get; set; }

        public int PositionID { get; set; }

        public string PositionName { get; set; }

        public int ItemScore { get; set; }

        public string ItemDescription { get; set; }

        public List<string> UploadFileList { get; set; }

        public DateTime AddDate { get; set; }

        public int ApproveStatus { get; set; }

        public string RejectReason { get; set; }

        public int ProcessuUserID { get; set; }

        public string ProcessuUserName { get; set; }

        public ApprovedApplyDetail()
        {
            this.UploadFileList = new List<string>();
        }
    }
}
