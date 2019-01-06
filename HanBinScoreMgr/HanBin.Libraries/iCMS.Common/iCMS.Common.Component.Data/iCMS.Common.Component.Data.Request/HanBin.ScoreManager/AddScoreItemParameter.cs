using HanBin.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Data.Request.HanBin.ScoreManager
{
    public class AddScoreItemParameter : BaseRequest
    {
        public int ItemScore { get; set; }

        public string ItemDescription { get; set; }

        public int Type { get; set; }

        public int AddUserID { get; set; }

        public int CurrentUserID { get; set; }
    }

    public class EditScoreItemParameter : BaseRequest
    {
        public int ItemID { get; set; }

        public int ItemScore { get; set; }

        public string ItemDescription { get; set; }

        public int EditUserID { get; set; }

        public int CurrentUserID { get; set; }
    }

    public class DeleteScoreItemParameter : BaseRequest
    {
        public int ItemID { get; set; }

        public int CurrentUserID { get; set; }
    }
}
