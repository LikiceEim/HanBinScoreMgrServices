using iCMS.Common.Component.Data.Base.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Core.DB.Models
{
  public  class Organization:EntityBase
    {
        public int OrganizationID { get; set; }

        public string OrganizationName { get; set; }

        public string OrganizationShortName { get; set; }

        public int OrganizationTypeID { get; set; }



    }
}

