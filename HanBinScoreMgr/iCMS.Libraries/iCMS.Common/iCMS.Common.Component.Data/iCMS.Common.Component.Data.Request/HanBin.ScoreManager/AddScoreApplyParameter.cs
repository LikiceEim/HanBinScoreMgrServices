using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Request.HanBin.ScoreManager
{
    public class AddScoreApplyParameter
    {
        public int OfficerID { get; set; }

        public int ScoreItemID { get; set; }

        public string ApplySummary { get; set; }

        public int ProposeID { get; set; }

        public List<string> UploadFileList { get; set; }
    }
}
