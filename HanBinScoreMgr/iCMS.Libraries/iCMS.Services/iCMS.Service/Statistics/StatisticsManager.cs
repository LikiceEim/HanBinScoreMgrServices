/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 * 命名空间：iCMS.Service.Statistics
 * 文件名：  StatisticsManager
 * 创建人：  王颖辉
 * 创建时间：2016-10-19
 * 描述：统计服务
 *
 * 修改人：张辽阔
 * 修改时间：2016-11-14
 * 描述：增加错误编码
/************************************************************************************/

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System;
using System.Text;

using Microsoft.Practices.Unity;

using iCMS.Common.Component.Data.Enum;
using iCMS.Common.Component.Data.Request.Statistics;
using iCMS.Common.Component.Data.Response.Statistics;
using iCMS.Common.Component.Tool;
using iCMS.Frameworks.Core.DB.Models;
using iCMS.Service.Common;
using iCMS.Common.Component.Data.Base;
using iCMS.Frameworks.Core.Repository;
using System.Configuration;

namespace iCMS.Service.Web.Statistics
{
    #region 统计模块

    /// <summary>
    /// 统计模块
    /// </summary>
    public class StatisticsManager : IStatisticsManager
    {
        #region 变量

        private readonly IRepository<MonitorTree> monitorTreeRepository;
        private readonly IRepository<Device> deviceRepository;
        private readonly IRepository<MeasureSite> measureSiteRepository;
        private readonly IRepository<UserRalationDevice> userRalationDeviceRepository;
        private readonly IRepository<UserRelationWS> userRalationWSRepository;
        private readonly IRepository<UserRelationDeviceAlmRecord> userRalationDeviceAlmRecordRepository;
        private readonly IRepository<UserRelationWSAlmRecord> userRalationWSAlmRecordRepository;
        private readonly ICacheDICT cacheDICT;

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-07
        /// 创建记录：设备关联网关数据访问对象
        /// </summary>
        [Dependency]
        public IRepository<DeviceRelationWG> deviceRelationWGRepository { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-07
        /// 创建记录：网关数据访问对象
        /// </summary>
        [Dependency]
        public IRepository<Gateway> gatewayRepository { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：传感器数据访问对象
        /// </summary>
        [Dependency]
        public IRepository<WS> wsRepository { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：测量位置温度告警设置数据访问对象
        /// </summary>
        [Dependency]
        public IRepository<TempeDeviceSetMSiteAlm> tempeDeviceSetMSiteAlmRepository { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：转速测量定义数据访问对象
        /// </summary>
        [Dependency]
        public IRepository<SpeedSamplingMDF> speedSamplingMDFRepository { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：振动信号数据访问对象
        /// </summary>
        [Dependency]
        public IRepository<VibSingal> vibSingalRepository { get; set; }

        #endregion

        #region 构造函数
        public StatisticsManager(IRepository<MonitorTree> monitorTreeRepository,
            IRepository<Device> deviceRepository,
            IRepository<UserRalationDevice> userRalationDeviceRepository,
            IRepository<MeasureSite> measureSiteRepository,
            IRepository<UserRelationWS> userRalationWSRepository,
            IRepository<UserRelationDeviceAlmRecord> userRalationDeviceAlmRecordRepository,
            IRepository<UserRelationWSAlmRecord> userRalationWSAlmRecordRepository,
            ICacheDICT cacheDICT)
        {
            this.monitorTreeRepository = monitorTreeRepository;
            this.deviceRepository = deviceRepository;
            this.userRalationDeviceRepository = userRalationDeviceRepository;
            this.measureSiteRepository = measureSiteRepository;
            this.userRalationWSRepository = userRalationWSRepository;
            this.userRalationDeviceAlmRecordRepository = userRalationDeviceAlmRecordRepository;
            this.userRalationWSAlmRecordRepository = userRalationWSAlmRecordRepository;
            this.cacheDICT = cacheDICT;
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
        /// 修改：查询优化 王龙杰 2017-10-30
        public BaseResponse<DevRunningStatusDataByMTIDResult> GetDevRunningStatusDataByMTID(DevRunningStatusDataByMTIDParameter parameter)
        {
            BaseResponse<DevRunningStatusDataByMTIDResult> response = new BaseResponse<DevRunningStatusDataByMTIDResult>();
            DevRunningStatusDataByMTIDResult result = new DevRunningStatusDataByMTIDResult();

            try
            {
                var isLeaf = IsMonitorTreeLeaf(parameter.MonitorTreeId);
                if (isLeaf && (parameter.MonitorTreeId > 0))    //是叶子节点，则统计此MoitorTree节点的统计数据
                {
                    DevRunningStatCountDataInfoByMTId devRunStatByMTId = new DevRunningStatCountDataInfoByMTId();
                    devRunStatByMTId.MonitorTreeId = parameter.MonitorTreeId;

                    var tempMonitorTree = new iCMSDbContext().MonitorTree.Where(w => w.MonitorTreeID == parameter.MonitorTreeId).
                                        Select(s => new { Name = s.Name, Type = s.Type }).FirstOrDefault();

                    devRunStatByMTId.MonitorTreeName = tempMonitorTree.Name;

                    #region 2016/8/22 Add MonitorTree Type, BY qxm
                    int mtType = tempMonitorTree.Type;
                    devRunStatByMTId.MonitorTreeType = cacheDICT.GetInstance().GetCacheType<MonitorTreeType>(p => p.ID == mtType).SingleOrDefault().Describe;
                    #endregion

                    var devices = deviceRepository.GetDatas<Device>(t => t.MonitorTreeID == parameter.MonitorTreeId, false).ToList();
                    if (devices != null && devices.Count > 0)
                    {
                        devRunStatByMTId.TotalCount = devices.Count;
                        devRunStatByMTId.UseCount = devices.Where(t => t.UseType == 0).ToList().Count;
                        devRunStatByMTId.UnUseCount = devRunStatByMTId.TotalCount - devRunStatByMTId.UseCount;

                        //只有当 使用状态为 可用(UseType == 0) 的时候，AlmStatus 字段才有效
                        //Modified by QXN 2016/8/11
                        // AlmStatus ==0,1 都属于 正常， 2016/09/01 经段确认， QXM
                        devRunStatByMTId.NomalCount = devices.Where(t => t.UseType == 0 && (t.AlmStatus == 1 || t.AlmStatus == 0)).ToList().Count;
                        devRunStatByMTId.WarningCount = devices.Where(t => t.UseType == 0 && t.AlmStatus == 2).ToList().Count;
                        devRunStatByMTId.AlarmCount = devices.Where(t => t.UseType == 0 && t.AlmStatus == 3).ToList().Count;
                    }
                    result.DevRunningStatCountDataInfo.Add(devRunStatByMTId);
                    result.Total = 1;
                    response.Result = result;

                    return response;
                }
                else
                {
                    //不是叶子节点，统计此节点下一层级的统计数据
                    var childNodes = monitorTreeRepository.GetDatas<MonitorTree>(m => m.PID == parameter.MonitorTreeId, false).ToList();

                    List<DevRunningStatCountDataInfoByMTId> tempList = new List<DevRunningStatCountDataInfoByMTId>();
                    foreach (var node in childNodes)
                    {
                        DevRunningStatCountDataInfoByMTId devRunStatByMTId = null;
                        CalculatorDevRunningStat(node.MonitorTreeID, out devRunStatByMTId);
                        tempList.Add(devRunStatByMTId);
                    }

                    var total = 0;
                    ListSortDirection sortDirection = parameter.Order.ToLower().Equals("desc") ? ListSortDirection.Descending : ListSortDirection.Ascending;
                    result.DevRunningStatCountDataInfo.AddRange(tempList.AsQueryable<DevRunningStatCountDataInfoByMTId>()
                        .Where(parameter.Page, parameter.PageSize, out total, new PropertySortCondition(parameter.Sort, sortDirection)));
                    result.Total = total;

                    response.Result = result;
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<DevRunningStatusDataByMTIDResult>("006081");
                return response;
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
            BaseResponse<DevRunningStatusDataByUserIDResult> response = new BaseResponse<DevRunningStatusDataByUserIDResult>();
            DevRunningStatusDataByUserIDResult result = new DevRunningStatusDataByUserIDResult();
            try
            {
                GetDevRunningStatCountDataByUserId runningStat = new GetDevRunningStatCountDataByUserId();
                using (iCMSDbContext dataContext = new iCMSDbContext())
                {
                    //查询优化，王龙杰 2017-10-30
                    var devices = (from urd in dataContext.UserRalationDevice
                                   join d in dataContext.Device on urd.DevId equals d.DevID
                                   where urd.UserID == parameter.UserID
                                   select new
                                   {
                                       DevID = d.DevID,
                                       UseType = d.UseType,
                                       AlmStatus = d.AlmStatus
                                   }).ToList();

                    if (devices != null && devices.Count > 0)
                    {
                        runningStat.TotalCount = devices.Count;
                        runningStat.UseCount = devices.Where(t => t.UseType == 0).ToList().Count;
                        runningStat.UnUseCount = runningStat.TotalCount - runningStat.UseCount;
                        // AlmStatus ==0,1 都属于 正常， 2016/09/01 经段确认， QXM
                        runningStat.NomalCount = devices.Where(t => t.UseType == 0 && (t.AlmStatus == 1 || t.AlmStatus == 0)).ToList().Count;
                        runningStat.WarningCount = devices.Where(t => t.UseType == 0 && t.AlmStatus == 2).ToList().Count;
                        runningStat.AlarmCount = devices.Where(t => t.UseType == 0 && t.AlmStatus == 3).ToList().Count;
                    }

                    result.GetDevRunningStatCountDataByUserId.Add(runningStat);
                    result.Total = 1;
                    response.Result = result;
                    return response;
                }
            }
            catch (System.Exception e)
            {
                LogHelper.WriteLog(e);
                result.GetDevRunningStatCountDataByUserId = new List<GetDevRunningStatCountDataByUserId>();
                result.Total = 0;
                response.Code = "002741";
                response.Result = result;
                return response;
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
        /// <param name="DataType">0表示所有类型，1速度，2加速度，3包络，4位移，5LQ,6设备温度,7转速</param>
        /// <param name="Sort"></param>
        /// <param name="Order"></param>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public BaseResponse<DevHistoryDataResult> GetDevHistoryData(DevHistoryDataParameter parameter)
        {
            //转速排序临时解决方案， 转速排序传递 "SpeedData"
            if (parameter.Sort.Equals("SpeedValue"))
            {
                parameter.Sort = "SpeedData";
            }

            #region 变量定义
            BaseResponse<DevHistoryDataResult> response = new BaseResponse<DevHistoryDataResult>();
            response.IsSuccessful = true;
            response.Code = string.Empty;
            DevHistoryDataResult result = new DevHistoryDataResult();
            //监测树节点查询条件，-1：查询所有，其他：对条件添加 MSiteID IN(...) 语句
            string monitorTreeCondition = "";
            //检测类型查询条件，-1：查询总试图View_DevHistoryData，其他：只查询对应检测类型试图
            string viewName = "";
            //状态查询条件
            string dataStatCondition = "";
            //时间查询条件
            string timeCondition = "";
            //用户查询条件
            string userIDCondition = "";
            List<DevHistoryCollectDataInfo2> historyDatas = null;
            List<int> totalResult = null;
            #endregion

            try
            {
                #region 时间进行处理
                switch (parameter.DateType)
                {
                    case EnumDataType.LastOneDay:
                        {
                            parameter.BDate = DateTime.Now.AddDays(-1);
                            parameter.EDate = DateTime.Now;
                        }
                        break;
                    case EnumDataType.LastOneWeek:
                        {
                            parameter.BDate = DateTime.Now.AddDays(-7);
                            parameter.EDate = DateTime.Now;
                        }
                        break;
                    case EnumDataType.LastOneMonth:
                        {
                            parameter.BDate = DateTime.Now.AddDays(-30);
                            parameter.EDate = DateTime.Now;
                        }
                        break;
                    case EnumDataType.LastOneYear:
                        {
                            parameter.BDate = DateTime.Now.AddDays(-365);
                            parameter.EDate = DateTime.Now;
                        }
                        break;
                }
                #endregion

                using (iCMSDbContext dbContext = new iCMSDbContext())
                {
                    #region 参数初始化
                    //监测树
                    monitorTreeCondition = CalculateMSIDs(parameter.MonitorTreeId, parameter.DevId, parameter.MSId, dbContext);

                    //检测类型
                    switch (parameter.DataType)
                    {
                        case -1: //全部 
                            /* 感觉查询全部的高高报时候这个SQL有问题， 需要与龙杰沟通后进行处理 Marked by QXM, 2018/05/21*/
                            viewName = "View_DevHistoryData";
                            dataStatCondition = parameter.DataStat == -1 ? "" : string.Format(@"and ( TempStat={0} or LQStat={0})", parameter.DataStat);
                            break;
                        case 1: //速度
                            viewName = "View_VELHistoryData";
                            dataStatCondition = parameter.DataStat == -1 ? "" : string.Format(" and SpeedVirtualValueStat={0}", parameter.DataStat);
                            break;
                        case 2:  //加速度
                            viewName = "View_ACCHistoryData";
                            dataStatCondition = parameter.DataStat == -1 ? "" : string.Format(" and ACCPEAKValueStat={0}", parameter.DataStat);
                            break;
                        case 3: //包络
                            viewName = "View_ENVLHistoryData";
                            dataStatCondition = parameter.DataStat == -1 ? "" : string.Format(" and EnvelopPEAKValueStat={0}", parameter.DataStat);
                            break;
                        case 4: //位移
                            viewName = "View_DISPHistoryData";
                            dataStatCondition = parameter.DataStat == -1 ? "" : string.Format(" and DisplacementDPEAKValueStat={0}", parameter.DataStat);
                            break;
                        case 5:  //LQ
                            viewName = "View_LQHistoryData";
                            dataStatCondition = parameter.DataStat == -1 ? "" : string.Format(" and LQStat={0}", parameter.DataStat);
                            break;
                        case 6: //设备温度
                            viewName = "View_DeviceTempHistortyData";
                            dataStatCondition = parameter.DataStat == -1 ? "" : string.Format(" and TempStat={0}", parameter.DataStat);
                            break;
                        //add by lwj---2018.05.03---转速
                        case 7:
                            viewName = "View_SpeedHistoryData";
                            dataStatCondition = parameter.DataStat == -1 ? "" : string.Format(" and SpeedStat={0}", parameter.DataStat);
                            break;
                        default: break;
                    }
                    //过滤用户信息，通过用户Id
                    if (parameter.UserID != -1)
                    {
                        userIDCondition = @" And DevID IN ( SELECT TSURD.DevId FROM dbo.T_SYS_USER_RELATION_DEVICE AS TSURD WHERE TSURD.UserID = {0} ) ";
                        userIDCondition = string.Format(userIDCondition, parameter.UserID);
                    }

                    #endregion

                    if (parameter.DateType == EnumDataType.Last)
                    {
                        #region  查询最近一次的历史数据
                        string sql_total = ConstObject.SQL_TOTAL_LAST;
                        string sql_current = ConstObject.SQL_CURRENT_LAST;
                        dataStatCondition = dataStatCondition + " AND MSiteName IS NOT NULL AND DevName IS NOT NULL";
                        string SQL_TOTAL = string.Format(sql_total, viewName, monitorTreeCondition, dataStatCondition, userIDCondition);
                        string SQL_CURRENT = string.Format(sql_current,
                                                            parameter.Sort,
                                                            parameter.Order,
                                                            viewName,
                                                            monitorTreeCondition,
                                                            dataStatCondition,
                                                            userIDCondition,
                                                            (parameter.Page - 1) * parameter.PageSize + 1,
                                                            parameter.PageSize * parameter.Page);

                        totalResult = dbContext.Database.SqlQuery<int>(SQL_TOTAL).ToList();
                        historyDatas = dbContext.Database.SqlQuery<DevHistoryCollectDataInfo2>(SQL_CURRENT).ToList();
                        #endregion
                    }
                    else
                    {
                        #region
                        timeCondition = string.Format("AND CollectitTime > '{0}' AND CollectitTime < '{1}' AND MSiteName IS NOT NULL AND DevName IS NOT NULL ", parameter.BDate, parameter.EDate);
                        string sql_total = ConstObject.SQL_TOTAL;
                        string sql_current = ConstObject.SQL_CURRENT;

                        string SQL_TOTAL = string.Format(sql_total, viewName, monitorTreeCondition, timeCondition, dataStatCondition, userIDCondition);
                        string SQL_CURRENT = string.Format(sql_current,
                                                            parameter.Sort,
                                                            parameter.Order,
                                                            viewName,
                                                            monitorTreeCondition,
                                                            timeCondition,
                                                            dataStatCondition,
                                                            userIDCondition,
                                                            (parameter.Page - 1) * parameter.PageSize + 1,
                                                            parameter.PageSize * parameter.Page);

                        totalResult = dbContext.Database.SqlQuery<int>(SQL_TOTAL).ToList();
                        historyDatas = dbContext.Database.SqlQuery<DevHistoryCollectDataInfo2>(SQL_CURRENT).ToList();
                        #endregion
                    }

                    #region  设备是否删除  王颖辉 2017-02-20
                    //获取设备信息
                    var deviceIDList = dbContext.Device.GetDatas<Device>(dbContext, item => item.DevID > 0).Select(item => item.DevID);

                    for (int i = 0; i < historyDatas.Count(); i++)
                    {
                        var info = historyDatas[i];
                        if (deviceIDList != null && deviceIDList.Count() > 0)
                        {
                            if (deviceIDList.Contains(info.DevId))
                            {
                                info.IsDeleted = true;
                            }
                            else
                            {
                                info.IsDeleted = false;
                            }
                        }
                    }
                    #endregion
                }
                result = new DevHistoryDataResult { Total = totalResult[0], DevHistoryCollectDataInfo = historyDatas };
                response.Result = result;
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<DevHistoryDataResult>("006091");
                return response;
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
        /// 設備運行狀態查詢，如果UserID非空則查詢的是用戶相關的設備運行狀態
        public BaseResponse<DevRunStatusDataResult> QueryDevRunStatusData(DevRunStatusDataParameter parameter)
        {
            BaseResponse<DevRunStatusDataResult> response = new BaseResponse<DevRunStatusDataResult>();

            DevRunStatusDataResult result = new DevRunStatusDataResult();
            List<DevRunningStatListDataInfo> devRunningDataInfos = new List<DevRunningStatListDataInfo>();
            var total = 0;
            var sortOrder = parameter.Order.Trim().ToLower().Equals("desc") ? ListSortDirection.Descending : ListSortDirection.Ascending;
            try
            {
                if (!parameter.Sort.ValidateStringEmpty())
                {
                    switch (parameter.Sort)
                    {
                        case "LastUpdateTime":
                            parameter.Sort = "DevSDate";
                            break;

                        case "DevRunningStat":
                            parameter.Sort = "RunStatus";
                            break;

                        case "DevAlarmStat":
                            parameter.Sort = "AlmStatus";
                            break;

                        case "DevTypeName":
                            parameter.Sort = "DevType";
                            break;
                    }
                }

                List<Device> devices = null;

                if (parameter.UserID.HasValue)
                {
                    var deviceIds = userRalationDeviceRepository
                        .GetDatas<UserRalationDevice>(t => t.UserID == parameter.UserID.Value, true)
                        .Select(t => t.DevId)
                        .ToList();
                    devices = deviceRepository
                        .GetDatas<Device>(t => deviceIds.Contains(t.DevID), true)
                        .Where(parameter.Page, parameter.PageSize, out total, new PropertySortCondition(parameter.Sort, sortOrder))
                        .ToList();
                }
                else
                {
                    if (parameter.MonitorTreeID.HasValue)
                    {
                        List<int> midList = new List<int>();
                        GetAllChildren(parameter.MonitorTreeID.Value, midList);
                        devices = deviceRepository
                            .GetDatas<Device>(t => midList.Contains(t.MonitorTreeID), true)
                            .ToList();
                    }
                    else
                    {
                        devices = deviceRepository.GetDatas<Device>(p => true, true).ToList();
                    }
                    if (parameter.DevId.HasValue)
                    {
                        devices = devices.Where(t => t.DevID == parameter.DevId.Value).ToList();
                    }
                    if (!string.IsNullOrEmpty(parameter.DevNo))
                    {
                        devices = devices.Where(t => t.DevNO.ToString().Trim().ToLower().Equals(parameter.DevNo.Trim().ToLower())).ToList();
                    }
                    if (parameter.DevRunningStat != -1)
                    {
                        devices = devices.Where(t => t.RunStatus == parameter.DevRunningStat).ToList();
                    }
                    if (parameter.DevAlarmStat != -1)
                    {
                        devices = devices.Where(t => t.AlmStatus == parameter.DevAlarmStat).ToList();
                    }
                    if (parameter.DevType != -1)
                    {
                        devices = devices.Where(t => t.DevType == parameter.DevType).ToList();
                    }
                    if (parameter.UseType != -1)
                    {
                        devices = devices.Where(t => t.UseType == parameter.UseType).ToList();
                    }
                }
                devices = devices.AsQueryable()
                    .Where(parameter.Page, parameter.PageSize, out total, new PropertySortCondition(parameter.Sort, sortOrder))
                    .ToList();

                var linq = from dev in devices
                           join devType in new iCMSDbContext().DeviceType
                           on dev.DevType equals devType.ID into DevGroups
                           from devGroup in DevGroups.DefaultIfEmpty(new DeviceType())
                           select new DevRunningStatListDataInfo
                           {
                               DevId = dev.DevID,
                               DevName = dev.DevName,
                               DevNo = dev.DevNO.ToString(),
                               DevType = dev.DevType,
                               DevTypeName = devGroup.Name,
                               UseType = dev.UseType,
                               DevRunningStat = dev.RunStatus,
                               //张辽阔 2016-09-12 修改
                               DevAlarmStat = dev.AlmStatus, //dev.RunStatus == 1 ? dev.AlmStatus : -1,
                               LastUpdateTime = dev.DevSDate.ToString()
                           };
                devRunningDataInfos.AddRange(linq.ToList());
                result = new DevRunStatusDataResult { DevRunningStatListDataInfo = devRunningDataInfos, Total = total };

                response.Result = result;
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                result = new DevRunStatusDataResult
                {
                    DevRunningStatListDataInfo = new List<DevRunningStatListDataInfo>(),
                    Total = 0
                };
                response.Result = result;
                response.Code = "002751";
                response.IsSuccessful = false;
                return response;
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
            BaseResponse<MonitorTreeTypeCountResult> response = new BaseResponse<MonitorTreeTypeCountResult>();
            MonitorTreeTypeCountResult result = new MonitorTreeTypeCountResult();
            try
            {
                using (var dataContext = new iCMSDbContext())
                {
                    result.MonitorTreeTypeCount = dataContext.MonitorTreeType.Where(w => w.IsUsable == 1).Count();
                    response.IsSuccessful = true;
                    response.Result = result;
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "009341";
                response.Result = null;
                return response;
            }
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
            BaseResponse<MonitorTreeListForSelectResult> response = new BaseResponse<MonitorTreeListForSelectResult>();
            MonitorTreeListForSelectResult result = new MonitorTreeListForSelectResult();
            List<SelectMTList> selectMTList = new List<SelectMTList>();

            if (parameter.UserID == -1)
            {
                parameter.UserID = 1011;
            }
            try
            {
                using (var dataContext = new iCMSDbContext())
                {
                    if (parameter.Type == -1 || parameter.Type == 1 || parameter.Type == 2)
                    {
                        selectMTList = GetMonitorTreeOrDeviceSelectList(parameter, dataContext);
                    }
                    else
                    {
                        switch (parameter.Type)
                        {
                            //测量位置
                            case 3:
                                selectMTList = (from ms in dataContext.Measuresite
                                                join mst in dataContext.MeasureSiteType on ms.MSiteName equals mst.ID
                                                where ms.DevID == parameter.ParentID
                                                select new SelectMTList
                                                {
                                                    ID = ms.MSiteID,
                                                    Name = mst.Name,
                                                }).ToList();
                                break;
                            //振动信号
                            case 4:
                                selectMTList = (from vib in dataContext.VibSingal
                                                join vibt in dataContext.VibratingSingalType on vib.SingalType equals vibt.ID
                                                where vib.MSiteID == parameter.ParentID && vib.DAQStyle == 1
                                                select new SelectMTList
                                                {
                                                    ID = vib.SingalID,//值给的测量位置，修改为振动信号
                                                    Name = vibt.Name,
                                                }).ToList();
                                break;
                            //特征值
                            case 5:
                                selectMTList = (from vibss in dataContext.SignalAlmSet
                                                join vibsst in dataContext.EigenValueType on vibss.ValueType equals vibsst.ID
                                                where vibss.SingalID == parameter.ParentID
                                                select new SelectMTList
                                                {
                                                    ID = vibss.SingalAlmID,
                                                    Name = vibsst.Name,
                                                }).ToList();
                                break;
                            default:
                                break;
                        }
                    }

                    response.IsSuccessful = true;
                    result.SelectMTList = selectMTList;
                    response.Result = result;
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "009351";
                response.Result = null;
                return response;
            }
        }

        private List<SelectMTList> GetMonitorTreeOrDeviceSelectList(MonitorTreeListForSelectParameter parameter, iCMSDbContext dataContext)
        {
            List<SelectMTList> fullMTList = new List<SelectMTList>();
            List<SelectMTList> selectMTList = new List<SelectMTList>();
            IQueryable<SelectMTList> linqDeviceList = null;

            linqDeviceList = (from urd in dataContext.UserRalationDevice
                              join d in dataContext.Device on urd.DevId equals d.DevID
                              where urd.UserID == parameter.UserID
                              select new SelectMTList
                              {
                                  ID = d.DevID,
                                  ParentID = d.MonitorTreeID,
                                  Name = d.DevName
                              });


            if (parameter.Type == 2)
            {
                selectMTList = linqDeviceList.Where(w => w.ParentID == parameter.ParentID).Distinct().ToList();
            }
            else
            {
                List<MonitorTreeType> mtType = dataContext.MonitorTreeType.Where(w => w.IsUsable == 1).
                        OrderByDescending(o => o.Describe).ToList();
                if (mtType.Count > 0)
                {
                    var firstMtType = mtType.First().ID;
                    var devIDList = linqDeviceList.Select(s => s.ID).Distinct().ToList();
                    List<SelectMTList> leafMTList =
                        (from mt in dataContext.MonitorTree
                         join d in dataContext.Device on mt.MonitorTreeID equals d.MonitorTreeID
                         where mt.Type == firstMtType &&
                         devIDList.Contains(d.DevID)
                         select new SelectMTList
                         {
                             ID = mt.MonitorTreeID,
                             ParentID = mt.PID,
                             Type = mt.Type,
                             Name = mt.Name
                         }).Distinct().ToList();
                    fullMTList.AddRange(leafMTList);
                    mtType.RemoveAt(0);
                    foreach (var item in mtType)
                    {
                        var tempMTParentID = leafMTList.Select(s => s.ParentID).ToList();
                        List<SelectMTList> tempMTList =
                                (from mt in dataContext.MonitorTree
                                 where mt.Type == item.ID && tempMTParentID.Contains(mt.MonitorTreeID)
                                 select new SelectMTList
                                 {
                                     ID = mt.MonitorTreeID,
                                     ParentID = mt.PID,
                                     Type = mt.Type,
                                     Name = mt.Name
                                 }).Distinct().ToList();
                        fullMTList.AddRange(tempMTList);
                        leafMTList.Clear();
                        leafMTList.AddRange(tempMTList);
                    }
                }
                if (parameter.Type == -1)
                {
                    selectMTList = fullMTList.Where(w => w.Type == mtType.Last().ID).ToList();
                }
                else if (parameter.Type == 1)
                {
                    selectMTList = fullMTList.Where(w => w.ParentID == parameter.ParentID).ToList();
                }
            }

            return selectMTList;
        }

        #endregion

        #region 获取设备报警记录

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-08-03
        /// 创建记录：获取设备报警记录
        /// 
        /// 修改人：张辽阔
        /// 修改时间：2016-11-10
        /// 修改记录：修改结束时间排序逻辑
        ///           判断是否有启停机权限和趋势报警是否开启
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
        public BaseResponse<DevAlarmRecordResult> QueryDevAlarmRecord(DevAlarmRecordParameter parameter)
        {
            BaseResponse<DevAlarmRecordResult> response = new BaseResponse<DevAlarmRecordResult>();
            response.IsSuccessful = true;
            response.Code = string.Empty;
            StringBuilder whereCondition = new StringBuilder();
            string sort = string.IsNullOrEmpty(parameter.Sort) ? "LatestStartTime" : parameter.Sort;
            string order = parameter.Order.ToLower().Equals("asc") ? "asc" : "desc";
            if (parameter.UserID == -1)
            {
                parameter.UserID = 1011;
            }

            try
            {
                if (parameter.Page == 0)
                {
                    parameter.Page = 1;
                }
                if (!parameter.IsStartStopFunc.HasValue)
                {
                    parameter.IsStartStopFunc = false;
                }
                using (var dataContext = new iCMSDbContext())
                {
                    int count = 0;
                    //查询总数视图，当需要根据ViewStatus字段查询时，在查询语句中添加ViewStatus，否则不添加ViewStatus
                    string strViewCount = string.Empty;
                    //查询结果集视图
                    string strViewQuery = string.Format(ConstObject.SQL_View_QueryDevAlarmRecord,
                        parameter.UserID);
                    string sql_Total = "";
                    string sql_Query = "";

                    #region 根据UeerID过滤管理设备权限 王龙杰 2017-10-16
                    List<int> userRelationDeviceID = new List<int>();
                    if (parameter.UserID != -1011)
                    {
                        userRelationDeviceID = dataContext.UserRalationDevice
                            .Where(w => w.UserID == parameter.UserID)
                            .Select(s => s.DevId.Value)
                            .Distinct()
                            .ToList();
                        //用户无管理设备权限，返回空集合
                        if (userRelationDeviceID.Count == 0)
                        {
                            return response;
                        }
                    }

                    #endregion

                    List<DevAlarmRecordInfo> devAlmRecordInfoList = null;

                    #region 查询参数

                    #region 动态关联参数：依次判断 特征值—振动信号—测点—设备—监测树(关联设备ID)
                    //特征值
                    if (parameter.SignalAlmID.HasValue && parameter.SignalAlmID.Value > 0)
                    {
                        int SingalAlmID = parameter.SignalAlmID.Value;
                        whereCondition.Append(" AND a.SingalAlmID = " + SingalAlmID + "");
                    }
                    else
                    {
                        //振动信号
                        if (parameter.SignalID.HasValue && parameter.SignalID.Value > 0)
                        {
                            int SingalID = parameter.SignalID.Value;
                            whereCondition.Append(" AND a.SingalID = " + SingalID + "");
                        }
                        else
                        {
                            //测点
                            if (parameter.MSiteID.HasValue && parameter.MSiteID.Value > 0)
                            {
                                int MSiteID = Convert.ToInt32(parameter.MSiteID);
                                whereCondition.Append(" AND a.MSiteID = " + MSiteID + "");
                            }
                            else
                            {
                                //设备
                                if (parameter.DevID.HasValue && userRelationDeviceID.Contains(parameter.DevID.Value))
                                {
                                    int DevID = Convert.ToInt32(parameter.DevID);
                                    whereCondition.Append(" AND a.DevID = " + DevID + "");
                                }
                                else
                                {
                                    //监测树
                                    if (parameter.MonitorTreeID.HasValue && parameter.MonitorTreeID.Value > 0)
                                    {
                                        int MonitorTreeID = parameter.MonitorTreeID.Value;
                                        List<int> childNodesID = new List<int>();
                                        GetAllChildren(MonitorTreeID, childNodesID);
                                        List<int> conditionDeciceID = dataContext.Device.Where(w => childNodesID.Contains(w.MonitorTreeID))
                                                .Select(s => s.DevID).ToList();
                                        if (conditionDeciceID.Count > 0)
                                        {
                                            whereCondition.Append(" AND a.DevID in (" + string.Join(",", conditionDeciceID) + ")");
                                        }
                                    }
                                    else
                                    {
                                        whereCondition.Append(" AND a.DevID in (" + string.Join(",", userRelationDeviceID) + ")");
                                    }
                                }
                            }
                        }
                    }

                    #endregion

                    #region 固定参数：报警类型、报警级别、检索时间、是否已读
                    //查询是否开启趋势报警
                    bool isTrendAlarmUsed =
                        (from p in dataContext.Config
                         where p.Name == "趋势报警开关"
                             && p.IsUsed == 1
                         //&& p.Value == "1" //趋势报报警使用IsUsed 王颖辉修改 张阳提出 2017-03-07
                         select p.Value)
                        .Any();

                    //报警类型
                    if (parameter.MSAlmID.HasValue && parameter.MSAlmID.Value > -1)
                    {
                        int MSAlmID = parameter.MSAlmID.Value;
                        whereCondition.Append(" AND a.MSAlmID = " + MSAlmID + "");
                    }
                    //没有启停机权限或者未开启趋势报警
                    else if ((parameter.IsStartStopFunc.HasValue && !parameter.IsStartStopFunc.Value) || !isTrendAlarmUsed)
                    {
                        int[] alarmRecordTypeArray = new int[0];
                        if (parameter.IsStartStopFunc.HasValue && !parameter.IsStartStopFunc.Value
                            && !isTrendAlarmUsed)
                        {
                            alarmRecordTypeArray = new int[]
                            {
                                (int)EnumAlarmRecordType.DeviceVibration,
                                (int)EnumAlarmRecordType.DeviceTemperature,
                            };
                        }
                        else if (parameter.IsStartStopFunc.HasValue && !parameter.IsStartStopFunc.Value)
                        {
                            alarmRecordTypeArray = new int[]
                            {
                                (int)EnumAlarmRecordType.DeviceVibration,
                                (int)EnumAlarmRecordType.DeviceTemperature,
                                (int)EnumAlarmRecordType.TrendAlarm,
                            };
                        }
                        else if (!isTrendAlarmUsed)
                        {
                            alarmRecordTypeArray = new int[]
                            {
                                (int)EnumAlarmRecordType.DeviceVibration,
                                (int)EnumAlarmRecordType.DeviceTemperature,
                                (int)EnumAlarmRecordType.DeviceShutDown,
                            };
                        }
                        whereCondition.Append(" AND a.MSAlmID in (" + string.Join(",", alarmRecordTypeArray) + ")");
                    }
                    //报警级别
                    if (parameter.AlmStatus.HasValue && parameter.AlmStatus.Value > -1)
                    {
                        int AlmStatus = parameter.AlmStatus.Value;
                        whereCondition.Append(" AND a.AlmStatus = " + AlmStatus + "");
                    }
                    //检索时间
                    if (!parameter.DateType.ValidateStringEmpty()
                        && !parameter.DateType.ToUpper().Equals("T")
                        && parameter.BDate.HasValue
                        && parameter.EDate.HasValue)
                    {

                        whereCondition.Append(" AND ((a.BDate>='" + parameter.BDate + "' AND a.BDate<='" + parameter.EDate + "')" +
                                              " OR (a.EDate>='" + parameter.BDate + "' AND a.EDate<='" + parameter.EDate + "')" +
                                              " OR (a.LatestStartTime>='" + parameter.BDate + "' AND a.LatestStartTime<='" + parameter.EDate + "'))");
                    }
                    //是否已读
                    if (parameter.ViewStatus != -1)
                    {
                        strViewCount = string.Format(ConstObject.SQL_View_CountDevAlarmRecord,
                                                     ConstObject.SQL_ViewStatusCondition);
                        int ViewStatus = parameter.ViewStatus;
                        whereCondition.Append(" AND a.ViewStatus = " + ViewStatus + "");
                    }
                    else
                    {
                        strViewCount = string.Format(ConstObject.SQL_View_CountDevAlarmRecord, "");
                    }

                    #endregion

                    #endregion

                    //分页
                    if (parameter.Page > -1)
                    {
                        //查询最近一次的数据
                        if (!parameter.DateType.ValidateStringEmpty() && parameter.DateType.ToUpper().Equals("T"))
                        {
                            sql_Total = string.Format(ConstObject.SQL_TOTAL_DEVALMRECORD_LAST,
                                strViewCount,
                                whereCondition);
                            sql_Query = string.Format(ConstObject.SQL_DEVALMRECORD_LAST_PAGE,
                                strViewQuery,
                                sort,
                                order,
                                whereCondition,
                                (parameter.Page - 1) * parameter.PageSize + 1,
                                parameter.PageSize * parameter.Page);
                        }
                        else
                        {
                            sql_Total = string.Format(ConstObject.SQL_TOTAL_DEVALMRECORD,
                                strViewCount,
                                whereCondition);
                            sql_Query = string.Format(ConstObject.SQL_DEVALMRECORD_PAGE,
                                strViewQuery,
                                sort,
                                order,
                                whereCondition,
                                (parameter.Page - 1) * parameter.PageSize + 1,
                                parameter.PageSize * parameter.Page);
                        }
                    }
                    else
                    {
                        //查询最近一次的数据
                        if (!parameter.DateType.ValidateStringEmpty() && parameter.DateType.ToUpper().Equals("T"))
                        {
                            sql_Total = string.Format(ConstObject.SQL_TOTAL_DEVALMRECORD_LAST,
                                strViewCount,
                                whereCondition);
                            sql_Query = string.Format(ConstObject.SQL_DEVALMRECORD_LAST,
                                strViewQuery,
                                whereCondition);
                        }
                        else
                        {
                            sql_Total = string.Format(ConstObject.SQL_TOTAL_DEVALMRECORD,
                                strViewCount,
                                whereCondition);
                            sql_Query = string.Format(ConstObject.SQL_DEVALMRECORD,
                                strViewQuery,
                                whereCondition);
                        }
                    }

                    count = dataContext.Database.SqlQuery<int>(sql_Total).FirstOrDefault();
                    devAlmRecordInfoList = dataContext.Database.SqlQuery<DevAlarmRecordInfo>(sql_Query).ToList();
                    DevAlarmRecordResult result = new DevAlarmRecordResult
                    {
                        Total = count,
                        AlarmRecordInfo = devAlmRecordInfoList,
                    };
                    response.Result = result;
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response.Result = null;
                response.Code = "002761";
                response.IsSuccessful = false;
                return response;
            }
        }

        #region 设备报警记录排序实体

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-10
        /// 创建记录：设备报警记录排序实体
        /// </summary>
        public class _DevAlarmRecordOrderInfo
        {
            /// <summary>
            /// 1：表示报警未结束，2：表示报警已结束
            /// </summary>
            public int IsEnd { get; set; }

            /// <summary>
            /// 报警记录ID
            /// </summary>
            public int AlmRecordID { get; set; }

            /// <summary>
            /// 设备ID
            /// </summary>
            public int DevID { get; set; }

            /// <summary>
            /// 设备名称
            /// </summary>
            public string DevName { get; set; }

            /// <summary>
            /// 设备编号
            /// </summary>
            public string DevNo { get; set; }

            /// <summary>
            /// 测量位置ID
            /// </summary>
            public int MSiteID { get; set; }

            /// <summary>
            /// 测量位置名称
            /// </summary>
            public string MSiteName { get; set; }

            /// <summary>
            /// 振动信号ID
            /// </summary>
            public int SingalID { get; set; }

            /// <summary>
            /// 振动信号名称
            /// </summary>
            public string SingalName { get; set; }

            /// <summary>
            /// 特征值ID
            /// </summary>
            public int SingalAlmID { get; set; }

            /// <summary>
            /// 特征值
            /// </summary>
            public string SingalValue { get; set; }

            /// <summary>
            /// 监测树ID
            /// </summary>
            public string MonitorTreeID { get; set; }

            /// <summary>
            /// 报警类型
            /// </summary>
            public int MSAlmID { get; set; }

            /// <summary>
            /// 报警状态
            /// </summary>
            public int AlmStatus { get; set; }

            /// <summary>
            /// 采集数据
            /// </summary>
            public float? SamplingValue { get; set; }

            /// <summary>
            /// 高报阈值
            /// </summary>
            public float? WarningValue { get; set; }

            /// <summary>
            /// 高高报阈值
            /// </summary>
            public float? DangerValue { get; set; }

            /// <summary>
            /// 开始时间
            /// </summary>
            public DateTime BDate { get; set; }

            /// <summary>
            /// 结束时间
            /// </summary>
            public DateTime EDate { get; set; }

            /// <summary>
            /// 添加时间
            /// </summary>
            public DateTime AddDate { get; set; }

            /// <summary>
            /// 最近发生时间
            /// </summary>
            public DateTime LatestStartTime { get; set; }

            /// <summary>
            /// 报警内容
            /// </summary>
            public string Content { get; set; }
        }

        #endregion

        #endregion

        #region 获取传感器报警记录

        /// <summary>
        /// 修改人：张辽阔
        /// 修改时间：2016-11-10
        /// 修改记录：修改结束时间排序逻辑
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public BaseResponse<WsnAlarmRecordResult> GetWSNAlarmDatas(WsnAlarmDataParameter param)
        {
            BaseResponse<WsnAlarmRecordResult> response = new BaseResponse<WsnAlarmRecordResult>();
            response.IsSuccessful = true;
            response.Code = string.Empty;
            string exceptionCode = string.Empty;
            WsnAlarmRecordResult result = new WsnAlarmRecordResult();
            if (param.UserID == -1)
            {
                param.UserID = 1011;
            }
            try
            {
                if (param.Page == 0)
                {
                    param.Page = 1;
                }
                using (var dataContext = new iCMSDbContext())
                {
                    int count = 0;
                    List<AlarmRecordInfo> wsnAlmRecordInfoList = null;
                    //var wsnAlmRecordInfoQueryable =
                    //    from p in dataContext.WsnAlmrecord
                    //    join dev in dataContext.Device on p.DevID equals dev.DevID into p_device
                    //    from dev in p_device.DefaultIfEmpty()
                    //    select new AlarmRecordInfo
                    //    {
                    //        IsEnd =1,
                    //        AlmRecordID = p.AlmRecordID,
                    //        DevID = p.DevID,
                    //        DevName = dev.DevName,
                    //        DevNo = dev.DevNO,
                    //        MSiteID = p.MSiteID,
                    //        MSiteName = (from ms in dataContext.Measuresite
                    //                     join mst in dataContext.MeasureSiteType
                    //                     on ms.MSiteName equals mst.ID
                    //                     where ms.MSiteID == p.MSiteID
                    //                     select mst.Name).FirstOrDefault(),
                    //        WGID = p.WGID,
                    //        WGName = (from wg in dataContext.WG
                    //                  where wg.WGID == p.WGID
                    //                  select wg.WGName).FirstOrDefault(),
                    //        WSID = p.WSID,
                    //        WSName = (from ws in dataContext.WS
                    //                  where ws.WSID == p.WSID
                    //                  select ws.WSName).FirstOrDefault(),
                    //        MonitorTreeID = p.MonitorTreeID,
                    //        MSAlmID = p.MSAlmID,
                    //        AlmStatus = p.AlmStatus,
                    //        SamplingValue = p.SamplingValue,
                    //        WarningValue = p.WarningValue,
                    //        DangerValue = p.DangerValue,
                    //        BDate = p.BDate,
                    //        EDate = p.EDate,
                    //        AddDate = p.AddDate,
                    //        LatestStartTime = p.LatestStartTime,
                    //        Content = p.Content,
                    //        ViewStatus = (from urWSar in dataContext.UserRelationWSAlmRecord
                    //                      where urWSar.UserID == param.UserID && urWSar.MonitorDeviceType == param.Type && urWSar.WSNAlmRecordID == p.AlmRecordID
                    //                      select urWSar).Any()
                    //    };

                    string sql = @"SELECT CASE TSWA.BDate
                                   WHEN TSWA.EDate THEN
                                       1
                                   ELSE
                                       2
                               END IsEnd,
                               AlmRecordID,
                               TSD.DevID,
                               TSD.DevName,
                               TSD.DevNO,
                               TSWA.MSiteID,
                               (
                                   SELECT TDMST.Name
                                   FROM dbo.T_DICT_MEASURE_SITE_TYPE AS TDMST
                                   WHERE TDMST.ID = TSWA.MSiteID
                               ) MSiteName,
                               TSWA.WGID,
                               (
                                   SELECT TSW.WGName FROM dbo.T_SYS_WG AS TSW WHERE TSW.WGID = TSWA.WGID
                               ) WGName,
                               TSWA.WSID,
                               TSWA.WSName,
                               TSWA.MonitorTreeID,
                               TSWA.MSAlmID,
                               TSWA.AlmStatus,
                               TSWA.SamplingValue,
                               TSWA.WarningValue,
                               TSWA.DangerValue,
                               TSWA.BDate,
                               TSWA.EDate,
                               TSWA.AddDate,
                               TSWA.LatestStartTime,
                               TSWA.Content,
                               cast((
                                   SELECT CASE COUNT(1)
                                              WHEN 0 THEN
                                                  0
                                              ELSE
                                                  1
                                          END
                                   FROM dbo.T_SYS_USER_RELATION_WS_ALMRECORD AS TSURWA
		                           WHERE TSURWA.MonitorDeviceType={0} AND TSURWA.WSNAlmRecordID=TSWA.AlmRecordID AND TSURWA.UserID={1}
                               ) as BIT) ViewStatus,
                              F.MonitorTreeRoute
                        FROM dbo.T_SYS_WSN_ALMRECORD AS TSWA
                        LEFT JOIN dbo.T_SYS_DEVICE AS TSD ON TSWA.DevID = TSD.DevID
                        CROSS APPLY F_GetMonitorTreeRouteByMonitorID(TSD.MonitorTreeID) F";


                    #region 处理网关还是传感器最后一次
                    if (param.DateType == EnumDataType.Last)
                    {
                        //网关,6
                        if (param.Type == 1)
                        {
                            sql += @"  INNER JOIN
                            (
                                SELECT alarm.WGID,
                                       MAX(alarm.LatestStartTime) LatestStartTime
                                FROM T_SYS_WSN_ALMRECORD alarm
		                        WHERE alarm.MSAlmID =6
                                GROUP BY alarm.WGID
                            ) b
                                ON b.WGID = TSWA.WGID
                                   AND b.LatestStartTime = TSWA.LatestStartTime";
                        }
                        else
                        {
                            //传感器,3,,4,5
                            sql += @" INNER JOIN
                                    (
                                        SELECT alarm.WSID,
                                               alarm.MSAlmID,
                                               MAX(alarm.LatestStartTime) LatestStartTime
                                        FROM T_SYS_WSN_ALMRECORD alarm
                                        WHERE alarm.MSAlmID IN ( 3, 4, 5 )
                                        GROUP BY alarm.WSID,
                                                 alarm.MSAlmID
                                    ) b
                                        ON b.WSID = TSWA.WSID
                                           AND b.LatestStartTime = TSWA.LatestStartTime";
                        }
                    }
                    #endregion

                    sql += " where 1=1 ";

                    sql = string.Format(sql, param.Type, param.UserID);
                    string querySql = string.Empty;
                    #region 查询参数

                    // var query = dataContext.Database.SqlQuery<AlarmRecordInfo>(sql);

                    #region 根据UeerID过滤管理网关/传感器权限 王龙杰 2017-10-16
                    List<int> userRelationWSID = new List<int>();
                    List<int> userRelationWGID = new List<int>();

                    userRelationWSID = dataContext.UserRelationWS.Where(w => w.UserID == param.UserID).
                                                                  Select(s => s.WSID).Distinct().ToList();
                    //用户无管理设备权限，返回空集合
                    if (userRelationWSID.Count == 0)
                    {
                        return response;
                    }

                    #endregion

                    List<int> msAlmID = new List<int>();
                    switch (param.Type)
                    {
                        case 1: //网关
                            exceptionCode = "010081";
                            msAlmID.AddRange(new int[] { (int)EnumAlarmRecordType.WGLinked });
                            userRelationWGID.AddRange(dataContext.WS.Where(w => userRelationWSID.Contains(w.WSID)).
                                                                     Select(s => s.WGID).Distinct().ToList());

                            string wgIDList = string.Empty;
                            //查找到应的网关
                            if (userRelationWGID.Count > 0)
                            {

                                foreach (var id in userRelationWGID)
                                {
                                    wgIDList += id + ",";
                                }
                                wgIDList = wgIDList.TrimEnd(',');

                            }
                            else
                            {
                                wgIDList = "0";
                            }

                            sql = sql + " and TSWA.WGID IN (" + wgIDList + ")";

                            break;
                        case 2://传感器
                            exceptionCode = "002771";
                            msAlmID.AddRange(new int[] { (int)EnumAlarmRecordType.WSTemperature,
                                                         (int)EnumAlarmRecordType.WSVoltage,
                                                         (int)EnumAlarmRecordType.WSLinked });

                            string wsIDList = string.Empty;
                            //查找到应的网关
                            if (userRelationWSID.Count > 0)
                            {

                                foreach (var id in userRelationWSID)
                                {
                                    wsIDList += id + ",";
                                }
                                wsIDList = wsIDList.TrimEnd(',');

                            }
                            else
                            {
                                wsIDList = "0";
                            }

                            sql = sql + " and TSWA.WSID IN (" + wsIDList + ")";

                            break;
                        default:
                            break;
                    }

                    #region 报警类别
                    string msalarmIDList = string.Empty;
                    foreach (var id in msAlmID)
                    {
                        msalarmIDList += id + ",";
                    }

                    msalarmIDList = msalarmIDList.TrimEnd(',');
                    sql = sql + " and TSWA.MSAlmID IN (" + msalarmIDList + ")";
                    #endregion



                    //不为最近一次，通过时间进行查询数据  王颖辉 2018-01-17
                    if (param.DateType != EnumDataType.Last)
                    {
                        switch (param.DateType)
                        {
                            case EnumDataType.LastOneDay:
                                {
                                    param.BDate = DateTime.Now.AddDays(-1);
                                    param.EDate = DateTime.Now;
                                }
                                break;
                            case EnumDataType.LastOneWeek:
                                {
                                    param.BDate = DateTime.Now.AddDays(-7);
                                    param.EDate = DateTime.Now;
                                }
                                break;
                            case EnumDataType.LastOneMonth:
                                {
                                    param.BDate = DateTime.Now.AddDays(-30);
                                    param.EDate = DateTime.Now;
                                }
                                break;
                            case EnumDataType.LastOneYear:
                                {
                                    param.BDate = DateTime.Now.AddDays(-365);
                                    param.EDate = DateTime.Now;
                                }
                                break;
                        }

                        string sqlDate = @" and ((
                                                      TSWA.BDate >= '{0}'
                                                      AND TSWA.BDate <= '{1}'
                                                  )
                                                  OR(
                                                         TSWA.LatestStartTime >= '{0}'
                                                         AND TSWA.LatestStartTime <= '{1}'
                                                     ))";
                        sqlDate = string.Format(sqlDate, param.BDate, param.EDate);

                        sql = sql + sqlDate;
                    }

                    //如果为零的话，则为全部
                    if (param.AlmStatus.HasValue && param.AlmStatus > -1)
                    {
                        sql = sql + " and TSWA.AlmStatus =" + param.AlmStatus.Value + "";
                    }

                    if (param.MSAlmID.HasValue && param.MSAlmID > -1)
                    {
                        int MSAlmID = param.MSAlmID.Value;

                        sql = sql + " and TSWA.MSAlmID =" + param.MSAlmID.Value + "";

                        if (MSAlmID == (int)EnumAlarmRecordType.WSLinked
                            || MSAlmID == (int)EnumAlarmRecordType.WSTemperature
                            || MSAlmID == (int)EnumAlarmRecordType.WSVoltage)
                        {
                            if (param.DevID.HasValue && param.DevID > 0)
                            {
                                sql = sql + " and TSWA.DevID =" + param.DevID + "";
                            }
                            if (param.MSiteID.HasValue && param.MSiteID > 0)
                            {
                                sql = sql + " and TSWA.MSiteID =" + param.MSiteID + "";
                            }
                            if (param.WGID.HasValue && param.WGID > 0)
                            {
                                sql = sql + " and TSWA.WGID =" + param.WGID + "";
                            }
                            if (param.WSID.HasValue && param.WSID > 0)
                            {
                                sql = sql + " and TSWA.WSID =" + param.WSID + "";

                            }
                            if (param.MonitorTreeID.HasValue && param.MonitorTreeID > 0)
                            {
                                //稍后修改
                                ///   sql = sql + " and TSWA.WSID =" + param.WSID + "";
                                //wsnAlmRecordInfoQueryable = wsnAlmRecordInfoQueryable.ToArray()
                                //    .Where(p => p.MonitorTreeID.Split('#').Contains(param.MonitorTreeID.Value.ToString()))
                                //    .AsQueryable();
                            }
                        }
                        else if (MSAlmID == (int)EnumAlarmRecordType.WGLinked)
                        {
                            if (param.WGID.HasValue && param.WGID > 0)
                            {

                                sql = sql + " and TSWA.WGID =" + param.WGID + "";
                            }
                            if (param.MonitorTreeID.HasValue && param.MonitorTreeID > 0)
                            {
                                //稍后修改
                                //wsnAlmRecordInfoQueryable = wsnAlmRecordInfoQueryable.ToArray()
                                //    .Where(p => p.MonitorTreeID.Split('#').Contains(param.MonitorTreeID.Value.ToString()))
                                //    .AsQueryable();
                            }
                        }
                    }
                    else
                    {
                        if (param.WGID.HasValue && param.WGID > 0)
                        {
                            sql = sql + " and TSWA.WGID =" + param.WGID + "";
                            //  wsnAlmRecordInfoQueryable = wsnAlmRecordInfoQueryable.Where(p => p.WGID == param.WGID);
                        }
                        if (param.WSID.HasValue && param.WSID > 0)
                        {
                            sql = sql + " and TSWA.WSID =" + param.WSID + "";
                            // wsnAlmRecordInfoQueryable = wsnAlmRecordInfoQueryable.Where(p => p.WSID == param.WSID);
                        }
                    }
                    //是否已读，UserID != -1时不处理
                    if (param.ViewStatus != -1)
                    {

                        string viewStatus = @" and  (
                                           SELECT CASE COUNT(1)
                                                      WHEN 0 THEN
                                                          0
                                                      ELSE
                                                          1
                                                  END
                                           FROM dbo.T_SYS_USER_RELATION_WS_ALMRECORD AS TSURWA
		                                   WHERE TSURWA.MonitorDeviceType={0} AND TSURWA.WSNAlmRecordID=TSWA.AlmRecordID AND TSURWA.UserID={1}
                                       )";

                        viewStatus = string.Format(viewStatus, param.Type, param.UserID);

                        if (param.ViewStatus == 0)
                        {
                            viewStatus += "=0 ";
                        }
                        else
                        {
                            viewStatus += ">0 ";
                        }

                        sql = sql + viewStatus;
                    }

                    #endregion

                    #region 分页操作
                    //进行分页
                    if (param.Page > -1)
                    {
                        //分页
                        querySql = @"SELECT *
                                            FROM
                                            (
                                                SELECT ROW_NUMBER() OVER (ORDER BY {0} {1}) AS RowNumber,
                                                       a.*
                                                FROM
                                                (
                                                   {2}
                                                ) a
                                            ) t
                                            WHERE RowNumber
                                            BETWEEN {3} AND {4}";

                        int startIndex = (param.Page - 1) * param.PageSize + 1;
                        int endIndex = param.Page * param.PageSize;
                        querySql = string.Format(querySql, param.Sort, param.Order, sql, startIndex, endIndex);

                    }
                    #endregion

                    wsnAlmRecordInfoList = dataContext.Database.SqlQuery<AlarmRecordInfo>(querySql).ToList();
                    string sqlCount = "SELECT count(1) FROM ({0}) temptable";
                    sqlCount = string.Format(sqlCount, sql);
                    var countList = dataContext.Database.SqlQuery<int>(sqlCount).ToList();
                    if (countList != null && countList.Any())
                    {
                        count = countList[0];
                    }
                    result.AlarmRecordInfo = wsnAlmRecordInfoList;
                    result.Total = count;

                    response.Result = result;
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<WsnAlarmRecordResult>(exceptionCode);
                result.AlarmRecordInfo = new List<AlarmRecordInfo>();
                result.Total = 0;
                response.Result = result;
                return response;
            }
        }

        #region 传感器报警记录排序实体

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-10
        /// 创建记录：传感器报警记录排序实体
        /// </summary>
        public class _AlarmRecordOrderInfo
        {
            /// <summary>
            /// 1：表示报警未结束，2：表示报警已结束
            /// </summary>
            public int IsEnd { get; set; }

            /// <summary>
            /// 报警记录ID
            /// </summary>
            public int AlmRecordID { get; set; }

            /// <summary>
            /// 设备ID
            /// </summary>
            public int DevID { get; set; }

            /// <summary>
            /// 设备名称
            /// </summary>
            public string DevName { get; set; }

            /// <summary>
            /// 设备编号
            /// </summary>
            public string DevNo { get; set; }

            /// <summary>
            /// 测量位置ID
            /// </summary>
            public int MSiteID { get; set; }

            /// <summary>
            /// 测量位置名称
            /// </summary>
            public string MSiteName { get; set; }

            /// <summary>
            /// 网关ID
            /// </summary>
            public int WGID { get; set; }

            /// <summary>
            /// 网关名称
            /// </summary>
            public string WGName { get; set; }

            /// <summary>
            /// 传感器ID
            /// </summary>
            public int WSID { get; set; }

            /// <summary>
            /// 传感器名称
            /// </summary>
            public string WSName { get; set; }

            /// <summary>
            /// 监测树ID
            /// </summary>
            public string MonitorTreeID { get; set; }

            /// <summary>
            /// 报警类型
            /// </summary>
            public int MSAlmID { get; set; }

            /// <summary>
            /// 报警状态
            /// </summary>
            public int AlmStatus { get; set; }

            /// <summary>
            /// 采集数据
            /// </summary>
            public float? SamplingValue { get; set; }

            /// <summary>
            /// 高报阈值
            /// </summary>
            public float? WarningValue { get; set; }

            /// <summary>
            /// 高高报阈值
            /// </summary>
            public float? DangerValue { get; set; }

            /// <summary>
            /// 开始时间
            /// </summary>
            public DateTime BDate { get; set; }

            /// <summary>
            /// 结束时间
            /// </summary>
            public DateTime EDate { get; set; }

            /// <summary>
            /// 添加时间
            /// </summary>
            public DateTime AddDate { get; set; }

            /// <summary>
            /// 最近发生时间
            /// </summary>
            public DateTime LatestStartTime { get; set; }

            /// <summary>
            /// 报警内容
            /// </summary>
            public string Content { get; set; }
        }

        #endregion

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
            BaseResponse<AlertCountResult> response = new BaseResponse<AlertCountResult>();
            AlertCountResult result = new AlertCountResult();
            if (parameter.UserID == -1)
            {
                parameter.UserID = 1011;
            }
            try
            {
                using (var dataContext = new iCMSDbContext())
                {
                    //报警提醒报警设置
                    string BJTXBJSZ = cacheDICT.GetInstance().GetCacheType<Config>(p => p.Code == "CONFIG_151_BJTXBJSZ").SingleOrDefault().Value;
                    //报警提醒时间设置
                    string BJTXSJSZ = cacheDICT.GetInstance().GetCacheType<Config>(p => p.Code == "CONFIG_150_BJTXSJSZ").SingleOrDefault().Value;
                    int iBJTXBJSZ = 0;
                    int iBJTXSJSZ = 0;
                    try
                    {
                        iBJTXBJSZ = Convert.ToInt32(BJTXBJSZ);
                        iBJTXSJSZ = Convert.ToInt32(BJTXSJSZ);
                    }
                    catch (Exception ex)
                    {
                        response.IsSuccessful = false;
                        response.Code = "009362";
                        response.Result = null;
                        return response;
                    }
                    DateTime alarmTime = DateTime.Now.AddHours(-iBJTXSJSZ);
                    List<int> alarmSts = new List<int>();
                    alarmSts.AddRange(iBJTXBJSZ == 1 ? new List<int>() { 2, 3 } : new List<int>() { iBJTXBJSZ });

                    result.AlertDeviceCount = dataContext.UserRelationDeviceAlmRecord.Where(w =>
                                            w.UserID == parameter.UserID && alarmSts.Contains(w.AlmStatus) && w.DeviceAlmTime > alarmTime).
                                            Select(s => s.DeviceID).Distinct().Count();
                    result.AlertWGCount = dataContext.UserRelationWSAlmRecord.Where(w =>
                                            w.UserID == parameter.UserID && w.MonitorDeviceType == 1
                                            && alarmSts.Contains(w.AlmStatus) &&
                                            w.WSNAlmTime > alarmTime).
                                            Select(s => s.MonitorDeviceID).Distinct().Count();
                    result.AlertWSCount = dataContext.UserRelationWSAlmRecord.Where(w =>
                                            w.UserID == parameter.UserID && w.MonitorDeviceType == 2
                                            && alarmSts.Contains(w.AlmStatus) &&
                                            w.WSNAlmTime > alarmTime).
                                            Select(s => s.MonitorDeviceID).Distinct().Count();
                    response.IsSuccessful = true;
                    response.Result = result;
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "008391";
                response.Result = null;
                return response;
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
            BaseResponse<GetAlertDeviceByUserIDResult> response = new BaseResponse<GetAlertDeviceByUserIDResult>();
            GetAlertDeviceByUserIDResult result = new GetAlertDeviceByUserIDResult();
            if (parameter.UserID == -1)
            {
                parameter.UserID = 1011;
            }
            if (parameter.Page == 0)
            {
                parameter.Page = 1;
            }
            try
            {
                using (var dataContext = new iCMSDbContext())
                {
                    //报警提醒报警设置
                    string BJTXBJSZ = cacheDICT.GetInstance().GetCacheType<Config>(p => p.Code == "CONFIG_151_BJTXBJSZ").SingleOrDefault().Value;
                    //报警提醒时间设置
                    string BJTXSJSZ = cacheDICT.GetInstance().GetCacheType<Config>(p => p.Code == "CONFIG_150_BJTXSJSZ").SingleOrDefault().Value;
                    int iBJTXBJSZ = 0;
                    int iBJTXSJSZ = 0;
                    try
                    {
                        iBJTXBJSZ = Convert.ToInt32(BJTXBJSZ);
                        iBJTXSJSZ = Convert.ToInt32(BJTXSJSZ);
                    }
                    catch (Exception ex)
                    {
                        response.IsSuccessful = false;
                        response.Code = "009362";
                        response.Result = null;
                        return response;
                    }
                    DateTime alarmTime = DateTime.Now.AddHours(-iBJTXSJSZ);
                    List<int> alarmSts = new List<int>();
                    alarmSts.AddRange(iBJTXBJSZ == 1 ? new List<int>() { 2, 3 } : new List<int>() { iBJTXBJSZ });

                    IQueryable<AlertDevice> linq = from urdar in
                                                       (from urdar1 in dataContext.UserRelationDeviceAlmRecord
                                                        where urdar1.UserID == parameter.UserID &&
                                                        alarmSts.Contains(urdar1.AlmStatus) &&
                                                        urdar1.DeviceAlmTime > alarmTime
                                                        select urdar1.DeviceID).Distinct()
                                                   join dev in dataContext.Device on urdar equals dev.DevID
                                                   join devType in dataContext.DeviceType on dev.DevType equals devType.ID
                                                   select new AlertDevice
                                                   {
                                                       DeviceID = dev.DevID,
                                                       DeviceName = dev.DevName,
                                                       DeviceType = devType.Name,
                                                       AlmStatus = (from dar in dataContext.UserRelationDeviceAlmRecord
                                                                    where dar.DeviceID == urdar
                                                                    select dar.AlmStatus).Max(),
                                                       MonitorTreeRouteID = (from dar in dataContext.DevAlmRecord
                                                                             where dar.DevID == urdar
                                                                             select dar.MonitorTreeID).FirstOrDefault(),
                                                       LatestStartTime = (from dar in dataContext.DevAlmRecord
                                                                          where dar.DevID == urdar
                                                                          select dar.LatestStartTime).Max()
                                                   };
                    //总记录数
                    int total = 0;
                    ListSortDirection sortOrder = parameter.Order.ToLower().Equals("asc") ? ListSortDirection.Ascending : ListSortDirection.Descending;


                    //修改排序字段，原因是前台 MonitorTreeRoute 自已定义后台为null 王颖辉 2018-02-05
                    if (parameter.Sort == "MonitorTreeRoute")
                    {
                        parameter.Sort = "MonitorTreeRouteID";
                    }

                    PropertySortCondition[] sortList = new PropertySortCondition[]
                    {
                        new PropertySortCondition(parameter.Sort, sortOrder),
                        new PropertySortCondition("LatestStartTime", sortOrder),
                    };
                    if (parameter.Page == -1)
                    {
                        result.Total = linq.Count();
                        result.AlertDeviceList = linq.OrderBy(sortList).ToList();
                    }
                    else
                    {
                        result.AlertDeviceList = linq.AsQueryable()
                            .Where(parameter.Page, parameter.PageSize, out total, sortList).Distinct()
                            .ToList();
                        result.Total = total;
                    }

                    foreach (var item in result.AlertDeviceList)
                    {
                        int deviceMTID = dataContext.Device.Where(w => w.DevID == item.DeviceID).Select(s => s.MonitorTreeID).FirstOrDefault();
                        List<int> pMonitorTreeIDs = new List<int>();
                        GetMonitorTreeRoute(deviceMTID, pMonitorTreeIDs);
                        item.MonitorTreeRoute = string.Join("#", dataContext.MonitorTree.Where(w => pMonitorTreeIDs.Contains(w.MonitorTreeID)).
                                            Select(s => s.Name).ToList());
                    }

                    response.IsSuccessful = true;
                    response.Result = result;
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "008401";
                response.Result = null;
                return response;
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
            BaseResponse<GetDeviceAlertByDeviceIDResult> response = new BaseResponse<GetDeviceAlertByDeviceIDResult>();
            GetDeviceAlertByDeviceIDResult result = new GetDeviceAlertByDeviceIDResult();
            if (parameter.Page <= 0)
            {
                parameter.Page = 1;
            }

            #region 修改超级管员id
            //修改超级管员id
            if (parameter.UserID == -1)
            {
                parameter.UserID = 1011;//超级管理员
            }
            #endregion

            try
            {
                using (var dataContext = new iCMSDbContext())
                {
                    //报警提醒报警设置
                    string BJTXBJSZ = cacheDICT.GetInstance().GetCacheType<Config>(p => p.Code == "CONFIG_151_BJTXBJSZ").SingleOrDefault().Value;
                    //报警提醒时间设置
                    string BJTXSJSZ = cacheDICT.GetInstance().GetCacheType<Config>(p => p.Code == "CONFIG_150_BJTXSJSZ").SingleOrDefault().Value;
                    int iBJTXBJSZ = 0;
                    int iBJTXSJSZ = 0;
                    try
                    {
                        iBJTXBJSZ = Convert.ToInt32(BJTXBJSZ);
                        iBJTXSJSZ = Convert.ToInt32(BJTXSJSZ);
                    }
                    catch (Exception ex)
                    {
                        response.IsSuccessful = false;
                        response.Code = "009362";
                        response.Result = null;
                        return response;
                    }
                    DateTime alarmTime = DateTime.Now.AddHours(-iBJTXSJSZ);
                    List<int> alarmSts = new List<int>();
                    alarmSts.AddRange(iBJTXBJSZ == 1 ? new List<int>() { 2, 3 } : new List<int>() { iBJTXBJSZ });

                    var linq = (from dar in dataContext.DevAlmRecord
                                where (from urdar in dataContext.UserRelationDeviceAlmRecord
                                       where urdar.UserID == parameter.UserID && urdar.DeviceID == parameter.ID
                                        && alarmSts.Contains(urdar.AlmStatus) &&
                                        urdar.DeviceAlmTime > alarmTime
                                       select urdar.DeviceAlmRecordID).Contains(dar.AlmRecordID)
                                select new DeviceAlarmRecord
                                {
                                    AlermRecordID = dar.AlmRecordID,
                                    DeviceID = dar.DevID,
                                    DeviceName = dar.DevName,
                                    DeviceNO = dar.DevNO,
                                    MSiteID = dar.MSiteID,
                                    MSiteName = dar.MSiteName,
                                    SingalID = dar.SingalID,
                                    SingalName = dar.SingalName,
                                    SingalAlmID = dar.SingalAlmID,
                                    SingalValue = dar.SingalValue,
                                    SamplingValue = dar.SamplingValue,
                                    WarningValue = dar.WarningValue,
                                    DangerValue = dar.DangerValue,
                                    MSAlmID = dar.MSAlmID,
                                    MonitorTreeID = dar.MonitorTreeID,
                                    AlmStatus = dar.AlmStatus,
                                    Content = dar.Content,
                                    BDate = dar.BDate,
                                    EDate = dar.EDate,
                                    LatestStartTime = dar.LatestStartTime,
                                });
                    int total = 0;
                    ListSortDirection sortOrder = parameter.Order.ToLower().Equals("asc") ? ListSortDirection.Ascending : ListSortDirection.Descending;
                    PropertySortCondition[] sortList = new PropertySortCondition[]
                    {
                        new PropertySortCondition(parameter.Sort, sortOrder),
                        new PropertySortCondition("LatestStartTime", sortOrder),
                    };
                    if (parameter.Page == -1)
                    {
                        result.Total = linq.Count();
                        result.DeviceAlarmRecordList = linq.OrderBy(sortList).ToList();
                    }
                    else
                    {
                        result.DeviceAlarmRecordList = linq.AsQueryable()
                            .Where(parameter.Page, parameter.PageSize, out total, sortList)
                            .ToList();
                        result.Total = total;
                    }

                    response.IsSuccessful = true;
                    response.Result = result;
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "008411";
                response.Result = null;
                return response;
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
            BaseResponse<bool> response = new BaseResponse<bool>();
            if (!parameter.AlermRecordID.HasValue && !parameter.ID.HasValue)
            {
                response.IsSuccessful = false;
                response.Code = "008432";
                response.Result = false;
                return response;
            }
            if (parameter.UserID == -1)
            {
                parameter.UserID = 1011;
            }
            try
            {
                using (iCMSDbContext dbContext = new iCMSDbContext())
                {
                    string sql = string.Empty;
                    //AlermRecordID不为空直接确认报警提醒，否则确认设备下全部报警提醒
                    if (parameter.AlermRecordID.HasValue)
                    {
                        sql = string.Format("delete from T_SYS_USER_RELATION_DEV_ALMRECORD where UserID={0} and DeviceAlmRecordID={1}",
                                                  parameter.UserID,
                                                  parameter.AlermRecordID);
                    }
                    else
                    {
                        sql = string.Format("delete from T_SYS_USER_RELATION_DEV_ALMRECORD where UserID={0} and DeviceID={1}",
                                                  parameter.UserID,
                                                  parameter.ID);
                    }

                    int res = dbContext.Database.ExecuteSqlCommand(sql);
                    response.IsSuccessful = true;
                    response.Result = true;
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "008421";
                response.Result = false;
                return response;
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
            BaseResponse<GetAlertSensorByUserIDResult> response = new BaseResponse<GetAlertSensorByUserIDResult>();
            GetAlertSensorByUserIDResult result = new GetAlertSensorByUserIDResult();
            if (parameter.UserID == -1)
            {
                parameter.UserID = 1011;
            }
            if (parameter.Page <= 0)
            {
                parameter.Page = 1;
            }
            try
            {
                using (var dataContext = new iCMSDbContext())
                {
                    //报警提醒报警设置
                    string BJTXBJSZ = cacheDICT.GetInstance().GetCacheType<Config>(p => p.Code == "CONFIG_151_BJTXBJSZ").SingleOrDefault().Value;
                    //报警提醒时间设置
                    string BJTXSJSZ = cacheDICT.GetInstance().GetCacheType<Config>(p => p.Code == "CONFIG_150_BJTXSJSZ").SingleOrDefault().Value;
                    int iBJTXBJSZ = 0;
                    int iBJTXSJSZ = 0;
                    try
                    {
                        iBJTXBJSZ = Convert.ToInt32(BJTXBJSZ);
                        iBJTXSJSZ = Convert.ToInt32(BJTXSJSZ);
                    }
                    catch (Exception ex)
                    {
                        response.IsSuccessful = false;
                        response.Code = "009362";
                        response.Result = null;
                        return response;
                    }
                    DateTime alarmTime = DateTime.Now.AddHours(-iBJTXSJSZ);
                    List<int> alarmSts = new List<int>();
                    alarmSts.AddRange(iBJTXBJSZ == 1 ? new List<int>() { 2, 3 } : new List<int>() { iBJTXBJSZ });

                    List<int> wsAlarmType = new List<int>() { 3, 4, 5 };

                    //总记录数
                    int total = 0;
                    ListSortDirection sortOrder = parameter.Order.ToLower().Equals("asc") ? ListSortDirection.Ascending : ListSortDirection.Descending;

                    //修改排序字段，原因是前台 MonitorTreeRoute 自已定义后台为null 王颖辉 2018-02-05
                    if (parameter.Sort == "MonitorTreeRoute")
                    {
                        parameter.Sort = "MonitorTreeRouteID";
                    }

                    PropertySortCondition[] sortList = new PropertySortCondition[]
                    {
                        new PropertySortCondition(parameter.Sort, sortOrder),
                        new PropertySortCondition("LatestStartTime", sortOrder),
                    };
                    switch (parameter.MonitorDeviceType)
                    {
                        case 1:
                            var linqWG =
                                    (from urWSar in
                                         ((from urWSar1 in dataContext.UserRelationWSAlmRecord
                                           where urWSar1.MonitorDeviceType == 1 &&
                                                 urWSar1.UserID == parameter.UserID &&
                                                 alarmSts.Contains(urWSar1.AlmStatus) &&
                                                 urWSar1.WSNAlmTime > alarmTime
                                           select urWSar1.MonitorDeviceID).Distinct())
                                     join wg in dataContext.WG on urWSar equals wg.WGID
                                     select new AlertSensor
                                     {
                                         MonitorDeviceID = wg.WGID,
                                         MonitorDeviceName = wg.WGName,
                                         AlmStatus = (from wsnar in dataContext.UserRelationWSAlmRecord
                                                      where wsnar.MonitorDeviceType == 1 && wsnar.MonitorDeviceID == urWSar
                                                      select wsnar.AlmStatus).Max(),
                                         MonitorTreeRouteID = (from wsnar in dataContext.WsnAlmrecord
                                                               where wsnar.WGID == urWSar && wsnar.MSAlmID == 6
                                                               select wsnar.MonitorTreeID).FirstOrDefault(),
                                         LatestStartTime = (from wsnar in dataContext.WsnAlmrecord
                                                            where wsnar.WGID == urWSar
                                                            select wsnar.LatestStartTime).Max()
                                     });
                            if (parameter.Page == -1)
                            {
                                total = linqWG.Count();
                                result.AlertSensorList = linqWG.OrderBy(sortList).ToList();
                            }
                            else
                            {
                                result.AlertSensorList = linqWG.AsQueryable()
                                .Where(parameter.Page, parameter.PageSize, out total, sortList).Distinct()
                                .ToList();
                            }

                            break;
                        case 2:
                            var linqWS =
                                    (from urWSar in
                                         ((from urWSar1 in dataContext.UserRelationWSAlmRecord
                                           where urWSar1.MonitorDeviceType == 2 &&
                                                 urWSar1.UserID == parameter.UserID &&
                                                 alarmSts.Contains(urWSar1.AlmStatus) &&
                                                 urWSar1.WSNAlmTime > alarmTime
                                           select urWSar1.MonitorDeviceID).Distinct())
                                     join ws in dataContext.WS on urWSar equals ws.WSID
                                     join wg in dataContext.WG on ws.WGID equals wg.WGID
                                     select new AlertSensor
                                     {
                                         MonitorDeviceID = ws.WSID,
                                         MonitorDeviceName = ws.WSName,
                                         WGName = wg.WGName,
                                         AlmStatus = (from wsnar in dataContext.UserRelationWSAlmRecord
                                                      where wsnar.MonitorDeviceType == 2 && wsnar.MonitorDeviceID == urWSar
                                                      select wsnar.AlmStatus).Max(),
                                         MonitorTreeRouteID = (from wsnar in dataContext.WsnAlmrecord
                                                               where wsnar.WSID == urWSar && wsAlarmType.Contains(wsnar.MSAlmID)
                                                               select wsnar.MonitorTreeID).FirstOrDefault(),
                                         LatestStartTime = (from wsnar in dataContext.WsnAlmrecord
                                                            where wsnar.WSID == urWSar
                                                            select wsnar.LatestStartTime).Max()
                                     });

                            if (parameter.Page == -1)
                            {
                                total = linqWS.Count();
                                result.AlertSensorList = linqWS.OrderBy(sortList).ToList();
                            }
                            else
                            {
                                result.AlertSensorList = linqWS.AsQueryable()
                                .Where(parameter.Page, parameter.PageSize, out total, sortList).Distinct()
                                .ToList();
                            }

                            break;
                        default: break;
                    }

                    result.Total = total;
                    if (parameter.MonitorDeviceType == 1)
                    {
                        foreach (var item in result.AlertSensorList)
                        {
                            int? WGMTID = dataContext.WG.Where(w => w.WGID == item.MonitorDeviceID).Select(s => s.MonitorTreeID).FirstOrDefault();
                            if (WGMTID.HasValue)
                            {
                                List<int> pMonitorTreeIDs = new List<int>();
                                GetMonitorTreeRoute(WGMTID.Value, pMonitorTreeIDs);
                                item.MonitorTreeRoute = string.Join("#", dataContext.MonitorTree.Where(w => pMonitorTreeIDs.Contains(w.MonitorTreeID)).
                                                    Select(s => s.Name).ToList());
                            }
                        }
                    }

                    response.IsSuccessful = true;
                    response.Result = result;
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "008451";
                response.Result = null;
                return response;
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
            BaseResponse<GetSensorAlertByIDResult> response = new BaseResponse<GetSensorAlertByIDResult>();
            GetSensorAlertByIDResult result = new GetSensorAlertByIDResult();
            if (parameter.ID < 1)
            {
                response.Result = result;
                return response;
            }
            if (parameter.Page <= 0)
            {
                parameter.Page = 1;
            }
            if (parameter.UserID == -1)
            {
                parameter.UserID = 1011;
            }
            try
            {
                using (var dataContext = new iCMSDbContext())
                {
                    //报警提醒报警设置
                    string BJTXBJSZ = cacheDICT.GetInstance().GetCacheType<Config>(p => p.Code == "CONFIG_151_BJTXBJSZ").SingleOrDefault().Value;
                    //报警提醒时间设置
                    string BJTXSJSZ = cacheDICT.GetInstance().GetCacheType<Config>(p => p.Code == "CONFIG_150_BJTXSJSZ").SingleOrDefault().Value;
                    int iBJTXBJSZ = 0;
                    int iBJTXSJSZ = 0;
                    try
                    {
                        iBJTXBJSZ = Convert.ToInt32(BJTXBJSZ);
                        iBJTXSJSZ = Convert.ToInt32(BJTXSJSZ);
                    }
                    catch (Exception ex)
                    {
                        response.IsSuccessful = false;
                        response.Code = "009362";
                        response.Result = null;
                        return response;
                    }
                    DateTime alarmTime = DateTime.Now.AddHours(-iBJTXSJSZ);
                    List<int> alarmSts = new List<int>();
                    alarmSts.AddRange(iBJTXBJSZ == 1 ? new List<int>() { 2, 3 } : new List<int>() { iBJTXBJSZ });

                    //修改排序字段，原因是前台 MonitorTreeRoute 自已定义后台为null 王颖辉 2018-02-05
                    if (parameter.Sort == "MonitorTreeRoute")
                    {
                        parameter.Sort = "MonitorTreeID";
                    }
                    int total = 0;
                    ListSortDirection sortOrder = parameter.Order.ToLower().Equals("asc") ? ListSortDirection.Ascending : ListSortDirection.Descending;
                    PropertySortCondition[] sortList = new PropertySortCondition[]
                    {
                        new PropertySortCondition(parameter.Sort, sortOrder),
                        new PropertySortCondition("LatestStartTime", sortOrder),
                    };
                    switch (parameter.MonitorDeviceType)
                    {
                        case 1:
                            var linqWG = (from wsnar in dataContext.WsnAlmrecord
                                          where (from urwsnar in dataContext.UserRelationWSAlmRecord
                                                 where urwsnar.UserID == parameter.UserID
                                                      && urwsnar.MonitorDeviceType == 1
                                                      && urwsnar.MonitorDeviceID == parameter.ID
                                                      && alarmSts.Contains(urwsnar.AlmStatus)
                                                      && urwsnar.WSNAlmTime > alarmTime
                                                 select urwsnar.WSNAlmRecordID).Contains(wsnar.AlmRecordID)
                                          select new SensorAlarmRecord
                                          {
                                              AlermRecordID = wsnar.AlmRecordID,
                                              DeviceID = wsnar.DevID,
                                              DeviceName = wsnar.DevName,
                                              DeviceNO = wsnar.DevNO,
                                              MSiteID = wsnar.MSiteID,
                                              MSiteName = wsnar.MSiteName,
                                              WGID = wsnar.WGID,
                                              WGName = wsnar.WGName,
                                              WSID = wsnar.WSID,
                                              WSName = wsnar.WSName,
                                              MSAlmID = wsnar.MSAlmID,
                                              MonitorTreeID = wsnar.MonitorTreeID,
                                              AlmStatus = wsnar.AlmStatus,
                                              Content = wsnar.Content,
                                              BDate = wsnar.BDate,
                                              EDate = wsnar.EDate,
                                              LatestStartTime = wsnar.LatestStartTime,
                                              SamplingDate = wsnar.LatestStartTime,
                                              AlarmValue = wsnar.WarningValue,
                                              DangerValue = wsnar.DangerValue,
                                              SamplingValue = wsnar.SamplingValue,

                                          });
                            if (parameter.Page == -1)
                            {
                                total = linqWG.Count();
                                result.SensorAlarmRecord = linqWG.OrderBy(sortList).ToList();
                            }
                            else
                            {
                                result.SensorAlarmRecord = linqWG.AsQueryable()
                                .Where(parameter.Page, parameter.PageSize, out total, sortList)
                                .ToList();
                            }

                            break;
                        case 2:
                            var linqWS = (from wsnar in dataContext.WsnAlmrecord
                                          where (from urwsnar in dataContext.UserRelationWSAlmRecord
                                                 where urwsnar.UserID == parameter.UserID
                                                      && urwsnar.MonitorDeviceType == 2
                                                      && urwsnar.MonitorDeviceID == parameter.ID
                                                      && alarmSts.Contains(urwsnar.AlmStatus)
                                                      && urwsnar.WSNAlmTime > alarmTime
                                                 select urwsnar.WSNAlmRecordID).Contains(wsnar.AlmRecordID)
                                          select new SensorAlarmRecord
                                          {
                                              AlermRecordID = wsnar.AlmRecordID,
                                              DeviceID = wsnar.DevID,
                                              DeviceName = wsnar.DevName,
                                              DeviceNO = wsnar.DevNO,
                                              MSiteID = wsnar.MSiteID,
                                              MSiteName = wsnar.MSiteName,
                                              WGID = wsnar.WGID,
                                              WGName = wsnar.WGName,
                                              WSID = wsnar.WSID,
                                              WSName = wsnar.WSName,
                                              MSAlmID = wsnar.MSAlmID,
                                              MonitorTreeID = wsnar.MonitorTreeID,
                                              AlmStatus = wsnar.AlmStatus,
                                              Content = wsnar.Content,
                                              BDate = wsnar.BDate,
                                              EDate = wsnar.EDate,
                                              LatestStartTime = wsnar.LatestStartTime,
                                              SamplingDate = wsnar.LatestStartTime,
                                              AlarmValue = wsnar.WarningValue,
                                              DangerValue = wsnar.DangerValue,
                                              SamplingValue = wsnar.SamplingValue,

                                          });
                            if (parameter.Page == -1)
                            {
                                total = linqWS.Count();
                                result.SensorAlarmRecord = linqWS.OrderBy(sortList).ToList();
                            }
                            else
                            {
                                result.SensorAlarmRecord = linqWS.AsQueryable()
                                .Where(parameter.Page, parameter.PageSize, out total, sortList)
                                .ToList();
                            }

                            break;
                        default: break;
                    }
                    result.Total = total;
                    response.IsSuccessful = true;
                    response.Result = result;
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "008461";
                response.Result = null;
                return response;
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
            BaseResponse<bool> response = new BaseResponse<bool>();
            if (!parameter.AlermRecordID.HasValue && !parameter.ID.HasValue)
            {
                response.IsSuccessful = false;
                response.Code = "008482";
                response.Result = false;
                return response;
            }
            if (parameter.UserID == -1)
            {
                parameter.UserID = 1011;
            }
            try
            {
                using (iCMSDbContext dbContext = new iCMSDbContext())
                {
                    string sql = string.Empty;
                    //AlermRecordID不为空直接确认报警提醒，否则确认设备下全部报警提醒
                    if (parameter.AlermRecordID.HasValue)
                    {
                        sql = string.Format(@"delete from T_SYS_USER_RELATION_WS_ALMRECORD where UserID={0} and 
                                                        MonitorDeviceType={1} and 
                                                        WSNAlmRecordID={2}",
                                                     parameter.UserID,
                                                     parameter.MonitorDeviceType,
                                                     parameter.AlermRecordID);
                    }
                    else
                    {
                        sql = string.Format(@"delete from T_SYS_USER_RELATION_WS_ALMRECORD where UserID={0} and 
                                                        MonitorDeviceType={1} and 
                                                        MonitorDeviceID={2}",
                                                     parameter.UserID,
                                                     parameter.MonitorDeviceType,
                                                     parameter.ID);
                    }

                    int res = dbContext.Database.ExecuteSqlCommand(sql);
                    response.IsSuccessful = true;
                    response.Result = true;
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "008471";
                response.Result = false;
                return response;
            }
        }

        #endregion

        #region 公共方法

        #region 判断是不是监测树叶子节点
        /// <summary>
        ///  判断是不是监测树叶子节点
        /// </summary>
        /// <param name="monitorTreeID"></param>
        /// <returns></returns>
        private bool IsMonitorTreeLeaf(int monitorTreeID)
        {
            var childNodes = monitorTreeRepository
                .GetDatas<MonitorTree>(m => m.PID == monitorTreeID, false)
                .ToList();
            if (childNodes != null && childNodes.Count > 0)
            {
                //不是监测树叶子节点
                return false;
            }
            else
            {
                //是监测树叶子节点
                return true;
            }
        }
        #endregion

        #region CalculatorDevRunningStat
        /// <summary>
        /// CalculatorDevRunningStat
        /// </summary>
        /// <param name="monitorTreeID"></param>
        /// <param name="viewData"></param>
        /// 修改：查询优化 王龙杰 2017-10-30
        private void CalculatorDevRunningStat(int monitorTreeID, out DevRunningStatCountDataInfoByMTId viewData)
        {
            var allChildNodes = new List<int>();
            GetAllChildren(monitorTreeID, allChildNodes);//获取所有的子节点，保存与全局对象中

            var allDevices = deviceRepository.GetDatas<Device>(t => allChildNodes.Contains(t.MonitorTreeID), false).ToList();
            viewData = new DevRunningStatCountDataInfoByMTId();

            viewData.MonitorTreeId = monitorTreeID;
            var tempMonitorTree = new iCMSDbContext().MonitorTree.Where(w => w.MonitorTreeID == monitorTreeID).
                                        Select(s => new { Name = s.Name, Type = s.Type }).FirstOrDefault();
            #region 2016/8/22 Add MonitorTree Type, BY qxm
            int mtType = tempMonitorTree.Type;
            viewData.MonitorTreeType = cacheDICT.GetInstance().GetCacheType<MonitorTreeType>(p => p.ID == mtType).SingleOrDefault().Describe;
            #endregion
            viewData.MonitorTreeName = tempMonitorTree.Name;
            if (allDevices != null && allDevices.Count > 0)
            {
                viewData.TotalCount = allDevices.Count;
                viewData.UseCount = allDevices.Where(t => t.UseType == 0).ToList().Count;
                viewData.UnUseCount = viewData.TotalCount - viewData.UseCount;

                // AlmStatus ==0,1 都属于 正常， 2016/09/01 经段确认， QXM
                viewData.NomalCount = allDevices.Where(t => t.UseType == 0 && (t.AlmStatus == 1 || t.AlmStatus == 0)).ToList().Count;
                viewData.WarningCount = allDevices.Where(t => t.UseType == 0 && t.AlmStatus == 2).ToList().Count;
                viewData.AlarmCount = allDevices.Where(t => t.UseType == 0 && t.AlmStatus == 3).ToList().Count;
            }
        }
        #endregion

        #region GetAllChildren
        /// <summary>
        /// GetAllChildren
        /// </summary>
        /// <param name="monitorTreeID"></param>
        /// <param name="allChildNodes"></param>
        private void GetAllChildren(int monitorTreeID, List<int> allChildNodes)
        {
            allChildNodes.Add(monitorTreeID);
            var childNodes = monitorTreeRepository.GetDatas<MonitorTree>(m => m.PID == monitorTreeID, false).ToList();

            foreach (var mt in childNodes)
            {
                //找到包含当前节点及所有子节点
                GetAllChildren(mt.MonitorTreeID, allChildNodes);
            }
        }
        #endregion

        #region CalculateMSIDs
        /// <summary>
        /// CalculateMSIDs
        /// </summary>
        /// <param name="monitorTreeId"></param>
        /// <param name="devId"></param>
        /// <param name="msId"></param>
        /// <returns></returns>
        private string CalculateMSIDs(int? monitorTreeId, int? devId, int? msId, iCMSDbContext dbContext)
        {
            string monitorTreeCondition = "";

            if (msId.HasValue && msId.Value != -1)
            {
                monitorTreeCondition = string.Format(" AND a.MSiteID={0}", msId.Value);
            }
            else if (devId.HasValue && devId.Value != -1)
            {
                monitorTreeCondition = string.Format(" AND a.DevID={0}", devId.Value);
            }
            else if (monitorTreeId.HasValue && monitorTreeId.Value != -1)
            {
                List<int> pMonitorTreeIDS = new List<int>();
                GetAllChildren(monitorTreeId.Value, pMonitorTreeIDS);

                List<int> devList = (from d in dbContext.Device
                                     where pMonitorTreeIDS.Contains(d.MonitorTreeID)
                                     select d.DevID).Distinct().ToList();

                if (devList.Count > 0)
                {
                    var devIds = string.Join(",", devList);
                    monitorTreeCondition = string.Format(" AND a.DevID in ({0})", devIds);
                }
            }

            return monitorTreeCondition;
        }
        #endregion

        #region GetMonitorTreeRouteByDeviceID
        /// <summary>
        /// GetMonitorTreeRouteByDeviceID
        /// </summary>
        /// <param name="leafID"></param>
        /// <param name="pMonitorTreeIDs"></param>
        private void GetMonitorTreeRoute(int leafID, List<int> pMonitorTreeIDs)
        {
            pMonitorTreeIDs.Add(leafID);
            var pNodes = monitorTreeRepository.GetDatas<MonitorTree>(m => m.MonitorTreeID == leafID, false).FirstOrDefault();

            if (pNodes != null)
            {
                //找到包含当前节点及所有子节点
                GetMonitorTreeRoute(pNodes.PID, pMonitorTreeIDs);
            }
        }
        #endregion

        #endregion

        #region iCMS 1.6 Maintain by QXM

        /// <summary>
        /// 获取用户所管理设备在不同条件（某工厂，车间，机组）下设备统计信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public BaseResponse<GetDeviceStatisticSummaryResult> GetDeviceStatisticSummary(GetDeviceStatisticSummaryParam param)
        {
            BaseResponse<GetDeviceStatisticSummaryResult> response = new BaseResponse<GetDeviceStatisticSummaryResult>();
            GetDeviceStatisticSummaryResult result = new GetDeviceStatisticSummaryResult();

            try
            {
                //最终查询的设备
                List<Device> finalDeviceList = new List<Device>();

                if (param.UserID == -1 || param.UserID == 1011)
                {
                    finalDeviceList = deviceRepository.GetDatas<Device>(t => true, true).ToList();
                }
                else
                {
                    List<int> allMonitorTreeChildren = new List<int>();

                    //获取所有监测树子节点
                    GetAllChildren(param.MonitorTreeID, allMonitorTreeChildren);

                    //获取监测树所有设备
                    var deviceListMT = deviceRepository
                        .GetDatas<Device>(t => allMonitorTreeChildren.Contains(t.MonitorTreeID), true)
                        .Select(t => t.DevID)
                        .ToList();
                    var deviceUser = userRalationDeviceRepository
                        .GetDatas<UserRalationDevice>(t => t.UserID == param.UserID, true)
                        .Select(t => t.DevId.Value)
                        .ToList();
                    var tempDeviceList = deviceListMT.Intersect(deviceUser);

                    finalDeviceList = deviceRepository
                        .GetDatas<Device>(t => tempDeviceList.Contains(t.DevID), true)
                        .ToList();
                }

                var finalDeviceIDList = finalDeviceList.Select(item => item.DevID);
                var devRelaWGIDList = deviceRelationWGRepository
                    .GetDatas<DeviceRelationWG>(item => finalDeviceIDList.Contains(item.DevId), false)
                    .Select(item => new { item.DevId, item.WGID, })
                    .ToList();
                var tempWGIDList = devRelaWGIDList.Select(item => item.WGID);
                var wgInfoList = gatewayRepository
                    .GetDatas<Gateway>(item => tempWGIDList.Contains(item.WGID), false)
                    .ToList();

                //设备运行总数
                result.RunningDeviceCount = finalDeviceList.Where(t => t.RunStatus == 1).Count();
                //设备停机总数
                result.StoppedDeviceCount = finalDeviceList.Where(t => t.RunStatus == 3).Count();
                //设备正常总数
                result.NormalDeviceCount = finalDeviceList.Where(t => t.AlmStatus == 0 || t.AlmStatus == 1).Count();
                //设备中级报警总数
                result.AlarmDeviceCount = finalDeviceList.Where(t => t.AlmStatus == 2).Count();
                //设备高级报警总数
                result.WarnDeviceCount = finalDeviceList.Where(t => t.AlmStatus == 3).Count();

                //无线网关ID
                var wirelessWGIDList = wgInfoList
                    .Where(item => item.DevFormType == (int)EnumDevFormType.SingleBoard
                        || item.DevFormType == (int)EnumDevFormType.iWSN)
                    .Select(item => item.WGID)
                    .ToList();
                //关联无线网关的设备ID
                var wirelessDevIDList = devRelaWGIDList
                    .Where(item => wirelessWGIDList.Contains(item.WGID))
                    .Select(item => item.DevId);
                //关联无线网关的设备总数
                result.WirelessDevice = finalDeviceList.Where(t => wirelessDevIDList.Contains(t.DevID)).Count();
                //有线网关ID
                var wireWGIDList = wgInfoList
                    .Where(item => item.DevFormType == (int)EnumDevFormType.Wired)
                    .Select(item => item.WGID)
                    .ToList();
                //关联有线网关的设备ID
                var wireDevIDList = devRelaWGIDList
                    .Where(item => wireWGIDList.Contains(item.WGID))
                    .Select(item => item.DevId);
                //关联有线网关的设备总数
                result.WireDevice = finalDeviceList.Where(t => wireDevIDList.Contains(t.DevID)).Count();

                response.Result = result;
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response.IsSuccessful = false;
                response.Code = "008161";

                return response;
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
            BaseResponse<GetAllWSInfoByUserIDResult> response = new BaseResponse<GetAllWSInfoByUserIDResult>();
            GetAllWSInfoByUserIDResult result = new GetAllWSInfoByUserIDResult();
            response.IsSuccessful = true;
            response.Result = result;

            if (parameter.UserID <= 0 && parameter.UserID != -1)
            {
                response.IsSuccessful = false;
                response.Code = "000000";
                return response;
            }

            try
            {
                if (parameter.UserID == -1)
                {
                    //传感器信息集合
                    var wsInfoList = wsRepository
                        .GetDatas<WS>(item => true, false)
                        .ToList();
                    //传感器ID集合
                    var wsIDList = wsInfoList.Select(wsInfo => (int?)wsInfo.WSID).ToList();
                    //传感器关联的测量位置信息集合
                    var mSiteInfoList = measureSiteRepository
                        .GetDatas<MeasureSite>(mSiteInfo => wsIDList.Contains(mSiteInfo.WSID), false)
                        .Select(mSiteInfo => new { mSiteInfo.MSiteID, mSiteInfo.WSID, })
                        .ToList();
                    //获取传感器信息
                    result.WSInfoList = wsInfoList
                        .Select(wsInfo => new WSSimpleInfo
                        {
                            WSID = wsInfo.WSID,
                            WSName = wsInfo.WSName,
                            WSState = mSiteInfoList.Any(mSiteInfo => mSiteInfo.WSID == wsInfo.WSID),
                            DevFormType = wsInfo.DevFormType,
                        })
                        .ToList();
                }
                else
                {
                    //获取用户关联的传感器信息集合
                    var userRelaWSIDList = userRalationWSRepository
                        .GetDatas<UserRelationWS>(item => item.UserID == parameter.UserID, false)
                        .Select(item => item.WSID)
                        .ToList();
                    if (userRelaWSIDList.Any())
                    {
                        //传感器信息集合
                        var wsInfoList = wsRepository
                            .GetDatas<WS>(item => userRelaWSIDList.Contains(item.WSID), false)
                            .ToList();
                        //传感器ID集合
                        var wsIDList = wsInfoList.Select(wsInfo => (int?)wsInfo.WSID).ToList();
                        //传感器关联的测量位置信息集合
                        var mSiteInfoList = measureSiteRepository
                            .GetDatas<MeasureSite>(mSiteInfo => wsIDList.Contains(mSiteInfo.WSID), false)
                            .Select(mSiteInfo => new { mSiteInfo.MSiteID, mSiteInfo.WSID, })
                            .ToList();
                        //获取传感器信息
                        result.WSInfoList = wsInfoList
                            .Select(wsInfo => new WSSimpleInfo
                            {
                                WSID = wsInfo.WSID,
                                WSName = wsInfo.WSName,
                                WSState = mSiteInfoList.Any(mSiteInfo => mSiteInfo.WSID == wsInfo.WSID),
                                DevFormType = wsInfo.DevFormType,
                            })
                            .ToList();
                    }
                }
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response.IsSuccessful = false;
                response.Code = "000000";
                return response;
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
            BaseResponse<GetAllDAUStateByUserIDResult> response = new BaseResponse<GetAllDAUStateByUserIDResult>();
            GetAllDAUStateByUserIDResult result = new GetAllDAUStateByUserIDResult();
            response.IsSuccessful = true;
            response.Result = result;

            if (parameter.UserID <= 0 && parameter.UserID != -1)
            {
                response.IsSuccessful = false;
                response.Code = "000000";
                return response;
            }

            try
            {
                if (parameter.UserID == -1)
                {
                    //获取所有的有线网关状态信息
                    result.WGStateList = gatewayRepository
                        .GetDatas<Gateway>(item => true, false)
                        .Where(item => item.DevFormType == (int)EnumDevFormType.Wired)
                        .Select(item => new WGSimpleStateInfo
                        {
                            WGID = item.WGID,
                            WGName = item.WGName,
                            IsAvailable = item.CurrentDAUStates.HasValue,
                        })
                        .ToList();
                }
                else
                {
                    //获取用户关联的传感器信息集合
                    var userRelaWSIDList = userRalationWSRepository
                        .GetDatas<UserRelationWS>(item => item.UserID == parameter.UserID, false)
                        .Select(item => item.WSID)
                        .ToList();
                    if (userRelaWSIDList.Any())
                    {
                        //根据传感器ID获取网关ID
                        var wgIDList = wsRepository
                            .GetDatas<WS>(item => userRelaWSIDList.Contains(item.WSID), false)
                            .Select(item => item.WGID)
                            .ToList();
                        if (wgIDList.Any())
                        {
                            //获取所有的有线网关状态信息
                            result.WGStateList = gatewayRepository
                                .GetDatas<Gateway>(item => item.DevFormType == (int)EnumDevFormType.Wired
                                    && wgIDList.Contains(item.WGID), false)
                                .Select(item => new WGSimpleStateInfo
                                {
                                    WGID = item.WGID,
                                    WGName = item.WGName,
                                    IsAvailable = item.CurrentDAUStates.HasValue,
                                })
                                .ToList();
                        }
                    }
                }
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response.IsSuccessful = false;
                response.Code = "000000";
                return response;
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
            BaseResponse<GetAllWSTypeByUserIDResult> response = new BaseResponse<GetAllWSTypeByUserIDResult>();
            GetAllWSTypeByUserIDResult result = new GetAllWSTypeByUserIDResult();
            response.IsSuccessful = true;
            response.Result = result;

            if (parameter.UserID < -1)
            {
                response.IsSuccessful = false;
                response.Code = "000000";
                return response;
            }
            if (parameter.UserID == -1)
            {
                parameter.UserID = 1011;
            }
            try
            {
                //获取用户关联的传感器信息集合
                var userRelaWSIDList = userRalationWSRepository
                    .GetDatas<UserRelationWS>(item => item.UserID == parameter.UserID, false)
                    .Select(item => item.WSID)
                    .ToList();
                if (userRelaWSIDList.Any())
                {
                    //根据传感器ID获取网关ID
                    var wgIDList = wsRepository
                        .GetDatas<WS>(item => userRelaWSIDList.Contains(item.WSID), false)
                        .Select(item => item.WGID)
                        .ToList();
                    if (wgIDList.Any())
                    {
                        //获取所有的网关状态信息
                        var wiredWGIDList = gatewayRepository
                            .GetDatas<Gateway>(item => item.DevFormType == (int)EnumDevFormType.Wired
                                && wgIDList.Contains(item.WGID), false)
                            .Select(item => item.WGID)
                            .ToList();
                        if (wiredWGIDList.Any())
                        {
                            //根据有线网关获取有线传感器
                            var wiredWSIDList = wsRepository
                                .GetDatas<WS>(item => wiredWGIDList.Contains(item.WGID), false)
                                .Select(item => (int?)item.WSID)
                                .ToList();
                            if (wiredWSIDList.Any())
                            {
                                //根据传感器获取挂靠的测点
                                var mSiteIDList = measureSiteRepository
                                    .GetDatas<MeasureSite>(item => wiredWSIDList.Contains(item.WSID), false)
                                    .Select(item => item.MSiteID)
                                    .ToList();
                                //计算振动数据类型总数
                                result.VibrationDataTypeCount = vibSingalRepository
                                    .GetDatas<VibSingal>(item => mSiteIDList.Contains(item.MSiteID), false)
                                    .Count();
                                //计算温度数据类型总数
                                result.TemperatureDataTypeCount = tempeDeviceSetMSiteAlmRepository
                                    .GetDatas<TempeDeviceSetMSiteAlm>(item => mSiteIDList.Contains(item.MsiteID), false)
                                    .Count();
                                //计算转速数据类型总数
                                result.SpeedDataTypeCount = speedSamplingMDFRepository
                                    .GetDatas<SpeedSamplingMDF>(item => mSiteIDList.Contains(item.MSiteID), false)
                                    .Count();
                                //计算油液数据类型总数
                                result.OilDataTypeCount = 0;
                                //计算过程量数据类型总数
                                result.PowerDataTypeCount = 0;
                            }
                        }
                    }
                }
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response.IsSuccessful = false;
                response.Code = "000000";
                return response;
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
            BaseResponse<GetTopThreeDAUInfoByUserIDResult> response = new BaseResponse<GetTopThreeDAUInfoByUserIDResult>();
            GetTopThreeDAUInfoByUserIDResult result = new GetTopThreeDAUInfoByUserIDResult();
            response.IsSuccessful = true;
            response.Result = result;

            if (parameter.UserID < -1)
            {
                response.IsSuccessful = false;
                response.Code = "000000";
                return response;
            }
            if (parameter.UserID == -1)
            {
                parameter.UserID = 1011;
            }

            try
            {
                //获取用户关联的传感器信息集合
                var userRelaWSIDList = userRalationWSRepository
                    .GetDatas<UserRelationWS>(item => item.UserID == parameter.UserID, false)
                    .Select(item => item.WSID)
                    .ToList();
                if (userRelaWSIDList.Any())
                {
                    //根据传感器ID获取网关ID
                    var wgIDList = wsRepository
                        .GetDatas<WS>(item => userRelaWSIDList.Contains(item.WSID), false)
                        .Select(item => item.WGID)
                        .ToList();
                    if (wgIDList.Any())
                    {
                        //获取所有的网关状态信息
                        var wiredWGIDList = gatewayRepository
                            .GetDatas<Gateway>(item => item.DevFormType == (int)EnumDevFormType.Wired
                                && wgIDList.Contains(item.WGID), false)
                            .Select(item => new { item.WGID, item.WGName, })
                            .ToList();
                        if (wiredWGIDList.Any())
                        {
                            var tempWiredWGIDList = wiredWGIDList.Select(item => item.WGID);
                            //根据有线网关获取有线传感器
                            var wiredWSIDList = wsRepository
                                .GetDatas<WS>(item => tempWiredWGIDList.Contains(item.WGID), false)
                                .Select(item => new
                                {
                                    item.WGID,
                                    item.WSID,
                                    item.WSName,
                                    item.ChannelId,
                                    item.DevFormType,
                                })
                                .ToList();
                            if (wiredWSIDList.Any())
                            {
                                //有线网关
                                result.DAUInfoList = wiredWGIDList
                                    .Select(wgInfoItem => new DAUStatisticsInfoResult
                                    {
                                        //网关ID
                                        WGID = wgInfoItem.WGID,
                                        //网关名称
                                        WGName = wgInfoItem.WGName,
                                        //振动传感器
                                        VibrationWSInfo = wiredWSIDList
                                            .Where(wsInfoItem => wsInfoItem.WGID == wgInfoItem.WGID
                                                && wsInfoItem.ChannelId >= 0 && wsInfoItem.ChannelId <= 15)
                                            .Select(vibWSInfoItem => new DAUDataSensorStatisticsInfo
                                            {
                                                WSID = vibWSInfoItem.WSID,
                                                WSName = vibWSInfoItem.WSName,
                                            })
                                            .ToList(),
                                        //转速传感器
                                        SpeedWSInfo = wiredWSIDList
                                            .Where(wsInfoItem => wsInfoItem.WGID == wgInfoItem.WGID
                                                && wsInfoItem.ChannelId >= 20 && wsInfoItem.ChannelId <= 21)
                                            .Select(vibWSInfoItem => new DAUDataSensorStatisticsInfo
                                            {
                                                WSID = vibWSInfoItem.WSID,
                                                WSName = vibWSInfoItem.WSName,
                                            })
                                            .ToList(),
                                        //油液传感器
                                        OilWSInfo = new List<DAUDataSensorStatisticsInfo>(),
                                        //过程量传感器
                                        PowerWSInfo = wiredWSIDList
                                            .Where(wsInfoItem => wsInfoItem.WGID == wgInfoItem.WGID
                                                && wsInfoItem.ChannelId >= 16 && wsInfoItem.ChannelId <= 19)
                                            .Select(vibWSInfoItem => new DAUDataSensorStatisticsInfo
                                            {
                                                WSID = vibWSInfoItem.WSID,
                                                WSName = vibWSInfoItem.WSName,
                                            })
                                            .ToList(),
                                    })
                                    .ToList();
                            }
                        }
                    }
                }
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response.IsSuccessful = false;
                response.Code = "000000";
                return response;
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
            BaseResponse<GetWSMaintainReportSimpleInfoResult> response = new BaseResponse<GetWSMaintainReportSimpleInfoResult>();
            GetWSMaintainReportSimpleInfoResult result = new GetWSMaintainReportSimpleInfoResult();
            response.IsSuccessful = true;
            response.Result = result;

            if (parameter.UserID == -1)
            {
                parameter.UserID = 1011;
            }

            try
            {
                try
                {
                    using (iCMSDbContext dbContext = new iCMSDbContext())
                    {
                        #region 查询当前用户未读的维修日志信息

                        string sql = string.Format(ConstObject.SQL_GetWSMaintainSimpleInfo, parameter.UserID);

                        var list = dbContext.Database.SqlQuery<MaintainReportSimpleInfoResult>(sql).ToList();

                        result.MaintainReport.AddRange(list);

                        #endregion

                        #region 查询当前日志所有的维修日志的总数

                        //查询当前日志所有的维修日志的总数ID
                        result.Total =
                            (from userRelaWS in dbContext.UserRelationWS
                             join report in dbContext.MaintainReport
                                on userRelaWS.WSID equals report.DeviceID
                             where !report.IsTemplate && userRelaWS.UserID == parameter.UserID
                             select 1)
                             .Count();

                        #endregion

                        response.Result = result;

                        return response;
                    }
                }
                catch (Exception e)
                {
                    LogHelper.WriteLog(e);
                    response.IsSuccessful = false;
                    response.Code = "000000";
                    //response.Reason = "获取用户未读的维修报告发生异常";
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response.IsSuccessful = false;
                response.Code = "000000";
                return response;
            }
        }

        #endregion

        #region 实时数据相关服务 Added by QXM, 2018/05/22

        #region 获取设备实时数据
        /// <summary>
        /// 获取设备实时数据， Added by QXM, 2018/04/24
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public BaseResponse<GetDeviceRealTimeDataResult> GetDeviceRealTimeData(DeviceRealTimeDataParameter param)
        {
            return null;
        }
        #endregion

        #region 排序当前用户所管理的设备顺序
        public BaseResponse<bool> OrderDeviceByUserIDAndDevcieID(OrderDeviceByUserIDAndDevcieIDParameter param)
        {
            return null;
        }
        #endregion

        #region 获取用户设备顺序
        public BaseResponse<GetOrderDeviceByUserIDResult> GetOrderDeviceByUserID(GetOrderDeviceByUserIDParameter param)
        {
            return null;
        }
        #endregion

        #region 获取当前实时数据配置项
        public BaseResponse<GetConfigTreeDevStateResult> GetConfigTreeDevState()
        {
            return null;
        }
        #endregion

        #region 设置当前实时数据配置
        public BaseResponse<bool> SetConfigDevState(SetConfigDevStateParameter param)
        {
            return null;
        }
        #endregion

        #region 设置设备实时数据是否显示停机设备
        public BaseResponse<bool> SetStopedDevIsShow(SetStopedDevIsShowParameter param)
        {
            return null;
        }
        #endregion

        #region 通过pid查询机组下的所有设备
        public BaseResponse<GetDevInfoByGroupIDResult> GetDevInfoByGroupID(GetDevInfoByGroupIDParameter param)
        {
            return null;
        }
        #endregion

        #endregion
    }
    #endregion
}