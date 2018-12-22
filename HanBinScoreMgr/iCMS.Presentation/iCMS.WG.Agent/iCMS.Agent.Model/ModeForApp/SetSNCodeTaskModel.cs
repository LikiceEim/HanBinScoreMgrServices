using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.WG.Agent.Model.Send;

namespace iCMS.WG.Agent.Model
{
    public class SetSNCodeTaskModel : TaskModelBase
    {
        /// <summary>
        /// 串码数据
        /// </summary>
        public List<SetSNCode> snCodeList = new List<SetSNCode>();
        public override string operatorName
        {
            get
            {
                return "SetSNCodeOper";
            }

        }
    }
}
