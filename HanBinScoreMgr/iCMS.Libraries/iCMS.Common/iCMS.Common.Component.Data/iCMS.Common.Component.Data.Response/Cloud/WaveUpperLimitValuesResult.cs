using iCMS.Frameworks.Core.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.Cloud
{
   public  class WaveUpperLimitValuesResult
    {
       public List<WaveUpperLimitValues> WaveUpperLimitValuesList { get; set; }

       public WaveUpperLimitValuesResult()
       {
           WaveUpperLimitValuesList = new List<WaveUpperLimitValues>();
       }
    }
}
