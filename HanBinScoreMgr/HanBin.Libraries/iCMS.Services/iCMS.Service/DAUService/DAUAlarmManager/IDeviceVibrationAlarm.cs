using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Service.Web.DAUService.DAUAlarmParameter;

namespace iCMS.Service.Web.DAUService.DAUAlarmManager
{
    #region 设备振动信号报警
    /// <summary>
    /// 设备振动信号报警
    /// </summary>
    public interface IDeviceVibrationAlarm
    {
        #region 设备振动报警管理
        /// <summary>
        /// 设备振动报警管理
        /// </summary>
        /// <param name="param"></param>
        void DevVibAlmRecordManager(DeviceVibtationAlarmParameter param);
        #endregion
    }
    #endregion
}