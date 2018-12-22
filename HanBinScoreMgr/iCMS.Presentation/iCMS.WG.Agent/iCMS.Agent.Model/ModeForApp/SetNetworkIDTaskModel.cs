using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.WG.Agent.Model.Send;

namespace iCMS.WG.Agent.Model
{
    public class SetNetworkIDTaskModel : TaskModelBase
    {
        public List<SetNetworkID> networkIDList = new List<SetNetworkID>();
        public override string operatorName
        {
            get
            {
                return "SetNetworkIDOper";
            }

        }
    }
}
