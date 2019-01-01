/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 * 命名空间：iCMS.Service.Statistics
 * 文件名：  IStatisticsManager
 * 创建人：  王颖辉
 * 创建时间：2016-10-21
 * 描述：统计业务处理类
/************************************************************************************/

using iCMS.Common.Component.Data.Request.Statistics;
using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Response.Statistics;

namespace iCMS.Service.Web.Statistics
{
    #region 统计处理类
    /// <summary>
    /// 统计处理类
    /// </summary>
    public interface IStatisticsManager
    {
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
        BaseResponse<DevRunningStatusDataByMTIDResult> GetDevRunningStatusDataByMTID(DevRunningStatusDataByMTIDParameter parameter);

        #endregion

        #region 设备运行状态统计
        /// <summary>
        /// 设备运行状态统计
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        BaseResponse<DevRunningStatusDataByUserIDResult> GetDevRunningStatusDataByUserID(DevRunningStatusDataByUserIDParameter parameter);

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
        BaseResponse<DevHistoryDataResult> GetDevHistoryData(DevHistoryDataParameter parameter);

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
        BaseResponse<DevRunStatusDataResult> QueryDevRunStatusData(DevRunStatusDataParameter parameter);

        #endregion

        #region 获取系统中可用的监测树类型数量

        /// <summary>
        /// 创建人：王龙杰
        /// 创建时间：2017-10-23
        /// 创建记录：获取系统中可用的监测树类型数量
        /// </summary>
        /// <returns></returns>
        BaseResponse<MonitorTreeTypeCountResult> GetMonitorTreeTypeCount();

        #endregion

        #region 监测树级联下拉列表查询条件接口

        /// <summary>
        /// 创建人：王龙杰
        /// 创建时间：2017-10-23
        /// 创建记录：监测树级联下拉列表查询条件接口
        /// </summary>
        /// <returns></returns>
        BaseResponse<MonitorTreeListForSelectResult> FormatMonitorTreeListForSelect(MonitorTreeListForSelectParameter parameter);

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
        BaseResponse<DevAlarmRecordResult> QueryDevAlarmRecord(DevAlarmRecordParameter parameter);

        #endregion

        #region 获取传感器报警记录

        BaseResponse<WsnAlarmRecordResult> GetWSNAlarmDatas(WsnAlarmDataParameter param);

        #endregion

        #region iCMS 1.6 Maintain by QXM

        BaseResponse<GetDeviceStatisticSummaryResult> GetDeviceStatisticSummary(GetDeviceStatisticSummaryParam param);

        #endregion

        #region 设备报警提醒

        /// <summary>
        /// 获取未确认报警的被监测设备以及检测设备数量
        /// 创建人：王龙杰
        /// 创建时间：2017-11-1
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<AlertCountResult> GetAlertCountByUserID(AlarmRemindDeviceCountParameter parameter);

        /// <summary>
        /// 通过用户ID获取当前用户所管理的未确认报警设备
        /// 创建人：王龙杰
        /// 创建时间：2017-10-12
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<GetAlertDeviceByUserIDResult> GetAlertDeviceByUserID(AlarmRemindDeviceParameter parameter);

        /// <summary>
        /// 通过用户ID、设备ID获取未确认的设备报警提醒
        /// 创建人：王龙杰
        /// 创建时间：2017-10-12
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<GetDeviceAlertByDeviceIDResult> GetDevAlertByDeviceID(DeviceAlertRecordParameter parameter);

        /// <summary>
        /// 设备报警提醒确认
        /// 创建人：王龙杰
        /// 创建时间：2017-10-12
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<bool> DevAlertConfirm(DeviceAlertConfirmParameter parameter);

        #endregion

        #region 网关/传感器报警提醒

        /// <summary>
        /// 通过用户ID获取当前用户所管理的未确认报警网关/传感器
        /// 创建人：王龙杰
        /// 创建时间：2017-10-12
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<GetAlertSensorByUserIDResult> GetAlertSensorByUserID(AlarmRemindDeviceParameter parameter);

        /// <summary>
        /// 通过用户ID、设备ID获取未确认的网关/传感器报警提醒
        /// 创建人：王龙杰
        /// 创建时间：2017-10-12
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<GetSensorAlertByIDResult> GetSensorAlertByID(DeviceAlertRecordParameter parameter);

        /// <summary>
        /// 网关/传感器报警提醒确认
        /// 创建人：王龙杰
        /// 创建时间：2017-10-12
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<bool> SensorAlertConfirm(DeviceAlertConfirmParameter parameter);

        #endregion

        #region 解决方案新增接口

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：查询用户管理的所有传感器的详细信息
        /// </summary>
        /// <param name="parameter">查询用户管理的所有传感器的详细信息参数</param>
        /// <returns></returns>
        BaseResponse<GetAllWSInfoByUserIDResult> GetAllWSInfoByUserID(GetAllWSInfoByUserIDParameter parameter);

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：查询用户管理的所有采集单元的连接状态
        /// </summary>
        /// <param name="parameter">查询用户管理的所有采集单元的连接状态参数</param>
        /// <returns></returns>
        BaseResponse<GetAllDAUStateByUserIDResult> GetAllDAUStateByUserID(GetAllDAUStateByUserIDParameter parameter);

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：通过用户id获取关联的传感器类型个数
        /// </summary>
        /// <param name="parameter">通过用户id获取关联的传感器类型个数参数</param>
        /// <returns></returns>
        BaseResponse<GetAllWSTypeByUserIDResult> GetAllWSTypeByUserID(GetAllWSTypeByUserIDParameter parameter);

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：通过用户id获取关联的传感器类型个数
        /// </summary>
        /// <param name="parameter">通过用户id获取关联的传感器类型个数参数</param>
        /// <returns></returns>
        BaseResponse<GetTopThreeDAUInfoByUserIDResult> GetTopThreeDAUInfoByUserID(GetTopThreeDAUInfoByUserIDParameter parameter);

        /// <summary>
        /// 创建人：王凯
        /// 创建时间：2018-05-09
        /// 创建记录：查看用户管理的有线传感器的未查看的维修日志
        /// </summary>
        /// <param name="parameter">查看用户管理的有线传感器的未查看的维修日志参数</param>
        /// <returns></returns>
        BaseResponse<GetWSMaintainReportSimpleInfoResult> GetWSMaintainReportSimpleInfo(GetWSMaintainReportSimpleInfoParameter parameter);

        #endregion

        #region 实时数据相关服务 Added by QXM, 2018/05/22

        /// <summary>
        /// 获取设备实时数据， Added by QXM, 2018/04/24
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        BaseResponse<GetDeviceRealTimeDataResult> GetDeviceRealTimeData(DeviceRealTimeDataParameter param);

        BaseResponse<bool> OrderDeviceByUserIDAndDevcieID(OrderDeviceByUserIDAndDevcieIDParameter param);

        BaseResponse<GetOrderDeviceByUserIDResult> GetOrderDeviceByUserID(GetOrderDeviceByUserIDParameter param);

        BaseResponse<GetConfigTreeDevStateResult> GetConfigTreeDevState();

        BaseResponse<bool> SetConfigDevState(SetConfigDevStateParameter param);

        BaseResponse<bool> SetStopedDevIsShow(SetStopedDevIsShowParameter param);
        BaseResponse<GetDevInfoByGroupIDResult> GetDevInfoByGroupID(GetDevInfoByGroupIDParameter param);
        #endregion
    }
    #endregion
}