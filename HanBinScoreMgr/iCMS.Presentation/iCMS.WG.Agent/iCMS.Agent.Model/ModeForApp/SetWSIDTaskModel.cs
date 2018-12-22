using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.WG.Agent.Model.Send;

namespace iCMS.WG.Agent.Model
{
    public class SetWSIDTaskModel : TaskModelBase
    {
        /// <summary>
        /// WSID
        /// </summary>
        public List<SetNewWSID> newWSIDList = new List<SetNewWSID>();
        public override string operatorName
        {
            get
            {
                return "SetNewWSIDOper";
            }
        } 
    }
}
