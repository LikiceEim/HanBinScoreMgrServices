/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Service.DiagnosticAnalysis
 *文件名：  IiCMSDiagnosticAnalysisService
 *创建人：  王颖辉
 *创建时间：2016-10-21
 *描述：诊断控件业务处理类
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iCMS.Common.Component.Data.Request.DiagnosticAnalysis;
using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Response.DiagnosticAnalysis;
using iCMS.Common.Component.Data.Response;
using iCMS.Common.Component.Data.Request.DiagnosticAnalysis.MonitorTree;
using iCMS.Common.Component.Data.Request.DevicesConfig;
using iCMS.Common.Component.Data.Response.Common;

namespace iCMS.Service.Web.DiagnosticAnalysis
{
    #region 诊断分析
    /// <summary>
    /// 诊断分析
    /// </summary>
    public interface IDiagnosticAnalysisManager
    {
        #region 形貌图展示
        /// <summary>
        /// 形貌图展示
        /// </summary>
        /// <returns></returns>
        BaseResponse<DevImgDataResult> GetDevImgData(DevImgDataParameter parameter);

        #endregion

        #region 通过设备Id,获取形貌图配置下的所有位置信息

        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2016-09-03
        /// 通过设备Id,获取形貌图配置下的所有位置信息
        /// </summary>
        /// <param name="deviceId">设备id</param>
        /// <returns></returns>
        BaseResponse<ConfigListByDeviceIDResult> GetConfigListByDeviceID(ConfigListByDeviceIDParameter parameter);

        #endregion

        #region 左侧导航监测树

        BaseResponse<MonitorTreeDataForNavigationResult> GetMonitorTreeDataForNavigation(GetMonitorTreeDataForNavigationParameter Parameter);

        #endregion
        
        #region 报警提醒
        BaseResponse<DevAlarmRemindDataResult> GetDevWarningDataByUser(QueryDevWarningDataParameter Parameter);
        #endregion        /// <summary>

        #region 获取无线传感器数据接口
        /// <summary>
        /// 获取无线传感器数据接口
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        BaseResponse<WSInfoResult> GetWSStatusData(GetWSStatusParameter param);
        #endregion

        #region 系统配置信息通用查询接口
        /// <summary>
        /// 创建人:王颖辉
        /// 创建时间:2017-10-13
        /// 创建内容：系统配置信息通用查询接口
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        BaseResponse<GetConfigListForDeviceMonitorCenterResult> GetConfigListForDeviceMonitorCenter(GetConfigListForDeviceMonitorCenterParameter parameter);
        #endregion

        //#region 向调用者暴露对形貌图数据展示
        ///// <summary>
        ///// 创建人：王颖辉
        ///// 创建时间:2017-10-13
        ///// 创建内容:向调用者暴露对形貌图数据展示
        ///// </summary>
        ///// <param name="parameter">参数</param>
        ///// <returns></returns>
        //BaseResponse<GetDevImgDataResult> GetDevImgData(GetDevImgDataParameter parameter);
        //#endregion

        #region 获取当前设备状态及报告统计
        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间:2017-10-13
        /// 创建内容:获取当前设备状态及报告统计
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        BaseResponse<GetDeviceTopographicMapStatisticsResult> GetDeviceTopographicMapStatistics(GetDeviceTopographicMapStatisticsParameter parameter);
        #endregion

        #region 获取对形貌图数据展示
        /// <summary>
        /// 创建人:王颖辉
        /// 创建时间:2017-10-13
        /// 创建内容：获取对形貌图数据展示
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<GetWSImgDataResult> GetWSImgData(GetWSImgDataParameter parameter);
        #endregion

        #region 获取当前传感器状态及报告统计
        /// <summary>
        /// 创建人:王颖辉
        /// 创建时间:2017-10-13
        /// 创建内容：获取当前传感器状态及报告统计
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<GetWSStatusAndReportStatisticsResult> GetWSStatusAndReportStatistics(GetWSStatusAndReportStatisticsParameter parameter);
        #endregion

        #region 通过WSID获取挂靠设备信息
        /// <summary>
        /// 创建人:王颖辉
        /// 创建时间:2017-10-13
        /// 创建内容：通过WSID获取挂靠设备信息
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        BaseResponse<GetDeviceInfoByWSIDResult> GetDeviceInfoByWSID(GetDeviceInfoByWSIDParameter parameter);
        #endregion

        #region 通过设备ID获取传感器
        /// <summary>
        /// 创建人:王颖辉
        /// 创建时间:2017-10-13
        /// 创建内容：通过设备ID获取传感器
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        BaseResponse<GetWSByDeviceIDResult> GetWSByDeviceID(GetWSByDeviceIDParameter parameter);
        #endregion

        #region 获取设备数量
        /// <summary>
        /// 获取设备数量
        /// </summary>
        /// <param name="parameter"></param>
        BaseResponse<ResponseResult> GetDeviceCount(GetDeviceCountParameter parameter);
        #endregion

        #region 获取WS数量
        /// <summary>
        /// 获取WS数量
        /// </summary>
        /// <param name="parameter"></param>
        BaseResponse<ResponseResult> GetWSCount(GetWSCountParameter parameter);
  
        #endregion
    }
    #endregion
}
