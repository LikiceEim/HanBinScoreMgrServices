using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.WG.Agent.Model
{
    public class GetSNCodeTaskModel : TaskModelBase
    {

        /// <summary>
        /// WGID
        /// </summary>
        public string WGID { set; get; }

        public string Mac { set; get; }
        
        public override string operatorName
        {
            get
            {
                return "GetSNCodeOper";
            }

        }
    }
}
