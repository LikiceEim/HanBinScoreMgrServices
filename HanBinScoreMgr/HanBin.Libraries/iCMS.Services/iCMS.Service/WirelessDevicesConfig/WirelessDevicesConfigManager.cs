/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 * 命名空间：iCMS.Service.WirelessDevicesConfig
 * 文件名：  WirelessDevicesConfigManager
 * 创建人：  张辽阔
 * 创建时间：2016-10-26
 * 描述：监测设备配置管理业务处理类
 *
 * 修改人：张辽阔
 * 修改时间：2016-11-15
 * 描述：增加错误编码
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Dynamic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Configuration;

using Microsoft.Practices.Unity;

using iCMS.Common.Component.Data.Request.WirelessDevicesConfig;
using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Tool;
using iCMS.Frameworks.Core.DB.Models;
using iCMS.Service.Common;
using iCMS.Common.Component.Data.Response.WirelessDevicesConfig;
using iCMS.Common.Component.Data.Enum;
using iCMS.Common.Component.Tool.Extensions;
using iCMS.Frameworks.Core.Repository;
using iCMS.Common.Component.Data.Response.Common;

namespace iCMS.Service.Web.WirelessDevicesConfig
{
    #region 监测设备配置实现类
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-10-26
    /// 创建记录：监测设备配置实现类
    /// </summary>
    public class WirelessDevicesConfigManager : IWirelessDevicesConfigManager
    {
        #region 变量
        private readonly IRepository<Gateway> gatewayRepository;
        private readonly IRepository<WS> wsRepository;
        private readonly IRepository<Operation> operationRepository;
        private readonly ICacheDICT cacheDICT;
        private readonly IRepository<UserRelationWSAlmRecord> userRelationWSAlmRecordRepository;
        private readonly IRepository<UserRelationMaintainReport> userRelationMaintainReportRepository;
        private readonly IRepository<Device> deviceRepository;
        private readonly IRepository<WirelessGatewayType> wirelessGatewayTypeRepository;
        private readonly IRepository<MonitorTree> monitorTreeRepository;
        private readonly IRepository<MeasureSite> measureSiteRepository;
        [Dependency]
        public IRepository<UserRelationWS> userRelationWSRepository
        {
            get;
            set;
        }

        #endregion

        #region 构造函数
        public WirelessDevicesConfigManager(IRepository<Gateway> gatewayRepository,
            IRepository<WS> wsRepository,
            IRepository<Operation> operationRepository,
            IRepository<UserRelationWSAlmRecord> userRelationWSAlmRecordRepository,
            IRepository<UserRelationMaintainReport> userRelationMaintainReportRepository,
            IRepository<Device> deviceRepository,
            IRepository<WirelessGatewayType> wirelessGatewayTypeRepository,
            ICacheDICT cacheDICT,
            IRepository<MonitorTree> monitorTreeRepository,
            IRepository<MeasureSite> measureSiteRepository)
        {
            this.gatewayRepository = gatewayRepository;
            this.wsRepository = wsRepository;
            this.operationRepository = operationRepository;
            this.userRelationWSAlmRecordRepository = userRelationWSAlmRecordRepository;
            this.userRelationMaintainReportRepository = userRelationMaintainReportRepository;
            this.deviceRepository = deviceRepository;
            this.wirelessGatewayTypeRepository = wirelessGatewayTypeRepository;
            this.cacheDICT = cacheDICT;
            this.monitorTreeRepository = monitorTreeRepository;
            this.measureSiteRepository = measureSiteRepository;
        }
        #endregion

        #region 无线网关

        #region 获取无线网关信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-26
        /// 创建记录：获取无线网关信息
        /// </summary>
        /// <param name="parameter">获取无线网关信息的请求参数</param>
        /// <returns></returns>
        public BaseResponse<GetWGDataResult> GetWGData(GetWGDataParameter parameter)
        {
            BaseResponse<GetWGDataResult> response = new BaseResponse<GetWGDataResult>();
            try
            {
                dynamic validateResult = Validate.ValidateGetWGDataParams(parameter);
                if (validateResult.Result)
                {
                    response.IsSuccessful = false;
                    response.Code = validateResult.Code;
                    return response;
                }

                if (parameter.sort.ToLower().Equals("WGTypeName".ToLower()))
                {
                    parameter.sort = "WGType";
                }

                if (parameter.page == 0)
                {
                    parameter.page = 1;
                }
                using (var dataContext = new iCMSDbContext())
                {
                    ListSortDirection sortOrder = parameter.order.ToLower().Equals("asc") ? ListSortDirection.Ascending : ListSortDirection.Descending;
                    PropertySortCondition[] sortList = new PropertySortCondition[]
                    {
                        new PropertySortCondition(parameter.sort, sortOrder),
                        new PropertySortCondition("WGID", sortOrder),
                    };
                    IQueryable<Gateway> wgInfoQuerable = dataContext.WG.OrderBy(sortList);

                    if (parameter.DevFormType == -1)// -1:返回所有的无线设备
                    {
                        wgInfoQuerable = wgInfoQuerable.Where(t => t.DevFormType == (int)EnumDevFormType.SingleBoard || t.DevFormType == (int)EnumDevFormType.iWSN);
                    }
                    else if (parameter.DevFormType == -2)//-2: 返回所有的有线设备
                    {
                        wgInfoQuerable = wgInfoQuerable.Where(t => t.DevFormType == (int)EnumDevFormType.Wired);
                    }
                    else
                    {
                        wgInfoQuerable = wgInfoQuerable.Where(t => t.DevFormType == parameter.DevFormType);
                    }

                    if (parameter.page > -1)
                    {
                        wgInfoQuerable = wgInfoQuerable.Skip((parameter.page - 1) * parameter.pageSize)
                            .Take(parameter.pageSize);
                    }

                    var quantity = wgInfoQuerable.Count();
                    var tempWGInfoList = wgInfoQuerable
                        .ToArray()
                        .Select(wg =>
                        {
                            var wgTypeNameObj = cacheDICT.GetInstance().GetCacheType<WirelessGatewayType>(p => p.ID == wg.WGType).FirstOrDefault();
                            string monitorTreeNames = string.Empty;
                            GetMonitorTreeNames(wg.MonitorTreeID ?? 0, ref monitorTreeNames);

                            var nameList = monitorTreeNames.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                            nameList.Reverse();
                            monitorTreeNames = String.Join("#", nameList);

                            return new GetWGDataInfo
                            {
                                WGID = wg.WGID,
                                WGName = wg.WGName,
                                WGFormType = wg.DevFormType,//设备形态
                                WGNO = wg.WGNO.ToString(),
                                NetWorkID = wg.NetWorkID.HasValue ? wg.NetWorkID.Value.ToString() : "",
                                WGTypeId = wg.WGType,
                                LinkStatus = wg.LinkStatus,
                                WGModel = wg.WGModel,
                                SoftwareVersion = wg.SoftwareVersion,
                                RunStatus = wg.RunStatus,
                                ImageID = wg.ImageID,
                                Remark = wg.Remark,
                                MonitorTreeID = wg.MonitorTreeID.HasValue ? wg.MonitorTreeID.Value : 0,
                                AddDate = wg.AddDate.ToString("yyyy-MM-dd HH:mm:ss"),
                                PersonInCharge = wg.PersonInCharge,
                                PersonInChargeTel = wg.PersonInChargeTel,
                                WGTypeName = wgTypeNameObj == null ? string.Empty : wgTypeNameObj.Name,
                                MonitorTreeNames = monitorTreeNames,
                                AgentAddress = wg.AgentAddress,

                                IPAddress = wg.WGIP,
                                Port = wg.WGPort,
                                GateWayMAC = wg.GateWayMAC,
                                SubNetMask = wg.SubNetMask,
                                Gateway = wg.GateWay,
                                SerizeCode = wg.SerizeCode,
                                MainBoardSerizeCode = wg.MainBoardSerizeCode,
                                BESPSerizeCode = wg.BESPSerizeCode,
                                ProductInfoSerizeCode = wg.ProductInfoSerizeCode,
                                PowerSupplySerizeCode = wg.PowerSupplySerizeCode,
                                CoreBoardSerizeCode = wg.CoreBoardSerizeCode,
                                CurrentDAUStates = wg.CurrentDAUStates,
                                MinibootVersion = wg.MinibootVersion,
                                SndbootVersion = wg.SndbootVersion,
                                FirmwareVersion = wg.FirmwareVersion,
                                FPGAVersion = wg.FPGAVersion
                            };
                        })
                        .ToList();

                    GetWGDataResult getWGDataResult = new GetWGDataResult
                    {
                        Total = quantity,
                        WGInfo = tempWGInfoList,
                    };
                    response.IsSuccessful = true;
                    response.Result = getWGDataResult;
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response.IsSuccessful = false;
                response.Code = "004811";
                return response;
            }
        }
        #endregion

        /// <summary>
        /// 根据WG的MonitorTreeID获取监测树名称， #隔开， Added by QXM, 2018/05/04
        /// </summary>
        /// <param name="monitorTreeID"></param>
        /// <param name="monitorTreeNames"></param>
        private void GetMonitorTreeNames(int monitorTreeID, ref string monitorTreeNames)
        {
            var monitorTree = monitorTreeRepository.GetByKey(monitorTreeID);
            if (monitorTree == null)
            {
                return;
            }
            else
            {
                //monitorTreeNames.
                monitorTreeNames += "#";
                monitorTreeNames += monitorTree.Name;

                GetMonitorTreeNames(monitorTree.PID, ref monitorTreeNames);
            }
        }


        #region 获取无线网关下拉列表
        /// <summary>
        /// 创建人：王龙杰
        /// 创建时间：2017-11-08
        /// 创建记录：获取无线网关下拉列表
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<GetWGSelectListResult> GetWGSelectList(GetWGSelectListParameter parameter)
        {
            BaseResponse<GetWGSelectListResult> response = new BaseResponse<GetWGSelectListResult>();
            GetWGSelectListResult result = new GetWGSelectListResult();
            List<WGSelect> WGSelectList = new List<WGSelect>();

            try
            {
                using (var dataContext = new iCMSDbContext())
                {
                    if (parameter.UserID == -1)
                    { parameter.UserID = 1011; }
                    WGSelectList = (from urws in dataContext.UserRelationWS
                                    join ws in dataContext.WS on urws.WSID equals ws.WSID
                                    join wg in dataContext.WG on ws.WGID equals wg.WGID
                                    where urws.UserID == parameter.UserID
                                    select new WGSelect
                                    {
                                        WGID = wg.WGID,
                                        WGName = wg.WGName
                                    }).Distinct().ToList();

                    response.IsSuccessful = true;
                    result.WGSelectList = WGSelectList;
                    response.Result = result;
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response.IsSuccessful = false;
                response.Code = "009631";
                return response;
            }
        }
        #endregion

        #region 添加无线网关信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-27
        /// 创建记录：添加无线网关信息
        /// </summary>
        /// <param name="parameter">添加无线网关信息的请求参数</param>
        /// <returns></returns>
        public BaseResponse<bool> AddWGData(AddWGDataParameter parameter)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();

            try
            {
                Validate validate = new Validate(gatewayRepository, wsRepository, operationRepository, deviceRepository, cacheDICT);

                dynamic validateResult = validate.ValidateAddWGDataParams(parameter);
                if (validateResult.Result)
                {
                    response.IsSuccessful = false;
                    response.Code = validateResult.Code;
                    return response;
                }

                Gateway gateway = validateResult.WGEntity as Gateway;

                try
                {
                    //测试Agent地址是否可用， Added by QXM, 2018/05/22
                    string testResponse = "0";
                    switch (gateway.DevFormType)
                    {
                        case (int)EnumDevFormType.SingleBoard:
                            var singleBoardAgent = InvokeContext.CreateWCFServiceByURL<iCMS.WG.AgentServer.IConfigService>(gateway.AgentAddress, "basichttpbinding");
                            testResponse = singleBoardAgent.IsAccess();
                            break;
                        case (int)EnumDevFormType.iWSN:

                            var iWSNAgent = InvokeContext.CreateWCFServiceByURL<iCMS.IOTGateWay.Agent.Server.IConfigService>(gateway.AgentAddress, "basichttpbinding");
                            testResponse = iWSNAgent.IsAccess();
                            break;
                        case (int)EnumDevFormType.Wired:
                            //var wiredAgent = 
                            RestClient restClient = new RestClient(gateway.AgentAddress);
                            BaseRequest baseRequest = new BaseRequest();
                            BaseResponse<ResponseResult> wiredRes = Json.JsonDeserialize<BaseResponse<ResponseResult>>(restClient.Post(baseRequest.ToClientString(), "CheckAgentService"));

                            break;
                    }
                }
                catch (Exception e)//地址不可用，必然抛出异常， 没有抛出异常，则地址可用
                {
                    response.IsSuccessful = false;
                    //Agent 地址不可用
                    response.Code = "010132";
                    return response;
                  }

                OperationResult result = gatewayRepository.AddNew<Gateway>(gateway);
                if (result.ResultType == EnumOperationResultType.Success)
                {
                    try
                    {
                        //只有无线网关 iMesh网关时候需要重启 Added by QXM,2018/05/21
                        if (gateway.DevFormType == (int)EnumDevFormType.SingleBoard)
                        {
                            var agent = InvokeContext.CreateWCFServiceByURL<iCMS.WG.AgentServer.IConfigService>(gateway.AgentAddress, "basichttpbinding");

                            agent.ReSetAgent();
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog(ex);
                    }
                    finally { }

                    response.IsSuccessful = true;
                    response.Result = true;
                    return response;
                }
                else
                {
                    response.IsSuccessful = false;
                    response.Code = "004821";
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response.IsSuccessful = false;
                response.Code = "004821";
                return response;
            }
        }
        #endregion

        #region 编辑无线网关信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-27
        /// 创建记录：编辑无线网关信息
        /// </summary>
        /// <param name="parameter">编辑无线网关信息的请求参数</param>
        /// <returns></returns>
        public BaseResponse<bool> EditWGData(EditWGDataParameter parameter)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();

            try
            {
                Validate validate = new Validate(gatewayRepository, wsRepository, operationRepository, deviceRepository, cacheDICT);
                dynamic validateResult = validate.ValidateEditWGDataParams(parameter);
                if (validateResult.Result)
                {
                    response.IsSuccessful = false;
                    response.Code = validateResult.Code;
                    return response;
                }
                Gateway gateway = validateResult.WGEntity as Gateway;

                //判断网关已挂靠ws个数
                string newWGTypeName = wirelessGatewayTypeRepository.GetDatas<WirelessGatewayType>(p => p.ID == gateway.WGType, true).
                   Select(s => s.Name).FirstOrDefault();
                if (newWGTypeName != "不限制")
                {
                    int countWS = wsRepository.GetDatas<WS>(p => p.WGID == parameter.WGID, true).Count();
                    int newCountWS = Convert.ToInt32(newWGTypeName);
                    if (countWS > newCountWS)
                    {
                        response.IsSuccessful = false;
                        response.Code = "010022";
                        return response;
                    }
                }

                int count = gatewayRepository
                    .GetDatas<Gateway>(item => item.AgentAddress == gateway.AgentAddress
                        && item.WGID == gateway.WGID, false)
                    .Count();

                OperationResult result = gatewayRepository.Update<Gateway>(gateway);
                if (result.ResultType == EnumOperationResultType.Success)
                {
                    //如果两次一样，则不进行修改
                    if (count == 0)
                    {
                        try
                        {
                            var agent = InvokeContext.CreateWCFServiceByURL<iCMS.WG.AgentServer.IConfigService>(gateway.AgentAddress, "basichttpbinding");
                            agent.ReSetAgent();
                        }
                        catch (Exception ex)
                        {
                            LogHelper.WriteLog(ex);
                        }
                        finally { }
                    }

                    response.IsSuccessful = true;
                    response.Result = true;
                    return response;
                }
                else
                {
                    response.IsSuccessful = false;
                    response.Code = "004831";
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response.IsSuccessful = false;
                response.Code = "004831";
                return response;
            }
        }
        #endregion

        #region 批量删除无线网关信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-27
        /// 创建记录：批量删除无线网关信息
        /// </summary>
        /// <param name="parameter">批量删除无线网关信息的请求参数</param>
        /// <returns></returns>
        public BaseResponse<bool> DeleteWGData(DeleteWGDataParameter parameter)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();

            try
            {
                Validate validate = new Validate(gatewayRepository, wsRepository, operationRepository, deviceRepository, cacheDICT);
                dynamic validateResult = validate.ValidateDeleteWGDataParams(parameter);
                if (validateResult.Result)
                {
                    response.IsSuccessful = false;
                    response.Code = validateResult.Code;
                    return response;
                }

                OperationResult result = gatewayRepository.Delete(p => parameter.WGID.Contains(p.WGID));
                if (result.ResultType == EnumOperationResultType.Success)
                {
                    //删除用户未读网关报警记录关联，以及用户未读网关维修日志关联
                    userRelationWSAlmRecordRepository.Delete(p => p.MonitorDeviceType == 1 && parameter.WGID.Contains(p.MonitorDeviceID));
                    userRelationMaintainReportRepository.Delete(p => p.ReportType == 3 && parameter.WGID.Contains(p.DeviceID));

                    response.IsSuccessful = true;
                    response.Result = true;
                    return response;
                }
                else
                {
                    response.IsSuccessful = false;
                    response.Code = "004841";
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response.IsSuccessful = false;
                response.Code = "004841";
                return response;
            }
        }
        #endregion

        #region 验证Agent是否可以访问
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-28
        /// 创建记录：验证Agent是否可以访问
        /// </summary>
        /// <param name="parameter">验证Agent是否可以访问的请求参数</param>
        /// <returns></returns>
        public BaseResponse<AgentAccessResult> IsAgentAccess(AgentAccessParameter parameter)
        {
            BaseResponse<AgentAccessResult> response = new BaseResponse<AgentAccessResult>();

            ////Test by QXM
            //response.IsSuccessful = true;
            //response.Result = new AgentAccessResult { IsAccess = "1" };
            //return response;

            try
            {
                if (parameter == null || string.IsNullOrWhiteSpace(parameter.agentUrlAddress))
                {
                    response.IsSuccessful = false;
                    response.Result = new AgentAccessResult { IsAccess = "0" };
                    return response;
                }

                var agent = InvokeContext.CreateWCFServiceByURL<iCMS.WG.AgentServer.IConfigService>(parameter.agentUrlAddress, "basichttpbinding");

                response.IsSuccessful = true;
                response.Result = new AgentAccessResult { IsAccess = agent.IsAccess() };
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response.IsSuccessful = false;
                response.Result = new AgentAccessResult { IsAccess = "0" };
                return response;
            }
            finally { }
        }
        #endregion

        #endregion

        #region 无线传感器

        #region 获取无线传感器下拉列表
        /// <summary>
        /// 创建人：王龙杰
        /// 创建时间：2017-11-10
        /// 创建记录：获取无线传感器下拉列表
        /// 
        /// 修改人：QXM
        /// 修改记录：获取传感器列表添加传感器类型参数
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<GetWSSelectListResult> GetWSSelectList(GetWSSelectListParameter parameter)
        {
            BaseResponse<GetWSSelectListResult> response = new BaseResponse<GetWSSelectListResult>();
            GetWSSelectListResult result = new GetWSSelectListResult();
            List<WSSelect> WGSelectList = new List<WSSelect>();

            try
            {
                using (var dataContext = new iCMSDbContext())
                {
                    var wsQuerable = dataContext.WS.AsQueryable();
                    if (parameter.UserID != -1)
                    {
                        wsQuerable =
                            from w in wsQuerable
                            join urws in dataContext.UserRelationWS
                                on w.WSID equals urws.WSID
                            where urws.UserID == parameter.UserID
                            select w;
                    }
                    if (parameter.Type == 1) //无线
                    {
                        wsQuerable = wsQuerable.Where(t => t.DevFormType == (int)EnumWSFormType.WireLessSensor
                            || t.DevFormType == (int)EnumWSFormType.Triaxial);
                    }
                    if (parameter.Type == 2)
                    {
                        wsQuerable = wsQuerable.Where(t => t.DevFormType == (int)EnumWSFormType.WiredSensor);
                    }

                    WGSelectList = wsQuerable.Select(t => new WSSelect
                    {
                        WSID = t.WSID,
                        WSName = t.WSName,
                        WSFormType = t.DevFormType,
                    })
                    .ToList();

                    result.WSSelectList = WGSelectList;
                    response.Result = result;
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response.IsSuccessful = false;
                response.Code = "009631";
                return response;
            }
        }
        #endregion

        #region 获取无线传感器信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-28
        /// 创建记录：获取无线传感器信息
        /// 
        /// 修改人：QXM
        /// 修改时间：2018/05/04
        /// 修改记录：解决方案融合，此方法扩展为获取系统所有传感器信息，故增加Type参数表示获取的传感器类型
        /// </summary>
        /// <param name="parameter">获取无线传感器信息的请求参数</param>
        /// <returns></returns>
        public BaseResponse<GetWSDataResult> GetWSData(GetWSDataParameter parameter)
        {
            BaseResponse<GetWSDataResult> response = new BaseResponse<GetWSDataResult>();

            try
            {
                dynamic validateResult = Validate.ValidateGetWSDataParams(parameter);
                if (validateResult.Result)
                {
                    response.IsSuccessful = false;
                    response.Code = validateResult.Code;
                    return response;
                }

                if (parameter.sort.Equals("AddData"))
                {
                    parameter.sort = "AddDate";
                }

                int count = 0;
                if (parameter.page == 0)
                {
                    parameter.page = 1;
                }
                using (var dataContext = new iCMSDbContext())
                {
                    IQueryable<WS> wsInfoList = dataContext.WS; //dataContext.WS;
                    //过滤用户数据
                    if (parameter.UserID == -1)
                    {
                        parameter.UserID = 1011;
                    }
                    var wsIDListByUser = dataContext.UserRelationWS.Where(item => item.UserID == parameter.UserID).Select(item => item.WSID).ToList();
                    wsInfoList = wsInfoList.Where(item => wsIDListByUser.Contains(item.WSID));

                    if (parameter.page <= -1)
                    {
                        wsInfoList = dataContext.WS;
                        count = wsInfoList.Count();
                    }
                    else
                    {
                        if (parameter.isUseStatus.HasValue)
                        {
                            wsInfoList = wsInfoList.Where(p => p.UseStatus == parameter.isUseStatus);
                        }

                        #region 获取传感器类型参数
                        if (parameter.Type == 1)
                        {
                            wsInfoList = wsInfoList.Where(t => t.DevFormType == (int)EnumWSFormType.WireLessSensor || t.DevFormType == (int)EnumWSFormType.Triaxial);
                        }
                        else if (parameter.Type == 2)
                        {
                            wsInfoList = wsInfoList.Where(t => t.DevFormType == (int)EnumWSFormType.WiredSensor);
                        }
                        #endregion

                        ListSortDirection sortOrder = parameter.order.ToLower().Equals("asc") ? ListSortDirection.Ascending : ListSortDirection.Descending;
                        PropertySortCondition[] sortList = new PropertySortCondition[]
                        {
                            new PropertySortCondition(parameter.sort, sortOrder),
                            new PropertySortCondition("WSID", sortOrder),
                        };
                        wsInfoList = wsInfoList.Where(parameter.page, parameter.pageSize, out count, sortList);
                    }

                    var wsList = wsInfoList.ToList();
                    var sensorTypeList = dataContext.SensorType.Where(item => true).ToList();
                    var wsIDList = wsList.Select(item => item.WSID).ToList();
                    var wgIDList = wsList.Select(item => item.WGID).ToList();
                    var wgList = dataContext.WG.Where(item => wgIDList.Contains(item.WGID)).ToList();
                    var operation = dataContext.Operation.Where(item => wgIDList.Contains(item.WSID.Value)).ToList();

                    var tempWSInfoList = wsList
                        .ToArray()
                        .Select(ws =>
                        {
                            var wgObj = wgList.Where(p => p.WGID == ws.WGID).FirstOrDefault();
                            var sensorTypeObj = sensorTypeList.Where(item => item.ID == ws.SensorType).FirstOrDefault();

                            //2代表升级
                            var operationObj = operation.Where(item => item.WSID == ws.WSID && item.OperationType == 2).OrderByDescending(item => item.AddDate).FirstOrDefault();

                            var operationStatus = 0;
                            var operationStatusName = string.Empty;
                            if (operationObj != null)
                            {
                                switch (operationObj.OperationResult)
                                {
                                    case "0":
                                        {
                                            operationStatus = Convert.ToInt32(operationObj.OperationResult);
                                            operationStatusName = "初始状态";
                                        }
                                        break;
                                    case "3":
                                        {
                                            operationStatus = Convert.ToInt32(operationObj.OperationResult);
                                            operationStatusName = "正在升级中";
                                        }
                                        break;
                                    default:
                                        {
                                            if (operationObj.EDate.HasValue)
                                            {
                                                operationStatusName = operationObj.EDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                                            }
                                        }
                                        break;
                                }
                            }

                            return new GetWSDataInfo
                            {
                                WSID = ws.WSID,
                                WGID = ws.WGID,
                                WGName = wgObj == null ? string.Empty : wgObj.WGName,
                                WSNO = ws.WSNO,
                                WSName = ws.WSName,
                                BatteryVolatage = ws.BatteryVolatage,
                                UseStatus = ws.UseStatus,
                                AlmStatus = ws.AlmStatus,
                                MACADDR = ws.MACADDR,
                                SensorTypeId = ws.SensorType,
                                LinkStatus = ws.LinkStatus,
                                OperationStatus = operationStatus,
                                OperationStatusName = operationStatusName,
                                SensorTypeName = sensorTypeObj == null ? string.Empty : sensorTypeObj.Name,
                                AddData = ws.AddDate.ToString("yyyy-MM-dd HH:mm:ss"),
                                Vendor = ws.Vendor,
                                WSModel = ws.WSModel,
                                SetupTime = ws.SetupTime.HasValue ? ws.SetupTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty,
                                SetupPersonInCharge = ws.SetupPersonInCharge,
                                SNCode = ws.SNCode,
                                FirmwareVersion = ws.FirmwareVersion,
                                AntiExplosionSerialNo = ws.AntiExplosionSerialNo,
                                RunStatus = ws.RunStatus,
                                ImageID = ws.ImageID,
                                PersonInCharge = ws.PersonInCharge,
                                PersonInChargeTel = ws.PersonInChargeTel,
                                Remark = ws.Remark,

                                WSFormType = ws.DevFormType,
                                Axial = ws.Axis,
                                AxialName = ws.AxisName,
                                ChannelID = ws.ChannelId,
                                SensorCollectType = ws.SensorCollectType
                            };
                        })
                        .ToList();

                    GetWSDataResult getWSDataResult = new GetWSDataResult
                    {
                        Total = count,
                        WSInfo = tempWSInfoList,
                    };
                    response.IsSuccessful = true;
                    response.Result = getWSDataResult;
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response.IsSuccessful = false;
                response.Code = "004851";
                return response;
            }
        }
        #endregion

        #region 添加无线传感器信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-28
        /// 创建记录：添加无线传感器信息
        /// </summary>
        /// <param name="parameter">添加无线传感器信息的请求参数</param>
        /// <returns></returns>
        public BaseResponse<bool> AddWSData(AddWSDataParameter parameter)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();

            try
            {
                Validate validate = new Validate(gatewayRepository, wsRepository, operationRepository, deviceRepository, cacheDICT);
                dynamic validateResult = validate.ValidateAddWSDataParams(parameter);
                if (validateResult.Result)
                {
                    response.IsSuccessful = false;
                    response.Code = validateResult.Code;
                    return response;
                }
                var ws = validateResult.WSEntity as WS;
                OperationResult result = wsRepository.AddNew<WS>(ws);
                if (result.ResultType == EnumOperationResultType.Success)
                {
                    //添加用户和传感器关系表
                    if (parameter.UserId > 0)
                    {
                        if (parameter.UserId == -1)
                        {
                            UserRelationWS userRelationWS = new UserRelationWS();
                            userRelationWS.UserID = 1011;//添加超级管理员
                            userRelationWS.WSID = ws.WSID;
                            userRelationWSRepository.AddNew(userRelationWS);
                        }
                        else
                        {
                            UserRelationWS userRelationWS = new UserRelationWS();
                            userRelationWS.UserID = parameter.UserId;
                            userRelationWS.WSID = ws.WSID;
                            userRelationWSRepository.AddNew(userRelationWS);
                        }
                    }
                    response.IsSuccessful = true;
                    response.Result = true;
                    return response;
                }
                else
                {
                    response.IsSuccessful = false;
                    response.Code = "004861";
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response.IsSuccessful = false;
                response.Code = "004861";
                return response;
            }
        }
        #endregion

        #region 编辑无线传感器信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-28
        /// 创建记录：编辑无线传感器信息
        /// </summary>
        /// <param name="parameter">添加无线传感器信息的请求参数</param>
        /// <returns></returns>
        public BaseResponse<bool> EditWSData(EditWSDataParameter parameter)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();

            try
            {
                Validate validate = new Validate(gatewayRepository, wsRepository, operationRepository, deviceRepository, cacheDICT);
                dynamic validateResult = validate.ValidateEditWSDataParams(parameter);
                if (validateResult.Result)
                {
                    response.IsSuccessful = false;
                    response.Code = validateResult.Code;
                    return response;
                }
                WS ws = validateResult.WSEntity as WS;
                int count = gatewayRepository
                    .GetDatas<WS>(item => item.MACADDR == ws.MACADDR
                        && item.WSID == ws.WSID, false)
                    .Count();
                OperationResult result = wsRepository.Update<WS>(ws);
                if (result.ResultType == EnumOperationResultType.Success)
                {
                    #region 系统融合添加， 如果更改的是三轴传感器，则需要同步修改公共信息
                    //Added by QXM, 2018/05/08
                    if (ws.DevFormType == (int)EnumWSFormType.Triaxial)
                    {
                        var otherRelatdWS = wsRepository.GetDatas<WS>(t => t.MACADDR.ToUpper().Equals(parameter.OriginMACADDR.ToUpper()), true).ToList();

                        if (otherRelatdWS.Any())
                        {
                            foreach (WS innerWS in otherRelatdWS)
                            {
                                innerWS.WSName = parameter.WSName;
                                innerWS.MACADDR = parameter.MACADDR;
                                innerWS.SensorType = parameter.SensorTypeId;
                                innerWS.WGID = parameter.WGID;
                                //innerWS.SetupTime = //parameter.SetupTime;
                                innerWS.Vendor = parameter.Vendor;
                                innerWS.AntiExplosionSerialNo = parameter.AntiExplosionSerialNo;
                                innerWS.SNCode = parameter.SNcode;
                                innerWS.PersonInCharge = parameter.PersonInCharge;
                                innerWS.PersonInChargeTel = parameter.PersonInChargeTel;
                                innerWS.SensorCollectType = parameter.SensorCollectType;

                                result = wsRepository.Update<WS>(innerWS);
                            }
                        }
                    }
                    #endregion

                    //如果两次一样，则不进行验证
                    if (count == 0)
                    {
                        try
                        {
                            Gateway gateway = gatewayRepository.GetDatas<Gateway>(item => item.WGID == ws.WGID, false).FirstOrDefault();
                            if (gateway != null)
                            {
                                var agent = InvokeContext.CreateWCFServiceByURL<iCMS.WG.AgentServer.IConfigService>(gateway.AgentAddress, "basichttpbinding");
                                agent.ReSetAgent();
                            }
                        }
                        catch (Exception ex)
                        {
                            LogHelper.WriteLog(ex);
                        }
                        finally { }
                    }

                    response.IsSuccessful = true;
                    response.Result = true;
                    return response;
                }
                else
                {
                    response.IsSuccessful = false;
                    response.Code = "004871";
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response.IsSuccessful = false;
                response.Code = "004871";
                return response;
            }
        }
        #endregion

        #region 批量删除无线传感器信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-28
        /// 创建记录：批量删除无线传感器信息
        /// </summary>
        /// <param name="parameter">批量删除无线传感器信息的请求参数</param>
        /// <returns></returns>
        public BaseResponse<bool> DeleteWSData(DeleteWSDataParameter parameter)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();

            try
            {
                var wsList = wsRepository.GetDatas<WS>(t => true, true).ToList();
                #region 三轴传感器 删除， 特殊处理：删除时候同时删除， Added by QXM, 2018/05/16

                List<int> additionalWSIDList = new List<int>();//存储三轴传感器其他轴向ID信息
                foreach (int wsid in parameter.WSID)
                {
                    WS ws = wsList.Where(t => t.WSID == wsid).FirstOrDefault();
                    if (ws != null && ws.DevFormType == (int)EnumWSFormType.Triaxial)
                    {
                        //如果是三轴，找到其他轴向WS并添加
                        var wsRelated = wsList.Where(t => !string.IsNullOrEmpty(t.MACADDR)
                            && t.WSID != ws.WSID &&
                            t.MACADDR.ToUpper().Equals(ws.MACADDR.ToUpper())).Select(t => t.WSID).ToList();
                        additionalWSIDList.AddRange(wsRelated);
                    }
                }

                if (additionalWSIDList.Any())
                {
                    parameter.WSID.AddRange(additionalWSIDList);
                }

                #endregion

                Validate validate = new Validate(gatewayRepository, wsRepository, operationRepository, deviceRepository, cacheDICT);
                dynamic validateResult = validate.ValidateDeleteWSDataParams(parameter);
                if (validateResult.Result)
                {
                    response.IsSuccessful = false;
                    response.Code = validateResult.Code;
                    return response;
                }

                OperationResult result = wsRepository.Delete(t => parameter.WSID.Contains(t.WSID)); ;
                if (result.ResultType == EnumOperationResultType.Success)
                {
                    //删除关联关系，用户未读网关报警记录关联，以及用户未读网关维修日志关联
                    userRelationWSAlmRecordRepository.Delete(p => p.MonitorDeviceType == 2 && parameter.WSID.Contains(p.MonitorDeviceID));
                    userRelationMaintainReportRepository.Delete(p => p.ReportType == 2 && parameter.WSID.Contains(p.DeviceID));
                    userRelationWSRepository.Delete(item => parameter.WSID.Contains(item.WSID));

                    response.IsSuccessful = true;
                    response.Result = true;
                    return response;
                }
                else
                {
                    response.IsSuccessful = false;
                    response.Code = "004881";
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "004881";
                return response;
            }
        }
        #endregion

        #region 获取某一网关下的无线传感器的状态信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-28
        /// 创建记录：获取某一网关下的无线传感器的状态信息
        /// </summary>
        /// <param name="parameter">获取某一网关下的无线传感器的状态信息的请求参数</param>
        /// <returns></returns>
        public BaseResponse<WSStatusInfoResult> GetWSStatusInfoByWGNO(WSStatusInfoByWGNOParameter parameter)
        {
            BaseResponse<WSStatusInfoResult> response = new BaseResponse<WSStatusInfoResult>();

            try
            {
                if (parameter == null)
                {
                    response.IsSuccessful = false;
                    response.Code = "004892";
                    return response;
                }

                OperTimeOut();

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@WGNO", parameter.WGNO);
                sqlParam[1] = new SqlParameter("@CMDType", parameter.CMDType);
                OperationResult operationResult = operationRepository
                    .SqlQuery<WSStatusInfo>("EXEC SP_GetWSStatsInfoByWGNO @WGNO,@CMDType", sqlParam);
                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    response.IsSuccessful = true;
                    response.Result = new WSStatusInfoResult { WSStatusInfos = operationResult.AppendData as List<WSStatusInfo> };
                    return response;
                }
                else
                {
                    response.IsSuccessful = false;
                    response.Code = "004902";
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "004911";
                return response;
            }
        }
        #endregion

        #region 获取多个设备下的无线传感器的状态信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-31
        /// 创建记录：获取多个设备下的无线传感器的状态信息
        /// </summary>
        /// <param name="parameter">获取多个设备下的无线传感器的状态信息的请求参数</param>
        /// <returns></returns>
        public BaseResponse<WSStatusInfoResult> GetWSStatusInfoByDevice(WSStatusInfoByDeviceParameter parameter)
        {
            BaseResponse<WSStatusInfoResult> response = new BaseResponse<WSStatusInfoResult>();

            try
            {
                if (parameter == null)
                {
                    response.IsSuccessful = false;
                    response.Code = "004922";
                    return response;
                }

                string[] deviceIDstrArray = parameter.DeviceIDs.Split(',');
                int[] deviceIDintArray = new int[deviceIDstrArray.Length];
                deviceIDintArray = Array.ConvertAll<string, int>(deviceIDstrArray, s => Convert.ToInt32(s));

                if (parameter.OpType == 1)
                {
                    GetWSStatus(deviceIDintArray);
                }
                OperTimeOut();

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@DeviceID", parameter.DeviceIDs);
                sqlParam[1] = new SqlParameter("@CMDType", parameter.CMDType);
                OperationResult operationResult = operationRepository
                    .SqlQuery<WSStatusInfo>("EXEC SP_GetWsStatusInfoByDeviceID @DeviceID,@CMDType", sqlParam);
                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    response.IsSuccessful = true;
                    response.Result = new WSStatusInfoResult { WSStatusInfos = operationResult.AppendData as List<WSStatusInfo> };
                    return response;
                }
                else
                {
                    LogHelper.WriteLog("执行存储出错：" + operationResult.Message);
                    response.IsSuccessful = false;
                    response.Code = "004932";
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "004941";
                return response;
            }
        }
        #endregion

        #region 获取某一测点下的无线传感器的状态信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-31
        /// 创建记录：获取某一测点下的无线传感器的状态信息
        /// </summary>
        /// <param name="parameter">获取某一测点下的无线传感器的状态信息的请求参数</param>
        /// <returns></returns>
        public BaseResponse<WSStatusInfoResult> GetWSStatusInfoByMSite(WSStatusInfoByMSiteParameter parameter)
        {
            BaseResponse<WSStatusInfoResult> response = new BaseResponse<WSStatusInfoResult>();

            try
            {
                if (parameter == null)
                {
                    response.IsSuccessful = false;
                    response.Code = "004952";
                    return response;
                }

                OperTimeOut();

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@MSiteID", parameter.MSiteID);
                sqlParam[1] = new SqlParameter("@CMDType", parameter.CMDType);
                OperationResult operationResult = operationRepository
                    .SqlQuery<WSStatusInfo>("EXEC SP_GetWsStatusInfoByMSiteID @MSiteID,@CMDType", sqlParam);
                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    response.IsSuccessful = true;
                    response.Result = new WSStatusInfoResult { WSStatusInfos = operationResult.AppendData as List<WSStatusInfo> };
                    return response;
                }
                else
                {
                    LogHelper.WriteLog("执行存储出错：" + operationResult.Message);
                    response.IsSuccessful = false;
                    response.Code = "004962";
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "004971";
                return response;
            }
        }
        #endregion

        #region 获取1+个无线传感器的状态信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-31
        /// 创建记录：获取1+个无线传感器的状态信息
        /// </summary>
        /// <param name="parameter">获取1+个无线传感器的状态信息的请求参数</param>
        /// <returns></returns>
        public BaseResponse<WSStatusInfoResult> GetWSStatusInfo(WSStatusInfoParameter parameter)
        {
            BaseResponse<WSStatusInfoResult> response = new BaseResponse<WSStatusInfoResult>();

            try
            {
                if (parameter == null)
                {
                    response.IsSuccessful = false;
                    response.Code = "004982";
                    return response;
                }

                if (parameter.WSMACList == null)
                {
                    response.IsSuccessful = false;
                    response.Code = "004982";
                    return response;
                }

                string[] wsMACsStrArray = parameter.WSMACList.Split(',');
                if (parameter.OpType == 1)
                {
                    GetWSStatus(null, wsMACsStrArray, parameter.Type);
                }
                OperTimeOut();

                SqlParameter[] sqlParam = new SqlParameter[2];
                OperationResult operationResult = null;
                switch (parameter.Type)
                {
                    case 1:
                        string macStr = string.Empty;
                        for (int i = 0; i < wsMACsStrArray.Length; i++)
                        {
                            macStr += "'" + wsMACsStrArray[i] + "',";
                        }
                        macStr = macStr.Remove(macStr.LastIndexOf(","), 1);
                        sqlParam[0] = new SqlParameter("@MAC", macStr);
                        sqlParam[1] = new SqlParameter("@CMDType", parameter.CMDType);
                        operationResult = operationRepository
                            .SqlQuery<WSStatusInfo>("EXEC SP_GetWsStatusInfoByMAC @MAC,@CMDType", sqlParam);
                        break;

                    case 2:
                        //如果数据为空时，刚报错，进行修改 2016-12-30 王颖辉
                        sqlParam[0] = new SqlParameter("@WSID", parameter.WSMACList == "" ? "0" : parameter.WSMACList);
                        sqlParam[1] = new SqlParameter("@CMDType", parameter.CMDType);
                        operationResult = operationRepository
                            .SqlQuery<WSStatusInfo>("EXEC SP_GetWsStatusInfoByWSID @WSID,@CMDType", sqlParam);
                        break;

                    default:
                        break;
                }
                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    response.IsSuccessful = true;
                    response.Result = new WSStatusInfoResult { WSStatusInfos = operationResult.AppendData as List<WSStatusInfo> };
                    return response;
                }
                else
                {
                    LogHelper.WriteLog("执行存储出错：" + operationResult.Message);
                    response.IsSuccessful = false;
                    response.Code = "004992";
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "005001";
                return response;
            }
        }
        #endregion

        #region 获取同一操作标识Key值下的无线传感器的状态信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-31
        /// 创建记录：获取同一操作标识Key值下的无线传感器的状态信息
        /// </summary>
        /// <param name="parameter">获取同一操作标识Key值下的无线传感器的状态信息的请求参数</param>
        /// <returns></returns>
        public BaseResponse<WSStatusInfoResult> GetWSStatusInfoByKey(WSStatusInfoByKeyParameter parameter)
        {
            BaseResponse<WSStatusInfoResult> response = new BaseResponse<WSStatusInfoResult>();

            try
            {
                if (parameter == null)
                {
                    response.IsSuccessful = false;
                    response.Code = "005012";
                    return response;
                }
                OperTimeOut();

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@Key", parameter.WSKey);
                sqlParam[1] = new SqlParameter("@CMDType", parameter.CMDType);
                OperationResult operationResult = operationRepository
                    .SqlQuery<WSStatusInfo>("EXEC SP_GetWsStatusInfoByKey @Key,@CMDType", sqlParam);
                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    response.IsSuccessful = true;
                    response.Result = new WSStatusInfoResult { WSStatusInfos = operationResult.AppendData as List<WSStatusInfo> };
                    return response;
                }
                else
                {
                    LogHelper.WriteLog("执行存储出错：" + operationResult.Message);
                    response.IsSuccessful = false;
                    response.Code = "005022";
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "005031";
                return response;
            }
        }
        #endregion

        #endregion

        #region 公共私有方法

        #region 判断Operation表中下发测量定义和升级是否超时
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-28
        /// 创建记录：判断Operation表中下发测量定义和升级是否超时
        /// </summary>
        private void OperTimeOut()
        {
            try
            {
                List<Operation> operationList = operationRepository.GetDatas<Operation>(p => p.OperationResult == "3", false).ToList();
                if (operationList != null || operationList.Count > 0)
                {
                    foreach (Operation operation in operationList)
                    {
                        switch (operation.OperationType)
                        {
                            case 1:
                                TimeSpan tsMeasDef = DateTime.Now - Convert.ToDateTime(operation.Bdate);
                                if (tsMeasDef.Days > 0 || tsMeasDef.Hours > 0)
                                {
                                    operation.OperationResult = EnumOperationStatus.NoResponseTimeOut.ToString("d");
                                    operation.OperationReson = "下发测量定义后1分钟后WS无响应！";
                                    operation.EDate = System.DateTime.Now;
                                    operationRepository.Update(operation);
                                }
                                else
                                {
                                    if (tsMeasDef.Minutes >= Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MeasDefTimeOut"].ToString()))
                                    {
                                        operation.OperationResult = EnumOperationStatus.NoResponseTimeOut.ToString("d");
                                        operation.OperationReson = "下发测量定义后1分钟后WS无响应！";
                                        operation.EDate = System.DateTime.Now;
                                        operationRepository.Update(operation);
                                    }
                                }
                                break;

                            case 2:
                                TimeSpan tsUpdateFirmware = DateTime.Now - Convert.ToDateTime(operation.Bdate);
                                if (tsUpdateFirmware.Days > 0 || tsUpdateFirmware.Hours > 0)
                                {
                                    operation.OperationResult = EnumOperationStatus.NoResponseTimeOut.ToString("d");
                                    operation.OperationReson = "下发升级后30分钟后WS无响应！";
                                    operation.EDate = System.DateTime.Now;
                                    operationRepository.Update(operation);
                                }
                                else
                                {
                                    if (tsUpdateFirmware.Minutes >= Convert.ToInt32(ConfigurationManager.AppSettings["UpdateFirmwareTimeOut"].ToString()))
                                    {
                                        operation.OperationResult = EnumOperationStatus.NoResponseTimeOut.ToString("d");
                                        operation.OperationReson = "下发升级后30分钟后WS无响应！";
                                        operation.EDate = System.DateTime.Now;
                                        operationRepository.Update(operation);
                                    }
                                }
                                break;

                            case 3://触发式上传
                                TimeSpan tsUpdateTrigger = DateTime.Now - Convert.ToDateTime(operation.Bdate);
                                if (tsUpdateTrigger.Days > 0 || tsUpdateTrigger.Hours > 0)
                                {
                                    operation.OperationResult = EnumOperationStatus.NoResponseTimeOut.ToString("d");
                                    operation.OperationReson = "下发升级后30分钟后WS无响应！";
                                    operation.EDate = System.DateTime.Now;
                                    operationRepository.Update(operation);
                                }
                                else
                                {
                                    if (tsUpdateTrigger.Minutes >= Convert.ToInt32(ConfigurationManager.AppSettings["UpdateTriggerTimeOut"].ToString()))
                                    {
                                        operation.OperationResult = EnumOperationStatus.NoResponseTimeOut.ToString("d");
                                        operation.OperationReson = "下发升级后30分钟后WS无响应！";
                                        operation.EDate = System.DateTime.Now;
                                        operationRepository.Update(operation);
                                    }
                                }
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
            }
        }
        #endregion

        #region 手动刷新调用Agent刷新WS状态
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-31
        /// 创建记录：手动刷新调用Agent刷新WS状态
        /// </summary>
        /// <param name="deviceIDArray"></param>
        /// <param name="wsMACsstrArray"></param>
        /// <param name="type"></param>
        private void GetWSStatus(int[] deviceIDArray = null, string[] wsMACsstrArray = null, int? type = null)
        {
            try
            {
                if (deviceIDArray != null && wsMACsstrArray == null && type == null)
                {
                    using (var dataContext = new iCMSDbContext())
                    {
                        var wgids =
                            (from measuresite in dataContext.Measuresite
                             from ws in dataContext.WS
                             where measuresite.WSID == ws.WSID && deviceIDArray.Contains(measuresite.DevID)
                             select new
                             {
                                 ws.WGID
                             })
                            .Distinct()
                            .ToList();
                        foreach (var wg in wgids)
                        {
                            try
                            {
                                //通过网关获取Agent地址
                                string agentAddress = string.Empty;
                                Gateway gateway = gatewayRepository.GetDatas<Gateway>(p => p.WGID == wg.WGID, false).FirstOrDefault();
                                if (gateway != null)
                                {
                                    agentAddress = gateway.AgentAddress;
                                }

                                //为空时不进行操作
                                if (!string.IsNullOrEmpty(agentAddress))
                                {
                                    iCMS.WG.AgentServer.IConfigService server =
                                        InvokeContext.CreateWCFServiceByURL<iCMS.WG.AgentServer.IConfigService>(agentAddress, "basichttpbinding");
                                    server.GetAllWSInfo();
                                }
                            }
                            catch (Exception ex)
                            {
                                Exception e = new Exception("WGID：" + wg.WGID.ToString() + "：" + ex.Message);
                                LogHelper.WriteLog(e);
                            }
                        }
                    }
                }
                if (deviceIDArray == null && wsMACsstrArray != null && type != null)
                {
                    switch (type)
                    {
                        case 1:
                            using (var dataContext = new iCMSDbContext())
                            {

                                var wgids =
                                    (from ws in dataContext.WS
                                     where wsMACsstrArray.Contains(ws.MACADDR)
                                     select new
                                     {
                                         ws.WGID
                                     })
                                    .Distinct()
                                    .ToList();
                                foreach (var wg in wgids)
                                {
                                    try
                                    {
                                        //通过网关获取Agent地址
                                        string agentAddress = string.Empty;
                                        Gateway gateway = gatewayRepository.GetDatas<Gateway>(p => p.WGID == wg.WGID, false).FirstOrDefault();
                                        if (gateway != null)
                                        {
                                            agentAddress = gateway.AgentAddress;
                                        }

                                        //为空时不进行操作
                                        if (!string.IsNullOrEmpty(agentAddress))
                                        {
                                            iCMS.WG.AgentServer.IConfigService server =
                                                InvokeContext.CreateWCFServiceByURL<iCMS.WG.AgentServer.IConfigService>(agentAddress, "basichttpbinding");
                                            server.GetAllWSInfo();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Exception e = new Exception("WGID：" + wg.WGID.ToString() + "：" + ex.Message);
                                        LogHelper.WriteLog(e);
                                    }
                                }
                            }
                            break;

                        case 2:
                            using (var dataContext = new iCMSDbContext())
                            {
                                List<int> wsIdList = new List<int>();
                                foreach (string id in wsMACsstrArray)
                                {
                                    if (!string.IsNullOrEmpty(id))
                                    {
                                        int wsId = 0;
                                        int.TryParse(id, out wsId);
                                        wsIdList.Add(wsId);
                                    }
                                }

                                var wgids =
                                    (from ws in dataContext.WS
                                     where wsIdList.Contains(ws.WSID)
                                     select new
                                     {
                                         ws.WGID
                                     })
                                    .Distinct()
                                    .ToList();
                                foreach (var wg in wgids)
                                {
                                    try
                                    {
                                        //通过网关获取Agent地址
                                        string agentAddress = string.Empty;
                                        Gateway gateway = gatewayRepository.GetDatas<Gateway>(p => p.WGID == wg.WGID, false).FirstOrDefault();
                                        if (gateway != null)
                                        {
                                            agentAddress = gateway.AgentAddress;
                                        }

                                        //为空时不进行操作
                                        if (!string.IsNullOrEmpty(agentAddress))
                                        {
                                            iCMS.WG.AgentServer.IConfigService server =
                                                InvokeContext.CreateWCFServiceByURL<iCMS.WG.AgentServer.IConfigService>(agentAddress, "basichttpbinding");
                                            server.GetAllWSInfo();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Exception e = new Exception("WGID：" + wg.WGID.ToString() + "：" + ex.Message);
                                        LogHelper.WriteLog(e);
                                    }
                                }
                            }
                            break;

                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
            }
        }
        #endregion

        #endregion

        #region 验证方法的参数是否有效

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-26
        /// 创建记录：验证方法的参数是否有效
        /// </summary>
        private class Validate
        {
            #region 变量
            private readonly IRepository<Gateway> gatewayRepository;
            private readonly IRepository<WS> wsRepository;
            private readonly IRepository<Operation> operationRepository;
            private readonly ICacheDICT cacheDICT;
            private readonly IRepository<Device> deviceRepository;
            #endregion

            #region 构造函数
            /// <summary>
            /// 构造函数
            /// </summary>
            public Validate(IRepository<Gateway> gatewayRepository,
                IRepository<WS> wsRepository,
                IRepository<Operation> operationRepository,
                IRepository<Device> deviceRepository,
                ICacheDICT cacheDICT)
            {
                this.gatewayRepository = gatewayRepository;
                this.wsRepository = wsRepository;
                this.operationRepository = operationRepository;
                this.cacheDICT = cacheDICT;
                this.deviceRepository = deviceRepository;
            }
            #endregion

            #region 无线网关验证

            #region 验证“获取无线网关信息”的参数是否有效
            /// <summary>
            /// 创建人：张辽阔
            /// 创建时间：2016-10-26
            /// 创建记录：验证“获取无线网关信息”的参数是否有效
            /// </summary>
            /// <param name="parameter">获取无线网关信息的请求参数</param>
            /// <returns></returns>
            internal static dynamic ValidateGetWGDataParams(GetWGDataParameter parameter)
            {
                dynamic result = new ExpandoObject();
                if (parameter == null)
                {
                    result.Result = true;
                    result.Code = "005042";
                    return result;
                }
                if (string.IsNullOrWhiteSpace(parameter.sort))
                {
                    result.Result = true;
                    result.Code = "005052";
                    return result;
                }
                if (string.IsNullOrWhiteSpace(parameter.order))
                {
                    result.Result = true;
                    result.Code = "005062";
                    return result;
                }
                result.Result = false;
                return result;
            }
            #endregion

            #region 验证“添加无线网关信息”的参数是否有效
            /// <summary>
            /// 创建人：张辽阔
            /// 创建时间：2016-10-27
            /// 创建记录：验证“添加无线网关信息”的参数是否有效
            /// </summary>
            /// <param name="parameter">添加无线网关信息的请求参数</param>
            /// <returns></returns>
            internal dynamic ValidateAddWGDataParams(AddWGDataParameter parameter)
            {
                dynamic result = new ExpandoObject();
                if (parameter == null)
                {
                    result.Result = true;
                    result.Code = "005072";
                    return result;
                }
                if (string.IsNullOrWhiteSpace(parameter.WGName))
                {
                    result.Result = true;
                    result.Code = "005082";
                    return result;
                }
                if (gatewayRepository
                    .GetDatas<Gateway>(p => p.WGName == parameter.WGName, true)
                    .Any())
                {
                    result.Result = true;
                    result.Code = "005092";
                    return result;
                }
                //if (parameter.WGNO <= 5 || parameter.WGNO >= 100)
                //{
                //    result.Result = true;
                //    result.Code = "005102";
                //    return result;
                //}
                if (parameter.WGTypeId <= 0)
                {
                    result.Result = true;
                    result.Code = "005112";
                    return result;
                }
                if (parameter.MonitorTreeID <= 0)
                {
                    result.Result = true;
                    result.Code = "005122";
                    return result;
                }

                #region 负责人和负责人电话不需要为空 2017-03-07 王颖辉
                //if (string.IsNullOrWhiteSpace(parameter.PersonInCharge))
                //{
                //    result.Result = true;
                //    result.Code = "005132";
                //    return result;
                //}
                //if (string.IsNullOrWhiteSpace(parameter.PersonInChargeTel))
                //{
                //    result.Result = true;
                //    result.Code = "005142";
                //    return result;
                //}

                #endregion


                Gateway wgObj = new Gateway();
                wgObj.WGName = parameter.WGName;
                wgObj.WGNO = parameter.WGNO;
                wgObj.WGType = parameter.WGTypeId;
                wgObj.MonitorTreeID = parameter.MonitorTreeID;
                wgObj.PersonInCharge = parameter.PersonInCharge;
                wgObj.PersonInChargeTel = parameter.PersonInChargeTel;
                wgObj.WGModel = parameter.WGModel;
                wgObj.SoftwareVersion = parameter.SoftwareVersion;
                wgObj.Remark = parameter.Remark;
                wgObj.AgentAddress = parameter.AgentAddress;
                wgObj.GateWayMAC = parameter.GateWayMAC;
                wgObj.WGIP = parameter.IPAddress;
                wgObj.WGPort = parameter.Port;
                wgObj.DevFormType = parameter.WGFormType;

                result.Result = false;
                result.WGEntity = wgObj;
                return result;
            }
            #endregion

            #region 验证“编辑无线网关信息”的参数是否有效
            /// <summary>
            /// 创建人：张辽阔
            /// 创建时间：2016-07-28
            /// 创建记录：验证“编辑无线网关信息”的参数是否有效
            /// </summary>
            /// <param name="parameter">编辑无线网关信息的请求参数</param>
            /// <returns></returns>
            internal dynamic ValidateEditWGDataParams(EditWGDataParameter parameter)
            {
                dynamic result = new ExpandoObject();
                if (parameter == null)
                {
                    result.Result = true;
                    result.Code = "005152";
                    return result;
                }
                if (parameter.WGID <= 0)
                {
                    result.Result = true;
                    result.Code = "005162";
                    return result;
                }
                if (string.IsNullOrWhiteSpace(parameter.WGName))
                {
                    result.Result = true;
                    result.Code = "005172";
                    return result;
                }
                if (gatewayRepository
                    .GetDatas<Gateway>(p => p.WGID != parameter.WGID
                        && p.WGName == parameter.WGName,
                        true)
                    .Any())
                {
                    result.Result = true;
                    result.Code = "005182";
                    return result;
                }
                //if (parameter.WGNO <= 5 || parameter.WGNO >= 100)
                //{
                //    result.Result = true;
                //    result.Code = "005192";
                //    return result;
                //}
                if (parameter.WGTypeId <= 0)
                {
                    result.Result = true;
                    result.Code = "005202";
                    return result;
                }
                if (parameter.MonitorTreeID <= 0)
                {
                    result.Result = true;
                    result.Code = "005212";
                    return result;
                }

                #region 负责人和负责人电话不需要为空 2017-03-07 王颖辉
                //if (string.IsNullOrWhiteSpace(parameter.PersonInCharge))
                //{
                //    result.Result = true;
                //    result.Code = "005222";
                //    return result;
                //}
                //if (string.IsNullOrWhiteSpace(parameter.PersonInChargeTel))
                //{
                //    result.Result = true;
                //    result.Code = "005232";
                //    return result;
                //}
                #endregion

                Gateway wgObj = gatewayRepository.GetDatas<Gateway>(p => p.WGID == parameter.WGID, true)
                    .SingleOrDefault();
                if (wgObj == null)
                {
                    result.Result = true;
                    result.Code = "005162";
                    return result;
                }

                wgObj.WGName = parameter.WGName;
                wgObj.WGNO = parameter.WGNO;
                wgObj.WGType = parameter.WGTypeId;
                wgObj.MonitorTreeID = parameter.MonitorTreeID;
                wgObj.PersonInCharge = parameter.PersonInCharge;
                wgObj.PersonInChargeTel = parameter.PersonInChargeTel;
                wgObj.WGModel = parameter.WGModel;
                wgObj.SoftwareVersion = parameter.SoftwareVersion;
                wgObj.Remark = parameter.Remark;
                wgObj.AgentAddress = parameter.AgentAddress;

                wgObj.WGIP = parameter.IPAddress;
                wgObj.WGPort = parameter.Port;
                wgObj.GateWayMAC = parameter.GateWayMAC;

                result.Result = false;
                result.WGEntity = wgObj;
                return result;
            }
            #endregion

            #region 验证“批量删除无线网关信息”的参数是否有效
            /// <summary>
            /// 创建人：张辽阔
            /// 创建时间：2016-10-28
            /// 创建记录：验证“批量删除无线网关信息”的参数是否有效
            /// </summary>
            /// <param name="parameter">批量删除无线网关信息的请求参数</param>
            /// <returns></returns>
            internal dynamic ValidateDeleteWGDataParams(DeleteWGDataParameter parameter)
            {
                dynamic result = new ExpandoObject();
                if (parameter == null)
                {
                    result.Result = true;
                    result.Code = "005242";
                    return result;
                }
                if (parameter.WGID == null || !parameter.WGID.Any())
                {
                    result.Result = true;
                    result.Code = "005252";
                    return result;
                }
                if (wsRepository
                    .GetDatas<WS>(t => parameter.WGID.Contains(t.WGID), true)
                    .Any())
                {
                    result.Result = true;
                    result.Code = "005262";
                    return result;
                }
                if (deviceRepository.GetDatas<DeviceRelationWG>(t => parameter.WGID.Contains(t.WGID), true).Any())
                {
                    result.Result = true;
                    result.Code = "010112";
                    return result;
                }
                result.Result = false;
                return result;
            }
            #endregion

            #endregion

            #region 无线传感器验证

            #region 验证“获取无线传感器信息”的参数是否有效
            /// <summary>
            /// 创建人：张辽阔
            /// 创建时间：2016-10-28
            /// 创建记录：验证“获取无线传感器信息”的参数是否有效
            /// </summary>
            /// <param name="parameter">获取无线传感器信息的请求参数</param>
            /// <returns></returns>
            internal static dynamic ValidateGetWSDataParams(GetWSDataParameter parameter)
            {
                dynamic result = new ExpandoObject();
                if (parameter == null)
                {
                    result.Result = true;
                    result.Code = "005272";
                    return result;
                }
                if (string.IsNullOrWhiteSpace(parameter.sort))
                {
                    result.Result = true;
                    result.Code = "005282";
                    return result;
                }
                if (string.IsNullOrWhiteSpace(parameter.order))
                {
                    result.Result = true;
                    result.Code = "005292";
                    return result;
                }
                result.Result = false;
                return result;
            }
            #endregion

            #region 验证“添加无线传感器信息”的参数是否有效
            /// <summary>
            /// 创建人：张辽阔
            /// 创建时间：2016-10-28
            /// 创建记录：验证“添加无线传感器信息”的参数是否有效
            /// </summary>
            /// <param name="parameter">添加无线传感器信息的请求参数</param>
            /// <returns></returns>
            internal dynamic ValidateAddWSDataParams(AddWSDataParameter parameter)
            {
                dynamic result = new ExpandoObject();
                if (parameter == null)
                {
                    result.Result = true;
                    result.Code = "005302";
                    return result;
                }
                if (string.IsNullOrWhiteSpace(parameter.WSName))
                {
                    result.Result = true;
                    result.Code = "005312";
                    return result;
                }

                if (wsRepository
                    .GetDatas<WS>(p => p.WSName == parameter.WSName, true)
                    .Any())
                {
                    result.Result = true;
                    result.Code = "005322";
                    return result;
                }
                if (string.IsNullOrWhiteSpace(parameter.MACADDR))
                {
                    result.Result = true;
                    result.Code = "005332";
                    return result;
                }
                if (parameter.SensorTypeId <= 0)
                {
                    result.Result = true;
                    result.Code = "005342";
                    return result;
                }
                if (parameter.WGID <= 0)
                {
                    result.Result = true;
                    result.Code = "005352";
                    return result;
                }
                DateTime setupTime;
                if (!DateTime.TryParse(parameter.SetupTime, out setupTime))
                {
                    result.Result = true;
                    result.Code = "005362";
                    return result;
                }
                Gateway gatewayObj = gatewayRepository.GetByKey(parameter.WGID);
                if (gatewayObj == null)
                {
                    result.Result = true;
                    result.Code = "005352";
                    return result;
                }

                WirelessGatewayType gatewayTypeObj = cacheDICT.GetInstance()
                    .GetCacheType<WirelessGatewayType>(p => p.ID == gatewayObj.WGType)
                    .SingleOrDefault();
                if (gatewayTypeObj == null)
                {
                    result.Result = true;
                    result.Code = "005372";
                    return result;
                }
                int wsCount = wsRepository
                    .GetDatas<WS>(p => p.WGID == gatewayObj.WGID, true)
                    .Count();
                int gatewayTypeCount;
                int.TryParse(gatewayTypeObj.Name, out gatewayTypeCount);
                if (wsCount >= gatewayTypeCount)
                {
                    result.Result = true;
                    result.Code = "005382";
                    return result;
                }

                WS wsObj = new WS();
                wsObj.WSName = parameter.WSName;
                wsObj.MACADDR = parameter.MACADDR;
                wsObj.SensorType = parameter.SensorTypeId;
                wsObj.WGID = parameter.WGID;
                wsObj.SetupTime = setupTime;
                wsObj.Vendor = parameter.Vendor;
                wsObj.AntiExplosionSerialNo = parameter.AntiExplosionSerialNo;
                wsObj.SNCode = parameter.SNCode;
                wsObj.PersonInCharge = parameter.PersonInCharge;
                wsObj.PersonInChargeTel = parameter.PersonInChargeTel;
                wsObj.RunStatus = 1;
                wsObj.TriggerStatus = 0;

                wsObj.ChannelId = parameter.ChannelID;
                wsObj.Axis = parameter.Axial;
                wsObj.AxisName = parameter.AxialName;
                wsObj.DevFormType = parameter.WSFormType;
                wsObj.SensorCollectType = parameter.SensorCollectType;


                //添加有线传感器时候，WS连接状态由DAU当前状态决定， Added by QXM, 2018/05/21
                if (parameter.WSFormType == (int)EnumWSFormType.WiredSensor)
                {
                    if (gatewayObj.CurrentDAUStates.HasValue)
                    {
                        int DAUStatus = gatewayObj.CurrentDAUStates.Value;

                        if (DAUStatus == (int)EnumDAUStatus.InitStatus
                            || DAUStatus == (int)EnumDAUStatus.IdleStatus
                            || DAUStatus == (int)EnumDAUStatus.TestStatus
                            || DAUStatus == (int)EnumDAUStatus.ConfigingStatus || DAUStatus == (int)EnumDAUStatus.CollectingStatus)
                        {
                            wsObj.LinkStatus = (int)EnumWSLinkStatus.Connect;
                        }
                    }

                }
                result.Result = false;
                result.WSEntity = wsObj;
                return result;
            }
            #endregion

            #region 验证“添加无线传感器信息”的参数是否有效
            /// <summary>
            /// 创建人：张辽阔
            /// 创建时间：2016-10-28
            /// 创建记录：验证“添加无线传感器信息”的参数是否有效
            /// 
            /// 修改人：QXM
            /// 修改时间：2018/05/08
            /// 修改记录：融合添加三轴传感器，三轴传感器不验证重复
            /// </summary>
            /// <param name="parameter">添加无线传感器信息的请求参数</param>
            /// <returns></returns>
            internal dynamic ValidateAddWSListDataParams(AddWSListDataParameter parameter)
            {
                dynamic result = new ExpandoObject();
                var list = parameter.WSListData;
                if (list == null)
                {
                    result.Result = true;
                    result.Code = "005302";
                    return result;
                }
                if (list.Count() > 0)
                {
                    List<WS> wsList = new List<WS>();
                    List<WSListData> errorList = new List<WSListData>();

                    #region 验证数据重复
                    //验证数据重复
                    List<int> wsRomvoeList = new List<int>();
                    var nameList = list.GroupBy(item => item.WSName).Where(g => g.Count() > 1).Select(item => new
                    {
                        item.Key
                    }).ToList();

                    var macList = list.GroupBy(item => item.MACADDR).Where(g => g.Count() > 1).Select(item => new
                    {
                        item.Key
                    }).ToList();

                    #region 名称重复

                    foreach (var name in nameList)
                    {
                        int count = 0;
                        //判断重复项
                        foreach (var info in list)
                        {
                            //如果是三轴传感器，则不进行验证， Added by QXM, 2018/05/08
                            if (info.WSFormType == (int)EnumWSFormType.Triaxial)
                            {
                                continue;
                            }

                            if (info.WSName == name.Key)
                            {
                                count++;
                                //名称大于一个
                                if (count > 1)
                                {
                                    WSListData error = new WSListData();
                                    error.Code = "005322";
                                    error.Index = info.Index;
                                    errorList.Add(error);
                                }
                            }
                        }

                    }
                    #endregion

                    #region mac重复
                    //mac重复
                    foreach (var mac in macList)
                    {
                        int count = 0;
                        //判断重复项
                        foreach (var info in list)
                        {
                            //如果是三轴传感器，则不进行验证， Added by QXM, 2018/05/08
                            if (info.WSFormType == (int)EnumWSFormType.Triaxial)
                            {
                                continue;
                            }

                            if (info.MACADDR == mac.Key)
                            {
                                count++;
                                //名称大于一个
                                if (count > 1)
                                {

                                    //判断不存在名称里面
                                    if (!errorList.Where(item => item.Index == info.Index).Any())
                                    {
                                        WSListData error = new WSListData();
                                        error.Code = "006112";
                                        error.Index = info.Index;
                                        errorList.Add(error);
                                    }
                                }
                            }

                        }
                    }
                    #endregion

                    #region 删除重复数据
                    //删除重复数据
                    var errorRemovelt = errorList.Select(item => item.Index).ToList();

                    foreach (var info in errorRemovelt)
                    {
                        int index = list.FindIndex(item => item.Index == info);
                        list.RemoveAt(index);
                    }
                    #endregion
                    #endregion

                    foreach (WSData info in list)
                    {
                        if (string.IsNullOrWhiteSpace(info.WSName))
                        {
                            result.Result = true;
                            result.Code = "005312";
                            WSListData error = new WSListData();
                            error.Code = "005312";
                            error.Index = info.Index;
                            errorList.Add(error);
                            continue;
                        }

                        if (info.WSName.Trim().Length > 200)
                        {
                            result.Result = true;
                            result.Code = "008052";
                            WSListData error = new WSListData();
                            error.Code = "008052";
                            error.Index = info.Index;
                            errorList.Add(error);
                            continue;
                        }

                        if (wsRepository
                            .GetDatas<WS>(p => p.WSName == info.WSName, true)
                            .Any())
                        {
                            result.Result = true;
                            result.Code = "005322";
                            WSListData error = new WSListData();
                            error.Code = "005322";
                            error.Index = info.Index;
                            errorList.Add(error);
                            continue;
                        }

                        if (info.WSFormType != (int)EnumWSFormType.WiredSensor)
                        {
                            if (string.IsNullOrWhiteSpace(info.MACADDR))
                            {
                                result.Result = true;
                                result.Code = "005332";
                                WSListData error = new WSListData();
                                error.Code = "005332";
                                error.Index = info.Index;
                                errorList.Add(error);
                                continue;
                            }

                            if (wsRepository
                            .GetDatas<WS>(p => p.MACADDR == info.MACADDR, true)
                            .Any())
                            {
                                result.Result = true;
                                result.Code = "006112";
                                WSListData error = new WSListData();
                                error.Code = "006112";
                                error.Index = info.Index;
                                errorList.Add(error);
                                continue;
                            }

                            //MAC地址验证
                            if (!info.MACADDR.ValidateMAC())
                            {
                                result.Result = true;
                                result.Code = "008062";
                                WSListData error = new WSListData();
                                error.Code = "008062";
                                error.Index = info.Index;
                                errorList.Add(error);
                                continue;
                            }
                            //传感器类型验证
                            if (info.SensorTypeId <= 0)
                            {
                                result.Result = true;
                                result.Code = "005342";
                                WSListData error = new WSListData();
                                error.Code = "005342";
                                error.Index = info.Index;
                                errorList.Add(error);
                                continue;
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(info.PersonInCharge))
                        {
                            //验证负责人名称长度
                            if (!info.PersonInCharge.Trim().ValidateLength(50))
                            {
                                result.Result = true;
                                result.Code = "008102";
                                WSListData error = new WSListData();
                                error.Code = "008102";
                                error.Index = info.Index;
                                errorList.Add(error);
                                continue;
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(info.PersonInChargeTel))
                        {
                            if (!info.PersonInChargeTel.ValidatePhone())
                            {
                                result.Result = true;
                                result.Code = "008072";
                                WSListData error = new WSListData();
                                error.Code = "008072";
                                error.Index = info.Index;
                                errorList.Add(error);
                                continue;
                            }
                        }



                        if (info.WGID <= 0)
                        {
                            result.Result = true;
                            result.Code = "005352";
                            WSListData error = new WSListData();
                            error.Code = "005352";
                            error.Index = info.Index;
                            errorList.Add(error);
                            continue;
                        }
                        DateTime setupTime;
                        if (!DateTime.TryParse(info.SetupTime, out setupTime))
                        {
                            result.Result = true;
                            result.Code = "008112";
                            WSListData error = new WSListData();
                            error.Code = "008112";
                            error.Index = info.Index;
                            errorList.Add(error);
                            continue;
                        }
                        Gateway gatewayObj = gatewayRepository.GetByKey(info.WGID);
                        if (gatewayObj == null)
                        {
                            result.Result = true;
                            result.Code = "005352";
                            WSListData error = new WSListData();
                            error.Code = "005352";
                            error.Index = info.Index;
                            errorList.Add(error);
                            continue;
                        }
                        WirelessGatewayType gatewayTypeObj = cacheDICT.GetInstance()
                            .GetCacheType<WirelessGatewayType>(p => p.ID == gatewayObj.WGType)
                            .SingleOrDefault();
                        if (gatewayTypeObj == null)
                        {
                            result.Result = true;
                            result.Code = "005372";
                            WSListData error = new WSListData();
                            error.Code = "005372";
                            error.Index = info.Index;
                            errorList.Add(error);
                            continue;
                        }

                        if (info.WSFormType != (int)EnumWSFormType.WiredSensor)//有线传感器不进行数目判断， Modified by QXM, 2018/05/09
                        {
                            int gatewayTypeCount;

                            //融合到三轴传感器，添加一个三轴传感器其实是添加了三个传感器， 故不能根据个数进行判断，改为根据MAC地址数进行判断, Modifed by QXM, 2018/05/09
                            int wsCount = wsRepository
                                          .GetDatas<WS>(p => p.WGID == gatewayObj.WGID, true).GroupBy(t => t.MACADDR).Count();
                            //网关可以挂靠多少个WS

                            if (gatewayTypeObj.Name != "不限制")
                            {
                                int.TryParse(gatewayTypeObj.Name, out gatewayTypeCount);
                                if (wsCount >= gatewayTypeCount)
                                {
                                    result.Result = true;
                                    result.Code = "005382";
                                    WSListData error = new WSListData();
                                    error.Code = "005382";
                                    error.Index = info.Index;
                                    errorList.Add(error);
                                    continue;
                                }

                                //验证可以再挂多少个WS
                                //int addCount = wsList.Where(item => item.WGID == gatewayObj.WGID).Count();
                                //融合到三轴传感器，添加一个三轴传感器其实是添加了三个传感器， 故不能根据个数进行判断，改为根据MAC地址数进行判断, Modifed by QXM, 2018/05/09
                                int addCount = wsList.Where(item => item.WGID == gatewayObj.WGID).GroupBy(t => t.MACADDR).Count();

                                //已经添加的数量
                                int sumCount = wsCount + addCount;

                                //如果已经等于总数，达到最大数量
                                if (sumCount == gatewayTypeCount)
                                {
                                    result.Result = true;
                                    result.Code = "005382";
                                    WSListData error = new WSListData();
                                    error.Code = "005382";
                                    error.Index = info.Index;
                                    errorList.Add(error);
                                    continue;
                                }
                            }
                        }
                        WS wsObj = new WS();
                        wsObj.WSName = info.WSName;
                        wsObj.MACADDR = info.MACADDR.ToUpper();
                        wsObj.SensorType = info.SensorTypeId;
                        wsObj.WGID = info.WGID;
                        wsObj.SetupTime = setupTime;
                        wsObj.Vendor = info.Vendor;
                        wsObj.AntiExplosionSerialNo = info.AntiExplosionSerialNo;
                        wsObj.SNCode = info.SNCode;
                        wsObj.PersonInCharge = info.PersonInCharge;
                        wsObj.PersonInChargeTel = info.PersonInChargeTel;
                        wsObj.RunStatus = 1;
                        wsObj.TriggerStatus = 0;

                        wsObj.ChannelId = info.ChannelID;
                        wsObj.DevFormType = info.WSFormType;
                        wsObj.Axis = info.Axial;
                        wsObj.AxisName = info.AxialName;
                        wsObj.SensorCollectType = info.SensorCollectType;

                        //添加有线传感器时候，WS连接状态由DAU当前状态决定， Added by QXM, 2018/05/21
                        if (wsObj.DevFormType == (int)EnumWSFormType.WiredSensor)
                        {
                            if (gatewayObj.CurrentDAUStates.HasValue)
                            {
                                int DAUStatus = gatewayObj.CurrentDAUStates.Value;

                                if (DAUStatus == (int)EnumDAUStatus.InitStatus
                                    || DAUStatus == (int)EnumDAUStatus.IdleStatus
                                    || DAUStatus == (int)EnumDAUStatus.TestStatus
                                    || DAUStatus == (int)EnumDAUStatus.ConfigingStatus || DAUStatus == (int)EnumDAUStatus.CollectingStatus)
                                {
                                    wsObj.LinkStatus = (int)EnumWSLinkStatus.Connect;
                                }
                            }
                        }

                        wsList.Add(wsObj);
                    }

                    //result.Result = false;
                    result.WSList = wsList;
                    result.ErrorWSList = errorList;
                }
                return result;
            }
            #endregion

            #region 验证“编辑无线传感器信息”的参数是否有效
            /// <summary>
            /// 创建人：张辽阔
            /// 创建时间：2016-10-28
            /// 创建记录：验证“编辑无线传感器信息”的参数是否有效
            /// </summary>
            /// <param name="parameter">添加无线传感器信息的请求参数</param>
            /// <returns></returns>
            internal dynamic ValidateEditWSDataParams(EditWSDataParameter parameter)
            {
                dynamic result = new ExpandoObject();
                if (parameter == null)
                {
                    result.Result = true;
                    result.Code = "005392";
                    return result;
                }
                if (parameter.WGID <= 0)
                {
                    result.Result = true;
                    result.Code = "005402";
                    return result;
                }
                if (string.IsNullOrWhiteSpace(parameter.WSName))
                {
                    result.Result = true;
                    result.Code = "005412";
                    return result;
                }
                if (wsRepository
                    .GetDatas<WS>(p => p.WSID != parameter.WSID
                        && p.WSName == parameter.WSName,
                        true)
                    .Any())
                {
                    result.Result = true;
                    result.Code = "005422";
                    return result;
                }

                if (parameter.WSFormType != (int)EnumWSFormType.WiredSensor)
                {
                    if (string.IsNullOrWhiteSpace(parameter.MACADDR))
                    {
                        result.Result = true;
                        result.Code = "005432";
                        return result;
                    }
                    if (parameter.SensorTypeId <= 0)
                    {
                        result.Result = true;
                        result.Code = "005442";
                        return result;
                    }
                }

                if (parameter.WGID <= 0)
                {
                    result.Result = true;
                    result.Code = "005452";
                    return result;
                }
                DateTime setupTime;
                if (!DateTime.TryParse(parameter.SetupTime, out setupTime))
                {
                    result.Result = true;
                    result.Code = "005462";
                    return result;
                }
                WS wsObj = wsRepository
                    .GetByKey(parameter.WSID);
                if (wsObj == null)
                {
                    result.Result = true;
                    result.Code = "005402";
                    return result;
                }
                Gateway gatewayObj = gatewayRepository
                    .GetByKey(parameter.WGID);
                if (gatewayObj == null)
                {
                    result.Result = true;
                    result.Code = "005452";
                    return result;
                }
                if (wsObj.WGID != parameter.WGID)
                {
                    WirelessGatewayType gatewayTypeObj = cacheDICT.GetInstance()
                        .GetCacheType<WirelessGatewayType>(p => p.ID == gatewayObj.WGType)
                        .SingleOrDefault();
                    if (gatewayTypeObj == null)
                    {
                        result.Result = true;
                        result.Code = "005472";
                        return result;
                    }
                    int wsCount = wsRepository.GetDatas<WS>(p => p.WGID == parameter.WGID, true)
                        .Count();
                    int gatewayTypeCount;
                    int.TryParse(gatewayTypeObj.Name, out gatewayTypeCount);
                    if (wsCount >= gatewayTypeCount)
                    {
                        result.Result = true;
                        result.Code = "005482";
                        return result;
                    }
                }

                wsObj.WSName = parameter.WSName;
                wsObj.MACADDR = parameter.MACADDR;
                wsObj.SensorType = parameter.SensorTypeId;
                wsObj.WGID = parameter.WGID;
                wsObj.ChannelId = parameter.ChannelID;
                wsObj.SetupTime = setupTime;
                wsObj.Vendor = parameter.Vendor;
                wsObj.AntiExplosionSerialNo = parameter.AntiExplosionSerialNo;
                wsObj.SNCode = parameter.SNcode;
                wsObj.PersonInCharge = parameter.PersonInCharge;
                wsObj.PersonInChargeTel = parameter.PersonInChargeTel;

                wsObj.DevFormType = parameter.WSFormType;
                wsObj.Axis = parameter.Axial;
                wsObj.AxisName = parameter.AxialName;
                wsObj.SensorCollectType = parameter.SensorCollectType;

                result.WSEntity = wsObj;
                result.Result = false;
                return result;
            }
            #endregion

            #region 验证“批量删除无线传感器信息”的参数是否有效
            /// <summary>
            /// 创建人：张辽阔
            /// 创建时间：2016-10-28
            /// 创建记录：验证“批量删除无线传感器信息”的参数是否有效
            /// </summary>
            /// <param name="parameter">批量删除无线传感器信息的请求参数</param>
            /// <returns></returns>
            internal dynamic ValidateDeleteWSDataParams(DeleteWSDataParameter parameter)
            {
                dynamic result = new ExpandoObject();
                if (parameter == null)
                {
                    result.Result = true;
                    result.Code = "005492";
                    return result;
                }
                if (parameter.WSID == null || !parameter.WSID.Any())
                {
                    result.Result = true;
                    result.Code = "005502";
                    return result;
                }
                if (wsRepository
                    .GetDatas<WS>(t => parameter.WSID.Contains(t.WSID), true)
                    .Any(t => t.UseStatus == 1))
                {
                    result.Result = true;
                    result.Code = "005512";
                    return result;
                }
                result.Result = false;
                return result;
            }
            #endregion

            #endregion
        }

        #endregion

        #region 无线传感器集合数据添加接口
        /// <summary>
        /// 无线传感器集合数据添加接口
        /// </summary>
        /// <returns></returns>
        public BaseResponse<AddWSListDataResult> AddWSListData(AddWSListDataParameter parameter)
        {
            BaseResponse<AddWSListDataResult> response = new BaseResponse<AddWSListDataResult>();
            AddWSListDataResult addWSListDataResult = new AddWSListDataResult();

            #region 验证用户
            //批量添加无线传感器时，用户id不正确
            if (parameter.UserId < -1 || parameter.UserId == 0)
            {
                response.IsSuccessful = false;
                response.Code = "009773";
                return response;
            }

            if (parameter.UserId == -1)
            {
                parameter.UserId = 1011;
            }
            #endregion
            try
            {
                var newList = new List<WSData>();
                foreach (var info in parameter.WSListData)
                {
                    WSData wSData = new WSData();
                    wSData.WGID = info.WGID;
                    wSData.ChannelID = info.ChannelID;
                    wSData.SensorTypeId = info.SensorTypeId;
                    wSData.SetupTime = info.SetupTime;
                    wSData.Index = info.Index;
                    wSData.WSName = !string.IsNullOrEmpty(info.WSName) ? info.WSName.Trim() : string.Empty;
                    wSData.MACADDR = !string.IsNullOrEmpty(info.MACADDR) ? info.MACADDR.Trim() : string.Empty;
                    wSData.SNCode = !string.IsNullOrEmpty(info.SNCode) ? info.SNCode.Trim() : string.Empty;
                    wSData.Vendor = !string.IsNullOrEmpty(info.Vendor) ? info.Vendor.Trim() : string.Empty;
                    wSData.AntiExplosionSerialNo = !string.IsNullOrEmpty(info.AntiExplosionSerialNo) ? info.AntiExplosionSerialNo.Trim() : string.Empty;
                    wSData.PersonInCharge = !string.IsNullOrEmpty(info.PersonInCharge) ? info.PersonInCharge.Trim() : string.Empty;
                    wSData.PersonInChargeTel = !string.IsNullOrEmpty(info.PersonInChargeTel) ? info.PersonInChargeTel.Trim() : string.Empty;

                    wSData.WSFormType = info.WSFormType;
                    wSData.Axial = info.Axial;
                    wSData.AxialName = info.AxialName;
                    wSData.SensorCollectType = info.SensorCollectType;
                    newList.Add(wSData);
                }

                parameter.WSListData = newList;

                //验证数据
                Validate validate = new Validate(gatewayRepository, wsRepository, operationRepository, deviceRepository, cacheDICT);
                dynamic validateResult = validate.ValidateAddWSListDataParams(parameter);
                var wsList = validateResult.WSList as List<WS>;
                if (wsList.Count <= 0)
                {
                    response.IsSuccessful = false;
                    response.Code = validateResult.Code;
                    return response;
                }

                #region 有线传感器通道是否被使用
                foreach (WS ws in wsList)
                {
                    if (ws.DevFormType == (int)EnumWSFormType.WiredSensor)
                    {
                        //WG的通道是否被占用
                        var isUsed = wsRepository.GetDatas<WS>(t => t.WGID == ws.WGID && t.ChannelId.HasValue && t.ChannelId.Value == ws.ChannelId, true).Any();
                        if (isUsed)
                        {
                            response.IsSuccessful = false;
                            response.Code = "010122";
                            //response.Reason = "添加有线WS时候，通道已关联有线WS";
                            return response;
                        }
                    }
                }
                #endregion

                OperationResult result = wsRepository.AddNew<WS>(wsList);
                if (result.ResultType == EnumOperationResultType.Success)
                {
                    //添加用户和传感器关系表
                    List<UserRelationWS> userRelationWSList = new List<UserRelationWS>();
                    foreach (WS tempWS in wsList)
                    {
                        UserRelationWS userRelationWS = new UserRelationWS();
                        userRelationWS.UserID = parameter.UserId;
                        userRelationWS.WSID = tempWS.WSID;
                        userRelationWSList.Add(userRelationWS);

                        if (parameter.UserId != 1011)//如果不是超级管理员，需要添加超级管理员与WS的关系
                        {
                            UserRelationWS adminRel = new UserRelationWS();
                            adminRel.UserID = 1011;
                            adminRel.WSID = tempWS.WSID;
                            userRelationWSList.Add(adminRel);
                        }
                    }
                    userRelationWSRepository.AddNew<UserRelationWS>(userRelationWSList);

                    response.IsSuccessful = true;
                    var errorWSList = validateResult.ErrorWSList;
                    addWSListDataResult.WSListData = errorWSList as List<WSListData>;
                    response.Result = addWSListDataResult;
                    return response;
                }
                else
                {
                    response.IsSuccessful = false;
                    response.Code = "004861";
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response.IsSuccessful = false;
                response.Code = "004861";
                return response;
            }
        }
        #endregion

        #region 获取无线网关数据
        /// <summary>
        /// 获取无线网关数据
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<GetWGDataByUserIDResult> GetWGDataByUserID(GetWGDataByUserIDParameter parameter)
        {
            BaseResponse<GetWGDataByUserIDResult> baseResponse = new BaseResponse<GetWGDataByUserIDResult>();
            GetWGDataByUserIDResult result = new GetWGDataByUserIDResult();
            result.WGInfo = new List<WGInfoForGetWGDataByUserID>();
            baseResponse.IsSuccessful = false;

            #region 验证数据
            //获取无线网关数据时，用户id不正确
            if (parameter.UserID < -1 || parameter.UserID == 0)
            {
                baseResponse.Code = "009992";
                return baseResponse;
            }
            #endregion

            try
            {
                var wgIDList = new List<int>();
                if (parameter.UserID == -1)
                {
                    wgIDList = wsRepository.GetDatas<WS>(item => true, true).Select(item => item.WGID).ToList();
                }
                else
                {
                    var wsIDlist = userRelationWSRepository.GetDatas<UserRelationWS>(item => item.UserID == parameter.UserID, true).Select(item => item.WSID).ToList();
                    wgIDList = wsRepository.GetDatas<WS>(item => wsIDlist.Contains(item.WSID), true).Select(item => item.WGID).ToList();
                }

                if (wgIDList != null && wgIDList.Any())
                {
                    result.WGInfo = gatewayRepository.GetDatas<Gateway>(item => wgIDList.Contains(item.WGID), true).Select(item => new WGInfoForGetWGDataByUserID
                    {
                        WGID = item.WGID,
                        WGName = item.WGName,
                    }).ToList();
                }

                baseResponse.IsSuccessful = true;
                baseResponse.Result = result;


            }
            catch (Exception ex)
            {
                baseResponse.Code = "010001";
                LogHelper.WriteLog(ex);
            }
            return baseResponse;
        }

        #region 获取网关简单信息
        public BaseResponse<GetWGSimpleInfoResult> GetWGSimpleInfo(GetWGSimpleInfoParameter param)
        {
            BaseResponse<GetWGSimpleInfoResult> response = new BaseResponse<GetWGSimpleInfoResult>();
            GetWGSimpleInfoResult result = new GetWGSimpleInfoResult();
            try
            {
                //int monitorTreeID = param.MonitorTreeID;
                //List<int> allChildren = new List<int>();
                //GetAllChildren(monitorTreeID, allChildren);

                var deviceList = deviceRepository.GetDatas<Device>(t => true, true).ToList();
                var deviceIDList = deviceList.Select(t => t.DevID).ToList();

                var measureSiteList = measureSiteRepository.GetDatas<MeasureSite>(t => deviceIDList.Contains(t.DevID), true).ToList();

                using (iCMSDbContext dataContext = new iCMSDbContext())
                {
                    //找到监测树下的所有WG
                    var wgList = dataContext.WG.ToList();
                    var wgIDList = wgList.Select(t => t.WGID).ToList();

                    var wsList = wsRepository.GetDatas<WS>(t => wgIDList.Contains(t.WGID), true).ToList();

                    //遍历WG
                    List<WGSimpleInfo> simpleInfoList = wgList.ToArray().Select(t =>
                    {
                        int wgID = t.WGID;
                        //找到WG下的所有WS
                        var curWSList = wsList.Where(wss => wss.WGID == wgID).Select(wss => wss.WSID).ToList();

                        //存储语 此WG有联系的设备ID
                        List<int> relatedDeviceIDList = new List<int>();

                        foreach (int curDevID in deviceIDList)
                        {
                            //找到设备下的测点所用的WS
                            var wsidList = measureSiteList.Where(ms => ms.DevID == curDevID && ms.WSID.HasValue).Select(ms => ms.WSID.Value).ToList();

                            foreach (int innerwsid in wsidList)
                            {
                                if (curWSList.Contains(innerwsid))
                                {
                                    relatedDeviceIDList.Add(curDevID);
                                    break;
                                }
                            }
                        }

                        return new WGSimpleInfo
                        {
                            WGID = t.WGID,
                            WGName = t.WGName,
                            DevFormType = t.DevFormType,
                            RelatedDeviceIDList = relatedDeviceIDList
                        };
                    }).ToList();

                    result.WGSimpleInfo.AddRange(simpleInfoList);
                    response.Result = result;
                    return response;
                }
            }
            catch (Exception e)
            {
                response.IsSuccessful = false;
                response.Code = "000000";

                return response;
            }
        }
        #endregion

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
    }
    #endregion
}