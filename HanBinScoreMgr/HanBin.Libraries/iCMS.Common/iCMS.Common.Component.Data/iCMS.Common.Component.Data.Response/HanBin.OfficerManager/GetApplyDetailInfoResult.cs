using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Data.Response.HanBin.OfficerManager
{
    public class GetApplyDetailInfoResult
    {
        /// <summary>
        /// 积分申请ID
        /// </summary>
        public int ApplyID { get; set; }
        /// <summary>
        /// 条目ID
        /// </summary>
        public int ItemID { get; set; }

        public int ItemScore { get; set; }
        /// <summary>
        /// 条目类型 1：加分申请；2：减分申请
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 条目描述
        /// </summary>
        public string ItemDescription { get; set; }
        /// <summary>
        /// 申请摘要描述
        /// </summary>
        public string ApplySummary { get; set; }
        /// <summary>
        /// 申请上传的文件路径
        /// </summary>
        public List<string> UploadFileList { get; set; }

        public GetApplyDetailInfoResult()
        {
            this.UploadFileList = new List<string>();
        }
    }
}
