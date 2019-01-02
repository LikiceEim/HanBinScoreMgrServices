using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Data.Response.HanBin.ScoreManager
{
    public class GetWhatsToDoDetailListResult
    {
        public List<ApplyDetail> ApplyDetailList { get; set; }

        public int Total { get; set; }

        public GetWhatsToDoDetailListResult()
        {
            this.ApplyDetailList = new List<ApplyDetail>();
        }
    }
    public class ApplyDetail
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

        /// <summary>
        /// 积分提出者
        /// </summary>
        public int ProposeID { get; set; }

        public string ProposeName { get; set; }

        public ApplyDetail()
        {
            this.UploadFileList = new List<string>();
        }
    }
}
