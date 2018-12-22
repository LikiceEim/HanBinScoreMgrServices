using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Setup.DBUpgrade.UpgradeFM.Common
{
    public enum EnumMeasureAlmType
    {
        [Description("温度")]
        Temperature = 0,
        [Description("电池电压")]
        BatteryVoltage = 1,
    }
}
