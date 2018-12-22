using iCMS.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Request.Cloud
{
    public class WaveLowerLimitValuesByIDParameter :BaseRequest
    {
        public int ID { get; set; }
    }
    public class WaveUpperLimitValuesByIDParameter : BaseRequest 
    {
        public int ID { get; set; }
    }
    public class WaveLengthValuesByIDParameter : BaseRequest 
    {
        public int ID { get; set; }
    }

}
