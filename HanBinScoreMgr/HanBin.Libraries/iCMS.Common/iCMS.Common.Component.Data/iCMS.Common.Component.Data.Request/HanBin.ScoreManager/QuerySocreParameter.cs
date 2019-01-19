using HanBin.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Data.Request.HanBin.ScoreManager
{
    public class QuerySocreParameter : BaseRequest
    {
        public int? OrganTypeID { get; set; }

        public int? LevelID { get; set; }

        public DateTime? BirthdayFrom { get; set; }

        public DateTime? BirthdayTo { get; set; }

        public int? Gender { get; set; }

        /// <summary>
        /// 用于搜索身份证号码，姓名
        /// </summary>
        public string Keyword { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public int CurrentUserID { get; set; }
    }
}
