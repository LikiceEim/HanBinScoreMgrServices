/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 * 命名空间：iCMS.Presentation.Server.Statistics
 * 文件名：  StatisticsService
 * 创建人：  王颖辉
 * 创建时间：2016-10-21
 * 描述：统计服务接口
 *
 * 修改人：张辽阔
 * 修改时间：2016-11-10
 * 描述：增加错误编码
 *
 * 修改人：张辽阔
 * 修改时间：2016-12-15
 * 描述：未通过安全验证时，增加日志记录
/************************************************************************************/

using System.Threading.Tasks;
using System.ServiceModel;

using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Request.Statistics;
using iCMS.Common.Component.Data.Response.Statistics;
using iCMS.Common.Component.Tool;
using iCMS.Presentation.Common;
using iCMS.Service.Web.Statistics;

namespace iCMS.Presentation.Server.Statistics
{
    #region 统计服务类

    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“StatisticsService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 StatisticsService.svc 或 StatisticsService.svc.cs，然后开始调试。
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [CustomExceptionBehaviour(typeof(CustomExceptionHandler))]
    public class StatisticsService : BaseService, IStatisticsService
    {
        #region 变量

        public IStatisticsManager statisticsManager { get; private set; }

        #endregion

        #region 构造函数

        public StatisticsService(IStatisticsManager statisManager)
        {
            statisticsManager = statisManager;
        }

        #endregion

        #region 获取设备运行状态的统计

        /// <summary>
        /// 获取设备运行状态的统计
        /// </summary>
        /// <param name="MonitorTreeId"></param>
        /// <param name="Sort"></param>
        /// <param name="Order"></param>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public BaseResponse<DevRunningStatusDataByMTIDResult> GetDevRunningStatusDataByMTID(DevRunningStatusDataByMTIDParameter parameter)
        {
            if (ValidateData<DevRunningStatusDataByMTIDParameter>(parameter))
            {
                return statisticsManager.GetDevRunningStatusDataByMTID(parameter);
            }
            else
            {
                BaseResponse<DevRunningStatusDataByMTIDResult> result = new BaseResponse<DevRunningStatusDataByMTIDResult>();
                result.IsSuccessful = false;
                result.Code = "000531";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 设备运行状态统计

        /// <summary>
        /// 设备运行状态统计
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public BaseResponse<DevRunningStatusDataByUserIDResult> GetDevRunningStatusDataByUserID(DevRunningStatusDataByUserIDParameter parameter)
        {
            if (ValidateData<DevRunningStatusDataByUserIDParameter>(parameter))
            {
                return statisticsManager.GetDevRunningStatusDataByUserID(parameter);
            }
            else
            {
                BaseResponse<DevRunningStatusDataByUserIDResult> result = new BaseResponse<DevRunningStatusDataByUserIDResult>();
                result.IsSuccessful = false;
                result.Code = "000541";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 设备历史数据查询

        /// <summary>
        /// 设备历史数据查询
        /// </summary>
        /// <param name="MonitorTreeId"></param>
        /// <param name="DevId"></param>
        /// <param name="MSId"></param>
        /// <param name="BDate">如果开始时间和结束时间空，则返回最后一次的数据</param>
        /// <param name="EDate">如果开始时间和结束时间空，则返回最后一次的数据</param>
        /// <param name="DataStat">-1表示所有状态，1正常，2高报，3高高报</param>
        /// <param name="DataType">0表示所有类型，1速度，2加速度，3包络，4位移，5位移，6LQ,7设备温度</param>
        /// <param name="Sort"></param>
        /// <param name="Order"></param>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public BaseResponse<DevHistoryDataResult> GetDevHistoryData(DevHistoryDataParameter parameter)
        {
            if (ValidateData<DevHistoryDataParameter>(parameter))
            {
                return statisticsManager.GetDevHistoryData(parameter);
            }
            else
            {
                BaseResponse<DevHistoryDataResult> result = new BaseResponse<DevHistoryDataResult>();
                result.IsSuccessful = false;
                result.Code = "000551";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 设备运行状态查询

        /// <summary>
        /// 修改人：张辽阔
        /// 修改时间：2016-08-20
        /// 修改记录：修改排序字段错误的问题
        /// </summary>
        /// <param name="MonitorTreeId"></param>
        /// <param name="DevId"></param>
        /// <param name="DevNo"></param>
        /// <param name="DevRunningStat"></param>
        /// <param name="DevAlarmStat"></param>
        /// <param name="Sort"></param>
        /// <param name="Order"></param>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <param name="DevType"></param>
        /// <param name="UseType"></param>
        /// <returns></returns>
        public BaseResponse<DevRunStatusDataResult> QueryDevRunStatusData(DevRunStatusDataParameter parameter)
        {
            if (ValidateData<DevRunStatusDataParameter>(parameter))
            {
                return statisticsManager.QueryDevRunStatusData(parameter);
            }
            else
            {
                BaseResponse<DevRunStatusDataResult> result = new BaseResponse<DevRunStatusDataResult>();
                result.IsSuccessful = false;
                result.Code = "000561";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 获取设备报警记录

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-08-03
        /// 创建记录：获取设备报警记录
        /// </summary>
        /// <param name="bDate">开始时间</param>
        /// <param name="eDate">结束时间</param>
        /// <param name="msAlmID">报警类型</param>
        /// <param name="monitorTreeID">位置结点</param>
        /// <param name="almStatus">报警状态</param>
        /// <param name="devID">设备id</param>
        /// <param name="mSiteID">测量位置id</param>
        /// <param name="singalID">振动信号ID</param>
        /// <param name="singalAlmID">特征值ID</param>
        /// <param name="dateType">日期类型，T表示最近一次</param>
        /// <param name="sort">排序名</param>
        /// <param name="order">排序方式</param>
        /// <param name="page">页数，从1开始, 若为-1返回所有的报警记录</param>
        /// <param name="pageSize">页面行数，从1开始</param>
        /// <param name="isStartStopFunc">是否有启停机权限</param>
        /// <returns></returns>
        public BaseResponse<DevAlarmRecordResult> QueryDevAlarmRecord(
            DevAlarmRecordParameter parameter)
        {
            if (ValidateData<DevAlarmRecordParameter>(parameter))
            {
                return statisticsManager.QueryDevAlarmRecord(parameter);
            }
            else
            {
                BaseResponse<DevAlarmRecordResult> result = new BaseResponse<DevAlarmRecordResult>();
                result.IsSuccessful = false;
                result.Code = "000571";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 获取传感器报警记录

        public BaseResponse<WsnAlarmRecordResult> GetWSNAlarmDatas(WsnAlarmDataParameter param)
        {
            if (ValidateData<WsnAlarmDataParameter>(param))
            {
                return statisticsManager.GetWSNAlarmDatas(param);
            }
            else
            {
                string exceptionCode = string.Empty;
                if (param.Type == 1)
                {
                    exceptionCode = "010071";
                }
                else
                {
                    exceptionCode = "000581";
                }
                BaseResponse<WsnAlarmRecordResult> result = new BaseResponse<WsnAlarmRecordResult>();
                result.IsSuccessful = false;
                result.Code = exceptionCode;
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region iCMS 1.6 Maintain by QXM
        public BaseResponse<GetDeviceStatisticSummaryResult> GetDeviceStatisticSummary(GetDeviceStatisticSummaryParam param)
        {
            if (ValidateData<DevRunningStatusDataByMTIDParameter>(param))
            {
                return statisticsManager.GetDeviceStatisticSummary(param);
            }
            else
            {
                BaseResponse<GetDeviceStatisticSummaryResult> result = new BaseResponse<GetDeviceStatisticSummaryResult>();
                result.IsSuccessful = false;
                result.Code = "000000";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }
        #endregion

        #region 获取系统中可用的监测树类型数量
        /// <summary>
        /// 创建人：王龙杰
        /// 创建时间：2017-10-23
        /// 创建记录：获取系统中可用的监测树类型数量
        /// </summary>
        /// <returns></returns>
        public BaseResponse<MonitorTreeTypeCountResult> GetMonitorTreeTypeCount()
        {
            return statisticsManager.GetMonitorTreeTypeCount();
        }

        #endregion

        #region 监测树级联下拉列表查询条件接口
        /// <summary>
        /// 创建人：王龙杰
        /// 创建时间：2017-10-23
        /// 创建记录：监测树级联下拉列表查询条件接口
        /// </summary>
        /// <returns></returns>
        public BaseResponse<MonitorTreeListForSelectResult> FormatMonitorTreeListForSelect(MonitorTreeListForSelectParameter parameter)
        {
            if (ValidateData<MonitorTreeListForSelectParameter>(parameter))
            {
                return statisticsManager.FormatMonitorTreeListForSelect(parameter);
            }
            else
            {
                BaseResponse<MonitorTreeListForSelectResult> result = new BaseResponse<MonitorTreeListForSelectResult>();
                result.IsSuccessful = false;
                result.Code = "008222";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 设备报警提醒

        /// <summary>
        /// 获取未确认报警的被监测设备以及检测设备数量
        /// 创建人：王龙杰
        /// 创建时间：2017-11-1
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<AlertCountResult> GetAlertCountByUserID(AlarmRemindDeviceCountParameter parameter)
        {
            if (ValidateData<AlarmRemindDeviceCountParameter>(parameter))
            {
                return statisticsManager.GetAlertCountByUserID(parameter);
            }
            else
            {
                BaseResponse<AlertCountResult> result = new BaseResponse<AlertCountResult>();
                result.IsSuccessful = false;
                result.Code = "008222";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 通过用户ID获取当前用户所管理的未确认报警设备
        /// 创建人：王龙杰
        /// 创建时间：2017-10-12
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<GetAlertDeviceByUserIDResult> GetAlertDeviceByUserID(AlarmRemindDeviceParameter parameter)
        {
            if (ValidateData<AlarmRemindDeviceParameter>(parameter))
            {
                return statisticsManager.GetAlertDeviceByUserID(parameter);
            }
            else
            {
                BaseResponse<GetAlertDeviceByUserIDResult> result = new BaseResponse<GetAlertDeviceByUserIDResult>();
                result.IsSuccessful = false;
                result.Code = "008222";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 通过用户ID、设备ID获取未确认的设备报警提醒
        /// 创建人：王龙杰
        /// 创建时间：2017-10-12
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<GetDeviceAlertByDeviceIDResult> GetDevAlertByDeviceID(DeviceAlertRecordParameter parameter)
        {
            if (ValidateData<DeviceAlertRecordParameter>(parameter))
            {
                return statisticsManager.GetDevAlertByDeviceID(parameter);
            }
            else
            {
                BaseResponse<GetDeviceAlertByDeviceIDResult> result = new BaseResponse<GetDeviceAlertByDeviceIDResult>();
                result.IsSuccessful = false;
                result.Code = "008222";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 设备报警提醒确认
        /// 创建人：王龙杰
        /// 创建时间：2017-10-12
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<bool> DevAlertConfirm(DeviceAlertConfirmParameter parameter)
        {
            if (ValidateData<DeviceAlertConfirmParameter>(parameter))
            {
                return statisticsManager.DevAlertConfirm(parameter);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "008222";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }
        #endregion

        #region 网关/传感器报警提醒

        /// <summary>
        /// 通过用户ID获取当前用户所管理的未确认报警网关/传感器
        /// 创建人：王龙杰
        /// 创建时间：2017-10-12
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<GetAlertSensorByUserIDResult> GetAlertSensorByUserID(AlarmRemindDeviceParameter parameter)
        {
            if (ValidateData<AlarmRemindDeviceParameter>(parameter))
            {
                return statisticsManager.GetAlertSensorByUserID(parameter);
            }
            else
            {
                BaseResponse<GetAlertSensorByUserIDResult> result = new BaseResponse<GetAlertSensorByUserIDResult>();
                result.IsSuccessful = false;
                result.Code = "008222";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 通过用户ID、设备ID获取未确认的网关/传感器报警提醒
        /// 创建人：王龙杰
        /// 创建时间：2017-10-12
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<GetSensorAlertByIDResult> GetSensorAlertByID(DeviceAlertRecordParameter parameter)
        {
            if (ValidateData<DeviceAlertRecordParameter>(parameter))
            {
                return statisticsManager.GetSensorAlertByID(parameter);
            }
            else
            {
                BaseResponse<GetSensorAlertByIDResult> result = new BaseResponse<GetSensorAlertByIDResult>();
                result.IsSuccessful = false;
                result.Code = "008222";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 网关/传感器报警提醒确认
        /// 创建人：王龙杰
        /// 创建时间：2017-10-12
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<bool> SensorAlertConfirm(DeviceAlertConfirmParameter parameter)
        {
            if (ValidateData<DeviceAlertConfirmParameter>(parameter))
            {
                return statisticsManager.SensorAlertConfirm(parameter);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "008222";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 解决方案新增接口

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：查询用户管理的所有传感器的详细信息
        /// </summary>
        /// <param name="parameter">查询用户管理的所有传感器的详细信息参数</param>
        /// <returns></returns>
        public BaseResponse<GetAllWSInfoByUserIDResult> GetAllWSInfoByUserID(GetAllWSInfoByUserIDParameter parameter)
        {
            if (ValidateData<GetAllWSInfoByUserIDParameter>(parameter))
            {
                return statisticsManager.GetAllWSInfoByUserID(parameter);
            }
            else
            {
                BaseResponse<GetAllWSInfoByUserIDResult> result = new BaseResponse<GetAllWSInfoByUserIDResult>();
                result.IsSuccessful = false;
                result.Code = "000000";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：查询用户管理的所有采集单元的连接状态
        /// </summary>
        /// <param name="parameter">查询用户管理的所有采集单元的连接状态参数</param>
        /// <returns></returns>
        public BaseResponse<GetAllDAUStateByUserIDResult> GetAllDAUStateByUserID(GetAllDAUStateByUserIDParameter parameter)
        {
            if (ValidateData<GetAllDAUStateByUserIDParameter>(parameter))
            {
                return statisticsManager.GetAllDAUStateByUserID(parameter);
            }
            else
            {
                BaseResponse<GetAllDAUStateByUserIDResult> result = new BaseResponse<GetAllDAUStateByUserIDResult>();
                result.IsSuccessful = false;
                result.Code = "000000";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：通过用户id获取关联的传感器类型个数
        /// </summary>
        /// <param name="parameter">通过用户id获取关联的传感器类型个数参数</param>
        /// <returns></returns>
        public BaseResponse<GetAllWSTypeByUserIDResult> GetAllWSTypeByUserID(GetAllWSTypeByUserIDParameter parameter)
        {
            if (ValidateData<GetAllWSTypeByUserIDParameter>(parameter))
            {
                return statisticsManager.GetAllWSTypeByUserID(parameter);
            }
            else
            {
                BaseResponse<GetAllWSTypeByUserIDResult> result = new BaseResponse<GetAllWSTypeByUserIDResult>();
                result.IsSuccessful = false;
                result.Code = "000000";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：通过用户id获取关联的传感器类型个数
        /// </summary>
        /// <param name="parameter">通过用户id获取关联的传感器类型个数参数</param>
        /// <returns></returns>
        public BaseResponse<GetTopThreeDAUInfoByUserIDResult> GetTopThreeDAUInfoByUserID(GetTopThreeDAUInfoByUserIDParameter parameter)
        {
            if (ValidateData<GetTopThreeDAUInfoByUserIDParameter>(parameter))
            {
                return statisticsManager.GetTopThreeDAUInfoByUserID(parameter);
            }
            else
            {
                BaseResponse<GetTopThreeDAUInfoByUserIDResult> result = new BaseResponse<GetTopThreeDAUInfoByUserIDResult>();
                result.IsSuccessful = false;
                result.Code = "000000";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 创建人：王凯
        /// 创建时间：2018-05-09
        /// 创建记录：查看用户管理的有线传感器的未查看的维修日志
        /// </summary>
        /// <param name="parameter">查看用户管理的有线传感器的未查看的维修日志参数</param>
        /// <returns></returns>
        public BaseResponse<GetWSMaintainReportSimpleInfoResult> GetWSMaintainReportSimpleInfo(GetWSMaintainReportSimpleInfoParameter parameter)
        {
            if (ValidateData<GetWSMaintainReportSimpleInfoParameter>(parameter))
            {
                return statisticsManager.GetWSMaintainReportSimpleInfo(parameter);
            }
            else
            {
                BaseResponse<GetWSMaintainReportSimpleInfoResult> result = new BaseResponse<GetWSMaintainReportSimpleInfoResult>();
                result.IsSuccessful = false;
                result.Code = "000000";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion


        #region 获取设备实时数据
        public BaseResponse<GetDeviceRealTimeDataResult> GetDeviceRealTimeData(DeviceRealTimeDataParameter param)
        {
            if (ValidateData<DeviceRealTimeDataParameter>(param))
            {
                return statisticsManager.GetDeviceRealTimeData(param);
            }
            else
            {
                BaseResponse<GetDeviceRealTimeDataResult> result = new BaseResponse<GetDeviceRealTimeDataResult>();
                result.IsSuccessful = false;
                result.Code = "008222";
                Task.Run(() => LogHelper.WriteLog("未通过安全验证：（" + result.Code + "：" + result.Reason + "）"));
                return result;
            }
        }
        #endregion

        #region 通过用户ID、设备ID排序当前用户所管理的设备顺序
        public BaseResponse<bool> OrderDeviceByUserIDAndDevcieID(OrderDeviceByUserIDAndDevcieIDParameter param)
        {
            if (ValidateData<OrderDeviceByUserIDAndDevcieIDParameter>(param))
            {
                return statisticsManager.OrderDeviceByUserIDAndDevcieID(param);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "008222";
                Task.Run(() => LogHelper.WriteLog("未通过安全验证：（" + result.Code + "：" + result.Reason + "）"));
                return result;
            }
        }

        #endregion


        #region 通过用户ID获取当前用户所管理的设备顺序展示
        public BaseResponse<GetOrderDeviceByUserIDResult> GetOrderDeviceByUserID(GetOrderDeviceByUserIDParameter param)
        {
            if (ValidateData<GetOrderDeviceByUserIDParameter>(param))
            {
                return statisticsManager.GetOrderDeviceByUserID(param);
            }
            else
            {
                BaseResponse<GetOrderDeviceByUserIDResult> result = new BaseResponse<GetOrderDeviceByUserIDResult>();
                result.IsSuccessful = false;
                result.Code = "008222";
                Task.Run(() => LogHelper.WriteLog("未通过安全验证：（" + result.Code + "：" + result.Reason + "）"));
                return result;
            }
        }
        #endregion

        #region 获取当前实时数据配置项

        public BaseResponse<GetConfigTreeDevStateResult> GetConfigTreeDevState(BaseRequest param)
        {
            if (ValidateData<BaseRequest>(param))
            {
                return statisticsManager.GetConfigTreeDevState();
            }
            else
            {
                BaseResponse<GetConfigTreeDevStateResult> result = new BaseResponse<GetConfigTreeDevStateResult>();
                result.IsSuccessful = false;
                result.Code = "008222";
                Task.Run(() => LogHelper.WriteLog("未通过安全验证：（" + result.Code + "：" + result.Reason + "）"));
                return result;
            }
        }
        #endregion

        #region 设置当前实时数据配置项

        public BaseResponse<bool> SetConfigDevState(SetConfigDevStateParameter param)
        {
            if (ValidateData<BaseRequest>(param))
            {
                return statisticsManager.SetConfigDevState(param);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "008222";
                Task.Run(() => LogHelper.WriteLog("未通过安全验证：（" + result.Code + "：" + result.Reason + "）"));
                return result;
            }
        }
        #endregion

        #region 设置设备实时数据是否显示停机设备
        public BaseResponse<bool> SetStopedDevIsShow(SetStopedDevIsShowParameter param)
        {
            if (ValidateData<SetStopedDevIsShowParameter>(param))
            {
                return statisticsManager.SetStopedDevIsShow(param);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "008222";
                Task.Run(() => LogHelper.WriteLog("未通过安全验证：（" + result.Code + "：" + result.Reason + "）"));
                return result;
            }
        }
        #endregion

        #region 通过pid查询机组下的所有设备

        public BaseResponse<GetDevInfoByGroupIDResult> GetDevInfoByGroupID(GetDevInfoByGroupIDParameter param)
        {
            if (ValidateData<GetDevInfoByGroupIDParameter>(param))
            {
                return statisticsManager.GetDevInfoByGroupID(param);
            }
            else
            {
                BaseResponse<GetDevInfoByGroupIDResult> result = new BaseResponse<GetDevInfoByGroupIDResult>();
                result.IsSuccessful = false;
                result.Code = "008222";
                Task.Run(() => LogHelper.WriteLog("未通过安全验证：（" + result.Code + "：" + result.Reason + "）"));
                return result;
            }
        }
        #endregion
    }

    #endregion
}