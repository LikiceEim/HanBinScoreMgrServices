using iCMS.Common.Component.Data.Base.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Core.DB.Models
{
    public class UploadFile : EntityBase
    {
        public int ID { get; set; }

        public int ApplyID { get; set; }

        public string FilePath { get; set; }


    }
}
