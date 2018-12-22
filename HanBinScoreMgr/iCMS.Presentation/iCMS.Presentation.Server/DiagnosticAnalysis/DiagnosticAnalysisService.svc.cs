/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Presentation.Server.DiagnosticAnalysis
 *文件名：  DiagnosticAnalysisService
 *创建人：  王颖辉
 *创建时间：2016-10-21
 *描述：诊断分析服务接口
 *
 *修改人：张辽阔
 *修改时间：2016-11-10
 *描述：增加错误编码
 *
 *修改人：张辽阔
 *修改时间：2016-12-15
 *描述：未通过安全验证时，增加日志记录
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Request.DiagnosticAnalysis.MonitorTree;
using iCMS.Common.Component.Data.Response;
using iCMS.Presentation.Common;
using iCMS.Service.Web.DiagnosticAnalysis;
using iCMS.Common.Component.Data.Request.DiagnosticAnalysis;
using iCMS.Service.Web.SystemInitSets;
using iCMS.Common.Component.Data.Response.DiagnosticAnalysis;
using iCMS.Common.Component.Data.Response.SystemInitSets;
using iCMS.Common.Component.Tool.IoC;
using iCMS.Common.Component.Tool;
using iCMS.Common.Component.Data.Request.DevicesConfig;
using iCMS.Common.Component.Data.Response.Common;

namespace iCMS.Presentation.Server.DiagnosticAnalysis
{
    #region 诊断分析
    /// <summary>
    /// 诊断分析
    /// </summary>
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [CustomExceptionBehaviour(typeof(CustomExceptionHandler))]
    public class DiagnosticAnalysisService : BaseService, IDiagnosticAnalysisService
    {
        #region 变量

        public IDiagnosticAnalysisManager diagnosticAnalysisManager { get; private set; }

        public ISystemInitManager systemInitManager { get; private set; }

        #endregion

        #region  构造函数
        public DiagnosticAnalysisService(IDiagnosticAnalysisManager diagnoManager, ISystemInitManager initManager)
        {
            diagnosticAnalysisManager = diagnoManager;
            this.systemInitManager = initManager;
        }
        #endregion

        #region 形貌图展示

        /// <summary>
        /// 形貌图展示
        /// </summary>
        /// <returns></returns>
        public BaseResponse<DevImgDataResult> GetDevImgData(DevImgDataParameter parameter)
        {
            if (ValidateData<DevImgDataParameter>(parameter))
            {
                return diagnosticAnalysisManager.GetDevImgData(parameter);
            }
            else
            {
                BaseResponse<DevImgDataResult> result = new BaseResponse<DevImgDataResult>();
                result.IsSuccessful = false;
                result.Code = "000351";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #region 通过设备Id,获取形貌图配置下的所有位置信息

        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2016-09-03
        /// 通过设备Id,获取形貌图配置下的所有位置信息
        /// </summary>
        /// <param name="deviceId">设备id</param>
        /// <returns></returns>
        public BaseResponse<ConfigListByDeviceIDResult> GetConfigListByDeviceID(ConfigListByDeviceIDParameter parameter)
        {
            if (ValidateData<ConfigListByDeviceIDParameter>(parameter))
            {
                return diagnosticAnalysisManager.GetConfigListByDeviceID(parameter);
            }
            else
            {
                BaseResponse<ConfigListByDeviceIDResult> result = new BaseResponse<ConfigListByDeviceIDResult>();
                result.IsSuccessful = false;
                result.Code = "000361";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #endregion

        #region 左侧导航监测树

        public BaseResponse<MonitorTreeDataForNavigationResult> GetMonitorTreeDataForNavigation(GetMonitorTreeDataForNavigationParameter Parameter)
        {
            if (ValidateData<BaseRequest>(Parameter))
            {
                return diagnosticAnalysisManager.GetMonitorTreeDataForNavigation(Parameter);
            }
            else
            {
                BaseResponse<MonitorTreeDataForNavigationResult> result = new BaseResponse<MonitorTreeDataForNavigationResult>();
                result.IsSuccessful = false;
                result.Code = "000371";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 获取形貌图显示配置
        /// <summary>
        /// 获取形貌图显示配置 ,LF
        /// </summary>
        /// <returns></returns>
        public BaseResponse<SystemConfigResult> GetTopographicMapSets(BaseRequest Parameter)
        {
            if (ValidateData<BaseRequest>(Parameter))
            {
                return systemInitManager.GetTopographicMapSets();
            }
            else
            {
                BaseResponse<SystemConfigResult> result = new BaseResponse<SystemConfigResult>();
                result.IsSuccessful = false;
                result.Code = "000381";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }
        #endregion

        #region 获取形貌图设备图片信息
        /// <summary>
        /// 获取形貌图设备图片信息,LF
        /// </summary>
        /// <returns></returns>
        public BaseResponse<SystemConfigResult> GetTopographicMapPictureInfo(BaseRequest Parameter)
        {
            if (ValidateData<BaseRequest>(Parameter))
            {
                return systemInitManager.GetTopographicMapPictureInfo();
            }
            else
            {
                BaseResponse<SystemConfigResult> result = new BaseResponse<SystemConfigResult>();
                result.IsSuccessful = false;
                result.Code = "000391";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }
        #endregion

        #region 获取无线传感器数据接口
        /// <summary>
        /// 获取无线传感器数据接口
        /// </summary>
        /// <returns></returns>
        public BaseResponse<WSInfoResult> GetWSStatusData(GetWSStatusParameter param)
        {
            if (ValidateData<GetWSStatusParameter>(param))
            {
                return diagnosticAnalysisManager.GetWSStatusData(param);
            }
            else
            {
                BaseResponse<WSInfoResult> result = new BaseResponse<WSInfoResult>();
                result.IsSuccessful = false;
                result.Code = "000401";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }
        #endregion

        #region 报警提醒
        /// <summary>
        /// 报警提醒
        /// </summary>
        /// <returns></returns>
        public BaseResponse<DevAlarmRemindDataResult> GetDevAlarmRemindDataByUserID(QueryDevWarningDataParameter Parameter)
        {
            if (ValidateData<QueryDevWarningDataParameter>(Parameter))
            {
                return diagnosticAnalysisManager.GetDevWarningDataByUser(Parameter);
            }
            else
            {
                BaseResponse<DevAlarmRemindDataResult> result = new BaseResponse<DevAlarmRemindDataResult>();
                result.IsSuccessful = false;
                result.Code = "000411";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }
        #endregion

        #region 系统配置信息通用查询接口
        /// <summary>
        /// 创建人:王颖辉
        /// 创建时间:2017-10-13
        /// 创建内容：系统配置信息通用查询接口
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public BaseResponse<GetConfigListForDeviceMonitorCenterResult> GetConfigListForDeviceMonitorCenter(GetConfigListForDeviceMonitorCenterParameter parameter)
        {
            if (ValidateData<GetConfigListForDeviceMonitorCenterParameter>(parameter))
            {
                return diagnosticAnalysisManager.GetConfigListForDeviceMonitorCenter(parameter);
            }
            else
            {
                BaseResponse<GetConfigListForDeviceMonitorCenterResult> result = new BaseResponse<GetConfigListForDeviceMonitorCenterResult>();
                result.IsSuccessful = false;
                result.Code = "009131";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }

        }
        #endregion

        #region 获取当前设备状态及报告统计
        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间:2017-10-13
        /// 创建内容:获取当前设备状态及报告统计
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public BaseResponse<GetDeviceTopographicMapStatisticsResult> GetDeviceTopographicMapStatistics(GetDeviceTopographicMapStatisticsParameter parameter)
        {
            if (ValidateData<GetDeviceTopographicMapStatisticsParameter>(parameter))
            {
                return diagnosticAnalysisManager.GetDeviceTopographicMapStatistics(parameter);
            }
            else
            {
                BaseResponse<GetDeviceTopographicMapStatisticsResult> result = new BaseResponse<GetDeviceTopographicMapStatisticsResult>();
                result.IsSuccessful = false;
                result.Code = "009141";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }
        #endregion

        #region 获取对形貌图数据展示
        /// <summary>
        /// 创建人:王颖辉
        /// 创建时间:2017-10-13
        /// 创建内容：获取对形貌图数据展示
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<GetWSImgDataResult> GetWSImgData(GetWSImgDataParameter parameter)
        {
            if (ValidateData<GetWSImgDataParameter>(parameter))
            {
                return diagnosticAnalysisManager.GetWSImgData(parameter);
            }
            else
            {
                BaseResponse<GetWSImgDataResult> result = new BaseResponse<GetWSImgDataResult>();
                result.IsSuccessful = false;
                result.Code = "009151";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }
        #endregion

        #region 获取当前传感器状态及报告统计
        /// <summary>
        /// 创建人:王颖辉
        /// 创建时间:2017-10-13
        /// 创建内容：获取当前传感器状态及报告统计
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<GetWSStatusAndReportStatisticsResult> GetWSStatusAndReportStatistics(GetWSStatusAndReportStatisticsParameter parameter)
        {
            if (ValidateData<GetWSStatusAndReportStatisticsParameter>(parameter))
            {
                return diagnosticAnalysisManager.GetWSStatusAndReportStatistics(parameter);
            }
            else
            {
                BaseResponse<GetWSStatusAndReportStatisticsResult> result = new BaseResponse<GetWSStatusAndReportStatisticsResult>();
                result.IsSuccessful = false;
                result.Code = "009161";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }
        #endregion

        #region 通过WSID获取挂靠设备信息
        /// <summary>
        /// 创建人:王颖辉
        /// 创建时间:2017-10-13
        /// 创建内容：通过WSID获取挂靠设备信息
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public BaseResponse<GetDeviceInfoByWSIDResult> GetDeviceInfoByWSID(GetDeviceInfoByWSIDParameter parameter)
        {
            if (ValidateData<GetDeviceInfoByWSIDParameter>(parameter))
            {
                return diagnosticAnalysisManager.GetDeviceInfoByWSID(parameter);
            }
            else
            {
                BaseResponse<GetDeviceInfoByWSIDResult> result = new BaseResponse<GetDeviceInfoByWSIDResult>();
                result.IsSuccessful = false;
                result.Code = "009181";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }
        #endregion

        #region 通过设备ID获取传感器
        /// <summary>
        /// 创建人:王颖辉
        /// 创建时间:2017-10-13
        /// 创建内容：通过设备ID获取传感器
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public BaseResponse<GetWSByDeviceIDResult> GetWSByDeviceID(GetWSByDeviceIDParameter parameter)
        {
            if (ValidateData<GetWSByDeviceIDParameter>(parameter))
            {
                return diagnosticAnalysisManager.GetWSByDeviceID(parameter);
            }
            else
            {
                BaseResponse<GetWSByDeviceIDResult> result = new BaseResponse<GetWSByDeviceIDResult>();
                result.IsSuccessful = false;
                result.Code = "009191";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }
        #endregion

        #region 获取设备数量
        /// <summary>
        /// 获取设备数量
        /// </summary>
        /// <param name="parameter"></param>
        public BaseResponse<ResponseResult> GetDeviceCount(GetDeviceCountParameter parameter)
        {
            if (ValidateData<GetDeviceCountParameter>(parameter))
            {
                return diagnosticAnalysisManager.GetDeviceCount(parameter);
            }
            else
            {
                BaseResponse<ResponseResult> result = new BaseResponse<ResponseResult>();
                result.IsSuccessful = false;
                result.Code = "009843";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }
        #endregion

        #region 获取WS数量
        /// <summary>
        /// 获取WS数量
        /// </summary>
        /// <param name="parameter"></param>
        public BaseResponse<ResponseResult> GetWSCount(GetWSCountParameter parameter)
        {
            if (ValidateData<GetWSCountParameter>(parameter))
            {
                return diagnosticAnalysisManager.GetWSCount(parameter);
            }
            else
            {
                BaseResponse<ResponseResult> result = new BaseResponse<ResponseResult>();
                result.IsSuccessful = false;
                result.Code = "009853";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion
    }

    #endregion
}