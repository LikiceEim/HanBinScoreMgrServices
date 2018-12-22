using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Service.Web.DAUService.DAUAlarmParameter;

namespace iCMS.Service.Web.DAUService.DAUAlarmManager
{
    public interface IDeviceTemperatureAlarm
    {
        #region 设备温度报警管理
        /// <summary>
        /// 设备温度报警管理
        /// </summary>
        /// <param name="param"></param>
        void DevTemperatureAlmRecordManager(DeviceTemperatureAlarmParameter param);
        #endregion
    }
}