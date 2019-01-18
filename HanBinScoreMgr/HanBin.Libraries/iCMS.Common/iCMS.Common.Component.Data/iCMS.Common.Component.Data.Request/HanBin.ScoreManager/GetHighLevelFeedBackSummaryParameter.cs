using HanBin.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Data.Request.HanBin.ScoreManager
{
    public class GetHighLevelFeedBackSummaryParameter : BaseRequest
    {
        public int RankNumber { get; set; }

        /// <summary>
        /// 当前登陆的用户ID, 二级管理员ID
        /// </summary>
        public int CurrentUserID { get; set; }
    }
}
