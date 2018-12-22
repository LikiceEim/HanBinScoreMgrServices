using iCMS.Frameworks.Core.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.Cloud
{
    public class VibSignalTypeResult
    {
        public List<VibratingSingalType> VibSignalTypeList { get; set; }
        public VibSignalTypeResult()
        {
            VibSignalTypeList = new List<VibratingSingalType>();
        }
    }
}
