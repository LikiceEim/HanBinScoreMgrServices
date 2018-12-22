using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.WG.Agent.Model
{
    public class RestoreWGTaskModel : TaskModelBase
    {
        /// <summary>
        /// 待恢复出厂设置的WG的MAC地址结合
        /// </summary>
        public List<string> macList { set; get; }

        public override string operatorName
        {
            get
            {
                return "RestoreWGOper";
            }

        }
    }
}
