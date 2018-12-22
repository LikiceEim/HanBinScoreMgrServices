using iCMS.Frameworks.Core.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.Cloud
{
    public class VibratingSingalCharHisAccResult
    {
        public List<VibratingSingalCharHisAcc> VibratingSingalCharHisAccList { get; set; }

        public VibratingSingalCharHisAccResult()
        {
            VibratingSingalCharHisAccList = new List<VibratingSingalCharHisAcc>();
        }
    }
    public class VibratingSingalCharHisDispResult
    {
        public List<VibratingSingalCharHisDisp> VibratingSingalCharHisDispList { get; set; }

        public VibratingSingalCharHisDispResult()
        {
            VibratingSingalCharHisDispList = new List<VibratingSingalCharHisDisp>();
        }
    }

    public class VibratingSingalCharHisEnvlResult
    {
        public List<VibratingSingalCharHisEnvl> VibratingSingalCharHisEnvlList { get; set; }

        public VibratingSingalCharHisEnvlResult()
        {
            VibratingSingalCharHisEnvlList = new List<VibratingSingalCharHisEnvl>();
        }
    }
    public class VibratingSingalCharHisVelResult
    {
        public List<VibratingSingalCharHisVel> VibratingSingalCharHisVelList { get; set; }

        public VibratingSingalCharHisVelResult()
        {
            VibratingSingalCharHisVelList = new List<VibratingSingalCharHisVel>();
        }
    }
    public class VibratingSingalCharHisLQResult
    {
        public List<VibratingSingalCharHisLQ> VibratingSingalCharHisLQList { get; set; }

        public VibratingSingalCharHisLQResult()
        {
            VibratingSingalCharHisLQList = new List<VibratingSingalCharHisLQ>();
        }
    }
}
