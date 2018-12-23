using iCMS.Common.Component.Data.Base.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Core.DB.Models
{
    [Table("DATA_SCORE_CHANGE_HISTORY")]
    public class ScoreChangeHistory : EntityBase
    {
        [Key]
        public int HisID { get; set; }

        public int ApplyID { get; set; }

        public int OfficerID { get; set; }

        public int ItemID { get; set; }

        public int ItemScore { get; set; }

        public int? ProcessUserID { get; set; }

        public int ProposeID { get; set; }

        public string Content { get; set; }

        public int AddUserID { get; set; }
    }
}
