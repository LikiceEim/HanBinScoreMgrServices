using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.DAUAgent
{
    public class SetDAUAgentMDFResult
    {
        public List<SetDAUAgentMDFItemResult> DAUAgentMDFItemResultList { get; set; }

        public SetDAUAgentMDFResult()
        {
            this.DAUAgentMDFItemResultList = new List<SetDAUAgentMDFItemResult>();
        }
    }

    public class SetDAUAgentMDFItemResult
    {
        public int DAUID { get; set; }

        public bool IsSuccess { get; set; }
    }
}
