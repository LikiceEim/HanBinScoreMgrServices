using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.WG.Agent.Model
{
    public class TimeCalibrationTaskModel : TaskModelBase
    {
        public override string operatorName
        {
            get
            {
                return "TimeCalibrationOper";
            }

        }
    }
}
