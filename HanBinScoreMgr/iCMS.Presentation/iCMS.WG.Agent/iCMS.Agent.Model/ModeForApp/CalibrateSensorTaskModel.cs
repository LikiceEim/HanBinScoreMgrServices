using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.WG.Agent.Model.Send;

namespace iCMS.WG.Agent.Model
{
    public class CalibrateSensorTaskModel : TaskModelBase
    {
        /// <summary>
        /// 传感器校准列表
        /// </summary>
        public List<CheckMonitor> checkMonitorList { get; set; }
        public override string operatorName
        {
            get
            {
                return "CalibrateSensorOper";
            }

        }
    }
}
