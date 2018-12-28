using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Request.HanBin.ScoreManager
{
    public class AddScoreItemParameter
    {
        public int ItemScore { get; set; }

        public string ItemDescription { get; set; }

        public int Type { get; set; }

        public int AddUserID { get; set; }
    }

    public class EditScoreItemParameter
    {
        public int ItemID { get; set; }

        public int ItemScore { get; set; }

        public string ItemDescription { get; set; }

        public int EditUserID { get; set; }

    }

    public class DeleteScoreItemParameter
    {
        public int ItemID { get; set; }
    }
}
