using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Setup.DBUpgrade.UpgradeFM.Common
{
    public enum AlarmInfoType
    {
        //振动
        devVib = 1,
        //设备温度
        devTemperature = 2,
        //传感器温度
        wsTemperature = 3,
        //传感器电池电压
        wsVoltage = 4,
        //传感器连接
        wsLink = 5,
        //网关连接
        wgLink = 6,
        //停机
        devStop = 7,
        //趋势报警
        trendAlarm = 8,
    }
}
