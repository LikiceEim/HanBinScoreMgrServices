using iCMS.Common.Component.Data.Base.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Frameworks.Core.DB.Models
{
    [Table("T_SYS_FACTORY")]
    public class Factory : EntityBase
    {
        public int ID { get; set; }

        public string FactoryID { get; set; }

        public string FactoryName { get; set; }
    }
}
